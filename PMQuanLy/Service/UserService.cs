using Microsoft.AspNetCore.Mvc;
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


        public User Login(string email, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == email && u.Password == password);

            if (user == null)
                return null;

            return user;
        }
        public User Register(User newRegister)
        {
            // Kiểm tra xem email đã tồn tại chưa
            if (_dbContext.Users.Any(u => u.Email == newRegister.Email))
            {
                return null; // Email đã tồn tại, không thể đăng ký
            }

            var newUser = new User
            {
                Email = newRegister.Email,
                Password = newRegister.Password,
                FirstName = newRegister.FirstName,
                LastName = newRegister.LastName,
                DateOfBirth = newRegister.DateOfBirth,
                Gender = newRegister.Gender,
                Address = newRegister.Address,
                Role = newRegister.Role,
            };

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            return newUser;
        }

        public string CheckRoleUser(string role)
        {
            if (role == "Student")
            {
                // Thực hiện các thao tác cho role Student
                return "Đang hoạt động với vai trò Student";
            }
            else if (role == "Teacher")
            {
                // Thực hiện các thao tác cho role Teacher
                return "Đang hoạt động với vai trò Teacher";
            }
            else if (role == "Admin")
            {
                // Thực hiện các thao tác cho role Admin
                return "Đang hoạt động với vai trò Admin";
            }
            else
            {
                // Xử lý khi không có quyền truy cập
                return "Không có quyền truy cập";
            }
        }
    }
}
