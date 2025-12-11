using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Sample.Core.Interfaces.UnitOfWork;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.Purchase;
using SSL.Sample.SSL.Sample.Models;
using SSL.Sample.SSL.Sample.Services.Vendor;
using System.Data;
using System.Reflection;

namespace SSL.Sample.SSL.Sample.Services.Purchase
{
    public class PurchaseService : IPurchaseService
    {
        private IUnitOfWork _unitOfWork;
        readonly CommonDataService _common = new CommonDataService();

        public PurchaseService(IUnitOfWork unitOfWork, CommonDataService common)
        {
            _unitOfWork = unitOfWork;
            _common = common;
        }

        public List<AVendorVM> GetAllVendorData()
        {
            try
            {
                return _common.Select_Data_List<AVendorVM>("Sp_Select_AVendor_Info", "get_all_vendor");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public ResultModel<List<PurchaseHeaderVM>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<PurchaseHeaderVM>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
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

        public ResultModel<PurchaseHeaderVM> Insert(PurchaseHeaderVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {
                if (model is null)
                {
                    return new ResultModel<PurchaseHeaderVM>()
                    {
                        Status = Status.Warning,
                        Message = MessageModel.NotFoundForSave,
                    };
                }

                PurchaseHeaderVM master = context.Repositories.PurchaseRepository.Insert(model);

                if (master.PurchaseId <= 0)
                {
                    return new ResultModel<PurchaseHeaderVM>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.MasterInsertFailed,
                        Data = master,
                        Success = true
                    };
                }
                //Details
                foreach (var detail in model.PurchaseHeaderDetails)
                {
                    detail.APurchaseHeaderId = master.PurchaseId;
                    //detail.BranchId = 1;
                    //detail.CompanyId = 1;

                    PurchaseDetailVM returnDetail = context.Repositories.PurchaseDetailsRepository.Insert(detail);

                    if (returnDetail.Id == 0)
                    {
                        return new ResultModel<PurchaseHeaderVM>()
                        {
                            Status = Status.Fail,
                            Message = MessageModel.DetailInsertFailed,
                            Data = master,
                            Success = false
                        };
                    }
                }

                context.SaveChanges();


                return new ResultModel<PurchaseHeaderVM>()
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

                return new ResultModel<PurchaseHeaderVM>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.InsertFail,
                    Exception = e
                };
            }
        }

