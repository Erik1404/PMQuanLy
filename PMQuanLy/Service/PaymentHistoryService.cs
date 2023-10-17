using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Interface;
using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public class PaymentHistoryService : IPaymentHistoryService
    {
        private readonly PMQLDbContext _dbContext;

        public PaymentHistoryService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PaymentHistory>> GetAllPaymentHistories()
        {
            return await _dbContext.PaymentHistories.ToListAsync();
        }
       
        public async Task<PaymentHistory> GetPaymentHistoryById(int paymentHistoryId)
        {
            return await _dbContext.PaymentHistories.FirstOrDefaultAsync(p => p.PaymentHistoryId == paymentHistoryId);
        }


        public async Task<PaymentHistory> AddPaymentHistory(PaymentHistory paymentHistory)
        {
            _dbContext.PaymentHistories.Add(paymentHistory);
            await _dbContext.SaveChangesAsync();

            return paymentHistory;
        }
    }
}
