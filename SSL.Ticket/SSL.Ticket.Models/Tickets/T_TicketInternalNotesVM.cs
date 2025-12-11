using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Models.Tickets
{
    public class T_TicketInternalNotesVM
    {
        public int Id { get; set; }

        public int? T_TicketId { get; set; }

        public string ShortNote { get; set; }

        public string Description { get; set; }

        public string AssigneeUserId { get; set; }

        public DateTime? CreatedAt { get; set; }

        public bool? IsActive { get; set; }
    }
}
