using Microsoft.Data.SqlClient;
using SSL.CS.SSL.Common.Models;
using SSL.Sample.SSL.Sample.Core.Interfaces.Repository.VendorGroupRepository;
using SSL.Sample.SSL.Sample.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbConfig = SSL.Sample.SSL.Sample.Models.DbConfig;

namespace SSL.Sample.SSL.Sample.Repository.SqlServer.VendorGroup
{
    public class VendorGroupRepository : Repository, IVendorGroupRepository
    {
        private DbConfig _dbConfig;

        public VendorGroupRepository(SqlConnection context, SqlTransaction transaction, DbConfig dbConfig)
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

                Delete from VendorGroupTestTable

                where Id=@Id
                ";


                var command = CreateCommand(sql);

                command.Parameters.Add("@Id", SqlDbType.NVarChar).Value = id;

                int rowcount = Convert.ToInt32(command.ExecuteNonQuery());

                if (rowcount <= 0)
                {
                    throw new Exception(CS.SSL.Common.Models.MessageModel.DeleteFail);
                }

                return 1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<VendorGroupVm> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public List<VendorGroupVm> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public VendorGroupVm Insert(VendorGroupVm model)
        {
            try
            {
                string sqlText = "";
                int VendorGroupID = 0;

                sqlText = @"

                            INSERT INTO [dbo].[VendorGroupTestTable]
                                       (VendorGroupName
                                       ,GroupType
                                       ,VendorGroupDescription
                                       ,Comments
                                       ,ActiveStatus)
                                 VALUES
                                       (@VGroupname
                                       ,@VGroupType
                                       ,@VGroupDesc
                                       ,@VGroupComments
                                       ,@VGroupStatus
)  

                            SELECT SCOPE_IDENTITY() ";


                var command = CreateCommand(sqlText);
                //int value = (Convert.ToDateTime(model.EndDate) - Convert.ToDateTime(model.StartDate)).Days;

                command.Parameters.Add("@VGroupname", SqlDbType.NVarChar).Value = model.VendorGroupName;
                command.Parameters.Add("@VGroupType", SqlDbType.NVarChar).Value = model.GroupType;
                command.Parameters.Add("@VGroupDesc", SqlDbType.NVarChar).Value = model.VendorGroupDescription;
                command.Parameters.Add("@VGroupComments", SqlDbType.NVarChar).Value = model.Comments;
                command.Parameters.Add("@VGroupStatus", SqlDbType.NVarChar).Value = model.ActiveStatus;

                model.VendorGroupID = Convert.ToInt32(command.ExecuteScalar());

                return model;

            }


            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public VendorGroupVm Update(VendorGroupVm model)
        {
            try
            {

                string sql = "";

                sql = @"

                Update [VendorGroupTestTable]

                 set

                VendorGroupName=@VGroupname,GroupType = @VGroupType,VendorGroupDescription = @VendorGroupDescription,Comments = @VGroupComments,ActiveStatus = @VGroupStatus

                where VendorGroupID=@Id
                ";


                var command = CreateCommand(sql);


                command.Parameters.Add("@Id", SqlDbType.Int).Value = model.VendorGroupID;
                command.Parameters.Add("@VGroupname", SqlDbType.NVarChar).Value = model.VendorGroupName;
                command.Parameters.Add("@VGroupType", SqlDbType.NVarChar).Value = model.GroupType;
                command.Parameters.Add("@VGroupDesc", SqlDbType.NVarChar).Value = model.VendorGroupDescription;
                command.Parameters.Add("@VGroupComments", SqlDbType.NVarChar).Value = model.Comments;
                command.Parameters.Add("@VGroupStatus", SqlDbType.NVarChar).Value = model.ActiveStatus;





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
