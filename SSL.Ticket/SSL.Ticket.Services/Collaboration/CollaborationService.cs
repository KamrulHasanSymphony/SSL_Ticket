using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Collaboration;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Task;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Services.Collaboration
{
    public class CollaborationService : ICollaborationService
    {
        private IUnitOfWork _unitOfWork;
        readonly CommonDataService _common ;

        public CollaborationService(IUnitOfWork unitOfWork, CommonDataService common)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _common = common ?? throw new ArgumentNullException(nameof(common));
        }

        public ResultModel<List<T_TaskCollaborationsVM>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<T_TaskCollaborationsVM>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
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

        public ResultModel<T_TaskCollaborationsVM> Insert(T_TaskCollaborationsVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {

                T_TaskCollaborationsVM master = context.Repositories.CollaborationRepository.Insert(model);

                context.SaveChanges();
                return new ResultModel<T_TaskCollaborationsVM>()
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

        public ResultModel<T_TaskCollaborationsVM> Update(T_TaskCollaborationsVM model)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TaskCollaborationsVM> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TaskCollaborationsVM> InsertActive(T_TaskCollaborationsVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {

                T_TaskCollaborationsVM actmaster = context.Repositories.CollaborationRepository.InsertActive(model);
                context.SaveChanges();
                return new ResultModel<T_TaskCollaborationsVM>()
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
