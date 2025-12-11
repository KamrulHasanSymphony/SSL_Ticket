using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Topics
{
    public interface ITopicsServices : IBaseService<T_TopicsVM>
    {
        GridEntity<T_TopicsVM> GetGridData(GridOptions options);

    }
}
