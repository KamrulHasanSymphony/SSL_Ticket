using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Models;
using SSL.Ticket.SSL.Ticket.Models.Tasks;

namespace SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Task
{
    public interface ITaskRepository : IBaseRepository<T_TasksVM>
    {
        string GenerateCode(string CodeGroup, string CodeName, int branchId = 1);
        string NewGenerateCode(int ID);
        int DeleteAttachments(string tableName, string[] conditionalFields, string[] conditionalValue);
        AuditIssueAttachments InsertAttachments(AuditIssueAttachments model);
        List<AuditIssueAttachments> GetAllAttachments(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null);
        AuditIssueAttachments UpdateAttachments(AuditIssueAttachments auditIssueAttachment);
		List<AuditIssueAttachments> GetAttachmentsById(int id);
	}
}
