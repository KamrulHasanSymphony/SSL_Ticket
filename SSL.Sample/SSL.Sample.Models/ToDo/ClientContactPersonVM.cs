using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class ClientContactPersonVM
    {

        public int Id { get; set; }
        [Display(Name = "Client Name")]
        public int ClientId { get; set; }
        [Display(Name = "Contact Person Name")]
        public int ContactPersonId { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedFrom { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateAt { get; set; }
        public string LastUpdateFrom { get; set; }

        [Display(Name = "Contact Person Name")]
        public string ContactPersonName { get; set; }
        public string ContactPersonDepartment { get; set; }
        public string ContactPersonDesignation { get; set; }
        public string ContactPersonPresentAddress { get; set; }
        public string ContactPersonMobile { get; set; }
        public string ContactPersonEmail { get; set; }

        public string Name { get; set; }
    }
}
