using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSL_ERP.Models;

namespace SSL.Core.Interfaces.Services.Company
{
    public interface ICompanyInfoService : IBaseService<CompanyInfo>
    {
        ResultModel<List<BranchProfile>> GetBranches(string[] conditionalFields, string[] conditionalValue);
    }
}
