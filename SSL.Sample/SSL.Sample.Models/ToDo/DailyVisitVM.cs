using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class DailyVisitVM : RegularVM
    {
        public int Id { get; set; }
        [Display(Name = "Lead Name")]
        public int ClientId { get; set; }
        [Display(Name = "Product Name")]
        public int ProductId { get; set; }
        [Display(Name = "Lead Contact Person")]
        public string ContactPersonName { get; set; }
        [Display(Name = "Lead Person Designation")]
        public string ContactPersonDesignation { get; set; }
        [Display(Name = "Visit History")]
        public string VisitHistory { get; set; }
        public string Attachment { get; set; }

        public string Operation { get; set; }

        [Display(Name = "Client Name")]
        public string ClientName { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Visit Date")]
        public string VisitDate { get; set; }
        [Display(Name = "Next Visit Date")]
        public string NextVisitDate { get; set; }

        [Display(Name = "Contact Person Name")]
        public int ContactPersonId { get; set; }
        [Display(Name = "Visit Date From")]
        public string VisitDateFrom { get; set; }
        [Display(Name = "Visit Date To")]
        public string VisitDateTo { get; set; }
        [Display(Name = "Visit By")]
        public string VisitBy { get; set; }

        [Display(Name = "Next Visit Date From")]
        public string NextVisitDateFrom { get; set; }
        [Display(Name = "Next Visit Date To")]
        public string NextVisitDateTo { get; set; }

        public string HeadGraphicLocation { get; set; }

        [Display(Name = "Visit Address")]
        public string VisitAddress { get; set; }

        [Display(Name = "Visit Type")]
        public string VisitType { get; set; }
        [Display(Name = "Next Visit Type")]
        public string NextVisitType { get; set; }

    }
}
