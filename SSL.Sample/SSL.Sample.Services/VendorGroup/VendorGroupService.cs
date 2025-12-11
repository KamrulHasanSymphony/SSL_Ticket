using SSL.CS.SSL.Common.Core.Interfaces.UnitOfWork;
using SSL.CS.SSL.Common.Models;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.VendorGroup;
using SSL.Sample.SSL.Sample.Models;

namespace SSL.Sample.SSL.Sample.Services.VendorGroup
{
    public class VendorGroupService : IVendorGroup
    {
        private IUnitOfWork _unitOfWork;

        public VendorGroupService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<VendorGroupVm> Delete(int id)
        {
            using var context = _unitOfWork.Create();
            try
            {
                //var count = context.Repositories.VendorGroupRepository.Delete(id);
                context.SaveChanges();
                return new ResultModel<VendorGroupVm>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.DeleteFail,
                    EffectedRows = 0
                };
            }
            catch (Exception e)
            {

                context.RollBack();

                return new ResultModel<VendorGroupVm>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.DeleteFail,
                    EffectedRows = 0
                };
            }
        }

        public ResultModel<List<VendorGroupVm>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetCount(string tableName, string fieldName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<VendorGroupVm>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<int> GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<VendorGroupVm> Insert(VendorGroupVm model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {
                if (model is null)
                {
                    return new ResultModel<VendorGroupVm>()
                    {
                        Status = Status.Warning,
                        Message = MessageModel.NotFoundForSave,
                    };
                }

                //VendorGroupVm master = context.Repositories.VendorGroupRepository.Insert(model);
               

                //if (master.VendorGroupID <= 0)
                //{
                //    return new ResultModel<VendorGroupVm>()
                //    {
                //        Status = Status.Fail,
                //        Message = MessageModel.MasterInsertFailed,
                //        Data = master
                //    };
                //}

                context.SaveChanges();

                //return new ResultModel<VendorGroupVm>()
                //{
                //    Status = Status.Success,
                //    Message = MessageModel.InsertSuccess,
                //    Data = master
                //};

                //Extra
                return new ResultModel<VendorGroupVm>();
            }
            catch (Exception e)
            {
                context.RollBack();

                return new ResultModel<VendorGroupVm>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.InsertFail,
                    Exception = e
                };
            }
        }

        public ResultModel<VendorGroupVm> Update(VendorGroupVm model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {
                //VendorGroupVm master = context.Repositories.VendorGroupRepository.Update(model);

                context.SaveChanges();

                return new ResultModel<VendorGroupVm>()
                {
                    Status = Status.Success,
                    Message = MessageModel.UpdateSuccess,
                    Data = model
                };

            }
            catch (Exception e)
            {
                context.RollBack();

                return new ResultModel<VendorGroupVm>()
                {
                    Status = Status.Fail,
                    Message = MessageModel.UpdateFail,
                    Exception = e
                };
            }
        }
    }
}
