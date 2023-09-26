using PMQuanLy.Data;
using PMQuanLy.Models;
using Microsoft.EntityFrameworkCore;


namespace PMQuanLy.Service
{
    public class StudentService : IStudentService
    {
        private readonly PMQLDbContext _dbContext;

        public StudentService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Login and Register for Student
        public Student LoginForStudent(string email, string password)
        {
            var student = _dbContext.Students.SingleOrDefault(u => u.Email == email && u.Password == password);

            if (student == null)
                return null;

            return student;
        }

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
                Password = newStudent.Password,
                FirstName = newStudent.FirstName,
                LastName = newStudent.LastName,
                DateOfBirth = newStudent.DateOfBirth,
                Gender = newStudent.Gender,
                Address = newStudent.Address,
                ParentPhone = newStudent.ParentPhone,
                ParentName = newStudent.ParentName,
                Avatar = newStudent.Avatar,
                Role = "Student", // Đặt vai trò là "Student"
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

            _dbContext.Students.Add(newStd);
            _dbContext.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
            return newStd;
        }

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

            if (!IsValidPhoneNumber(student.ParentPhone))
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
