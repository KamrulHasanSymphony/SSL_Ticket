using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class TDVisitPlanVM
    {
        public int Id { get; set; }
        [Display(Name = "Lead Name")]
        public int ClientId { get; set; }
        [Display(Name = "Product Name")]
        public int ProductId { get; set; }
        [Display(Name = "Lead Contact Person")]
        public int ContactPersonId { get; set; }
        [Display(Name = "Team Leader Name")]
        public int TeamLeaderTDUserId { get; set; }
        [Display(Name = "Visit Date")]
        public string VisitDate { get; set; }
        [Display(Name = "Visit Start Time")]
        public string VisitStartTime { get; set; }
        [Display(Name = "Visit End Time")]
        public string VisitEndTime { get; set; }
        [Display(Name = "Visit Plan Details")]
        public string VisitPlanInfo { get; set; }

        public string Attachment { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedFrom { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateAt { get; set; }
        public string LastUpdateFrom { get; set; }

        [Display(Name = "Plan Type")]
        public string VisitType { get; set; }
        [Display(Name = "Visit Address")]
        public string VisitPlanAddress { get; set; }

        public string TeamMembersWithComma { get; set; }

        public List<TDVisitPlanDetailVM> detailTDVisitPlanDetailVMs { get; set; }



        [Display(Name = "Client Name")]
        public string ClientName { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Contact Person Name")]
        public string ContactPersonName { get; set; }
        public string ContactNumber { get; set; }
        [Display(Name = "Team Leader Name")]
        public string TeamLeaderName { get; set; }


        public string Operation { get; set; }

        [Display(Name = "Contact Person Designation")]
        public string ContactPersonDesignation { get; set; }

        [Display(Name = "Visit Date From")]
        public string VisitDateFrom { get; set; }

        [Display(Name = "Visit Date To")]
        public string VisitDateTo { get; set; }
    }
}
