using PMQuanLy.Models;

namespace PMQuanLy.Interface
{
    public interface IScheduleService
    {
        Task<List<Schedule>> GetAllSchedules();

        Task<Schedule> AddSchedule(Schedule Schedule);
        Task<bool> DeleteSchedule(int ScheduleId);
        Task<bool> UpdateSchedule(Schedule Schedule);

        Task<int> CountCourseInTeacherId(int TeacherId);
        Task<List<Course>> ListCourseInTeacher(int TeacherId);
    }
}
