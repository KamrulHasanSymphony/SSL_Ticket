using Microsoft.Extensions.Configuration;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork;
using SSL.Ticket.SSL.Ticket.Models;


namespace SSL.Ticket.SSL.Ticket.UnitOfWork.SqlServer
{
    public class UnitOfWorkSqlServer : IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        private readonly CS.SSL.Common.Models.DbConfig _dbConfig;

        public UnitOfWorkSqlServer(IConfiguration configuration, CS.SSL.Common.Models.DbConfig dbConfig)
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
