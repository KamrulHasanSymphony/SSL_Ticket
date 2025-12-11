using Microsoft.Data.SqlClient;
using SSL.Common.SSL.Common.Models.KendoCommon;
using SSL.CS.SSL.Common.Models;
using SSL.Sample.SSL.Sample.Core.Interfaces.Repository.VendorRepository;
using SSL.Sample.SSL.Sample.Models;
using System.Data;
using DbConfig = SSL.Sample.SSL.Sample.Models.DbConfig;

namespace SSL.Sample.SSL.Sample.Repository.SqlServer.Vendor
{
    public class VendorRepository : Repository, IVendorRepository
    {
        private DbConfig _dbConfig;

        public VendorRepository(SqlConnection context, SqlTransaction transaction, DbConfig dbConfig)
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

                Delete from VendorTestTable

                where Id=@Id
                ";


                var command = CreateCommand(sql);

                command.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;

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

        public List<AVendorVM> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }


        public List<AVendorVM> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public AVendorVM Insert(AVendorVM model)
        {
            //SQL Query

            try
            {
                string sqlText = "";
                int VendorId = 0;

                sqlText = @"

                            INSERT INTO [dbo].[AVendor]
                                       (VendorCode
                                       ,VendorName
                                       ,VendorAddress
                                       ,VendorEmail
                                       ,BINCertificate)
                                 VALUES
                                       (@VendorCode                                        
                                       ,@VendorName
                                       ,@VendorAddress
                                       ,@VendorEmail
                                       ,@BINCertificate)  

                            SELECT SCOPE_IDENTITY()";


                var command = CreateCommand(sqlText);
                //int value = (Convert.ToDateTime(model.EndDate) - Convert.ToDateTime(model.StartDate)).Days;

                command.Parameters.Add("@VendorCode", SqlDbType.NVarChar).Value = model.VendorCode;
                command.Parameters.Add("@VendorName", SqlDbType.NVarChar).Value = model.VendorName;
                command.Parameters.Add("@VendorAddress", SqlDbType.NVarChar).Value = model.VendorAddress;
                command.Parameters.Add("@VendorEmail", SqlDbType.NVarChar).Value = model.VendorEmail;
                command.Parameters.Add("@BINCertificate", SqlDbType.NVarChar).Value = model.BINCertificate==null ?"": model.BINCertificate;

                model.VendorId = Convert.ToInt32(command.ExecuteScalar());

                return model;

            }


            catch (Exception ex)
            {
                throw ex.InnerException;
            }


        }

        public AVendorVM Update(AVendorVM model)
        {
            try
            {

                string sql = "";

                sql = @"

                Update [VendorTestTable]

                 set

                VName=@VendorName,Code = @VendorCode

                where Id=@Id
                ";


                var command = CreateCommand(sql);


                command.Parameters.Add("@Id", SqlDbType.Int).Value = model.VendorId;

                command.Parameters.Add("@VendorName", SqlDbType.NVarChar).Value = model.VendorName;
                command.Parameters.Add("@VendorCode", SqlDbType.NVarChar).Value = model.VendorCode;





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
