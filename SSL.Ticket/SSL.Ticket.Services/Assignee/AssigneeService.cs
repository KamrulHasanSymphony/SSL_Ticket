using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Assignee;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Collaboration;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Services.Assignee
{
    public class AssigneeService : IAssigneeService
    {
        private IUnitOfWork _unitOfWork;
        readonly CommonDataService _common;
        public AssigneeService(IUnitOfWork unitOfWork, CommonDataService common)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _common = common ?? throw new ArgumentNullException(nameof(common));
        }

        public ResultModel<List<T_TaskAssignesVM>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<T_TaskAssignesVM>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
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

        public ResultModel<T_TaskAssignesVM> Insert(T_TaskAssignesVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {

                T_TaskAssignesVM master = context.Repositories.AssigneeRepository.Insert(model);

                context.SaveChanges();
                return new ResultModel<T_TaskAssignesVM>()
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

        public ResultModel<T_TaskAssignesVM> Update(T_TaskAssignesVM model)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TaskAssignesVM> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TaskAssignesVM> InsertActive(T_TaskAssignesVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {

                T_TaskAssignesVM actmaster = context.Repositories.AssigneeRepository.InsertActive(model);
                context.SaveChanges();
                return new ResultModel<T_TaskAssignesVM>()
                {
                    Status = Status.Success,
                    Message = MessageModel.InsertSuccess,
                    Data = actmaster,
                    Success = true
                };

            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
    }
}
