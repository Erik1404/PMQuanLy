using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public interface IStudentService
    {
        Student LoginForStudent(string email, string password);
        Student RegisterForStudent(Student newStudent);

        Task<bool> DeleteStudent(int studentId);
        Task<bool> UpdateStudent(Student student);
    }
}
