using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class TDVisitPlanDetailVM
    {
        public int Id { get; set; }
        public int TDVisitPlanId { get; set; }
        [Display(Name = "Team Member Name")]
        public int TDUserId { get; set; }
        public string Remarks { get; set; }


        [Display(Name = "Team Member Name")]
        public string TeamMemberName { get; set; }

        [Display(Name = "Client Name")]
        public int ClientId { get; set; }
        [Display(Name = "Product Name")]
        public int ProductId { get; set; }
        [Display(Name = "Contact Person Name")]
        public int ContactPersonId { get; set; }

        [Display(Name = "Client Name")]
        public string ClientName { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Contact Person Name")]
        public string ContactPersonName { get; set; }
        [Display(Name = "Contact Person Designation")]
        public string ContactPersonDesignation { get; set; }

        [Display(Name = "Visit Date")]
        public string VisitDate { get; set; }
        [Display(Name = "Visit Start Time")]
        public string VisitStartTime { get; set; }
        [Display(Name = "Visit End Time")]
        public string VisitEndTime { get; set; }


        [Display(Name = "Team Leader Name")]
        public string TeamLeaderName { get; set; }

        public string TeamMemberEmail { get; set; }

        public string TeamLeaderEmail { get; set; }
    }
}
