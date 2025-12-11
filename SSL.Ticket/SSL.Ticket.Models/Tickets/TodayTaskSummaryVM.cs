using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Models.Tickets
{
    public class TodayTaskSummaryVM
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public DateTime? Date { get; set; }
        public bool? IsPost { get; set; }
        public string? CreatedBy { get; set; }
        public string? PostedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? PostedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string? CreatedFrom { get; set; }
        public string? PostedFrom { get; set; }
        public string? LastUpdatedFrom { get; set; }
        public string? Operation { get; set; }
        public string? ErrorMsg { get; set; }

        public List<string> IDs { get; set; }

    }
}
