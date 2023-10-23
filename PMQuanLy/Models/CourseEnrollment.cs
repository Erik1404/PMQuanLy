using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class CourseEnrollment
    {
        [Key]
        public int CourseEnrollmentId { get; set; }

        public int TeacherCourseId { get; set; }
        public TeacherCourse TeacherCourse { get; set; }
        
        public int CourseRegistrationId { get; set; }
        public CourseRegistration CourseRegistration { get; set; }


        // Chúng ta có : teachercourse : phân công giáo viên phụ trách môn học 
        //               CourseRegistration : Học sinh đăng ký môn học
        //              >> Gộp 2 table này lại để sử dụng dữ liệu
    }
    public enum CourseEnrollmentStatus
    {
        Open,
        Close,
    }
  
}
