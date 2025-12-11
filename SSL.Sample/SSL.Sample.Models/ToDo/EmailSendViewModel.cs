using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class EmailSendViewModel
    {
        public string Id { get; set; }
        public string FromAddress { get; set; }
        public string FromPwd { get; set; }
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string EmailBody { get; set; }
        public string Port { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedFrom { get; set; }
        public string ScheduleTime { get; set; }
        public string ScheduleDate { get; set; }
        public string SendTime { get; set; }
        public string BodyFileName { get; set; }
        public string Attachment1FileName { get; set; }
        public string Attachment2FileName { get; set; }
        public string Attachment3FileName { get; set; }
        public string Attachment4FileName { get; set; }
        public bool IsSend { get; set; }
        public bool WillSend { get; set; }
        public string DateTimeFrom { get; set; }
        public string DateTimeTo { get; set; }
        public string SendStatus { get; set; }
        public string FolderName { get; set; }
    }
}
