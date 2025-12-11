//using SSL.CS.SSL.Common.Core.Interfaces.UnitOfWork;
using Microsoft.Extensions.Options;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Sample.Core.Interfaces.UnitOfWork;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.VendorService;
using SSL.Sample.SSL.Sample.Models;

namespace SSL.Sample.SSL.Sample.Services.Vendor
{
    public class VendorServices : IVendorServices
    {

        private IUnitOfWork _unitOfWork;
        readonly CommonDataService _common = new CommonDataService();

        public VendorServices(IUnitOfWork unitOfWork, CommonDataService commonDataService)
        {
            _unitOfWork = unitOfWork;
            _common = commonDataService;
        }


        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<AVendorVM> Delete(int id)
        {
            using var context = _unitOfWork.Create();
            try
            {
                var count = context.Repositories.VendorRepository.Delete(id);
                context.SaveChanges();
                return new ResultModel<AVendorVM>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.DeleteFail,
                    EffectedRows = 0
                };
            }
            catch (Exception e)
            {

                context.RollBack();

                return new ResultModel<AVendorVM>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.DeleteFail,
                    EffectedRows = 0
                };
            }
        }

        public ResultModel<List<AVendorVM>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public GridEntity<VendorVm> GetAllTestData(GridOptions options)
        {
            //using var context = _unitOfWork.Create();
            try
            {

                var vendor = new GridEntity<VendorVm>();
                vendor = KendoGrid<VendorVm>.GetGridData_5(options, "sp_Select_Vendor_Grid", "get_Vendor_summary", "VName");
                return vendor;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<VendorGroupVm> GetAllVendorData()
        {
            try            
            {
                return _common.Select_Data_List<VendorGroupVm>("Sp_Select_Vendor_Info", "get_all_vendor_mode");
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

        public ResultModel<List<AVendorVM>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<int> GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<AVendorVM> Insert(AVendorVM model)
        {

            //string CodeGroup = "Audit";
            //string CodeName = "AuditMaster";

            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {
                if (model is null)
                {
                    return new ResultModel<AVendorVM>()
                    {
                        Status = Status.Warning,
                        Message = MessageModel.NotFoundForSave,
                    };
                }

                AVendorVM master = context.Repositories.VendorRepository.Insert(model);

                if (master.VendorId <= 0)
                {
                    return new ResultModel<AVendorVM>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.MasterInsertFailed,
                        Data = master,
                        Success = true
                    };
                }


                context.SaveChanges();


                
                return new ResultModel<AVendorVM>()
                {
                    Status = Status.Success,
                    Message = MessageModel.InsertSuccess,
                    Data = master,
                    Success = true
                };
            }
            catch (Exception e)
            {
                context.RollBack();

                return new ResultModel<AVendorVM>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.InsertFail,
                    Exception = e
                };
            }
        }



        public ResultModel<AVendorVM> Update(AVendorVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {
                AVendorVM master = context.Repositories.VendorRepository.Update(model);

                context.SaveChanges();

                return new ResultModel<AVendorVM>()
                {
                    Status = Status.Success,
                    Message = MessageModel.UpdateSuccess,
                    Data = model
                };

            }
            catch (Exception e)
            {
                context.RollBack();

                return new ResultModel<AVendorVM>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.UpdateFail,
                    Exception = e
                };
            }
        }

        public List<VendorGroupVm> GetAllCountryData(int id)
        {
            try
            {
                return _common.Select_Data_List<VendorGroupVm>("Sp_Select_Vendor_Info", "get_all_Country",id.ToString());
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        
    }
}
