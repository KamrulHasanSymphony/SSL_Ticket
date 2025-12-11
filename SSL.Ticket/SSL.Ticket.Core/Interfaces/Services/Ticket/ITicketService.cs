using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.Ticket.SSL.Ticket.Models;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;

namespace SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Ticket
{
    public interface ITicketService : IBaseService<T_TicketVm>
    {
        //List<OrganizationVM> GetAllOrganization();
        List<StackHolderVM> GetAllStackHolder();
        GridEntity<T_TicketVm> GetAllTicketMData(GridOptions options, string AssigneeUserId);
        List<T_ClientsVm> GetAllClient();
        List<T_SourcesVm> GetAllticketSourceData();
        List<T_ProductsVM> GetAllProductsData();
        GridEntity<UserBranch> GetUserProfileGrid(GridOptions options);
        List<T_DepartmentVm> GetAllDepartmentData();
        List<T_TicketTypeVm> GetAllTicketTypeData();
        GridEntity<T_TicketInternalNotesVM> GetAllEnternalNoteData(GridOptions options, int? id);
        List<T_TicketInternalNotesVM> GetInternalById(int id);
       
    }
}
