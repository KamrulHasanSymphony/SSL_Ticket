using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.Support
{
    public class TaskAttachment
    {
        public int Id { set; get; }
        public string TaskId { set; get; }
        public string FileName { set; get; }
        public string FilePath { set; get; }

    }
}
