using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Sample.SSL.Sample.Models;
using System.Data;

namespace SSL.Sample.SSL.Sample.Core.Interfaces.Services.Purchase
{
    public interface IPurchaseService : IBaseService<PurchaseHeaderVM>
    {
        List<PurchaseDetailVM> GetAllPurchaseByMasterId(int purchaseMasterId);
        List<AProductVM> GetAllProductData();
        List<AVendorVM> GetAllVendorData();
        List<AUOMConversitions> GetUomByUOM(string uomId);
        GridEntity<PurchaseHeaderVM> GetAllPurchaseData(GridOptions options);
        GridEntity<PurchaseDetailVM> GetAllPurchaseDetailData(GridOptions options, int purchaseId);
        GridEntity<AVendorVM> GetAllVendorData(GridOptions options);
        List<AProductVM> GetProductById(int aProductId);
        List<AUOMConversitions> GetAllUOMData();
        List<AVendorVM> GetVendorByVendorId(int vendorID);
        List<PurchaseHeaderVM> GetPurchaseById(string checkedPurchaseIds);        
    }
}
