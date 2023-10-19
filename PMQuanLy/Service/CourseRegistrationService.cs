using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Interface;
using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public class CourseRegistrationService : ICourseRegistrationService
    {
        private readonly PMQLDbContext _dbContext;

        public CourseRegistrationService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<CourseRegistration>> GetAllCourseRegistrations()
        {
            return await _dbContext.CourseRegistrations.ToListAsync();
        }

        public async Task<CourseRegistration> GetCourseRegistrationById(int courseRegistrationId)
        {
            return await _dbContext.CourseRegistrations.FindAsync(courseRegistrationId);
        }

        public async Task<CourseRegistration> RegisterStudentForCourse(int studentId, int courseId)
        {
            var student = await _dbContext.Students.FindAsync(studentId);
            var course = await _dbContext.Courses.FindAsync(courseId);



            if (student == null || course == null)
            {
                return null; // Không tìm thấy học sinh hoặc khóa học
            }

            if (student.Role != "Student")
            {
                return null; // Người dùng không phải là học sinh
            }

            var today = DateTime.Now;
            if (today < course.RegistrationStartDate || today > course.RegistrationEndDate)
            {
                return null; // Ngày hiện tại không nằm trong thời gian đăng ký
            }


            // Kiểm tra xem học sinh đã có Tuition hay chưa
            var existingTuition = await _dbContext.Tuitions
                .Include(t => t.CourseRegistrations)
                .Where(t => t.StudentId == studentId)
                .SingleOrDefaultAsync();

            var existingRegistration = existingTuition?.CourseRegistrations.FirstOrDefault(cr => cr.CourseId == courseId);

            if (existingRegistration != null)
            {
                // Học sinh đã đăng ký khóa học này, kiểm tra IsPaid
                if (existingTuition.IsPaid)
                {
                    // Tạo một Tuition mới vì IsPaid = True
                    var newTuition = new Tuition
                    {
                        StudentId = studentId,
                        IsPaid = false, // Đánh dấu là chưa thanh toán
                        TotalTuition = course.PriceCourse,
                        DiscountReason = "No Discount",
                        DiscountAmount = 0,
                        TotalAmountAfterDiscount = course.PriceCourse
                    };
                    _dbContext.Tuitions.Add(newTuition);

                    var newCourseRegistration = new CourseRegistration
                    {
                        StudentId = studentId,
                        CourseId = courseId,
                        RegistrationDate = DateTime.Now
                    };

                    newTuition.CourseRegistrations = new List<CourseRegistration> { newCourseRegistration };

                    _dbContext.SaveChanges();

                    return newCourseRegistration;
                }
                else
                {
                    // Học sinh vẫn chưa thanh toán, không thay đổi gì
                    return existingRegistration;
                }
            }

            // Học sinh chưa đăng ký môn này, tạo một CourseRegistration mới
            var courseRegistration = new CourseRegistration
            {
                StudentId = studentId,
                CourseId = courseId,
                RegistrationDate = DateTime.Now
            };

            // Kiểm tra xem existingTuition đã được tạo chưa
            if (existingTuition == null)
            {
                var totalTuition = course.PriceCourse;
                existingTuition = new Tuition
                {
                    StudentId = studentId,
                    IsPaid = false, // Ban đầu đánh dấu là chưa thanh toán
                    TotalTuition = totalTuition,
                    DiscountReason = "No Discount",
                    DiscountAmount = 0,
                    TotalAmountAfterDiscount = totalTuition,
                    CourseRegistrations = new List<CourseRegistration> { courseRegistration }
                };

                _dbContext.Tuitions.Add(existingTuition);
            }
            else
            {
                existingTuition.CourseRegistrations.Add(courseRegistration);
                existingTuition.TotalTuition += course.PriceCourse;
                existingTuition.TotalAmountAfterDiscount = existingTuition.TotalTuition;
            }

            await _dbContext.SaveChangesAsync();

            return courseRegistration;
        }

        public async Task<decimal> CalculateTotalTuitionForStudent(int studentId)
        {
            // Lấy danh sách các khóa học đã đăng ký bởi học sinh
            var courseRegistrations = await _dbContext.CourseRegistrations
                .Include(cr => cr.Course)
                .Where(cr => cr.StudentId == studentId)
                .ToListAsync();

            decimal totalTuition = 0;

            foreach (var registration in courseRegistrations)
            {
                totalTuition += registration.Course.PriceCourse;
            }

            return totalTuition;
        }


        public async Task<bool> UnregisterStudentFromCourse(int courseRegistrationId)
        {
            var courseRegistration = await _dbContext.CourseRegistrations.FindAsync(courseRegistrationId);

            if (courseRegistration == null)
            {
                return false;
            }

            // Tìm Tuition tương ứng
            var tuitions = await _dbContext.Tuitions
                .Include(t => t.CourseRegistrations)
                .ThenInclude(cr => cr.Course) // Đảm bảo Course cũng được tải
                .Where(t => t.StudentId == courseRegistration.StudentId)
                .ToListAsync();

            foreach (var tuition in tuitions)
            {
                var registrationToRemove = tuition.CourseRegistrations.FirstOrDefault(cr => cr.CourseRegistrationId == courseRegistrationId);
                if (registrationToRemove != null)
                {
                    tuition.CourseRegistrations.Remove(registrationToRemove);

                    // Nếu sau khi xóa CourseRegistration, danh sách CourseRegistrations của Tuition trống, thì xóa luôn Tuition
                    if (tuition.CourseRegistrations.Count == 0)
                    {
                        _dbContext.Tuitions.Remove(tuition);
                    }
                    else
                    {
                        // Tính toán tổng học phí mới
                        decimal newTotalTuition = tuition.CourseRegistrations.Sum(cr => cr.Course.PriceCourse);
                        tuition.TotalTuition = newTotalTuition;
                        tuition.DiscountReason = "No Discount";
                        tuition.DiscountAmount = 0;
                        tuition.TotalAmountAfterDiscount = newTotalTuition;
                    }
                }
            }

            _dbContext.CourseRegistrations.Remove(courseRegistration);
            await _dbContext.SaveChangesAsync();

            return true;
        }


        public async Task<List<CourseRegistration>> GetCourseRegistrationsForStudent(int studentId)
        {
            return await _dbContext.CourseRegistrations
                .Where(cr => cr.Student.UserId == studentId)
                .ToListAsync();
        }

        public async Task<List<CourseRegistration>> GetCourseRegistrationsForCourse(int courseId)
        {
            return await _dbContext.CourseRegistrations
                .Where(cr => cr.CourseId == courseId)
                .ToListAsync();
        }


        public async Task<int> CountStudentInCourse(int CourseId)
        {
            var count = _dbContext.CourseRegistrations.Where(x => x.CourseId == CourseId).Count();
            return count;
        }
    }
}
