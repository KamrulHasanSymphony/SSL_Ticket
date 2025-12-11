using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class EmailSettingVM
    {
        public string MailToAddress { get; set; }
        [Display(Name = "Email From")]
        public string MailFromAddress = "Khalid.Sarower@symphonysoftt.com"; 
        public bool USsel = true;
        public string Password = "K123456_";
        public string UserName = "Khalid.Sarower@symphonysoftt.com";
        public string ServerName = "smtp.gmail.com";
        //public string ServerName = "smtp-mail.outlook.com";
        //public string ServerName = "smtp.mail.yahoo.com";
        [Display(Name = "Mail Body")]
        public string MailBody { get; set; }

        [Display(Name = "Subject")]
        public string MailHeader { get; set; }
        public string Fiscalyear { get; set; }
        public int Port = 587;
        public IFormFile fileUploader { get; set; }
        public string FileName { get; set; }

        
        public List<MultipleMailToAddressVM> multipleMailToAddressVMs { get; set; }




        //////////public string MailToAddress { get; set; }
        //////////public string MailFromAddress { get; set; }// = "Khalid.Sarower@symphonysoftt.com"; 
        //////////public bool USsel = true;
        //////////public string Password  { get; set; }//= "K123456_";
        //////////public string UserName  { get; set; }//= "Khalid.Sarower@symphonysoftt.com";
        //////////public string ServerName  { get; set; }//= "smtp.gmail.com";
        //////////public string MailBody { get; set; }
        //////////public string MailHeader { get; set; }
        //////////public string Fiscalyear { get; set; }
        //////////public int Port { get; set; }// = 587;
        //////////public HttpPostedFileBase fileUploader { get; set; }
        //////////public string FileName { get; set; }
    }

     public class MultipleMailToAddressVM{

         public bool IsMail { get; set; }
         public string MailToAddress { get; set; }
     
     }
}
