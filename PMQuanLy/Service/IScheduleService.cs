using PMQuanLy.Models;

namespace PMQuanLy.Service
{
    public interface IScheduleService
    {
        Task<List<Schedule>> GetAllSchedules();
        Task<Schedule> AddSchedule(Schedule Schedule);
        Task<bool> DeleteSchedule(int ScheduleId);
        Task<bool> UpdateSchedule(Schedule Schedule);       
    }
}
