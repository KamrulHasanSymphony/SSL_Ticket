

using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.Sample.SSL.Sample.Models;

namespace SSL.Sample.SSL.Sample.Core.Interfaces.Services.VendorService
{
    public interface IVendorServices : IBaseService<AVendorVM>
    {
        List<VendorGroupVm> GetAllCountryData(int id);
        GridEntity<VendorVm> GetAllTestData(GridOptions options);
        List<VendorGroupVm> GetAllVendorData();
    }
}
