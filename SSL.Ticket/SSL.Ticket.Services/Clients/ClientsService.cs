using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Clients;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;

namespace SSL.Ticket.SSL.Ticket.Services.Clients
{
    public class ClientsService : IClientsService
    {
        private IUnitOfWork _unitOfWork;

        public ClientsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_ClientsVm> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<T_ClientsVm>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    List<T_ClientsVm> clientInfos =
                        context.Repositories.ClientsRepository.GetAll(conditionalFields, conditionalValue);
                    context.SaveChanges();

                    return new ResultModel<List<T_ClientsVm>>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.DataLoaded,
                        Data = clientInfos
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<List<T_ClientsVm>>()
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

        public GridEntity<T_ClientsVm> GetGridData(GridOptions options)
        {
            try
            {
                using IUnitOfWorkAdapter context = _unitOfWork.Create();

                using (var dbContext = _unitOfWork.Create())
                {
                    var detail = new GridEntity<T_ClientsVm>();
                    detail = KendoGrid<T_ClientsVm>.GetGridData_5(options, "sp_Select_Clients_Grid", "get_clients_summary", "Id");
                    return detail;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public ResultModel<List<T_ClientsVm>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<int> GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_ClientsVm> Insert(T_ClientsVm model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {


                T_ClientsVm master = context.Repositories.ClientsRepository.Insert(model);

                context.SaveChanges();
                return new ResultModel<T_ClientsVm>()
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

        public ResultModel<T_ClientsVm> InsertActive(T_ClientsVm model)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_ClientsVm> Update(T_ClientsVm model)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    T_ClientsVm master = context.Repositories.ClientsRepository.Update(model);

                    context.SaveChanges();


                    return new ResultModel<T_ClientsVm>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.UpdateSuccess,
                        Data = model
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<T_ClientsVm>()
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
