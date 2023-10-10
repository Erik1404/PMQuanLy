using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
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

            var existingRegistrations = await _dbContext.CourseRegistrations
                .Where(cr => cr.CourseId == courseId)
                .ToListAsync();

            if (existingRegistrations.Count >= course.MaximumStudents)
            {
                course.CourseStatus = CourseStatus.Close;
                _dbContext.Entry(course).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }

            // Tạo bản ghi đăng ký khóa học
            var registration = new CourseRegistration
            {
                StudentId = studentId,
                CourseId = courseId,
                RegistrationDate = DateTime.Now
            };

            _dbContext.CourseRegistrations.Add(registration);
            await _dbContext.SaveChangesAsync();

            // Kiểm tra xem đã đủ số lượng học sinh tối thiểu chưa
            if (existingRegistrations.Count + 1 >= course.MinimumStudents)
            {
                course.CourseStatus = CourseStatus.Open;
                _dbContext.Entry(course).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }

            return registration;
        }


        public async Task<bool> UnregisterStudentFromCourse(int courseRegistrationId)
        {
            var courseRegistration = await _dbContext.CourseRegistrations.FindAsync(courseRegistrationId);

            if (courseRegistration == null)
            {
                return false;
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
