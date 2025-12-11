using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Models
{
    public class TUserProfile
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string LogId { get; set; }

        public string Password { get; set; }
        public string Address { get; set; }

        public string VerificationCode { get; set; }

        public string AuthId { get; set; }
        public int DepartmentId { get; set; }
        public int ClientId { get; set; }

        public bool IsClient { get; set; }
        public bool IsActive { get; set; }

        public bool IsVerified { get; set; }

        public bool IsArchived { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedFrom { get; set; }

        public string LastUpdateBy { get; set; }

        public DateTime? LastUpdateAt { get; set; }

        public string LastUpdateFrom { get; set; }
    }
}
