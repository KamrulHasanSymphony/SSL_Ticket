using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Models;

namespace SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.UserProfile
{
    public interface IUserProfileService : IBaseService<TUserProfile>
    {
        ResultModel<TUserProfile> UpdateUserProfile(TUserProfile userProfile);
        bool VerifyUser(string userId, string verification);
    }
}
