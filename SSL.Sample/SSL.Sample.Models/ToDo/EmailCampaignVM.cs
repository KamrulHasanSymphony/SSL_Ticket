using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class EmailCampaignVM
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string EmailBody { get; set; }
        public string ScheduleDate { get; set; }
        public string ScheduleTime { get; set; }
        public string BodyFileName { get; set; }
        public string Attachment1FileName { get; set; }
        public string Attachment2FileName { get; set; }
        public string Attachment3FileName { get; set; }
        public string Attachment4FileName { get; set; }
        public string FolderName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedFrom { get; set; }
        public string EmailContactId { get; set; }

        public IFormFile file { get; set; }
        public IFormFile attch1 { get; set; }
        public IFormFile attch2 { get; set; }
        public IFormFile attch3 { get; set; }
        public IFormFile attch4 { get; set; }
        public List<EmailCampaignDetailVM> VmsX { get; set; }
        public List<EmailContactsVM> Vms { get; set; }

        public string EmailContactGroupId { get; set; }
        public string Operation { get; set; }
    }

    public class EmailCampaignDetailVM
    {
        public string Id { get; set; }
        public string EmailCampaignId { get; set; }
        public string EmailContactId { get; set; }
        public string ToAddress { get; set; }
        public string IsSend { get; set; }
        public string WillSend { get; set; }
        public string SendStatus { get; set; }
        public string SendTime { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedFrom { get; set; }

        public bool vIsSend { get; set; }
        public bool vWillSend { get; set; }
    }
}
