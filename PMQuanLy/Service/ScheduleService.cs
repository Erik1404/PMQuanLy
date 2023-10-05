using Microsoft.EntityFrameworkCore;
using PMQuanLy.Data;
using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public class ScheduleService : IScheduleService
    {
        private readonly PMQLDbContext _dbContext;

        public ScheduleService(PMQLDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Schedule>> GetAllSchedules()
        {
            return await _dbContext.Schedules.ToListAsync();
        }
        
        public async Task<Schedule> AddSchedule(Schedule Schedule)
        {
            _dbContext.Schedules.Add(Schedule);
            await _dbContext.SaveChangesAsync();
            return Schedule;
        }

        public async Task<bool> DeleteSchedule(int ScheduleId)
        {
            var Schedule = await _dbContext.Schedules.FindAsync(ScheduleId);
            if (Schedule == null)
                return false;
            _dbContext.Schedules.Remove(Schedule);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSchedule(Schedule Schedule)
        {
            _dbContext.Entry(Schedule).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
