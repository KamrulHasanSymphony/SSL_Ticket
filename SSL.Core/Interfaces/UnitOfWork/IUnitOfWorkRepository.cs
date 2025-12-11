//using Repository.Interfaces;

using SSL.Core.Interfaces.Repository;
using SSL.Core.Interfaces.Repository.Branch;
using SSL.Core.Interfaces.Repository.Company;
using SSL.Core.Interfaces.Repository.CompanyInfos;
using SSL.Core.Interfaces.Repository.TestRepository;
using SSL.Core.Interfaces.Repository.User;

namespace SSL.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
        //IProductRepository ProductRepository { get; }
        //IClientRepository ClientRepository { get; }
        //IInvoiceRepository InvoiceRepository { get; }
        //IInvoiceDetailRepository InvoiceDetailRepository { get; }

        ICommonRepository CommonRepository { get; }

        INewTestHeaderRepository TestHeaderRepository { get; }

        INewTestDetailsRepository TestDetailsRepository { get; }

     
        ICompanyInfoRepository CompanyInfoRepository { get; }
       
        IBranchProfileRepository BranchProfileRepository { get; }
        ICompanyinfosRepository CompanyInfosRepository { get; }
        IUserBranchRepository UserBranchRepository { get; }
      
    }
}
