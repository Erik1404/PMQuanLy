using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class Score
    {
        [Key]
        public int ScoreId { get; set; }
        public StudentScore StudentScore { get; set; }

        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public double AverageScore { get; set; }

        public string ScoreClassification { get; set; }

    }
}
