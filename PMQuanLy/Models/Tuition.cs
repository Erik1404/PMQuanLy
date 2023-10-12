using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class Tuition
    {
        [Key]
        public int TuitionId { get; set; }

        // Khóa ngoại đến Student (Học sinh)
        public int StudentId { get; set; }
        public Student Student { get; set; }

        // Danh sách các CourseRegistration (đăng ký khóa học) của học sinh
        public virtual ICollection<CourseRegistration> CourseRegistrations { get; set; }

        // Tổng học phí của học sinh
        public decimal TotalTuition { get; set; }

        // Ngày thanh toán
        public DateTime PaymentDate { get; set; }
    }
}
