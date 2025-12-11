
using SSL.Ticket.SSL.Ticket.Models.Tickets;

namespace SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Ticket
{
    public interface ISupportRepository : IBaseRepository<T_TicketVm>
    {
        
        string GenerateCode(string CodeGroup, string CodeName, int branchId = 1);

    }
}
