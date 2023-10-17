using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class Tuition
    {
        [Key]
        public int TuitionId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }

       

        // Trạng thái thanh toán (đã thanh toán hoặc chưa thanh toán)
        public bool IsPaid { get; set; }

        // Tổng học phí của học sinh
        public decimal TotalTuition { get; set; }

        // Danh sách các CourseRegistration (đăng ký khóa học) của học sinh
        public virtual ICollection<CourseRegistration> CourseRegistrations { get; set; }

        // Số tiền đã trả
        public decimal AmountPaid { get; set; }
        // Số tiền giảm giá
        public decimal DiscountAmount { get; set; }

        // Lý do giảm giá
        public string DiscountReason { get; set; }

        // Số tiền còn lại
        public decimal RemainingAmount { get; set; }

        // Tổng tiền sau khi áp dụng giảm giá
        public decimal TotalAmountAfterDiscount { get; set; }

    }
}
