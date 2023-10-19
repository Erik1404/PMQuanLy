using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }

        public string SchoolDay { get; set; }

        public string TimeClass { get; set; }
      
        public int PriceCourse { get; set; }

        public CourseStatus CourseStatus { get; set; }

        public int MinimumStudents { get; set; }
        public int MaximumStudents { get; set; }

        // Ngày bắt đầu học
        public DateTime DayStartCourse { get; set; }

        // Ngày kết thúc
        public DateTime DayuEndCourse { get; set; }

        // Ngày mở đăng ký lớp học này
        public DateTime RegistrationStartDate { get; set; }
        public DateTime RegistrationEndDate { get; set; }


        public int SubjectId { get; set; }
    }

    public enum CourseStatus
    {
       Open,
       Close,
    }

}
