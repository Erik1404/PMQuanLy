using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudents();
        Task<Student> GetStudentById(int studentId);
        Task<bool> DeleteStudent(int studentId);
        Task<bool> UpdateStudent(Student student);


    }
}
