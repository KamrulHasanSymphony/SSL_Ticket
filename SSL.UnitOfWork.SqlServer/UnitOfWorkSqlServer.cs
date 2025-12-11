using Microsoft.Extensions.Configuration;
using SSL_ERP.Models;
using UnitOfWork.Interfaces;

namespace SSL.UnitOfWork.SqlServer
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

            return new UnitOfWorkSqlServerAdapter(connectionString, _dbConfig);
        }


        public IUnitOfWorkAdapter CreateSage()
        {
            var connectionString = _configuration.GetConnectionString("DBContextSage");
            connectionString = connectionString.Replace("@sageDb", _dbConfig.SageDbName);

            return new UnitOfWorkSqlServerAdapter(connectionString, _dbConfig);
        }

        public IUnitOfWorkAdapter CreateAuth()
        {
            var connectionString = _configuration.GetConnectionString("AuthContext");

            return new UnitOfWorkSqlServerAdapter(connectionString, _dbConfig);
        }
        public IUnitOfWorkAdapter CreateDLR(string db)
        {
            var connectionString = _configuration.GetConnectionString("DBContextDLR");
            _dbConfig.DLRDbName = db;
            connectionString = connectionString.Replace("@DLRDB", _dbConfig.DLRDbName);

            return new UnitOfWorkSqlServerAdapter(connectionString, _dbConfig);
        }

    }
}
