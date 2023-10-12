using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllCourses();
        List<Course> SearchCourse(string keyword);
        Task<Course> GetCourseById(int courseId);
        Task<Course> AddCourse(Course Course);
        Task<bool> DeleteCourse(int CourseId);
        Task<bool> UpdateCourse(Course Course);
    }
}
