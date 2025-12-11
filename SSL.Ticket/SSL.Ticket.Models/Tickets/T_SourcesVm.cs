using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Models.Tickets
{
    public class T_SourcesVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? CreateOn { get; set; }

        public string CreateBy { get; set; }

        public DateTime? UpdateOn { get; set; }

        public string UpdateBy { get; set; }

        public bool? IsActive { get; set; }
    }
}
