using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateofBirth { get; set; }
        public string Email { get; set; }
        public int PhoneParent { get; set; }
        public string Address { get; set; }
        public string ParentName { get; set; }
        public string Password { get; set; }
        public string AvatarStudent { get; set; }
    }
}
