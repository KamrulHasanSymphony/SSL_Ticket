using SSL.Common.SSL.Common.Models.KendoCommon;
using CompanyInfo = SSL.Ticket.SSL.Ticket.Models.CompanyInfo;

namespace SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Company
{
    public interface ICompanyService : IBaseService<CompanyInfo>
    {
        GridEntity<CompanyInfo> GetGridData(GridOptions options);
    }
}
