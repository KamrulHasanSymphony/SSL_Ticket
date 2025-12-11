using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Product;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Services.Product
{
    public class ProductService : IProductServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_ProductsVM> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ResultModel<List<T_ProductsVM>> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    List<T_ProductsVM> pInfos =
                        context.Repositories.ProductRepository.GetAll(conditionalFields, conditionalValue);
                    context.SaveChanges();

                    return new ResultModel<List<T_ProductsVM>>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.DataLoaded,
                        Data = pInfos
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<List<T_ProductsVM>>()
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

        public GridEntity<T_ProductsVM> GetGridData(GridOptions options)
        {
            try
            {
                using IUnitOfWorkAdapter context = _unitOfWork.Create();

                using (var dbContext = _unitOfWork.Create())
                {
                    var detail = new GridEntity<T_ProductsVM>();
                    detail = KendoGrid<T_ProductsVM>.GetGridData_5(options, "sp_Select_Product_Grid", "get_product_summary", "Id");
                    return detail;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public ResultModel<List<T_ProductsVM>> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<int> GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_ProductsVM> Insert(T_ProductsVM model)
        {
            using IUnitOfWorkAdapter context = _unitOfWork.Create();
            try
            {


                T_ProductsVM master = context.Repositories.ProductRepository.Insert(model);

                context.SaveChanges();
                return new ResultModel<T_ProductsVM>()
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

        public ResultModel<T_ProductsVM> InsertActive(T_ProductsVM model)
        {
            throw new NotImplementedException();
        }

        public ResultModel<T_ProductsVM> Update(T_ProductsVM model)
        {
            using (var context = _unitOfWork.Create())
            {

                try
                {
                    T_ProductsVM master = context.Repositories.ProductRepository.Update(model);

                    context.SaveChanges();


                    return new ResultModel<T_ProductsVM>()
                    {
                        Status = Status.Success,
                        Message = MessageModel.UpdateSuccess,
                        Data = model
                    };

                }
                catch (Exception e)
                {
                    context.RollBack();

                    return new ResultModel<T_ProductsVM>()
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
