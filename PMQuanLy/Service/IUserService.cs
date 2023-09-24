using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        User Register(string email, string password);
    }
}
