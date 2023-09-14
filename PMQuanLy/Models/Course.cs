using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime CourseStart { get; set; } // Time Course Start
        public int NumStudent { get; set; }
        public int Price { get; set; }
        public string Status { get; set; }  // Check the tuition fee has been completed or not
    }
}
