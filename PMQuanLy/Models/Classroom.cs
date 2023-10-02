namespace PMQuanLy.Models
{
    public class Classroom
    {
        public int ClassroomId { get; set; }
        public string ClassName { get; set; }
        public string ClassDesc { get; set; }
        // Thêm các trường dữ liệu khác nếu cần


        // Khóa ngoại để liên kết với Subject (Môn học)
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }



    }
}
