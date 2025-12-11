using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SSL.Core.Interfaces.Repository.TestRepository;
using SSL_ERP.Models;

namespace SSL.Repository.SqlServer.TestRepository
{
    public class TestDetailsRepository :Repository, INewTestDetailsRepository
    {
        public TestDetailsRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public List<TestDetailVM> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm)
        {
            throw new NotImplementedException();
        }

        public List<TestDetailVM> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm)
        {
            throw new NotImplementedException();
        }

        public TestDetailVM Insert(TestDetailVM model)
        {
            throw new NotImplementedException();
        }

        public TestDetailVM Update(TestDetailVM model)
        {
            throw new NotImplementedException();
        }

        public string TestDetailMethod(string id)
        {
            throw new NotImplementedException();
        }
    }
}
