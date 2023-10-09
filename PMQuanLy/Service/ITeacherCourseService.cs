using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public interface ITeacherCourseService
    {
        Task<bool> AddCourseForTeacher(int teacherId, int courseId);
        Task<bool> AddTeacherToCourse(int courseId, int teacherId);


        Task<List<Course>> GetCoursesForTeacher(int teacherId);
        Task<List<Teacher>> GetTeachersForCourse(int courseId);


        Task<bool> RemoveCourseFromTeacher(int teacherId, int courseId);
        Task<bool> RemoveTeacherFromCourse(int courseId, int teacherId);
    }
}