        public ResultModel<PurchaseHeaderVM> Update(PurchaseHeaderVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {
                if (model is null)
                {
                    return new ResultModel<PurchaseHeaderVM>()
                    {
                        Status = Status.Warning,
                        Message = MessageModel.NotFoundForSave,
                    };
                }

                PurchaseHeaderVM master = context.Repositories.PurchaseRepository.Update(model);

                if (master.PurchaseId <= 0)
                {
                    return new ResultModel<PurchaseHeaderVM>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.MasterInsertFailed,
                        Data = master,
                        Success = true
                    };
                }
                //Details
                foreach (var detail in model.PurchaseHeaderDetails)
                {
                    detail.APurchaseHeaderId = master.PurchaseId;
                    //detail.BranchId = 1;
                    //detail.CompanyId = 1;

                    PurchaseDetailVM returnDetail = context.Repositories.PurchaseDetailsRepository.Update(detail);

                    if (returnDetail.Id == 0)
                    {
                        return new ResultModel<PurchaseHeaderVM>()
                        {
                            Status = Status.Fail,
                            Message = MessageModel.DetailInsertFailed,
                            Data = master,
                            Success = false
                        };
                    }
                }

                context.SaveChanges();


                return new ResultModel<PurchaseHeaderVM>()
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

                return new ResultModel<PurchaseHeaderVM>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.InsertFail,
                    Exception = e
                };
            }
        }

        public ResultModel<PurchaseHeaderVM> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public List<AProductVM> GetAllProductData()
        {
            try
            {
                return _common.Select_Data_List<AProductVM>("Sp_Select_AVendor_Info", "get_all_product");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<PurchaseDetailVM> GetAllPurchaseByMasterId(int purchaseMasterId)
        {
            try
            {
                return _common.Select_Data_List<PurchaseDetailVM>("Select_Purchase_Details_By_Id", "get_all_purchase_details", purchaseMasterId.ToString());
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<AUOMConversitions> GetUomByUOM(string uomId)
        {
            try
            {
                return _common.Select_Data_List<AUOMConversitions>("Select_UOM_By_UOM", "get_all_uom_by_uom", uomId);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public List<AVendorVM> GetVendorByVendorId(int vendorID)
        {
            try
            {
                return _common.Select_Data_List<AVendorVM>("Select_VENDOR_By_VendorID", "get_vendor_by_vendor", vendorID.ToString());
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
        public List<AUOMConversitions> GetAllUOMData()
        {
            try
            {
                return _common.Select_Data_List<AUOMConversitions>("Select_UOM_By_UOM", "get_all_uom_data");
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public GridEntity<PurchaseHeaderVM> GetAllPurchaseData(GridOptions options)
        {
            try
            {

                var purchase = new GridEntity<PurchaseHeaderVM>();
                purchase = KendoGrid<PurchaseHeaderVM>.GetGridData_5(options, "sp_Select_Purchase_Grid", "get_Purchase_summary", "Code");
                return purchase;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public GridEntity<PurchaseDetailVM> GetAllPurchaseDetailData(GridOptions options, int purchaseId)
        {
            try
            {

                var detail = new GridEntity<PurchaseDetailVM>();
                detail = KendoGrid<PurchaseDetailVM>.GetGridData_5(options, "sp_Select_Purchase_Details_Grid", "get_Purchase_Details_summary", "ProductName",purchaseId.ToString());
                return detail;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public List<AProductVM> GetProductById(int aProductId)
        {
            try
            {
                return _common.Select_Data_List<AProductVM>("Sp_Select_AVendor_Info", "get_all_product_by_id", aProductId.ToString());
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public GridEntity<AVendorVM> GetAllVendorData(GridOptions options)
        {
            try
            {

                var vendor = new GridEntity<AVendorVM>();
                vendor = KendoGrid<AVendorVM>.GetGridData_5(options, "sp_Select_AVendor_Grid", "get_vendor_summary", "VendorCode");
                return vendor;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        //public DataTable GetExcelDataWeb(List<string> ds)
        //{
        //    throw new NotImplementedException();
        //}

        public List<PurchaseHeaderVM> GetPurchaseById(string checkedPurchaseIds)
        {
            try
            {
                string purchaseIdsString = string.Join(",", checkedPurchaseIds);

                return _common.ExecuteStoredProcedure<PurchaseHeaderVM>("Sp_Select_Purchase_Info", "get_all_purchase_by_id", purchaseIdsString);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        //public ResultModel<CmnDocument> CreateUpdateDocument(CmnDocument model)
        //{
        //    using IUnitOfWorkAdapter context = _unitOfWork.Create();
        //    try
        //    {
        //        if (model is null)
        //        {
        //            return new ResultModel<CmnDocument>()
        //            {
        //                Status = Status.Warning,
        //                Message = MessageModel.NotFoundForSave,
        //            };
        //        }

        //        CmnDocument master = context.Repositories.CmnDocumentRepository.Insert(model);

        //        if (master.DocumentId <= 0)
        //        {
        //            return new ResultModel<CmnDocument>()
        //            {
        //                Status = Status.Fail,
        //                Message = MessageModel.MasterInsertFailed,
        //                Data = master,
        //                Success = false
        //            };
        //        }

        //        context.SaveChanges();


        //        return new ResultModel<CmnDocument>()
        //        {
        //            Status = Status.Success,
        //            Message = MessageModel.InsertSuccess,
        //            Data = master,
        //            Success = true
        //        };
        //    }
        //    catch (Exception e)
        //    {
        //        context.RollBack();

        //        return new ResultModel<CmnDocument>()
        //        {
        //            Status = Status.Fail,
        //            Message = MessageModel.InsertFail,
        //            Exception = e
        //        };
        //    }
        //}
    }
}
