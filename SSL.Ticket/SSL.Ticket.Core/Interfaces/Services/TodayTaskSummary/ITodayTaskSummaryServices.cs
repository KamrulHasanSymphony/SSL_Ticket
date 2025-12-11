using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.TodayTaskSummary
{
    public interface ITodayTaskSummaryServices : IBaseService<TodayTaskSummaryVM>
    {
        GridEntity<TodayTaskSummaryVM> GetGridData(GridOptions options, string? assigneeUserId);
        ResultModel<TodayTaskSummaryVM> MultiplePost(TodayTaskSummaryVM model);
    }
}
