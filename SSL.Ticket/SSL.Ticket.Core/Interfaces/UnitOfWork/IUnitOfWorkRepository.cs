using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Assignee;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Clients;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Collaboration;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Company;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.EnternalNote;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Product;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Task;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.TaskTime;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Ticket;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.TktEnternalNote;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Topics;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.UserProfile;
using SSL.Ticket.SSL.Ticket.Models;

namespace SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {

        ITicketRepository TicketRepository { get; }
        ITaskRepository TaskRepository { get; }
        ICollaborationRepository CollaborationRepository { get; }
        ICommonRepository CommonRepository { get; }       
        IEnternalNoteRepository EnternalNoteRepository { get; }
        IAssigneeRepository AssigneeRepository { get; }
        ITaskTimeRepository TaskTimeRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }
        ITktEnternalNoteRepository TktEnternalNoteRepository { get;}
        IClientsRepository ClientsRepository { get; }
        ITopicsRepository TopicsRepository { get; }
        IProductRepository ProductRepository { get; }
        ITodayTaskSummaryRepository TodayTaskSummaryRepository { get; }
    }
}
