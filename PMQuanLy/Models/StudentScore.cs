using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class StudentScore
    {
        [Key]
        public int StudentScoreId { get; set; }

        // Khóa ngoại đến CourseEnrollment (để biết học sinh thuộc lớp nào)
        public int CourseEnrollmentId { get; set; }
        public CourseEnrollment CourseEnrollment { get; set; }

        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public string ScoreName { get; set; }

        public double ScoreValue { get; set; }

        // Xếp loại (ví dụ: "Trung bình", "Khá", "Giỏi")
        public string ScoreClassification { get; set; }

        // Hệ số (ví dụ: 1, 3)
        public double ScoreCoefficient { get; set; }
    }
}
