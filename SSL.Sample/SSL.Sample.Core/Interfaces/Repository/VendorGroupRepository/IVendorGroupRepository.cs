
using SSL.Sample.Core.Interfaces.Repository;
using SSL.Sample.SSL.Sample.Models;

namespace SSL.Sample.SSL.Sample.Core.Interfaces.Repository.VendorGroupRepository
{
    public interface IVendorGroupRepository : IBaseRepository<VendorGroupVm>
    {
        int Delete(int id);
    }
}
