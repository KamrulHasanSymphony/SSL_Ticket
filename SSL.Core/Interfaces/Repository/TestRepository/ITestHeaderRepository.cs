using SSL_ERP.Models;

namespace SSL.Core.Interfaces.Repository.TestRepository
{
    public interface INewTestHeaderRepository : IBaseRepository<TestHeaderVM>
    {
        string TestMethod(string id);
    }
}
