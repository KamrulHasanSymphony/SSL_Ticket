using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class TDUserVM
    {
        public int Id { get; set; }
        public string LogId { get; set; }
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        public string Email { get; set; }
        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }

        [Display(Name = "Support User")]
        public bool IsSupportUser { get; set; }

        [Display(Name = "Client")]
        public bool IsClient { get; set; }

        [StringLength(450, ErrorMessage = "Remarks cannot be longer than 450 characters.")]
        public string Remarks { get; set; }

        public List<UserClientDetails> UserClientDetails { get; set; }


        public string ProjectName { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedFrom { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateAt { get; set; }
        public string LastUpdateFrom { get; set; }
        public string Operation { get; set; }
        public string OldPassword { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        [Display(Name = "Supervisor")]
        public bool IsSupervisor { get; set; }
        [Display(Name = "Supervisor LogId")]
        public string LogIdSupervisor { get; set; }

    }
}
