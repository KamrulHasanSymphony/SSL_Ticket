using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "User Id(Email)")]
        public string UserId { get; set; }

        [Display(Name = "Full Name")]
        public string UserName { get; set; }

        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
        [Display(Name = "Verification Code")]
        public string VerificationCode { get; set; }
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Alternative Email")]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]

        [Required(ErrorMessage = "Please enter your phone number.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Please enter a valid 11-digit phone number.")]
        public string PhoneNumber { get; set; }
        public string Operation { get; set; }
        [Display(Name = "Sage User Name")]
        public string? SageUserName { get; set; }
        public List<string> Roles { get; set; } // Add this line to include roles

        public bool IsAdmin { get; set; }
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        [Display(Name = "Client")]
        public int ClientId { get; set; }


        public Audit Audit;
        public UserProfile()
        {
            Audit = new Audit();
            Roles = new List<string>();
        }
    }
    public static class DefaultRoles
    {
        public const string User = "User";
    }
}
