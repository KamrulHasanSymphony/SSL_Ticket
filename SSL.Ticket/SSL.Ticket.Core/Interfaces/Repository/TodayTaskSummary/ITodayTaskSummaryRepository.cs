using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Product
{
    public interface ITodayTaskSummaryRepository : IBaseRepository<TodayTaskSummaryVM>
    {
        TodayTaskSummaryVM MultiplePost(TodayTaskSummaryVM model);
    }
}
