using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }
}
