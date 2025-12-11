using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models
{
    public class UOMVm
    {
        public int UOMId { get; set; }

        public string UOMName { get; set; }

        public string UOMCode { get; set; }

        public string Comments { get; set; }

        public string ActiveStatus { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public bool? IsArchive { get; set; }
    }
}
