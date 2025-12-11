using Shampan.Models.KendoCommon;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Company;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Services.Company
{
    public class CompanyService : ICompanyService
    {
        private IUnitOfWork _unitOfWork;

        public CompanyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<Models.CompanyInfo> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<Models.CompanyInfo>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            using (var context = _unitOfWork.CreateAuth())
            {

                try
                {
                    List<Models.CompanyInfo> companyInfos =
                        context.Repositories.CompanyRepository.GetAll(conditionalFields, conditionalValue);
                    context.SaveChanges();

                    return new ResultModel<List<Models.CompanyInfo>>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.DataLoaded,
                        Data = companyInfos
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<List<Models.CompanyInfo>>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.DataLoadedFailed,
                        Exception = e
                    };
                }


            }
        }

        public int GetCount(string tableName, string fieldName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public GridEntity<Models.CompanyInfo> GetGridData(GridOptions options)
        {
            try
            {
                using IUnitOfWorkAdapter context = _unitOfWork.Create();

                using (var dbContext = _unitOfWork.Create())
                {
                    var company = new GridEntity<Models.CompanyInfo>();
                    company = KendoGrid<Models.CompanyInfo>.GetGridData_5(options, "sp_Select_Company_Grid", "get_company_summary", "CompanyName");
                    return company;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public ResultModel<List<Models.CompanyInfo>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<int> GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<Models.CompanyInfo> Insert(Models.CompanyInfo model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {


                Models.CompanyInfo master = context.Repositories.CompanyRepository.Insert(model);

                context.SaveChanges();
                return new ResultModel<Models.CompanyInfo>()
                {
                    Status = Status.Success,
                    Message = MessageModel.InsertSuccess,
                    Data = master,
                    Success = true
                };

            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public ResultModel<Models.CompanyInfo> InsertActive(Models.CompanyInfo model)
        {
            throw new NotImplementedException();
        }

        public ResultModel<Models.CompanyInfo> Update(Models.CompanyInfo model)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    Models.CompanyInfo master = context.Repositories.CompanyRepository.Update(model);

                    context.SaveChanges();


                    return new ResultModel<Models.CompanyInfo>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.UpdateSuccess,
                        Data = model
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<Models.CompanyInfo>()
                    {
                        Status = Status.Fail,
                        Message = MessageModel.UpdateFail,
                        Exception = e
                    };
                }
            }
        }
    }
}
