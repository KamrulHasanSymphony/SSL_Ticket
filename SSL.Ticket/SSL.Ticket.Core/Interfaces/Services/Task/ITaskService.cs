using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Models;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;

namespace SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Task
{
    public interface ITaskService : IBaseService<T_TasksVM>
    {
        GridEntity<T_TaskAssignesVM> GetAllAssigneeData(GridOptions options, int? id);
        GridEntity<T_TaskCollaborationsVM> GetAllCollaborationData(GridOptions options, int? id);
        GridEntity<T_TaskInternalNotesVM> GetAllEnternalNoteData(GridOptions options, int? id);
        List<T_PrioritiesVM> GetAllPriorityData();
        List<T_RatingsVm> GetAllRatingData();
        List<T_StatusVM> GetAllStatusData();
        GridEntity<T_TaskTimesVM> GetAllTaskTimeData(GridOptions options, int? id);
        List<T_TicketVm> GetAllTicket();
        List<T_TopicsVM> GetAllTopicData();
        GridEntity<T_TasksVM> GetAllTTaskMData(GridOptions options, int? ticketId, string AssigneeUserId);
        List<T_TaskAssignesVM> GetAssigneById(int id);
        List<T_TaskCollaborationsVM> GetCollaborationById(int id);
        GridEntity<T_TaskCollaborationsVM> GetCollaborationByTaskId(GridOptions options, int? taskId);
        List<T_TaskInternalNotesVM> GetInternalById(int id);
        GridEntity<T_TaskInternalNotesVM> GetInternalNoteByTaskId(GridOptions options, int? taskId);
        List<T_TaskTimesVM> GetTimeByTask(int taskId);

        ResultModel<AuditIssueAttachments> DeleteAttachments(int id);
        ResultModel<List<AuditIssueAttachments>> GetAllAttachments(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null);
        List<T_TaskTypeVm> GetAllTaskTypeData();
        GridEntity<T_TasksVM> GetAllTaskOpenData(GridOptions options, int? ticketId, string assigneeUserId);
        GridEntity<T_TasksVM> GetTodayTaskData(GridOptions options, string? assigneeUserId);
        List<T_TasksVM> GetSchedulerData(string userName);
        GridEntity<T_TasksVM> GetPendingTaskData(GridOptions options, string? assigneeUserId);
        GridEntity<T_TasksVM> GetAllStartedTaskData(GridOptions options, string? assigneeUserId);
        List<T_TasksVM> GetTaskAssignee(int id, string? assigneeUserId);
        List<T_ClientsVm> GetAllClient();
        List<T_TasksVM> GetAllSchedulerData();
        List<T_TicketVm> GetTicketByTicketId(string selectedId);
        GridEntity<T_TasksVM> GetAllPendingTaskData(GridOptions options,string assigneeUserId = null);
        //List<T_TaskTimesVM> GetStackHolderById(int selectedId);
    }
}
