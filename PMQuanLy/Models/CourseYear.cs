namespace PMQuanLy.Models
{
    public class CourseYear
    {
        public int CourseYearId { get; set; }
        public string CourseYear_Year { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
