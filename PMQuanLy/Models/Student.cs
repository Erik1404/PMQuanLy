using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string ParentName { get; set; }
        public int ParentPhone { get; set; }
        public string Avatar { get; set; }

    }
}
