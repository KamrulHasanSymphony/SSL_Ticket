using SSL.Common.SSL.Common.Core.Interfaces.Repository;
using SSL.Sample.SSL.Sample.Models;
using SSL.Sample.SSL.Sample.Models.Support;
using SSL.Sample.SSL.Sample.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Core.Interfaces.Repository.Support
{
    public interface ISupportRepository : IBaseRepository<T_TicketVm>
    {
        AssignToVM AssignToInsert(AssignToVM detail);
        string GenerateCode(string CodeGroup, string CodeName, int branchId = 1);

    }
}
