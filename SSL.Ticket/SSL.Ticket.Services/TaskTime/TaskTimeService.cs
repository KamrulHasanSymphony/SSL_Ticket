using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Task;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.TaskTime;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Services.TaskTime
{
    public class TaskTimeService : ITaskTimeService
    {
        private IUnitOfWork _unitOfWork;
        //readonly CommonDataService _common = new CommonDataService();
        readonly CommonDataService _common;

        public TaskTimeService(IUnitOfWork unitOfWork, CommonDataService common)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _common = common ?? throw new ArgumentNullException(nameof(common));
        }

        public ResultModel<List<T_TaskTimesVM>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<T_TaskTimesVM>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
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

        public ResultModel<T_TaskTimesVM> Insert(T_TaskTimesVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {
                T_TaskTimesVM master = context.Repositories.TaskTimeRepository.Insert(model);

                context.SaveChanges();
                return new ResultModel<T_TaskTimesVM>()
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

        public ResultModel<T_TaskTimesVM> InsertActive(T_TaskTimesVM model)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TaskTimesVM> Update(T_TaskTimesVM model)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    T_TaskTimesVM master = context.Repositories.TaskTimeRepository.Update(model);

                    context.SaveChanges();


                    return new ResultModel<T_TaskTimesVM>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.UpdateSuccess,
                        Data = model
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<T_TaskTimesVM>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.UpdateFail,
                        Exception = e
                    };
                }
            }
        }

        public ResultModel<T_TaskTimesVM> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }
    }
}
