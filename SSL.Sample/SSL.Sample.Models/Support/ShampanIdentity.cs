using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Sample.SSL.Sample.Models.Support
{
    public class ShampanIdentity : IIdentity
    {
        public ShampanIdentity(string basicTicket)
        {
            string[] ticketData = basicTicket.Split(new string[] { "__#" }, StringSplitOptions.None);
            this.Name = ticketData[0];
            this.FullName = ticketData[1];
            this.CompanyId = ticketData[2];
            this.CompanyName = ticketData[3];
            this.BranchId = ticketData[4];
            this.BranchName = ticketData[5];
            this.WorkStationIP = ticketData[6];
            this.WorkStationName = ticketData[7];
            this.SessionDate = ticketData[8];
            this.Site = ticketData[9];
            this.UserId = ticketData[10];
            this.IsAdmin = Convert.ToBoolean(ticketData[11]);
            this.IsESS = Convert.ToBoolean(ticketData[12]);
            this.Location = ticketData[13];
            this.IsAuthenticated = true;
        }

        public string Name { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public string AuthenticationType { get { return "ShampanAuthentication"; } }
        public string FullName { get; private set; }
        public string CompanyId { get; private set; }
        public string CompanyName { get; private set; }
        public string BranchId { get; private set; }
        public string BranchName { get; private set; }
        public string WorkStationIP { get; private set; }
        public string WorkStationName { get; private set; }
        public string SessionDate { get; private set; }
        public string Site { get; private set; }
        public string UserId { get; private set; }
        public bool IsAdmin { get; private set; }
        public bool IsESS { get; private set; }
        public string Location { get; private set; }
        public string[] PermittedRoles { get; private set; }

        public static string CreateBasicTicket(
                                            string name
                                            , string fullName
                                            , string companyId
                                            , string companyName
                                            , string branchId
                                            , string branchName
                                            , string workStationIP
                                            , string workStationName
                                            , string sessionDate
                                            , string site
                                            , string userId
                                            , bool isAdmin
                                            , bool isESS
                                            , string Location

            )
        {
            return name
               + "__#" + fullName
               + "__#" + companyId
               + "__#" + companyName
               + "__#" + branchId
               + "__#" + branchName
               + "__#" + workStationIP
               + "__#" + workStationName
               + "__#" + sessionDate
               + "__#" + site
               + "__#" + userId
               + "__#" + isAdmin
               + "__#" + isESS
               + "__#" + Location

                ;
        }

        public static string CreateRoleTicket(string[] roles)
        {
            string rolesString = "";
            for (int i = 0; i < roles.Length; i++)
            {
                rolesString += roles[i] + ",";
            }
            rolesString.TrimEnd(new char[] { ',' });

            return rolesString + "__#";
        }

        public void SetRoles(string roleTicket)
        {
            this.PermittedRoles = roleTicket == "" ? new string[0] : roleTicket.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
