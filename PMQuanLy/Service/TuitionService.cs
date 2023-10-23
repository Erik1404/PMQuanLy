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

        public Tuition UpdateTuition(int tuitionId, decimal DiscountAmount, string DiscountReason)
        {
            var existingTuition = _dbContext.Tuitions.Find(tuitionId);

            if (existingTuition == null)
            {
                return null;
            }

            // Cập nhật thông tin Tuition dựa trên dữ liệu từ tuitionDTO
            existingTuition.DiscountAmount = DiscountAmount;
            existingTuition.DiscountReason = DiscountReason;

            // Tính toán TotalAmountAfterDiscount
            existingTuition.TotalAmountAfterDiscount = existingTuition.TotalTuition - DiscountAmount;

            _dbContext.SaveChanges();

            return existingTuition;
        }

        public Tuition Checkout(int tuitionId, decimal amountPaid)
        {
            var existingTuition = _dbContext.Tuitions.Find(tuitionId);

            if (existingTuition == null)
            {
                return null; 
            }

            // Kiểm tra nếu học sinh đã đóng nhiều hơn số tiền cần phải trả
            if (amountPaid >= existingTuition.TotalAmountAfterDiscount)
            {
                existingTuition.IsPaid = true;
            }

            // Cập nhật AmountPaid bằng tổng số tiền học sinh đã đóng
            existingTuition.AmountPaid += amountPaid;

            // Số tiền còn lại
            existingTuition.RemainingAmount = existingTuition.TotalAmountAfterDiscount - existingTuition.AmountPaid;

            var paymentHistory = new PaymentHistory
            {
                TuitionId = existingTuition.TuitionId,
                PaymentDate = DateTime.Now,
                AmountPaid = amountPaid,
                RemainingAmount = existingTuition.RemainingAmount,
                IsPaid = existingTuition.IsPaid,
            };

            _dbContext.PaymentHistories.Add(paymentHistory);
            _dbContext.SaveChanges();

            return existingTuition;
        }

    }
}
