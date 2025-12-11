using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.CS.SSL.Common.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public int BranchId { get; set; }

        public int CompanyId { get; set; }


        public string Operation { get; set; }

        public Audit Audit { get; set; }

    }
    public abstract class SessionModel
    {
        public static string sageDB;
        public static string DBName;
        public static string authDB;
        public static string SettingValue;
        public static string CompanyName;
        public static string sslConn;
        public static string sageConn;

        public static string AccountSegment;


    }
}
