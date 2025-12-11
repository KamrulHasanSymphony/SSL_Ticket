using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.EntarnalNotes;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Task;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Services.EnternalNote
{
    public class EnternalNoteService : IEnternalNoteService
    {
        private IUnitOfWork _unitOfWork;
        //readonly CommonDataService _common = new CommonDataService();
        readonly CommonDataService _common ;

        public EnternalNoteService(IUnitOfWork unitOfWork, CommonDataService common)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _common = common ?? throw new ArgumentNullException(nameof(common));
        }

        public ResultModel<List<T_TaskInternalNotesVM>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<T_TaskInternalNotesVM>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
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

        public ResultModel<T_TaskInternalNotesVM> Insert(T_TaskInternalNotesVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {
                
                    T_TaskInternalNotesVM master = context.Repositories.EnternalNoteRepository.Insert(model);

                    context.SaveChanges();
                    return new ResultModel<T_TaskInternalNotesVM>()
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

        public ResultModel<T_TaskInternalNotesVM> Update(T_TaskInternalNotesVM model)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TaskInternalNotesVM> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TaskInternalNotesVM> InsertActive(T_TaskInternalNotesVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {
                
                    T_TaskInternalNotesVM actmaster = context.Repositories.EnternalNoteRepository.InsertActive(model);
                    context.SaveChanges();
                    return new ResultModel<T_TaskInternalNotesVM>()
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
