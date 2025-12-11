using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class CRMServiceChargeVM : RegularVM
    {
        public int Id { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        [Display(Name = "Service Type")]
        public string ServiceType { get; set; }
        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }
        [Display(Name = "Unit Address")]
        public string UnitAddress { get; set; }
        [Display(Name = "Unit Start")]
        public string UnitStartDate { get; set; }
        [Display(Name = "Start Date")]
        public string ServiceStartDate { get; set; }
        [Display(Name = "End Date")]
        public string ServiceEndDate { get; set; }
        [Display(Name = "Amount(Tk)")]
        public decimal ServiceValue { get; set; }
        public string Notes { get; set; }
        public string NextIncreaseDate { get; set; }
        public decimal NextIncreaseValue { get; set; }
    }
}
