using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class RegularVM
    {
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public bool IsArchive { get; set; }
        public bool IsHold { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedFrom { get; set; }
        public string LastUpdateBy { get; set; }
        public string LastUpdateAt { get; set; }
        public string LastUpdateFrom { get; set; }
        //public HttpPostedFile FileAttachment { get; set; }
        public string Operation { get; set; }
        public string UpdateOn { get; set; }
        public string UpdateBy { get; set; }
        public string CreateOn { get; set; }
        public string CreatedOn { get; set; } 


    }
}
