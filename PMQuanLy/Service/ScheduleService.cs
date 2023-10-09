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



        public async Task<int> CountCourseInTeacherId(int TeacherId)
        {
            var count = _dbContext.Schedules.Where(x => x.TeacherId == TeacherId).Count();
            return count;
        }

        public async Task<List<Course>> ListCourseInTeacher(int TeacherId)
        {
            var q = from a in _dbContext.Schedules
                    join b in _dbContext.Courses
                    on a.CourseId equals b.CourseId
                    where a.TeacherId == TeacherId
                    select b;
            return await q.ToListAsync();
        }
    }
}
