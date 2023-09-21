namespace PMQuanLy.Models
{
    public class CourseRegistration
    {
        public int RegistrationId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
