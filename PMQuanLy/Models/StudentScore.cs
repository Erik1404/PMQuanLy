using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class StudentScore
    {
        [Key]
        public int StudentScoreId { get; set; }

        public int CourseEnrollmentId { get; set; }
        public CourseEnrollment CourseEnrollment { get; set; }

        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public string ScoreName { get; set; }

        public double ScoreValue { get; set; }

        // Xếp loại (ví dụ: "Trung bình", "Khá", "Giỏi")
        public string ScoreClassification { get; set; }

        // Hệ số
        public double ScoreCoefficient { get; set; }
    }
}
