using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.Ticket.SSL.Ticket.Models;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Clients
{
    public interface IClientsService : IBaseService<T_ClientsVm>
    {
        GridEntity<T_ClientsVm> GetGridData(GridOptions options);

    }
}
