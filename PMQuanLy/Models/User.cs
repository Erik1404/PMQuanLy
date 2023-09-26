using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PMQuanLy.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
    }

}
