using PMQuanLy.Models;

namespace PMQuanLy.Service.Teacher
{
    public interface ITeacherService
    {
        Teacher LoginForTeacher(string email, string password);
        Teacher RegisterForTeacher(Teacher newTeacher);
    }
}
