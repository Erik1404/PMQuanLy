namespace PMQuanLy.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string Subject_Name { get; set; }
        public string Subject_Description { get; set;}

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
