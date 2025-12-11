using Microsoft.AspNetCore.Http;
using SSL.Sample.SSL.Sample.Models.ToDo;

namespace SSL.Sample.SSL.Sample.Models.Support
{
    public class TaskVM : RegularVM
    {
        public TaskVM()
        {
            CollaborationList = new List<CollaborationVM>();
            TaskAttachments = new List<TaskAttachment>();
            ActivityList = new List<TaskVM>();
            AssigneeToList = new List<string>();
        }

        public int Id { get; set; }
        public string TaskCode { get; set; }
        public string TicketId { get; set; }
        public string TaskTitle { get; set; }
        public string ProgressId { get; set; }
        public string Process { get; set; }
        public string Rating { get; set; }
        public string ClientId { get; set; }
        public string Priority { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndDate { get; set; }
        public string EndTime { get; set; }
        public string HoldTime { get; set; }
        public string ReStartTime { get; set; }
        public string DepartmentId { get; set; }
        public string AssigneeTo { get; set; }

        public List<string> AssigneeToList { get; set; }

        public string DueDate { get; set; }
        public string Description { get; set; }
        public string TicketTitle { get; set; }
        public string TaskId { get; set; }
        public string TotalDurationSumInSeconds { get; set; }
        public string CollaborationDetails { get; set; }
        public string Organization{ get; set; }
        public string OrganizationName { get; set; }
        public string DayCount { get; set; }
        public string AssignTouUserName { get; set; }
        public bool IsUpdateButtonCheck { get; set; }
        public string CompleteStatus { get; set; }
        public string ClientOrganization { get; set; }
        public string CheckUpdate { get; set; }

 

        public List<CollaborationVM> CollaborationList { set; get; }
        public List<TaskAttachment> TaskAttachments { set; get; }
        public List<TaskVM> ActivityList { set; get; }


        //For File
        
        public List<IFormFile> Files { get; set; }
        public IFormFile SingleFile { get; set; }
        public string SingleFileName { get; set; }


      
        public bool IsComplete { get; set; }
        public bool Remark { get; set; }
        public string Problem { get; set; }
        public string Status { get; set; }
        public bool IsConfirm { get; set; }
        public string TransactionDateTime { get; set; }
        public string Operation { get; set; }
        public string Subject { get; set; }
        public int ContactPersonId { get; set; }


        public IFormFile File1 { get; set; }
        public string File1Name { get; set; }

        public IFormFile File2 { get; set; }
        public string File2Name { get; set; }

        public IFormFile File3 { get; set; }
        public string File3Name { get; set; }


    }
}
