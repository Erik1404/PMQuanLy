﻿using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public class TeacherService : ITeacherService
    {
        private readonly PMQLDbContext _dbContext;

        public TeacherService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Teacher>> GetAllTeachers()
        {
            // Lấy tất cả các User có vai trò (Role) là "Teacher" 
            var Teachers = await _dbContext.Teachers
                .Where(u => u.Role == "Teacher")
                .ToListAsync();

            // Chuyển danh sách User thành danh sách Teacher
            var TeacherList = Teachers.Select(u => new Teacher
            {
                Email = u.Email,
                Password = "No Check this",
                FirstName = u.FirstName,
                LastName = u.LastName,
                DateOfBirth = u.DateOfBirth,
                Gender = u.Gender,
                Address = u.Address,
                Role = u.Role,
                CooperationDay=u.CooperationDay,
                IdentityCard=u.IdentityCard,
                Avatar = u.Avatar,
            }).ToList();

            return TeacherList;
        }





        //Delete Teacher
        public async Task<bool> DeleteTeacher(int userId)
        {
            // Tìm kiếm người dùng với UserId cụ thể
            var Teacher = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            // Kiểm tra xem người dùng tồn tại và có vai trò là "Teacher" không
            if (Teacher != null && Teacher.Role == "Teacher")
            {
                _dbContext.Users.Remove(Teacher);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }




        public async Task<bool> UpdateTeacher(Teacher Teacher)
        {
            // Kiểm tra xem sinh viên có tồn tại không
            var existingTeacher = await _dbContext.Teachers.FindAsync(Teacher.UserId);

            if (existingTeacher == null)
            {
                throw new ArgumentException("Sinh viên không tồn tại.");
            }

            // Kiểm tra xem Role của sinh viên có phải là "Teacher" không
            if (existingTeacher.Role != "Teacher")
            {
                throw new ArgumentException("Không thể chỉnh sửa thông tin của người dùng không phải là sinh viên.");
            }

            // Cập nhật thông tin sinh viên và lưu vào cơ sở dữ liệu
            _dbContext.Entry(existingTeacher).CurrentValues.SetValues(Teacher);
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