using MySqlX.XDevAPI;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Ticket;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork;
using SSL.Ticket.SSL.Ticket.Models;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;

namespace SSL.Ticket.SSL.Ticket.Services.Ticket
{
    public class TicketService : ITicketService
    {
        private IUnitOfWork _unitOfWork;
        readonly CommonDataService _common ;
        private Session session;
        

        public TicketService(IUnitOfWork unitOfWork, CommonDataService common)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _common = common ?? throw new ArgumentNullException(nameof(common));
            

        }

        public List<StackHolderVM> GetAllStackHolder()
        {
            try
            {
                return _common.Select_Data_List<StackHolderVM>("Select_Dropdown", "get_all_stackHolder_data");
            }
            catch (Exception ex)

            {
                throw ex.InnerException;
            }
        }

        public GridEntity<T_TicketVm> GetAllTicketMData(GridOptions options, string AssigneeUserId)
        {
            try
            {
                using IUnitOfWorkAdapter context = _unitOfWork.Create();

                using (var dbContext = _unitOfWork.Create())
                {
                    var support = new GridEntity<T_TicketVm>();
                    support = KendoGrid<T_TicketVm>.GetGridData_5(options, "sp_Select_TicketMain_Grid", "get_ticket_summary", "t.TicketCode", AssigneeUserId, "", "", "");
                    return support;
                }
                    
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_ClientsVm> GetAllClient()
        {
            try
            {
                return _common.Select_Data_List<T_ClientsVm>("Select_Dropdown", "get_all_organization_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_SourcesVm> GetAllticketSourceData()
        {
            try
            {
                return _common.Select_Data_List<T_SourcesVm>("Select_Dropdown", "get_all_ticketsource_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_ProductsVM> GetAllProductsData()
        {
            try
            {
                return _common.Select_Data_List<T_ProductsVM>("Select_Dropdown", "get_all_products_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public List<T_DepartmentVm> GetAllDepartmentData()
        {
            try
            {
                return _common.Select_Data_List<T_DepartmentVm>("Select_Dropdown", "get_all_department_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_TicketTypeVm> GetAllTicketTypeData()
        {
            try
            {
                return _common.Select_Data_List<T_TicketTypeVm>("Select_Dropdown", "get_all_tickettype_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public ResultModel<List<T_TicketVm>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    var records = context.Repositories.TicketRepository.GetAll(conditionalFields, conditionalValue);
                    context.SaveChanges();

                    return new ResultModel<List<T_TicketVm>>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.DataLoaded,
                        Data = records
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<List<T_TicketVm>>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.DataLoadedFailed,
                        Exception = e
                    };
                }

            }
        }

        public ResultModel<List<T_TicketVm>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
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

        public ResultModel<T_TicketVm> Insert(T_TicketVm model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {
                
                string code = context.Repositories.TicketRepository.NewGenerateCode(model.TicketId);
                model.Code = code;
                string taskCode = context.Repositories.TaskRepository.NewGenerateCode(model.TicketId);
                model.TaskCode = taskCode;
                T_TicketVm master = context.Repositories.TicketRepository.Insert(model);
                
                context.SaveChanges();
                return new ResultModel<T_TicketVm>()
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

        public ResultModel<T_TicketVm> Update(T_TicketVm model)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    T_TicketVm master = context.Repositories.TicketRepository.Update(model);

                    context.SaveChanges();


                    return new ResultModel<T_TicketVm>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.UpdateSuccess,
                        Data = model
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<T_TicketVm>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.UpdateFail,
                        Exception = e
                    };
                }
            }
        }

        public ResultModel<T_TicketVm> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TicketVm> InsertActive(T_TicketVm model)
        {
            throw new NotImplementedException();
        }

        public GridEntity<UserBranch> GetUserProfileGrid(GridOptions options)
        {
            try
            {
                using (var context = _unitOfWork.Create())
                {
                    var user = new GridEntity<UserBranch>();
                    user = KendoGrid<UserBranch>.GetGridData_5(options, "sp_Select_UserBranch_Grid", "get_UserBranch_summary", "UserName", "", "", "", "");
                    return user;
                }
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public GridEntity<T_TicketInternalNotesVM> GetAllEnternalNoteData(GridOptions options, int? id)
        {
            try
            {
                using IUnitOfWorkAdapter context = _unitOfWork.Create();

                using (var dbContext = _unitOfWork.Create())
                {
                    var support = new GridEntity<T_TicketInternalNotesVM>();
                    support = KendoGrid<T_TicketInternalNotesVM>.GetGridData_5(options, "sp_Select_TicketNote_Grid", "get_ticket_Note_summary", "Id", id.ToString());
                    return support;
                }

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<T_TicketInternalNotesVM> GetInternalById(int id)
        {
            try
            {
                return _common.Select_Data_List<T_TicketInternalNotesVM>("GetById", "get_tktinternal_by_id", id.ToString());
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
