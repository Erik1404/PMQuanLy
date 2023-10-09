using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public interface ICourseRegistrationService
    {
        Task<List<CourseRegistration>> GetAllCourseRegistrations();
        Task<CourseRegistration> AddCourseRegistration(CourseRegistration CourseRegistration);
        Task<bool> DeleteCourseRegistration(int CourseRegistrationId);
        Task<bool> UpdateCourseRegistration(CourseRegistration CourseRegistration);
        Task<int> CountStudentInCourse(int CourseId);
        Task<List<Student>> ListStudentInCourse(int CourseId);
        Task<Course> FindCoureByStudentId(int StudentId);
    }
}
