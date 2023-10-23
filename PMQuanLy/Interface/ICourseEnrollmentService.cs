using PMQuanLy.Models;

namespace PMQuanLy.Interface
{
    public interface ICourseEnrollmentService
    {
        Task CreateStudentScore(int courseEnrollmentId, string scoreName, double scoreValue, double scoreCoefficient);
        
        Task<List<CourseEnrollment>> GetAllCourseEnrollments();
    }
}
