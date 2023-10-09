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
        public int QuantityStudent { get; set; }
        public int PriceCourse { get; set; }
        public int SubjectId { get; set; }
    }
}
