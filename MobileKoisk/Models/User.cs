﻿
namespace MobileKoisk.Models
{
    // User-related models
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public decimal AccountBalance { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
