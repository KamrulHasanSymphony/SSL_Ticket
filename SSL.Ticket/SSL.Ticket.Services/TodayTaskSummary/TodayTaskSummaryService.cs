using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Product;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.TodayTaskSummary;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Services.TodayTaskSummary
{
    public class TodayTaskSummaryService : ITodayTaskSummaryServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodayTaskSummaryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<TodayTaskSummaryVM> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<TodayTaskSummaryVM>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    List<TodayTaskSummaryVM> pInfos =
                        context.Repositories.TodayTaskSummaryRepository.GetAll(conditionalFields, conditionalValue);
                    context.SaveChanges();

                    return new ResultModel<List<TodayTaskSummaryVM>>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.DataLoaded,
                        Data = pInfos
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<List<TodayTaskSummaryVM>>()
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

        public GridEntity<TodayTaskSummaryVM> GetGridData(GridOptions options,string? assigneeUserId)
        {
            try
            {
                using IUnitOfWorkAdapter context = _unitOfWork.Create();

                using (var dbContext = _unitOfWork.Create())
                {
                    var detail = new GridEntity<TodayTaskSummaryVM>();
                    detail = KendoGrid<TodayTaskSummaryVM>.GetGridData_5(options, "sp_Select_TodayTaskSummary_Grid", "get_todaytask_summary", "Id", assigneeUserId);
                    return detail;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public ResultModel<List<TodayTaskSummaryVM>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<int> GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<TodayTaskSummaryVM> Insert(TodayTaskSummaryVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {


                TodayTaskSummaryVM master = context.Repositories.TodayTaskSummaryRepository.Insert(model);

                context.SaveChanges();
                return new ResultModel<TodayTaskSummaryVM>()
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

        public ResultModel<TodayTaskSummaryVM> InsertActive(TodayTaskSummaryVM model)
        {
            throw new NotImplementedException();
        }

        public ResultModel<TodayTaskSummaryVM> MultiplePost(TodayTaskSummaryVM model)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    TodayTaskSummaryVM master = context.Repositories.TodayTaskSummaryRepository.MultiplePost(model);

                    context.SaveChanges();


                    return new ResultModel<TodayTaskSummaryVM>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.PostSuccess,
                        Data = model
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<TodayTaskSummaryVM>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.UpdateFail,
                        Exception = e
                    };
                }
            }
        }

        public ResultModel<TodayTaskSummaryVM> Update(TodayTaskSummaryVM model)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    TodayTaskSummaryVM master = context.Repositories.TodayTaskSummaryRepository.Update(model);

                    context.SaveChanges();


                    return new ResultModel<TodayTaskSummaryVM>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.UpdateSuccess,
                        Data = model
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<TodayTaskSummaryVM>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.UpdateFail,
                        Exception = e
                    };
                }
            }
        }

        ResultModel<TodayTaskSummaryVM> IBaseService<TodayTaskSummaryVM>.Delete(int id)
        {
            throw new NotImplementedException();
        }

        ResultModel<List<TodayTaskSummaryVM>> IBaseService<TodayTaskSummaryVM>.GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm)
        {
            throw new NotImplementedException();
        }
    }
}
