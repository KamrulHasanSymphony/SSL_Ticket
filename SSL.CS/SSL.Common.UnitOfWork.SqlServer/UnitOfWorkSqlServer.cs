using Microsoft.Extensions.Configuration;
using SSL.CS.SSL.Common.Core.Interfaces.UnitOfWork;
using SSL.CS.SSL.Common.Models;

namespace SSL.CS.SSL.Common.UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServer : IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        private readonly DbConfig _dbConfig;

        public UnitOfWorkSqlServer(IConfiguration configuration, DbConfig dbConfig)
        {
            _configuration = configuration;
            _dbConfig = dbConfig;
        }

        public IUnitOfWorkAdapter Create()
        {
            var connectionString = _configuration.GetConnectionString("DBContext");

            connectionString = connectionString.Replace("@db", _dbConfig.DbName);
            CS.SSL.Common.Models.SessionModel.sslConn = connectionString;

            return new UnitOfWorkSqlServerAdapter(connectionString, _dbConfig);
        }




        public IUnitOfWorkAdapter CreateAuth()
        {
            var connectionString = _configuration.GetConnectionString("AuthContext");

            return new UnitOfWorkSqlServerAdapter(connectionString, _dbConfig);
        }
    

    }
}
