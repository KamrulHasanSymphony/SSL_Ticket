using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Sample.Core.Interfaces.UnitOfWork;
using SSL.Sample.SSL.Sample.Models.Support;
using SSL.Sample.SSL.Sample.Models.Tickets;
using SSL.Sample.SSL.Sample.Services.Vendor;

namespace SSL.Sample.SSL.Sample.Services.Support
{
    public class SupportService : ISupportService
    {
        private IUnitOfWork _unitOfWork;
        readonly CommonDataService _common = new CommonDataService();

        public SupportService(IUnitOfWork unitOfWork, CommonDataService common)
        {
            _unitOfWork = unitOfWork;
            _common = common;
        }
        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TicketVm> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<T_TicketVm>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    var records = context.Repositories.SupportRepository.GetAll(conditionalFields, conditionalValue);
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

        public GridEntity<T_TicketVm> GetAllTicketMData(GridOptions options)
        {
            try
            {

                var support = new GridEntity<T_TicketVm>();
                support = KendoGrid<T_TicketVm>.GetGridData_5(options, "sp_Select_TicketMain_Grid", "get_ticket_summary", "T.Id");
                return support;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public int GetCount(string tableName, string fieldName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<T_TicketVm>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<int> GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_TicketVm> Insert(T_TicketVm model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {
                string CodeGroup = "SP";
                string CodeName = "Support";                
                string code = context.Repositories.SupportRepository.GenerateCode(CodeGroup, CodeName, Convert.ToInt32(1));
                model.Code = code;

                T_TicketVm master = context.Repositories.SupportRepository.Insert(model);

                //foreach (var detail in model.AssignToLists)
                //{
                //    int id = Convert.ToInt32(detail.Id);  
                //    id = master.Id;
                //    //detail.BranchId = 1;
                //    //detail.CompanyId = 1;

                //    AssignToVM returnDetail = context.Repositories.SupportRepository.AssignToInsert(detail);
                //}
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
            throw new NotImplementedException();
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

        public List<OrganizationVM> GetAllOrganization()
        {
            try
            {
                return _common.Select_Data_List<OrganizationVM>("Select_Dropdown", "get_all_organization_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<TicketSourceVM> GetAllticketSourceData()
        {
            try
            {
                return _common.Select_Data_List<TicketSourceVM>("Select_Dropdown", "get_all_ticketsource_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<TicketSourceVM> GetAllticketTopicData()
        {
            try
            {
                return _common.Select_Data_List<TicketSourceVM>("Select_Dropdown", "get_all_tickettopic_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<TicketSourceVM> GetAllDepartmentData()
        {
            try
            {
                return _common.Select_Data_List<TicketSourceVM>("Select_Dropdown", "get_all_department_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<TicketSourceVM> GetAllStatusData()
        {
            try
            {
                return _common.Select_Data_List<TicketSourceVM>("Select_Dropdown", "get_all_status_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<TicketSourceVM> GetAllProductsData()
        {
            try
            {
                return _common.Select_Data_List<TicketSourceVM>("Select_Dropdown", "get_all_products_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public List<StackHolderVM> GetAllAssignToData()
        {
            try
            {
                return _common.Select_Data_List<StackHolderVM>("Select_Dropdown", "get_all_assignTo_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<AssignToVM> GetAssignToByTicketId(int id)
        {
            try
            {
                return _common.Select_Data_List<AssignToVM>("Select_Dropdown", "get_assignTo_by_Id", id.ToString());
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
