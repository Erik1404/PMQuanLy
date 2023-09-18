using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    // for login
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Check Role Student or Teacher
    }
}
