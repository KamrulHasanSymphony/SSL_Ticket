using SSL.Sample.Core.Interfaces.Repository;
using SSL.Sample.Core.Interfaces.Repository.TestRepository;
using SSL.Sample.SSL.Sample.Core.Interfaces.Repository.Product;
using SSL.Sample.SSL.Sample.Core.Interfaces.Repository.Purchase;
using SSL.Sample.SSL.Sample.Core.Interfaces.Repository.Support;
using SSL.Sample.SSL.Sample.Core.Interfaces.Repository.VendorRepository;


namespace SSL.Sample.SSL.Sample.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
     

        ICommonRepository CommonRepository { get; }

        INewTestHeaderRepository TestHeaderRepository { get; }

        INewTestDetailsRepository TestDetailsRepository { get; }
        IVendorRepository VendorRepository { get; }
        IProductRepository ProductRepository { get; }
        IPurchaseRepository PurchaseRepository { get; }
        IPurchaseDetailsRepository PurchaseDetailsRepository { get; }
        ICmnDocumentRepository CmnDocumentRepository { get; }
        ISupportRepository SupportRepository { get; }
    }
}
