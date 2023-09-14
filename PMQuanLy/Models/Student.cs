namespace PMQuanLy.Models
{
    public class Student : User
    {
        public int StudentId { get; set; }

        public string ParentName { get; set; }
        public string Classroom { get; set; }
    }
}
