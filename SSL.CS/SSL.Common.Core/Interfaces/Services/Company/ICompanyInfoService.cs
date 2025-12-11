using SSL.Common.SSL.Common.Core.Interfaces.Services;
using SSL.CS.SSL.Common.Models;


namespace SSL.CS.SSL.Common.Core.Interfaces.Services.Company

{
    public interface ICompanyInfoService : IBaseService<CompanyInfo>
    {
        ResultModel<List<BranchProfile>> GetBranches(string[] conditionalFields, string[] conditionalValue);
        
    }
}
