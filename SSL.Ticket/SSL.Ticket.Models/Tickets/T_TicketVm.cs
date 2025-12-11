using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SSL.Ticket.SSL.Ticket.Models.Tickets
{
    public class T_TicketVm
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string TaskCode { get; set; }
        public string TicketCode { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        public string TicketTitle { get; set; }
        [Display(Name = "Description")]

        public string Description { get; set; }
        public string TicketDescription { get; set; }
        [Display(Name = "Client")]

        public int? T_ClientId { get; set; }

        public string ClientsName { get; set; }
        [Display(Name = "Stack Holder")]

        public string StackHolderUserId { get; set; }
        public string ProfileName { get; set; }
        [Display(Name = "Product")]

        public int? T_ProductId { get; set; }
        public string ProductName { get; set; }
        [Display(Name = "Source")]

        public int? T_SourceId { get; set; }
        public string SourceName { get; set; }

        [Display(Name = "Create Date")]
        public DateTime? CreateDate { get; set; }
        [Display(Name = "Due Date")]

        public DateTime? DueDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreateOn { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateOn { get; set; }

        public bool? IsComplete { get; set; }
        public string Operation { get; set; }
        public string TopicName { get; set; }
        public string Rating { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        [Display(Name = "Topic")]

        public int? T_TopicId { get; set; }
        [Display(Name = "Rating")]

        public int? T_RatingId { get; set; }
        [Display(Name = "Priority")]

        public int? T_PriorityId { get; set; }
        [Display(Name = "Status")]

        public int? T_StatusId { get; set; }

        public string AssigneeUserId { get; set; }
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        [Display(Name = "Ticket Type")]
        public int? TicketTypeId { get; set; }
        public string TicketTypeName { get; set; }
        public int? TaskCount { get; set; }
        public IList<IFormFile>? Attachments { get; set; }
        public List<AuditIssueAttachments> AttachmentsList { get; set; }
        public T_TicketVm()
        {
            AttachmentsList = new List<AuditIssueAttachments>();
        }

    }
}
