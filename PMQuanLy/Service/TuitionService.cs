using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Interface;
using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public class TuitionService : ITuitionService
    {
        private readonly PMQLDbContext _dbContext;

        public TuitionService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<Tuition>> GetAllTuitions()
        {
            return await _dbContext.Tuitions
                .Include(t => t.Student)
                .Include(t => t.CourseRegistrations)
                .ThenInclude(cr => cr.Course)
                .ToListAsync();
        }
        public async Task<decimal> GetTuitionByStudentId(int studentId)
        {
            // Lấy danh sách các khóa học đã đăng ký bởi học sinh
            var courseRegistrations = await _dbContext.CourseRegistrations
                .Include(cr => cr.Course)
                .Where(cr => cr.StudentId == studentId)
                .ToListAsync();

            decimal totalTuition = 0;

            foreach (var registration in courseRegistrations)
            {
                totalTuition += registration.Course.PriceCourse;
            }

            return totalTuition;
        }

        public Tuition UpdateTuition(int tuitionId, Tuition tuition)
        {
            var existingTuition = _dbContext.Tuitions.Find(tuitionId);

            if (existingTuition == null)
            {
                return null; // Hoặc thực hiện xử lý khác tùy theo yêu cầu của bạn
            }

            // Cập nhật thông tin Tuition dựa trên dữ liệu từ tuitionDTO
            existingTuition.DiscountAmount = tuition.DiscountAmount;
            existingTuition.DiscountReason = tuition.DiscountReason;

            // Tính toán TotalAmountAfterDiscount
            existingTuition.TotalAmountAfterDiscount = existingTuition.TotalTuition - tuition.DiscountAmount;

            _dbContext.SaveChanges();

            return existingTuition;
        }
    }
}
