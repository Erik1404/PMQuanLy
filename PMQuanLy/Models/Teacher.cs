namespace PMQuanLy.Models
{
    public class Teacher : User
    {
        public int IdentityCard { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime CooperationDay { get; set; } // Ngày hợp tác


      /*  // Quan hệ nhiều-nhiều với Course (Khóa học)
        public List<TeacherCourse> TeacherCourses { get; set; }*/
    }
}