using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class CourseRegistration
    {
        [Key]
        public int CourseRegistrationId { get; set; }

        // Khóa ngoại đến Student (Học sinh)
        public int StudentId { get; set; }
        public Student Student { get; set; }

        // Khóa ngoại đến Course (Khóa học)
        public int CourseId { get; set; }
        public Course Course { get; set; }

      

        // Thời gian đăng ký
        public DateTime RegistrationDate { get; set; }
    }
}
