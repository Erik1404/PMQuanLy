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

        public async Task<List<Student>> GetAllStudents()
        {
            return await _dbContext.Students.ToListAsync();
        }

        /* public async Task<bool> GetStudentById(int studentId)
         {

             return _dbContext.Students.FirstOrDefault(s => s.StudentId == studentId);
         }*/
        //ADD STUDENT START
        public async Task<Student> AddStudent(Student student)
        {
            if (!IsValidEmail(student.Email))
            {
                throw new ArgumentException("Email không hợp lệ");
            }

            if (!IsValidPassword(student.Password))
            {
                throw new ArgumentException("Mật khẩu cần ít nhất 8 ký tự");
            }

            if (!IsValidPhoneNumber(student.PhoneParent))
            {
                throw new ArgumentException("Số điện thoại cần 10 số");
            }

            _dbContext.Students.Add(student);

            try
            {
                await _dbContext.SaveChangesAsync();
                return student;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        //ADD STUDENT END


        //Delete Student
        public async Task<bool> DeleteStudent(int studentId)
        {
            var student = await _dbContext.Students.FindAsync(studentId);
            if (student == null)
                return false;
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        // Update Student
        public async Task<bool> UpdateStudent(Student student)
        {
            // Kiểm tra các ràng buộc trước khi cập nhật
            if (!IsValidEmail(student.Email))
            {
                throw new ArgumentException("Invalid email address");
            }

            if (!IsValidPassword(student.Password))
            {
                throw new ArgumentException("Password must be at least 8 characters long");
            }

            if (!IsValidPhoneNumber(student.PhoneParent))
            {
                throw new ArgumentException("Phone number must be 10 digits");
            }

            _dbContext.Entry(student).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {

                throw;
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
