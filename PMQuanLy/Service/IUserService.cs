using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public interface IUserService
    {
        User Login(string email, string password);
        User Register(User newRegister);
        User RegisterForStudent (User newRegisterStd);
        string CheckRoleUser(string email);
    }
}
