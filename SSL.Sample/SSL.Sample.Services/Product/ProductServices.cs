using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Sample.Core.Interfaces.UnitOfWork;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.Product;
using SSL.Sample.SSL.Sample.Models;
using SSL.Sample.SSL.Sample.Services.Vendor;

namespace SSL.Sample.SSL.Sample.Services.Product
{
    public class ProductServices : IProductService
    {
        private IUnitOfWork _unitOfWork;
        readonly CommonDataService _common = new CommonDataService();
        public ProductServices(IUnitOfWork unitOfWork, CommonDataService commonDataService)
        {
            _unitOfWork = unitOfWork;
            _common = commonDataService;
        }

        public GridEntity<AProductVM> GetAllProductData(GridOptions options)
        {
            try
            {

                var vendor = new GridEntity<AProductVM>();
                vendor = KendoGrid<AProductVM>.GetGridData_5(options, "sp_Select_Product_Grid", "get_Product_summary", "ProductCode");
                return vendor;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public ResultModel<List<AProductVM>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<AProductVM>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
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

        public ResultModel<AProductVM> Insert(AProductVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {
                if (model is null)
                {
                    return new ResultModel<AProductVM>()
                    {
                        Status = Status.Warning,
                        Message = MessageModel.NotFoundForSave,
                    };
                }

                AProductVM master = context.Repositories.ProductRepository.Insert(model);

                if (master.ProductCode.Length <= 0)
                {
                    return new ResultModel<AProductVM>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.MasterInsertFailed,
                        Data = master
                    };
                }


                context.SaveChanges();


                return new ResultModel<AProductVM>()
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

                return new ResultModel<AProductVM>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.InsertFail,
                    Exception = e
                };
            }
        }

        public ResultModel<AProductVM> Update(AProductVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {
                AProductVM master = context.Repositories.ProductRepository.Update(model);

                context.SaveChanges();

                return new ResultModel<AProductVM>()
                {
                    Status = Status.Success,
                    Message = MessageModel.UpdateSuccess,
                    Data = model,
                    Success = true
                };

            }
            catch (Exception e)
            {
                context.RollBack();

                return new ResultModel<AProductVM>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.UpdateFail,
                    Exception = e
                };
            }
        }

        public ResultModel<AProductVM> Delete(int id)
        {
            using var context = _unitOfWork.Create();
            try
            {
                var count = context.Repositories.ProductRepository.Delete(id);
                context.SaveChanges();
                return new ResultModel<AProductVM>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.DeleteFail,
                    EffectedRows = 0
                };
            }
            catch (Exception e)
            {

                context.RollBack();

                return new ResultModel<AProductVM>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.DeleteFail,
                    EffectedRows = 0
                };
            }
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public List<AProductVM> GetAllCategoryData()
        {
            try
            {
                return _common.Select_Data_List<AProductVM>("Sp_Select_Category_Info", "get_all_category");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<UOMVm> GetAllUomData()
        {
            try
            {
                return _common.Select_Data_List<UOMVm>("Sp_Select_Category_Info", "get_all_uom");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
