using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.Support
{
   public class TicketAttachment
    {
       public int Id { set; get; }
       public string TicketId { set; get; }
       public string FileName { set; get; }
       public string FilePath { set; get; }

    }
}
