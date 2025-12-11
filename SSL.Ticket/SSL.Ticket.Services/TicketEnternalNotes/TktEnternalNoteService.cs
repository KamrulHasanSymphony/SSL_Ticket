using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.EntarnalNotes;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Ticket;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Services.TicketEnternalNotes
{
    public class TktEnternalNoteService : ITktEnternalNoteService
    {
        private IUnitOfWork _unitOfWork;
        //readonly CommonDataService _common = new CommonDataService();
        readonly CommonDataService _common;

        public TktEnternalNoteService(IUnitOfWork unitOfWork, CommonDataService common)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _common = common ?? throw new ArgumentNullException(nameof(common));
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TicketInternalNotesVM> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<T_TicketInternalNotesVM>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetCount(string tableName, string fieldName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<T_TicketInternalNotesVM>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<int> GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TicketInternalNotesVM> Insert(T_TicketInternalNotesVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {

                T_TicketInternalNotesVM master = context.Repositories.TktEnternalNoteRepository.Insert(model);

                context.SaveChanges();
                return new ResultModel<T_TicketInternalNotesVM>()
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

        public ResultModel<T_TicketInternalNotesVM> InsertActive(T_TicketInternalNotesVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {

                T_TicketInternalNotesVM actmaster = context.Repositories.TktEnternalNoteRepository.InsertActive(model);
                context.SaveChanges();
                return new ResultModel<T_TicketInternalNotesVM>()
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

        public ResultModel<T_TicketInternalNotesVM> Update(T_TicketInternalNotesVM model)
        {
            throw new NotImplementedException();
        }
    }
}
