using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public interface IUserService
    {
        User Login(string email, string password);
        User Register(User newRegister);
        string CheckRoleUser(string email);
    }
}
