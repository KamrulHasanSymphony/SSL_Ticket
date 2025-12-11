using Microsoft.AspNetCore.Http;
using SSL.CS.SSL.Common.Models;
using SSL.Sample.Core.Interfaces.UnitOfWork;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.CmnDocuments;
using SSL.Sample.SSL.Sample.Models;
using SSL.Sample.SSL.Sample.Services.Vendor;

namespace SSL.Sample.SSL.Sample.Services.Product
{
    public class DocumentService : ICmnDocumentService
    {
        private IUnitOfWork _unitOfWork;
        readonly CommonDataService _common = new CommonDataService();
        public DocumentService(IUnitOfWork unitOfWork, CommonDataService commonDataService)
        {
            _unitOfWork = unitOfWork;
            _common = commonDataService;
        }

        public ResultModel<List<CmnDocument>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<CmnDocument>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
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

        public ResultModel<CmnDocument> Insert(CmnDocument model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {
                if (model is null)
                {
                    return new ResultModel<CmnDocument>()
                    {
                        Status = Status.Warning,
                        Message = MessageModel.NotFoundForSave,
                    };
                }

                CmnDocument master = context.Repositories.CmnDocumentRepository.Insert(model);

                if (master.DocumentId <= 0)
                {
                    return new ResultModel<CmnDocument>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.MasterInsertFailed,
                        Data = master
                    };
                }


                context.SaveChanges();


                return new ResultModel<CmnDocument>()
                {
                    Status = Status.Success,
                    Message = MessageModel.InsertSuccess,
                    Data = master,
                    Success = true
                };
            }
            catch (Exception e)
            {
                context.RollBack();

                return new ResultModel<CmnDocument>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.InsertFail,
                    Exception = e
                };
            }
        }

        public ResultModel<CmnDocument> Update(CmnDocument model)
        {
            throw new NotImplementedException();
        }

        public ResultModel<CmnDocument> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public object Insert(List<IFormFile> files)
        {
            throw new NotImplementedException();
        }
    }
}
