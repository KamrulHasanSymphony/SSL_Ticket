using Microsoft.AspNetCore.Http;
using SSL.Sample.SSL.Sample.Models.ToDo;

namespace SSL.Sample.SSL.Sample.Models.Support
{
    public class SupportVM : RegularVM
    {
        public SupportVM()
        {
            CollaborationList = new List<CollaborationVM>();
            TicketAttachments = new List<TicketAttachment>();
            ActivityList = new List<SupportVM>();
            AssignToLists = new List<AssignToVM>();
            AssignToList = new List<string>();
        }

        public int Id { get; set; }               
        public string TicketCode { get; set; }
        public string TicketTitle { get; set; }
        public string CreatorEmail { get; set; }
        public string CreatorPhone { get; set; }
        public string TicketStackHolder { get; set; }
        public string TicketStackHolderCC { get; set; }
        public string StackHolderEmail { get; set; }
        public string StackHolderPhone { get; set; }       
        public string Description { get; set; }
        public string Notification{ get; set; }
        public string TicketSource { get; set; }
        public string TicketTopic { get; set; }
        public string DepartmentId { get; set; }
        public string CreateDate { get; set; }
        public string Est_EndDate { get; set; }
        public string DueDate { get; set; }
        public string ClientOrOrganization { get; set; }

        public string AssignTo { get; set; }
        public string LogId { get; set; }
        public string EnumValue { get; set; }

        public List<string> AssignToList { get; set; }

        public string Remark { get; set; }
        public string TicketId { get; set; }
        public string FilePath { get; set; }
        public string Complete { get; set; }
        public string CollaborationDetails { get; set; }
        public string Organization { get; set; }
        public string ClientOrganization { get; set; }
        public string TotalDays { get; set; }
        public string TotalDurationTime { get; set; }
        public string WorkingStatus { get; set; }

        public string DayCount { get; set; }
        
        public string TotalDurationSumInSeconds { get; set; }

        public string Problem { get; set; }
        public string Status { get; set; }
        public string Products { get; set; }
        public string ProductId { get; set; }


        public string CompleteStatus { get; set; }
        

        public bool IsConfirm { get; set; }
        public string TransactionDateTime { get; set; }
        public string Operation { get; set; }
        public string Subject { get; set; }
        public int ContactPersonId { get; set; }
        public bool IsUpdateButtonCheck { get; set; }
        public bool IsInternelNoteCheck { get; set; }
        public bool IsStackHolderCheck { get; set; }
        public string StackHolderUserName { get; set; }
        public string AssignTouUserName { get; set; }
        public string CheckUpdate { get; set; }


        public List<CollaborationVM> CollaborationList { set; get; }
        public List<TicketAttachment> TicketAttachments { set; get; }
        public List<SupportVM> ActivityList { set; get; }
        public List<AssignToVM> AssignToLists { set; get; }

        public IFormFile FileName { get; set; }
        public IFormFile File1 { get; set; }
        public string File1Name { get; set; }

        public IFormFile File2 { get; set; }
        public string File2Name { get; set; }

        public IFormFile File3 { get; set; }
        public string File3Name { get; set; }
        
        //For File
        public List<IFormFile> Files { get; set; }
        public IFormFile SingleFile { get; set; }
        public string SingleFileName { get; set; }

    }
}
