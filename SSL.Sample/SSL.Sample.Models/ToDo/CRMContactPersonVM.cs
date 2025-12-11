using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class CRMContactPersonVM : RegularVM
    {
        public int Id { get; set; }
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        [Display(Name = "Email 1")]
        public string Email1 { get; set; }
        [Display(Name = "Email 1")]
        public string Email2 { get; set; }
        [Display(Name = "Email 1")]
        public string Email3 { get; set; }
        [Display(Name = "Mobile 1")]
        public string Mobile1 { get; set; }
        [Display(Name = "Mobile 2")]
        public string Mobile2 { get; set; }
        [Display(Name = "Mobile 3")]
        public string Mobile3 { get; set; }
        public string Notes { get; set; }
    }
}
