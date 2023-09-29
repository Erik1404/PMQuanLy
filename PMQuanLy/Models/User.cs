﻿using System.ComponentModel.DataAnnotations;

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
        public string Role { get; set; } // chia role Student - Teacher - Admin
       
        public string Avatar { get; set; }


       /* //Student
        public int ParentPhone { get; set; }
        public string ParentName { get; set; }

        //Teacher
        public int IdentityCard { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime CooperationDay { get; set; } // Ngày hợp tác
        public string Subject { get; set; }*/
    }
}
