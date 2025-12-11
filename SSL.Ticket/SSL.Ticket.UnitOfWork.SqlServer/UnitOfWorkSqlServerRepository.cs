using Microsoft.Data.SqlClient;
using SSL.Common.SSL.Common.Core.Interfaces.Repository.CompanyInfos;
using SSL.CS.SSL.Common.Core.Interfaces.Repository.Company;
using SSL.CS.SSL.Common.Models;
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
using SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork;
using SSL.Ticket.SSL.Ticket.Repository.SqlServer.Assignee;
using SSL.Ticket.SSL.Ticket.Repository.SqlServer.Clients;
using SSL.Ticket.SSL.Ticket.Repository.SqlServer.Collaboration;
using SSL.Ticket.SSL.Ticket.Repository.SqlServer.Company;
using SSL.Ticket.SSL.Ticket.Repository.SqlServer.EnternalNote;
using SSL.Ticket.SSL.Ticket.Repository.SqlServer.Product;
using SSL.Ticket.SSL.Ticket.Repository.SqlServer.Task;
using SSL.Ticket.SSL.Ticket.Repository.SqlServer.TaskTime;
using SSL.Ticket.SSL.Ticket.Repository.SqlServer.Ticket;
using SSL.Ticket.SSL.Ticket.Repository.SqlServer.TktEnternalNote;
using SSL.Ticket.SSL.Ticket.Repository.SqlServer.TodayTaskSummary;
using SSL.Ticket.SSL.Ticket.Repository.SqlServer.Topics;
using SSL.Ticket.SSL.Ticket.Repository.SqlServer.UserProfile;
//using System.Data.SqlClient;

namespace SSL.Ticket.SSL.Ticket.UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
    {

        public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction, DbConfig dBConfig)
        {
            TicketRepository = new TicketRepository(context, transaction, dBConfig);
            TaskRepository = new TaskRepository(context, transaction, dBConfig);
            EnternalNoteRepository = new EnternalNoteRepository(context, transaction, dBConfig);
            CollaborationRepository = new CollaborationRepository(context, transaction, dBConfig);
            AssigneeRepository = new AssigneeRepository(context, transaction, dBConfig);
            TaskTimeRepository = new TaskTimeRepository(context, transaction, dBConfig);
            CompanyRepository = new CompanyRepository(context, transaction, dBConfig);
            UserProfileRepository = new UserProfileRepository(context, transaction, dBConfig);
            TktEnternalNoteRepository = new TktEnternalNoteRepository(context, transaction, dBConfig);
            ClientsRepository = new ClientsRepository(context, transaction, dBConfig);
            TopicsRepository = new TopicsRepository(context, transaction, dBConfig);
            ProductRepository = new ProductRepository(context, transaction, dBConfig);
            TodayTaskSummaryRepository = new TodayTaskSummaryRepository(context, transaction, dBConfig);
        }

        public ICommonRepository CommonRepository { get; }
        public ITicketRepository TicketRepository { get; }
        public ITaskRepository TaskRepository { get; }
        public IEnternalNoteRepository EnternalNoteRepository { get; }
        public ICollaborationRepository CollaborationRepository { get; }
        public IAssigneeRepository AssigneeRepository { get; }
        public ITaskTimeRepository TaskTimeRepository { get; }
        public ICompanyRepository CompanyRepository { get; }
        public ICompanyInfoRepository CompanyInfoRepository => throw new NotImplementedException();
        public ICompanyinfosRepository CompanyInfosRepository => throw new NotImplementedException();
        public IUserProfileRepository UserProfileRepository {get; }
        public ITktEnternalNoteRepository TktEnternalNoteRepository { get; }
        public IClientsRepository ClientsRepository { get; }
        public ITopicsRepository TopicsRepository { get; }
        public IProductRepository ProductRepository { get; }
        public ITodayTaskSummaryRepository TodayTaskSummaryRepository { get; }
    }
}
