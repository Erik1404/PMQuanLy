using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public class CourseService : ICourseService
    {
        private readonly PMQLDbContext _dbContext;

        public CourseService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Course>> GetAllCourses()
        {
            return await _dbContext.Courses.ToListAsync();
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            return await _dbContext.Courses.FindAsync(courseId);
        }

        public List<Course> SearchCourse(string keyword)
        {

            return _dbContext.Courses
                .Where(s =>
                    s.CourseId.ToString().Contains(keyword) ||
                    s.CourseName.Contains(keyword))
                .ToList();
        }
        public async Task<Course> AddCourse(Course Course)
        {
            // Tạo một đối tượng User từ dữ liệu của Student
            var newCourse = new Course
            {
                CourseName = Course.CourseName,
                Description = Course.Description,
                MinimumStudents = Course.MinimumStudents,
                MaximumStudents = Course.MaximumStudents,
                SchoolDay = Course.SchoolDay,
                TimeClass = Course.TimeClass,
                PriceCourse = Course.PriceCourse,
                CourseStatus = CourseStatus.Open, // Đặt vai trò là "Student"
               
            };
            _dbContext.Courses.Add(Course);
            await _dbContext.SaveChangesAsync();
            return Course;
        }

        public async Task<bool> DeleteCourse(int CourseId)
        {
            var Course = await _dbContext.Courses.FindAsync(CourseId);
            if (Course == null)
                return false;
            _dbContext.Courses.Remove(Course);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCourse(Course Course)
        {
            _dbContext.Entry(Course).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
