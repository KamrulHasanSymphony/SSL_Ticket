using SSL.Ticket.SSL.Ticket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.UserProfile
{
    public interface IUserProfileRepository : IBaseRepository<TUserProfile>
    {
        TUserProfile UpdateUserProfile(TUserProfile userProfile);
    }
}
