using Microsoft.Data.SqlClient;
using SSL.CS.SSL.Common.Models;
using SSL.Sample.SSL.Sample.Core.Interfaces.Repository.Purchase;
using SSL.Sample.SSL.Sample.Models;
using System.Data;
using DbConfig = SSL.Sample.SSL.Sample.Models.DbConfig;

namespace SSL.Sample.SSL.Sample.Repository.SqlServer.Purchase
{
    public class PurchaseRepository : Repository, IPurchaseRepository
    {
        private DbConfig _dbConfig;

        public PurchaseRepository(SqlConnection context, SqlTransaction transaction, DbConfig dbConfig)
        {
            this._context = context;
            this._transaction = transaction;

            this._dbConfig = dbConfig;
        }

        public List<PurchaseHeaderVM> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public List<PurchaseHeaderVM> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public PurchaseHeaderVM Insert(PurchaseHeaderVM model)
        {
            try
            {
                string sqlText = "";

                sqlText = @"

                            INSERT INTO [dbo].[APurchaseHeaders]
                                       (Code
                                       ,TransactionDate
                                       ,AVendorId
                                       ,Remarks
                                       ,SubTotal
                                       ,SubtotalVAT
                                       ,Total)
                                 VALUES
                                       (@Code                                        
                                       ,@TransactionDate
                                       ,@AVendorId
                                       ,@Remarks
                                       ,@SubTotal  
                                       ,@SubtotalVAT  
                                       ,@Total)  

                            SELECT SCOPE_IDENTITY()";


                var command = CreateCommand(sqlText);
                //int value = (Convert.ToDateTime(model.EndDate) - Convert.ToDateTime(model.StartDate)).Days;

                command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = model.Code;
                command.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = model.TransactionDate;
                command.Parameters.Add("@AVendorId", SqlDbType.Int).Value = model.AVendorId;
                command.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = model.Remarks == "" || model.Remarks == null ? "" : model.Remarks;
                command.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = model.SubTotal;
                command.Parameters.Add("@SubtotalVAT", SqlDbType.Decimal).Value = model.SubtotalVAT;
                command.Parameters.Add("@Total", SqlDbType.Decimal).Value = model.Total;

                model.PurchaseId = Convert.ToInt32(command.ExecuteScalar());

                return model;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }            
        }

        public PurchaseHeaderVM Update(PurchaseHeaderVM model)
        {
            try
            {
                string sqlText = "";

                sqlText = @"

                            Update [dbo].[APurchaseHeaders]
                                        Set
                                       Code = @Code
                                       ,TransactionDate = @TransactionDate
                                       ,AVendorId = @AVendorId
                                       ,Remarks = @Remarks
                                       ,SubTotal = @SubTotal
                                       ,SubtotalVAT = @SubtotalVAT
                                       ,Total = @Total
                                 Where Id = @PurchaseHeaderId ";


                var command = CreateCommand(sqlText);
                //int value = (Convert.ToDateTime(model.EndDate) - Convert.ToDateTime(model.StartDate)).Days;

                command.Parameters.Add("@PurchaseHeaderId", SqlDbType.Int).Value = model.PurchaseId;
                command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = model.Code;
                command.Parameters.Add("@TransactionDate", SqlDbType.DateTime).Value = model.TransactionDate;
                command.Parameters.Add("@AVendorId", SqlDbType.Int).Value = model.AVendorId;
                command.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = model.Remarks;
                command.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = model.SubTotal;
                command.Parameters.Add("@SubtotalVAT", SqlDbType.Decimal).Value = model.SubtotalVAT;
                command.Parameters.Add("@Total", SqlDbType.Decimal).Value = model.Total;

                int rowcount = Convert.ToInt32(command.ExecuteNonQuery());

                if (rowcount <= 0)
                {
                    throw new Exception(MessageModel.UpdateFail);
                }

                return model;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
    }
}
