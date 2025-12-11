using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SSL_ERP.Models.SageAPI.RequestResult
{
    public class Error
    {
        public string code { get; set; }
        public Message message { get; set; }
    }

    public class Message
    {
        public string lang { get; set; }
        public string value { get; set; }
    }

    public class Root
    {
        public Error error { get; set; }
    }

}
