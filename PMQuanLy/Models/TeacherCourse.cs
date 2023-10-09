using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class TeacherCourse
    {
        [Key]
        public int TeacherCourseId { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
