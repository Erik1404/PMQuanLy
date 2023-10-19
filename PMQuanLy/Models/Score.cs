using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class Score
    {
        [Key]
        public int ScoreId { get; set; }
      
        // 3 cột điểm
        public string Score1_Name { get; set; }
        public decimal Score1 { get; set; }

        public string Score2_Name { get; set; }
        public decimal Score2 { get; set; }

        public string Score3_Name { get; set; }
        public decimal Score3 { get; set; }

        // Khóa ngoại đến CourseRegistration
        public int CourseRegistrationId { get; set; }
        public CourseRegistration CourseRegistration { get; set; }
    }
}
