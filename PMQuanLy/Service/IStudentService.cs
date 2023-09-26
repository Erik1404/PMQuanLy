using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudents();
        /*Task<Student> AddStudent(Student student);*/
        Task<bool> DeleteStudent(int studentId);
        Task<bool> UpdateStudent(Student student);


        Student LoginForStudent(string email, string password);
        Student RegisterForStudent(Student newStudent);


    }
}
