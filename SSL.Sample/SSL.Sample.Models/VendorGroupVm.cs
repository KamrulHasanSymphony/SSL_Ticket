using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models
{
    public class VendorGroupVm
    {
        public int VendorGroupID { get; set; }
        public string VendorGroupName { get; set; }
        public string VendorGroupDescription { get; set; }
        public string GroupType { get; set; }
        public string Comments { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Country { get; set; }
    }
}
