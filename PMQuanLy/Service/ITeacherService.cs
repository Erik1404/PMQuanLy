using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public interface ITeacherService
    {

        Task<List<Teacher>> GetAllTeachers();
        /*Task<Teacher> AddTeacher(Teacher Teacher);*/
        Task<bool> DeleteTeacher(int TeacherId);
        Task<bool> UpdateTeacher(Teacher Teacher);
    }
}
