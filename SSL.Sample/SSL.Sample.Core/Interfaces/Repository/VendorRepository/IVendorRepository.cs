using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.Sample.Core.Interfaces.Repository;
using SSL.Sample.SSL.Sample.Models;

namespace SSL.Sample.SSL.Sample.Core.Interfaces.Repository.VendorRepository
{
    public interface IVendorRepository : IBaseRepository<AVendorVM>
    {
        //GridEntity<VendorVm> GetAllTestData(GridOptions options);
        int Delete(int id);

    }
}
