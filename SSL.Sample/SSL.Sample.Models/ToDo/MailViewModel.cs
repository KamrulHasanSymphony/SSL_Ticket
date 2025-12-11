using Microsoft.AspNetCore.Http;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class MailViewModel
    {
        public IFormFile file { get; set; }
        public IFormFile attch1 { get; set; }
        public IFormFile attch2 { get; set; }
        public IFormFile attch3 { get; set; }
        public IFormFile attch4 { get; set; }
        public List<MailDetailViewModel> Vms { get; set; }
        public string FromMail { get; set; }
        public string ToMail { get; set; }
        public string MailPassord { get; set; }
        public string MailSubject { get; set; }
        public string SendDate { get; set; }
        public string SendTime { get; set; }
        public bool WillSend { get; set; }
        public string MailId { get; set; }
        public string FolderName { get; set; }
        public string MailBody { get; set; }
        public int EmailGroupId { get; set; }        
        public string AttachmentPath { get; set; }
    }
}
