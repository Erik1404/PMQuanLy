using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class CourseRegistration
    {
        [Key]
        public int RegistrationId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
