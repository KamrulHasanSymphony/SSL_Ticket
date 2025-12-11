using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
   public class FeedbackDetailsVM : RegularVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FeedBackId { get; set; }
        public string TransactionDateTime { get; set; }
        public string Feedback { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string ClientName { get; set; }

        public IFormFile File1 { get; set; }
        public string File1Name { get; set; }

        public IFormFile File2 { get; set; }
        public string File2Name { get; set; }

        public IFormFile File3 { get; set; }
        public string File3Name { get; set; }


    }
}
