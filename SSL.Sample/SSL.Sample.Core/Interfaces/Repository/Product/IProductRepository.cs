using SSL.Sample.Core.Interfaces.Repository;
using SSL.Sample.SSL.Sample.Models;

namespace SSL.Sample.SSL.Sample.Core.Interfaces.Repository.Product
{
    public interface IProductRepository : IBaseRepository<AProductVM>
    {
        int Delete(int id);
    }
}
