using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.Sample.SSL.Sample.Models;
using SSL.Sample.SSL.Sample.Models.Support;
using SSL.Sample.SSL.Sample.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Core.Interfaces.Services.Support
{
    public interface ISupportService : IBaseService<T_TicketVm>
    {
        
        List<OrganizationVM> GetAllOrganization();
        List<StackHolderVM> GetAllStackHolder();
        GridEntity<T_TicketVm> GetAllTicketMData(GridOptions options);
        List<TicketSourceVM> GetAllticketSourceData();
        List<TicketSourceVM> GetAllticketTopicData();
        List<TicketSourceVM> GetAllDepartmentData();
        List<TicketSourceVM> GetAllStatusData();
        List<TicketSourceVM> GetAllProductsData();
        List<StackHolderVM> GetAllAssignToData();
        List<AssignToVM> GetAssignToByTicketId(int id);
    }
}
