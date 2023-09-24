using System.ComponentModel.DataAnnotations;

namespace PMQuanLy.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
