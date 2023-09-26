using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public int IdentityCard { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CooperationDay { get; set; } // Ngày hợp tác
        public string Subject { get; set; }
        public string Avatar { get; set; }
    }
}
