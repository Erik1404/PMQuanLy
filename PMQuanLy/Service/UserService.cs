using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Models;
using System.Security.Cryptography;
using System.Text;

namespace PMQuanLy.Service
{
    public class UserService : IUserService
    {
        private readonly PMQLDbContext _dbContext;

        public UserService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public User Authenticate(string email, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
                return null;

            return user;
        }

        public User Register(string email, string password)
        {
            // Kiểm tra xem email đã tồn tại chưa
            if(_dbContext.Users.Any(u => u.Email == email))
            {
                return null;
            }    
            var newUser = new User
            {
                Email = email,
                Password = password
            };

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            return newUser;
        }

    }
}
