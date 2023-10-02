using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string SubjectDesc { get; set; }

        // Quan hệ 1-nhiều với Classroom (Lớp học) 
        // Ví dụ : môn Bóng rổ - có lớp : Bóng rổ cơ bản, bóng rổ nâng cao
        public ICollection<Classroom> Classrooms { get; set; }


        // Quan hệ một-nhiều với Teacher (giáo viên)
        public ICollection<Teacher> Teachers { get; set; }
    }
}
