using SSL.Common.SSL.Common.Core.Interfaces.Repository;
using SSL.CS.SSL.Common.Models;

namespace SSL.CS.SSL.Common.Core.Interfaces.Repository.Company

{
    public interface ICompanyInfoRepository : IBaseRepository<CompanyInfo>
    {
        List<BranchProfile> GetBranches(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null);
    }
}
