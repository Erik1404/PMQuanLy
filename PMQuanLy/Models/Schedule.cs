using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
