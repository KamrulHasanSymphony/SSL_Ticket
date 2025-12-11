using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SSL.Ticket.SSL.Ticket.Models.Tasks
{
    public class T_TasksVM
    {
        
        public int Id { get; set; }
        public int TaskId { get; set; }

        public int CompanyId { get; set; }

        public string Code { get; set; }
        public string TaskCode { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string TaskTitle { get; set; }

        public string Description { get; set; }
        public string TaskDescription { get; set; }
        [Display(Name = "Ticket")]
        [Required(ErrorMessage = "Ticket is required")]
        public int? T_TicketId { get; set; }

        public string? TicketCode { get; set; }
        public string TicketTitle { get; set; }
        public string SourceName { get; set; }
        public string FullName { get; set; }
        public string TopicName { get; set; }
        public string Rating { get; set; }
        public string Priority { get; set; }
        public string PrioritiesName { get; set; }
        public string Status { get; set; }
        public string StatusName { get; set; }
        public string StartedBy { get; set; }
        [Display(Name = "Source")]
        public int? T_SourceId { get; set; }
        [Display(Name = "Topic")]

        public int? T_TopicId { get; set; }
        [Display(Name = "Rating")]

        public int? T_RatingId { get; set; }
        [Display(Name = "Priority")]

        public int? T_PriorityId { get; set; }
        [Display(Name = "Status")]

        public int? T_StatusId { get; set; }
        [Display(Name = "Progress(%)")]

        public int? ProgressInPercent { get; set; }
        [Display(Name = "Start Date")]

        public DateTime? StartDate { get; set; }
        [Display(Name = "Start Time")]

        public string? StartTime { get; set; }
        public string? StopTime { get; set; }
        public string? Start { get; set; }
        public string? End { get; set; }

        [Display(Name = "Required Time")]

        public int? RequiredTime { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }
        public string AssigneeUserIds { get; set; }

        public DateTime? UpdateOn { get; set; }

        public string UpdateBy { get; set; }

        public bool? IsComplete { get; set; }

        public bool? IsSendEmail { get; set; }

        public int? DependentTaskId { get; set; }
        public string Operation { get; set; }
        public string Self { get; set; }
        public string AssigneeUserId { get; set; }
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        [Display(Name = "Task Type")]
        [Required(ErrorMessage = "Task Type is required.")]
        public int? TaskTypeId { get; set; }
        public string TaskTypeName { get; set; }
        public string WorkingStatus { get; set; }
        public string WorkingTime { get; set; }
        public int? T_ClientId { get; set; }
        public string ClientsName { get; set; }
        public string? ProductName { get; set; }


        public IList<IFormFile>? Attachments { get; set; }
        public List<AuditIssueAttachments> AttachmentsList { get; set; }
        public T_TasksVM()
        {
            AttachmentsList = new List<AuditIssueAttachments>();
        }

    }
}
