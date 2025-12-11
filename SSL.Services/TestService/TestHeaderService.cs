using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSL.Core.Interfaces.Services.TestService;
using SSL_ERP.Models;
using UnitOfWork.Interfaces;

namespace SSL.Services.TestService
{
    public class NewTestHeaderService : INewTestHeaderService
    {
        private IUnitOfWork _unitOfWork;

        public NewTestHeaderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ResultModel<List<TestHeaderVM>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<TestHeaderVM>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            using var context = _unitOfWork.Create();
            try
            {
                List<TestHeaderVM> records = context.Repositories.TestHeaderRepository.GetIndexData(index, conditionalFields, conditionalValue);
                context.SaveChanges();

                return new ResultModel<List<TestHeaderVM>>()
                {
                    Status = Status.Success,
                    Message = MessageModel.DataLoaded,
                    Data = records
                };

            }
            catch (Exception e)
            {
                context.RollBack();

                return new ResultModel<List<TestHeaderVM>>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.DataLoadedFailed,
                    Exception = e
                };
            }
        }

        public ResultModel<int> GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            using var context = _unitOfWork.Create();
            try
            {
                int records = context.Repositories.TestHeaderRepository.GetIndexDataCount(index, conditionalFields, conditionalValue);
                context.SaveChanges();

                return new ResultModel<int>()
                {
                    Status = Status.Success,
                    Message = MessageModel.DataLoaded,
                    Data = records
                };

            }
            catch (Exception e)
            {
                context.RollBack();

                return new ResultModel<int>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.DataLoadedFailed,
                    Exception = e
                };
            }
        }

        public int GetCount(string tableName, string fieldName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    int count =
                        context.Repositories.TestHeaderRepository.GetCount(tableName,
                            fieldName, null, null);
                    context.SaveChanges();


                    return count;

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return 0;
                }

            }
        }

        public ResultModel<TestHeaderVM> Insert(TestHeaderVM model)
        {
            throw new NotImplementedException();
        }

        public ResultModel<TestHeaderVM> Update(TestHeaderVM model)
        {
            throw new NotImplementedException();
        }

        public ResultModel<TestHeaderVM> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public string TestMethod(string id)
        {
            throw new NotImplementedException();
        }
    }
}
