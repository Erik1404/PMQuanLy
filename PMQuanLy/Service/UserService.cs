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
       /* public User Register(User newRegister)
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
        }*/

        public Student RegisterForStudent(Student newStudent)
        {
            // Kiểm tra xem email đã tồn tại chưa
            if (_dbContext.Students.Any(u => u.Email == newStudent.Email))
            {
                return null; // Hoặc thực hiện xử lý lỗi nếu cần
            }

            // Tạo một đối tượng User từ dữ liệu của Student
            var newStd = new Student
            {
                Email = newStudent.Email,
                Password = newStudent.Password, // Sử dụng mật khẩu truyền vào từ client
                FirstName = newStudent.FirstName,
                LastName = newStudent.LastName,
                DateOfBirth = newStudent.DateOfBirth,
                Gender = newStudent.Gender,
                Address = newStudent.Address,
                Role = "Student", // Đặt vai trò là "Student"
                ParentName = newStudent.ParentName,
                ParentPhone = newStudent.ParentPhone,
                Avatar = newStudent.Avatar,
            };
            if (!IsValidEmail(newStd.Email))
            {
                throw new ArgumentException("Email không hợp lệ");
            }

            if (!IsValidPassword(newStd.Password))
            {
                throw new ArgumentException("Mật khẩu cần ít nhất 8 ký tự");
            }

            if (!IsValidPhoneNumber(newStd.ParentPhone))
            {
                throw new ArgumentException("Số điện thoại cần 10 số");
            }
            _dbContext.Users.Add(newStd);
            _dbContext.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            return newStd;
        }


        public Teacher RegisterForTeacher(Teacher newTeacher)
        {
            // Kiểm tra xem email đã tồn tại chưa
            if (_dbContext.Teachers.Any(u => u.Email == newTeacher.Email))
            {
                return null; // Hoặc thực hiện xử lý lỗi nếu cần
            }

            // Tạo một đối tượng User từ dữ liệu của Teacher
            var newStd = new Teacher
            {
                Email = newTeacher.Email,
                Password = newTeacher.Password, // Sử dụng mật khẩu truyền vào từ client
                FirstName = newTeacher.FirstName,
                LastName = newTeacher.LastName,
                DateOfBirth = newTeacher.DateOfBirth,
                Gender = newTeacher.Gender,
                Address = newTeacher.Address,
                Role = "Teacher", // Đặt vai trò là "Student"
                CooperationDay = newTeacher.CooperationDay,
                IdentityCard = newTeacher.IdentityCard,
                PhoneNumber = newTeacher.PhoneNumber,
                Subject = newTeacher.Subject,
                Avatar = newTeacher.Avatar,
            };
            if (!IsValidEmail(newStd.Email))
            {
                throw new ArgumentException("Email không hợp lệ");
            }

            if (!IsValidPassword(newStd.Password))
            {
                throw new ArgumentException("Mật khẩu cần ít nhất 8 ký tự");
            }

            if (!IsValidPhoneNumber(newStd.PhoneNumber))
            {
                throw new ArgumentException("Số điện thoại cần 10 số");
            }
            _dbContext.Users.Add(newStd);
            _dbContext.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            return newStd;
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



        // Check Form Email , Password and Phone
        private bool IsValidPhoneNumber(int phoneNumber)
        {
            string phoneNumberString = phoneNumber.ToString();

            // Phone number must be 10 digits and can start with number 0 with form NumberPhone of VN
            return phoneNumberString.Length == 10 && phoneNumberString.All(char.IsDigit);
        }
        private bool IsValidEmail(string email)
        {
            // Check Form EMAIL
            return !string.IsNullOrEmpty(email) && email.Contains("@");
        }

        private bool IsValidPassword(string password)
        {
            // Password need >= 8 characters
            return !string.IsNullOrEmpty(password) && password.Length >= 8;
        }
    }
}
