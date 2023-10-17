using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class PaymentHistory
    {
        [Key]
        public int PaymentHistoryId { get; set; }

        // Khóa ngoại đến Tuition
        public int TuitionId { get; set; }
        public Tuition Tuition { get; set; }

        // Trạng thái thanh toán (đã thanh toán hoặc chưa thanh toán)
        public bool IsPaid { get; set; }
        // Số tiền đã trả
        public decimal AmountPaid { get; set; }

        // Thời gian thanh toán
        public DateTime PaymentDate { get; set; }

        // Số tiền còn lại
        public decimal RemainingAmount { get; set; }
    }
}
