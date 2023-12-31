﻿using Microsoft.EntityFrameworkCore;
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
                throw new Exception("Không tìm thấy dữ liệu bạn cần để đăng ký. Thử lại sau");
            }

            if (student.Role != "Student")
            {
                throw new Exception("Tài khoản của bạn không thuộc vào diện đăng ký môn học");
            }

            var today = DateTime.Now;
            if (today < course.RegistrationStartDate || today > course.RegistrationEndDate)
            {
                return null; // Ngày hiện tại không nằm trong thời gian đăng ký
            }

            var existingTuition = await _dbContext.Tuitions
                .Include(t => t.CourseRegistrations)
                .Where(t => t.StudentId == studentId)
                .SingleOrDefaultAsync();
            var existingRegistration = existingTuition?.CourseRegistrations.FirstOrDefault(cr => cr.CourseId == courseId);

            if (existingRegistration != null)
            {
                if (existingTuition.IsPaid)
                {
                    var newTuition = new Tuition
                    {
                        StudentId = studentId,
                        IsPaid = false,
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

                    /*_dbContext.SaveChanges(); // Lưu thay đổi trước*/

                     existingTuition?.CourseRegistrations.FirstOrDefault(cr => cr.CourseId == courseId);
                  

                    return newCourseRegistration;
                }
                else
                {
                    return existingRegistration;
                }
            }

            var courseRegistration = new CourseRegistration
            {
                StudentId = studentId,
                CourseId = courseId,
                RegistrationDate = DateTime.Now,
            };

       
            // Kiểm tra xem existingTuition đã được tạo chưa
            if (existingTuition == null)
            {
                var totalTuition = course.PriceCourse;
                existingTuition = new Tuition
                {
                    StudentId = studentId,
                    IsPaid = false,
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
            _dbContext.SaveChanges(); // Lưu thay đổi sau

            var getteacherCourse = _dbContext.TeacherCourses.FirstOrDefault(tc => tc.CourseId == courseId);
            var CourseEnrollment = new CourseEnrollment
            {

                TeacherCourseId = getteacherCourse.TeacherCourseId,
                CourseRegistrationId = courseRegistration.CourseRegistrationId,
                
            };
            _dbContext.CourseEnrollments.Add(CourseEnrollment);
            _dbContext.SaveChanges();

            return courseRegistration;
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
                .ThenInclude(cr => cr.Course)
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
