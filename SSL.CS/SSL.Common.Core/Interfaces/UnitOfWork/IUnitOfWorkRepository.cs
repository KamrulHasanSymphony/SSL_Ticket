using SSL.Common.SSL.Common.Core.Interfaces.Repository;
using SSL.Common.SSL.Common.Core.Interfaces.Repository.CompanyInfos;
using SSL.CS.SSL.Common.Core.Interfaces.Repository.Company;

namespace SSL.Common.SSL.Common.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
     

        ICommonRepository CommonRepository { get; }
        ICompanyInfoRepository CompanyInfoRepository { get; }
        ICompanyinfosRepository CompanyInfosRepository { get; }
        //ISupportRepository SupportRepository { get; }
    }
}
