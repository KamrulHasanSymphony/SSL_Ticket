using Microsoft.Data.SqlClient;
using SSL.CS.SSL.Common.Models;
using SSL.Sample.SSL.Sample.Core.Interfaces.Repository.Purchase;
using SSL.Sample.SSL.Sample.Models;
using System.Data;
using DbConfig = SSL.Sample.SSL.Sample.Models.DbConfig;

namespace SSL.Sample.SSL.Sample.Repository.SqlServer.Purchase
{
    public class PurchaseDetailsRepository : Repository, IPurchaseDetailsRepository
    {
        private DbConfig _dbConfig;

        public PurchaseDetailsRepository(SqlConnection context, SqlTransaction transaction, DbConfig dbConfig)
        {
            this._context = context;
            this._transaction = transaction;

            this._dbConfig = dbConfig;
        }

        public List<PurchaseDetailVM> GetAll(string[] conditionalFields, string[] conditionalValue, CS.SSL.Common.Models.PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public List<PurchaseDetailVM> GetIndexData(CS.SSL.Common.Models.IndexModel index, string[] conditionalFields, string[] conditionalValue, CS.SSL.Common.Models.PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(CS.SSL.Common.Models.IndexModel index, string[] conditionalFields, string[] conditionalValue, CS.SSL.Common.Models.PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public PurchaseDetailVM Insert(PurchaseDetailVM model)
        {
            try
            {
                string sqlText = "";

                sqlText = @"

                            INSERT INTO [dbo].[APurchaseDetails]
                                       (APurchaseHeaderId
                                       ,AProductId
                                       ,Quantity
                                       ,UnitPrice
                                       ,SubTotal
                                       ,VATRate
                                       ,VATAmount
                                       ,Total
                                       ,UOM
                                       ,UOMn
                                       ,UOMc)
                                 VALUES
                                       (@APurchaseHeaderId                                        
                                       ,@AProductId
                                       ,@Quantity
                                       ,@UnitPrice
                                       ,@SubTotal  
                                       ,@VATRate  
                                       ,@VATAmount  
                                       ,@Total  
                                       ,@UOM  
                                       ,@UOMn  
                                       ,@UOMc)  

                            SELECT SCOPE_IDENTITY()";


                var command = CreateCommand(sqlText);

                command.Parameters.Add("@APurchaseHeaderId", SqlDbType.Int).Value = model.APurchaseHeaderId;
                command.Parameters.Add("@AProductId", SqlDbType.Int).Value = model.AProductId;
                command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = model.Quantity;
                command.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = model.UnitPrice;
                command.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = model.SubTotal;
                command.Parameters.Add("@VATRate", SqlDbType.Decimal).Value = model.VATRate;
                command.Parameters.Add("@VATAmount", SqlDbType.Decimal).Value = model.VATAmount;
                command.Parameters.Add("@Total", SqlDbType.Decimal).Value = model.Total;
                command.Parameters.Add("@UOM", SqlDbType.NVarChar).Value = model.UOM;
                command.Parameters.Add("@UOMn", SqlDbType.NVarChar).Value = model.UOMn;
                command.Parameters.Add("@UOMc", SqlDbType.Decimal).Value = model.UOMc;

                model.Id = Convert.ToInt32(command.ExecuteScalar());

                return model;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public PurchaseDetailVM Update(PurchaseDetailVM model)
        {
            try
            {
                string sqlText = "";

                if(model.Id == 0)
                {
                    sqlText = @"

                            INSERT INTO [dbo].[APurchaseDetails]
                                       (APurchaseHeaderId
                                       ,AProductId
                                       ,Quantity
                                       ,UnitPrice
                                       ,SubTotal
                                       ,VATRate
                                       ,VATAmount
                                       ,Total
                                       ,UOM
                                       ,UOMn
                                       ,UOMc)
                                 VALUES
                                       (@APurchaseHeaderId                                        
                                       ,@AProductId
                                       ,@Quantity
                                       ,@UnitPrice
                                       ,@SubTotal  
                                       ,@VATRate  
                                       ,@VATAmount  
                                       ,@Total  
                                       ,@UOM  
                                       ,@UOMn  
                                       ,@UOMc)  

                            SELECT SCOPE_IDENTITY()";

                    var command1 = CreateCommand(sqlText);

                    command1.Parameters.Add("@APurchaseHeaderId", SqlDbType.Int).Value = model.APurchaseHeaderId;
                    command1.Parameters.Add("@AProductId", SqlDbType.Int).Value = model.AProductId;
                    command1.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = model.Quantity;
                    command1.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = model.UnitPrice;
                    command1.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = model.SubTotal;
                    command1.Parameters.Add("@VATRate", SqlDbType.Decimal).Value = model.VATRate;
                    command1.Parameters.Add("@VATAmount", SqlDbType.Decimal).Value = model.VATAmount;
                    command1.Parameters.Add("@Total", SqlDbType.Decimal).Value = model.Total;
                    command1.Parameters.Add("@UOM", SqlDbType.NVarChar).Value = model.UOM;
                    command1.Parameters.Add("@UOMn", SqlDbType.NVarChar).Value = model.UOMn;
                    command1.Parameters.Add("@UOMc", SqlDbType.Decimal).Value = model.UOMc;

                    model.Id = Convert.ToInt32(command1.ExecuteScalar());
                }
                else
                {
                    sqlText = @"

                            Update [dbo].[APurchaseDetails]
                                set    APurchaseHeaderId = @APurchaseHeaderId
                                       ,AProductId = @AProductId
                                       ,Quantity = @Quantity
                                       ,UnitPrice = @UnitPrice
                                       ,SubTotal = @SubTotal
                                       ,VATRate = @VATRate
                                       ,VATAmount = @VATAmount
                                       ,Total = @Total
                                       ,UOM = @UOM
                                       ,UOMn = @UOMn
                                       ,UOMc = @UOMc
                                 Where Id = @PurchaseDetailId ";


                    var command = CreateCommand(sqlText);

                    command.Parameters.Add("@PurchaseDetailId", SqlDbType.Int).Value = model.Id;
                    command.Parameters.Add("@APurchaseHeaderId", SqlDbType.Int).Value = model.APurchaseHeaderId;
                    command.Parameters.Add("@AProductId", SqlDbType.Int).Value = model.AProductId;
                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = model.Quantity;
                    command.Parameters.Add("@UnitPrice", SqlDbType.Decimal).Value = model.UnitPrice;
                    command.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = model.SubTotal;
                    command.Parameters.Add("@VATRate", SqlDbType.Decimal).Value = model.VATRate;
                    command.Parameters.Add("@VATAmount", SqlDbType.Decimal).Value = model.VATAmount;
                    command.Parameters.Add("@Total", SqlDbType.Decimal).Value = model.Total;
                    command.Parameters.Add("@UOM", SqlDbType.NVarChar).Value = model.UOM;
                    command.Parameters.Add("@UOMn", SqlDbType.NVarChar).Value = model.UOMn;
                    command.Parameters.Add("@UOMc", SqlDbType.Decimal).Value = model.UOMc;

                    int rowcount = Convert.ToInt32(command.ExecuteNonQuery());

                    if (rowcount <= 0)
                    {
                        throw new Exception(MessageModel.UpdateFail);
                    }
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
