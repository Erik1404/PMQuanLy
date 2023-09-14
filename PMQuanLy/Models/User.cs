using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        /*public string ParentName { get; set; }*/
        public string AvatartUser { get; set; }

        public string Role { get; set; } // Student or Teacher


    }
}
