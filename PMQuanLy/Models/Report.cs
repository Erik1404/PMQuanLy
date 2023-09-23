using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime ReportDate { get; set; }
        public string Description { get; set; }
        // Thêm các thuộc tính khác cho báo cáo
    }
}
