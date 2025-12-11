using SSL.Sample.Models;

namespace SSL.Sample.Core.Interfaces.Repository.TestRepository
{
    public interface INewTestHeaderRepository : IBaseRepository<TestHeaderVM>
    {
        string TestMethod(string id);
    }
}
