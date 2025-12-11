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
   public class FeedBacksVM : RegularVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
       [Display(Name = "Product Name")]
        public int ProductId { get; set; }
       [Display(Name = "Client Name")]
        public int ClientId { get; set; }
        public string Problem { get; set; }
        public string Status { get; set; }
        public bool IsConfirm { get; set; }
        public string TransactionDateTime { get; set; }
        public string Operation { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string ClientName { get; set; }
        public string UserEmail { get; set; }
        public string Subject { get; set; }

        public string SupportType { get; set; }

        public string Name { get; set; }


        public int ContactPersonId { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonDesignation { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonMobile { get; set; }

        

        public IFormFile File1 { get; set; }
        public string File1Name { get; set; }

        public IFormFile File2 { get; set; }
        public string File2Name { get; set; }

        public IFormFile File3 { get; set; }
        public string File3Name { get; set; }

        public bool IsClient { get; set; }
        public string Priority { get; set; }


        public List<FeedbackDetailsVM> Details { get; set; }

        public FeedbackDetailsVM FBDVM { get; set; }

    }
}
