using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Models;
using System.Text.RegularExpressions;

namespace PMQuanLy.Service
{
    // Attention : go to Program.cs, Add builder to use Service
    public class StudentService : IStudentService
    {
        private readonly PMQLDbContext _dbContext;

        public StudentService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Student> GetStudentById(int studentId)
        {
            return await _dbContext.Students.FindAsync(studentId);
        }
        public async Task<List<Student>> GetAllStudents()
        {
            // Lấy tất cả các User có vai trò (Role) là "Student" 
            var students = await _dbContext.Students
                .Where(u => u.Role == "Student")
                .ToListAsync();

            // Chuyển danh sách User thành danh sách Student
            var studentList = students.Select(u => new Student
            {
                Email = u.Email,
                Password = "No Check this",
                FirstName = u.FirstName,
                LastName = u.LastName,
                DateOfBirth = u.DateOfBirth,
                Gender = u.Gender,
                Address = u.Address,
                Role = u.Role,
                ParentName = u.ParentName, // Thêm các trường dữ liệu cần thiết
                ParentPhone = u.ParentPhone,
                Avatar = u.Avatar,
            }).ToList();

            return studentList;
        }





        //Delete Student
        public async Task<bool> DeleteStudent(int userId)
        {
            // Tìm kiếm người dùng với UserId cụ thể
            var student = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            // Kiểm tra xem người dùng tồn tại và có vai trò là "Student" không
            if (student != null && student.Role == "Student")
            { 
                    _dbContext.Users.Remove(student);
                    await _dbContext.SaveChangesAsync();
                    return true;             
            }

            return false;
        }


      

        public async Task<bool> UpdateStudent(Student student)
        {
            // Kiểm tra xem sinh viên có tồn tại không
            var existingStudent = await _dbContext.Students.FindAsync(student.UserId);

            if (existingStudent == null)
            {
                throw new ArgumentException("Sinh viên không tồn tại.");
            }

            // Kiểm tra xem Role của sinh viên có phải là "Student" không
            if (existingStudent.Role != "Student")
            {
                throw new ArgumentException("Không thể chỉnh sửa thông tin của người dùng không phải là sinh viên.");
            }

            // Cập nhật thông tin sinh viên và lưu vào cơ sở dữ liệu
            _dbContext.Entry(existingStudent).CurrentValues.SetValues(student);
            await _dbContext.SaveChangesAsync();

            return true;
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
