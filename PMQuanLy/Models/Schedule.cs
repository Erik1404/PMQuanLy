namespace PMQuanLy.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
