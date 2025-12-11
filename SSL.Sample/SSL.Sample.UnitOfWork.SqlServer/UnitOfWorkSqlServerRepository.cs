using Microsoft.Data.SqlClient;
using SSL.Common.SSL.Common.Core.Interfaces.Repository.CompanyInfos;
using SSL.CS.SSL.Common.Core.Interfaces.Repository.Company;
using SSL.Sample.Core.Interfaces.Repository;
using SSL.Sample.Core.Interfaces.Repository.TestRepository;
using SSL.Sample.SSL.Sample.Core.Interfaces.Repository.Product;
using SSL.Sample.SSL.Sample.Core.Interfaces.Repository.Purchase;
using SSL.Sample.SSL.Sample.Core.Interfaces.Repository.Support;
using SSL.Sample.SSL.Sample.Core.Interfaces.Repository.VendorGroupRepository;
using SSL.Sample.SSL.Sample.Core.Interfaces.Repository.VendorRepository;
using SSL.Sample.SSL.Sample.Core.Interfaces.UnitOfWork;
using SSL.Sample.SSL.Sample.Models;
using SSL.Sample.SSL.Sample.Repository.SqlServer.CmnDocuments;
using SSL.Sample.SSL.Sample.Repository.SqlServer.Product;
using SSL.Sample.SSL.Sample.Repository.SqlServer.Purchase;
using SSL.Sample.SSL.Sample.Repository.SqlServer.Support;
using SSL.Sample.SSL.Sample.Repository.SqlServer.Vendor;
using SSL.Sample.SSL.Sample.Repository.SqlServer.VendorGroup;
//using System.Data.SqlClient;

namespace SSL.Sample.UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
    {

        public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction, DbConfig dBConfig)
        {
            VendorRepository = new VendorRepository(context, transaction, dBConfig);
            VendorGroupRepository = new VendorGroupRepository(context, transaction, dBConfig);
            ProductRepository = new ProductRepository(context, transaction, dBConfig);
            PurchaseRepository = new PurchaseRepository(context, transaction, dBConfig);
            PurchaseDetailsRepository = new PurchaseDetailsRepository(context, transaction, dBConfig);
            CmnDocumentRepository = new CmnDocumentRepository(context, transaction, dBConfig);
            SupportRepository = new SupportRepository(context, transaction, dBConfig);
        }

        public INewTestHeaderRepository TestHeaderRepository { get; }

        public INewTestDetailsRepository TestDetailsRepository { get; }

        public ICommonRepository CommonRepository { get; }

        public IVendorRepository VendorRepository { get; }
        public IVendorGroupRepository VendorGroupRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IPurchaseRepository PurchaseRepository { get; }
        public IPurchaseDetailsRepository PurchaseDetailsRepository { get; }
        public ICmnDocumentRepository CmnDocumentRepository { get; }
        public ISupportRepository SupportRepository { get; }
        public ICompanyInfoRepository CompanyInfoRepository => throw new NotImplementedException();
        public ICompanyinfosRepository CompanyInfosRepository => throw new NotImplementedException(); 
        ICommonRepository IUnitOfWorkRepository.CommonRepository => throw new NotImplementedException();
    }
}
