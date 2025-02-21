using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileKoisk.Models
{
    public class UserData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; } // "Customer" 
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; } = true;

        
       

        // Helper property to get age
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
        }

        // Constructor with required fields
        public UserData()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            UserType = "Customer"; // Default value
            PhoneNumber = string.Empty;
            DateOfBirth = DateTime.Today;
        }

        // Helper method to validate user age
        public bool IsAdult()
        {
            return Age >= 18;
        }

        // Method to update last login
        public void UpdateLastLogin()
        {
            LastLoginAt = DateTime.Now;
        }
    }
}
