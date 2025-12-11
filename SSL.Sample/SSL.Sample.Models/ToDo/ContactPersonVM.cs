using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class ContactPersonVM : RegularVM
    {

        public int Id { get; set; }
        [Display(Name = "Client Name")]
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }
        [Display(Name = "Present Address")]
        public string PresentAddress { get; set; }
        public string PABX { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string FAX { get; set; }
        public string Attachment { get; set; }

        [Display(Name = "Client Name")]
        public string ClientName { get; set; }

        public string Operation { get; set; }

    }
}
