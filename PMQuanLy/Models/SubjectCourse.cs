using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class SubjectCourse
    {
        [Key]
        public int SubjectCourseId { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
