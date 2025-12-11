using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.UserProfile;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork;
using SSL.Ticket.SSL.Ticket.Models;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Services.UserProfile
{
    public class UserProfileService : IUserProfileService
    {
        private IUnitOfWork _unitOfWork;
        readonly CommonDataService _common;

        public UserProfileService(IUnitOfWork unitOfWork, CommonDataService common)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _common = common ?? throw new ArgumentNullException(nameof(common));
        }

        public ResultModel<List<TUserProfile>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<TUserProfile>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<int> GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetCount(string tableName, string fieldName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<TUserProfile> Insert(TUserProfile model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {

                TUserProfile master = context.Repositories.UserProfileRepository.Insert(model);

                context.SaveChanges();
                return new ResultModel<TUserProfile>()
                {
                    Status = Status.Success,
                    Message = MessageModel.InsertSuccess,
                    Data = master,
                    Success = true
                };

            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public ResultModel<TUserProfile> InsertActive(TUserProfile model)
        {
            throw new NotImplementedException();
        }

        public ResultModel<TUserProfile> Update(TUserProfile model)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    TUserProfile master = context.Repositories.UserProfileRepository.Update(model);

                    context.SaveChanges();


                    return new ResultModel<TUserProfile>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.UpdateSuccess,
                        Data = model
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<TUserProfile>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.UpdateFail,
                        Exception = e
                    };
                }
            }
        }

        public ResultModel<TUserProfile> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<TUserProfile> UpdateUserProfile(TUserProfile userProfile)
        {
            using (var context = _unitOfWork.Create())
            {
                try
                {
                    TUserProfile master = context.Repositories.UserProfileRepository.UpdateUserProfile(userProfile);

                    context.SaveChanges();


                    return new ResultModel<TUserProfile>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.UpdateSuccess,
                        Data = userProfile
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<TUserProfile>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.UpdateFail,
                        Exception = e
                    };
                }
            }
        }
        public bool VerifyUser(string userId, string verification)
        {
            using (var context = _unitOfWork.Create())
            {
                List<TUserProfile> data = _common.Select_Data_List<TUserProfile>("GetById", "get_all_user_data", userId);
                var userProfile = data.FirstOrDefault(user => user.LogId == userId);

                // Check if the user profile is found and the verification code matches
                if (userProfile != null && userProfile.VerificationCode == verification)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

    }
}
