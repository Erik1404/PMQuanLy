using PMQuanLy.Models;

namespace PMQuanLy.Interface
{
    public interface IPaymentHistoryService
    {
        Task<List<PaymentHistory>> GetAllPaymentHistories();
        Task<PaymentHistory> GetPaymentHistoryById(int paymentHistoryId);
        Task<PaymentHistory> AddPaymentHistory(PaymentHistory paymentHistory);
    }
}
