using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class EmailContactsVM : RegularVM
    {
        public int Id { get; set; }
        public string EmailContactGroupId { get; set; }

        public string EmailGroup { get; set; }
        public string EmailAddress { get; set; }
        public string MailTo { get; set; }
        public string EmailName { get; set; }      
        public string Operation { get; set; }
        public bool IsActive { get; set; }
        public IFormFile File { get; set; }

        public string MultipleEmailContactGroupId { get; set; }
        public int TotalRecord { get; set; }
    }

    public class EmailContactGroupVM : RegularVM
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public bool IsActive { get; set; }
        public string Operation { get; set; }
    }
}
