﻿using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class Tuition
    {
        [Key]
        public int TuitionId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
