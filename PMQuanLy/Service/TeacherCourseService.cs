using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Interface;
using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public class TeacherCourseService : ITeacherCourseService
    {

        private readonly PMQLDbContext _dbContext;

        public TeacherCourseService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddCourseForTeacher(int teacherId, int courseId)
        {
            try
            {
                var teacher = await _dbContext.Teachers.FindAsync(teacherId);
                var course = await _dbContext.Courses.FindAsync(courseId);

                if (teacher == null || course == null)
                {
                    return false;
                }

                // Kiểm tra xem giáo viên đã giảng dạy khóa học này chưa
                if (_dbContext.TeacherCourses.Any(tc => tc.TeacherId == teacherId && tc.CourseId == courseId))
                {
                    throw new Exception("Giáo viên đã giảng dạy khóa học này rồi");
                }

                var teacherCourse = new TeacherCourse
                {
                    TeacherId = teacherId,
                    CourseId = courseId
                };

                _dbContext.TeacherCourses.Add(teacherCourse);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddTeacherToCourse(int courseId, int teacherId)
        {
            try
            {
                var course = await _dbContext.Courses.FindAsync(courseId);
                var teacher = await _dbContext.Teachers.FindAsync(teacherId);

                if (course == null || teacher == null)
                {
                    return false;
                }

                // Kiểm tra xem giáo viên đã được giao khóa học này chưa
                if (_dbContext.TeacherCourses.Any(tc => tc.TeacherId == teacherId && tc.CourseId == courseId))
                {
                    throw new Exception("Giáo viên đã giảng dạy khóa học này rồi");
                }

                var teacherCourse = new TeacherCourse
                {
                    TeacherId = teacherId,
                    CourseId = courseId
                };

                _dbContext.TeacherCourses.Add(teacherCourse);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Course>> GetCoursesForTeacher(int teacherId)
        {
            try
            {
                // Lấy danh sách khóa học cho giáo viên dựa trên teacherId
                var courses = await _dbContext.TeacherCourses
                    .Where(tc => tc.Teacher.UserId == teacherId)
                    .Select(tc => tc.Course)
                    .ToListAsync();

                return courses;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<Teacher>> GetTeachersForCourse(int courseId)
        {
            try
            {
                // Lấy danh sách giáo viên của khóa học dựa trên courseId
                var teachers = await _dbContext.TeacherCourses
                    .Where(tc => tc.Course.CourseId == courseId)
                    .Select(tc => tc.Teacher)
                    .ToListAsync();

                return teachers;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> RemoveCourseFromTeacher(int teacherId, int courseId)
        {
            try
            {
                var teacherCourse = _dbContext.TeacherCourses.FirstOrDefault(tc => tc.Teacher.UserId == teacherId && tc.CourseId == courseId);

                if (teacherCourse == null)
                {
                    return false;
                }

                _dbContext.TeacherCourses.Remove(teacherCourse);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoveTeacherFromCourse(int courseId, int teacherId)
        {
            try
            {
               
                var teacherCourse = _dbContext.TeacherCourses.FirstOrDefault(tc => tc.CourseId == courseId && tc.TeacherId == teacherId);

                if (teacherCourse == null)
                {
                    return false; 
                }

                _dbContext.TeacherCourses.Remove(teacherCourse);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
