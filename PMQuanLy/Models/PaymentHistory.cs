using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class PaymentHistory
    {
        [Key]
        public int PaymentHistoryId { get; set; }

        public int TuitionId { get; set; }
        public Tuition Tuition { get; set; }

        public decimal PaymentAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
