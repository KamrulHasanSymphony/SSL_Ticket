using SSL.Sample.Models;

namespace SSL.Sample.Core.Interfaces.Repository.TestRepository
{
    public interface INewTestDetailsRepository : IBaseRepository<TestDetailVM>
    {
        string TestDetailMethod(string id);
    }
}
