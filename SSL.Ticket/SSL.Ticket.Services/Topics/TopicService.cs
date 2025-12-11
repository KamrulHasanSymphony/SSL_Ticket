using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Topics;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Services.Topics
{
    public class TopicService : ITopicsServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public TopicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TopicsVM> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<T_TopicsVM>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    List<T_TopicsVM> clientInfos =
                        context.Repositories.TopicsRepository.GetAll(conditionalFields, conditionalValue);
                    context.SaveChanges();

                    return new ResultModel<List<T_TopicsVM>>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.DataLoaded,
                        Data = clientInfos
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<List<T_TopicsVM>>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.DataLoadedFailed,
                        Exception = e
                    };
                }


            }
        }

        public int GetCount(string tableName, string fieldName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public GridEntity<T_TopicsVM> GetGridData(GridOptions options)
        {
            try
            {
                using IUnitOfWorkAdapter context = _unitOfWork.Create();

                using (var dbContext = _unitOfWork.Create())
                {
                    var detail = new GridEntity<T_TopicsVM>();
                    detail = KendoGrid<T_TopicsVM>.GetGridData_5(options, "sp_Select_Topics_Grid", "get_topics_summary", "TopicId");
                    return detail;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public ResultModel<List<T_TopicsVM>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<int> GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TopicsVM> Insert(T_TopicsVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {


                T_TopicsVM master = context.Repositories.TopicsRepository.Insert(model);

                context.SaveChanges();
                return new ResultModel<T_TopicsVM>()
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

        public ResultModel<T_TopicsVM> InsertActive(T_TopicsVM model)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TopicsVM> Update(T_TopicsVM model)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    T_TopicsVM master = context.Repositories.TopicsRepository.Update(model);

                    context.SaveChanges();


                    return new ResultModel<T_TopicsVM>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.UpdateSuccess,
                        Data = model
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<T_TopicsVM>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.UpdateFail,
                        Exception = e
                    };
                }
            }
        }
    }
}
