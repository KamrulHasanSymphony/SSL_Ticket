using Microsoft.Data.SqlClient;
using SSL.Common.SSL.Common.Core.Interfaces.Repository;
using SSL.Common.SSL.Common.Core.Interfaces.Repository.CompanyInfos;
using SSL.Common.SSL.Common.Core.Interfaces.UnitOfWork;
using SSL.CS.SSL.Common.Core.Interfaces.Repository.Company;
using SSL.CS.SSL.Common.Models;
using SSL.CS.SSL.Common.Repository.SqlServer.Company;

namespace SSL.CS.SSL.Common.UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
    {

        public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction, DbConfig dBConfig)
        {            
            CompanyInfoRepository = new CompanyInfoRepository(context, transaction, dBConfig);
            //VendorRepository = new VendorRepository(context, transaction, dBConfig);
        }
       

        public ICommonRepository CommonRepository { get; }

        public ICompanyinfosRepository CompanyInfosRepository { get; }

        public ICompanyInfoRepository CompanyInfoRepository { get; }
    }
}
