using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
    }
}
