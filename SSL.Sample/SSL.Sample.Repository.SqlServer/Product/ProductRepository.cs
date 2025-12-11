using Microsoft.Data.SqlClient;
using SSL.CS.SSL.Common.Models;
using SSL.Sample.SSL.Sample.Core.Interfaces.Repository.Product;
using SSL.Sample.SSL.Sample.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbConfig = SSL.Sample.SSL.Sample.Models.DbConfig;

namespace SSL.Sample.SSL.Sample.Repository.SqlServer.Product
{
    public class ProductRepository: Repository,IProductRepository
    {
        private DbConfig _dbConfig;

        public ProductRepository(SqlConnection context, SqlTransaction transaction, DbConfig dbConfig)
        {
            this._context = context;
            this._transaction = transaction;

            this._dbConfig = dbConfig;
        }

        public int Delete(int id)
        {
            try
            {

                string sql = "";

                sql = @"

                Delete from Products

                where ItemNo=@ItemNo
                ";


                var command = CreateCommand(sql);

                command.Parameters.Add("@ItemNo", SqlDbType.NVarChar).Value = id;

                int rowcount = Convert.ToInt32(command.ExecuteNonQuery());

                if (rowcount <= 0)
                {
                    throw new Exception(MessageModel.DeleteFail);
                }

                return 1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AProductVM> GetAll(string[] conditionalFields, string[] conditionalValue, CS.SSL.Common.Models.PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public List<AProductVM> GetIndexData(CS.SSL.Common.Models.IndexModel index, string[] conditionalFields, string[] conditionalValue, CS.SSL.Common.Models.PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(CS.SSL.Common.Models.IndexModel index, string[] conditionalFields, string[] conditionalValue, CS.SSL.Common.Models.PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public AProductVM Insert(AProductVM model)
        {
            try
            {
                string sqlText = "";                

                sqlText = @"

                            INSERT INTO [dbo].[AProducts]
                                       ( ProductCode
                                        ,ProductName
                                        ,IsActive
                                        ,OpeningDate
                                        ,OpeningQuantity
                                        ,UOM
                                        ,VATRate                                        
                                        )
                                 VALUES
                                       (@ProductCode
                                        ,@ProductName
                                        ,@IsActive
                                        ,@OpeningDate
                                        ,@OpeningQuantity
                                        ,@UOM
                                        ,@VATRate)  

                            SELECT SCOPE_IDENTITY() ";


                var command = CreateCommand(sqlText);
                
                command.Parameters.Add("@ProductCode", SqlDbType.NVarChar).Value = model.ProductCode;
                command.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = model.ProductName;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                command.Parameters.Add("@OpeningDate", SqlDbType.DateTime).Value = model.OpeningDate;
                command.Parameters.Add("@OpeningQuantity", SqlDbType.Decimal).Value = model.OpeningQuantity;
                command.Parameters.Add("@UOM", SqlDbType.NVarChar).Value = model.UOM;
                command.Parameters.Add("@VATRate", SqlDbType.Decimal).Value = model.VATRate;

                model.ProductId = Convert.ToInt32(command.ExecuteScalar());

                return model;

            }


            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public AProductVM Update(AProductVM model)
        {
            try
            {

                string sql = "";

                sql = @"

                Update [AProducts]

                 set

                ProductCode=@ProductCode
                ,ProductName = @ProductName
                ,IsActive = @IsActive
                ,OpeningDate = @OpeningDate
                ,OpeningQuantity = @OpeningQuantity
                ,UOM=@UOM
                ,VATRate = @VATRate

                where Id=@ProductId
                ";


                var command = CreateCommand(sql);


                command.Parameters.Add("@ProductId", SqlDbType.Int).Value = model.ProductId;

                command.Parameters.Add("@ProductCode", SqlDbType.NVarChar).Value = model.ProductCode;
                command.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = model.ProductName;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                command.Parameters.Add("@OpeningDate", SqlDbType.DateTime).Value = model.OpeningDate;
                command.Parameters.Add("@OpeningQuantity", SqlDbType.Decimal).Value = model.OpeningQuantity;
                command.Parameters.Add("@UOM", SqlDbType.NVarChar).Value = model.UOM;
                command.Parameters.Add("@VATRate", SqlDbType.Decimal).Value = model.VATRate;

                int rowcount = Convert.ToInt32(command.ExecuteNonQuery());

                if (rowcount <= 0)
                {
                    throw new Exception(MessageModel.UpdateFail);
                }

                return model;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
