//using Repository.Interfaces;
//using Repository.SqlServer;
//using System.Data.SqlClient;

using Microsoft.Data.SqlClient;
using SSL.Core.Interfaces.Repository;
using SSL.Core.Interfaces.Repository.Branch;
using SSL.Core.Interfaces.Repository.Company;
using SSL.Core.Interfaces.Repository.CompanyInfos;
using SSL.Core.Interfaces.Repository.TestRepository;
using SSL.Core.Interfaces.Repository.User;
using SSL.Core.Interfaces.UnitOfWork;
using SSL_ERP.Models;

namespace SSL.UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServerRepository : IUnitOfWorkRepository
    {

        public UnitOfWorkSqlServerRepository(SqlConnection context, SqlTransaction transaction, DbConfig dBConfig)
        {


             

            //TestHeaderRepository = new TestHeaderRepository(context, transaction);
            //TestDetailsRepository = new TestDetailsRepository(context, transaction);
            //CompanyInfoRepository= new CompanyInfoRepository(context, transaction);
            //BranchProfileRepository = new BranchProfileRepository(context, transaction);
            //CompanyInfosRepository = new CompanyInfosRepository(context, transaction);
            //UserBranchRepository = new UserBranchRepository(context, transaction, dBConfig);

        }
        public ICompanyInfoRepository CompanyInfoRepository { get; }

        public ICommonRepository CommonRepository { get; }
        public IUserBranchRepository UserBranchRepository { get; }
 
        public IBranchProfileRepository BranchProfileRepository { get; }

        public ICompanyinfosRepository CompanyInfosRepository { get; }

    


        public INewTestHeaderRepository TestHeaderRepository { get; }

        public INewTestDetailsRepository TestDetailsRepository { get; }
      

	}
}
