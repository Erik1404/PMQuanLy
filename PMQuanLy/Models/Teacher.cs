namespace PMQuanLy.Models
{
    public class Teacher : User
    {
        public int IdentityCard { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime CooperationDay { get; set; } // Ngày hợp tác

        /// Khóa ngoại để liên kết với Subject (Môn học)
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}