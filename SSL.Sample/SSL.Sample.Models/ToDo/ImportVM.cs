using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSL.Sample.SSL.Sample.Models.ToDo
{
    public class ImportVM
    {

        public int BranchId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public IFormFile File { get; set; }
        public string FilePath { get; set; }
        public string LastModifiedBy { get; set; }
        public string LastModifiedOn { get; set; }
        public string TableName { get; set; }

    }
}
