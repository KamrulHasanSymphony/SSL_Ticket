using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Models.Tickets
{
    public class T_ClientsVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string District { get; set; }
        [Display(Name = "Contact No.")]
        public string CompanyContactNumber { get; set; }
        [Display(Name = "Company Email.")]

        public string CompanyEmail { get; set; }

        public string Website { get; set; }
        [Display(Name = "Concern Person.")]

        public string ConcernPerson { get; set; }
        [Display(Name = "Contact Person Number.")]

        public string ContactPersonNumber { get; set; }
        [Display(Name = "Contact Person Email.")]

        public string ContactPersonEmail { get; set; }

        public DateTime? CreateOn { get; set; }

        public string CreateBy { get; set; }

        public DateTime? UpdateOn { get; set; }

        public string UpdateBy { get; set; }
        [Display(Name = "Is Active.")]
        public bool IsActive { get; set; }
        public string Operation { get; set; }
        public string ErrorMsg { get; set; }
    }
}
