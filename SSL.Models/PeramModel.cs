using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL_ERP.Models
{
    public class PeramModel
    {
        public string UserLogInId { get; set; }  
        public string ItemNo { get; set; }
        public string TransactionType { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public string BatchId { get; set; }
	}
}
