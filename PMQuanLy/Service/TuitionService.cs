using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
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
            return await _dbContext.Tuitions.ToListAsync();
        }
        
        public async Task<Tuition> AddTuition(Tuition Tuition)
        {
            _dbContext.Tuitions.Add(Tuition);
            await _dbContext.SaveChangesAsync();
            return Tuition;
        }

        public async Task<bool> DeleteTuition(int TuitionId)
        {
            var Tuition = await _dbContext.Tuitions.FindAsync(TuitionId);
            if (Tuition == null)
                return false;
            _dbContext.Tuitions.Remove(Tuition);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTuition(Tuition Tuition)
        {
            _dbContext.Entry(Tuition).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
