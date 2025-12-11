using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSL.Core.Interfaces.Services.TestService;
using SSL_ERP.Models;
using UnitOfWork.Interfaces;

namespace SSL.Services.NewTestService
{
    public class NewTestDetailsService : INewTestDetailsService
    {
        private IUnitOfWork _unitOfWork;

        public NewTestDetailsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ResultModel<List<TestDetailVM>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<TestDetailVM>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<int> GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetCount(string tableName, string fieldName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<TestDetailVM> Insert(TestDetailVM model)
        {
            throw new NotImplementedException();
        }

        public ResultModel<TestDetailVM> Update(TestDetailVM model)
        {
            throw new NotImplementedException();
        }

        public ResultModel<TestDetailVM> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public string TestDetailMethod(string id)
        {
            throw new NotImplementedException();
        }
    }
}
