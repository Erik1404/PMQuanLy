using PMQuanLy.Models;

namespace PMQuanLy.Interface
{
    public interface ITuitionService
    {
        Task<List<Tuition>> GetAllTuitions();
        
        Task<decimal> GetTuitionByStudentId(int studentId);

        Tuition UpdateTuition(int tuitionId, decimal DiscountAmount, string DiscountReason);

        Tuition Checkout(int tuitionId, decimal amountPaid);

    }
}
