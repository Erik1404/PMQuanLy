using PMQuanLy.Models;

namespace PMQuanLy.Interface
{
    public interface ICourseYearService
    {
        Task<List<CourseYear>> GetAllCourseYears();
        List<CourseYear> SearchCourseYear(string keyword);
        Task<CourseYear> AddCourseYear(CourseYear CourseYear);
        Task<bool> DeleteCourseYear(int CourseYearId);
        Task<bool> UpdateCourseYear(CourseYear CourseYear);
    }
}
