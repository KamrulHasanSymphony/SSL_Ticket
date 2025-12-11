using Microsoft.Data.SqlClient;
using SSL.CS.SSL.Common.Models;
using SSL.Sample.SSL.Sample.Core.Interfaces.Repository.Purchase;
using SSL.Sample.SSL.Sample.Models;
using System.Data;
using DbConfig = SSL.Sample.SSL.Sample.Models.DbConfig;

namespace SSL.Sample.SSL.Sample.Repository.SqlServer.CmnDocuments
{
    public class CmnDocumentRepository : Repository, ICmnDocumentRepository
    {
        private DbConfig _dbConfig;

        public CmnDocumentRepository(SqlConnection context, SqlTransaction transaction, DbConfig dbConfig)
        {
            this._context = context;
            this._transaction = transaction;

            this._dbConfig = dbConfig;
        }

        public List<CmnDocument> GetAll(string[] conditionalFields, string[] conditionalValue, CS.SSL.Common.Models.PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public List<CmnDocument> GetIndexData(CS.SSL.Common.Models.IndexModel index, string[] conditionalFields, string[] conditionalValue, CS.SSL.Common.Models.PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(CS.SSL.Common.Models.IndexModel index, string[] conditionalFields, string[] conditionalValue, CS.SSL.Common.Models.PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public CmnDocument Insert(CmnDocument model)
        {
            try
            {
                string sqlText = "";

                sqlText = @"

                            INSERT INTO [dbo].[ACmnDocument]
                                       ( FileName
                                        ,FileExtension
                                        ,FileSize
                                        ,FileUniqueName
                                        ,ModuleMasterId
                                        ,ModuleName
                                        ,ActionType                                        
                                        )
                                 VALUES
                                       (@FileName
                                        ,@FileExtension
                                        ,@FileSize
                                        ,@FileUniqueName
                                        ,@ModuleMasterId
                                        ,@ModuleName
                                        ,@ActionType)  

                            SELECT SCOPE_IDENTITY() ";


                var command = CreateCommand(sqlText);

                command.Parameters.Add("@FileName", SqlDbType.NVarChar).Value = model.FileName;
                command.Parameters.Add("@FileExtension", SqlDbType.NVarChar).Value = model.FileExtension;
                command.Parameters.Add("@FileSize", SqlDbType.NVarChar).Value = model.FileSize;
                command.Parameters.Add("@FileUniqueName", SqlDbType.NVarChar).Value = model.FileUniqueName;
                command.Parameters.Add("@ModuleMasterId", SqlDbType.Int).Value = model.ModuleMasterId;
                command.Parameters.Add("@ModuleName", SqlDbType.NVarChar).Value = model.ModuleName;
                command.Parameters.Add("@ActionType", SqlDbType.NVarChar).Value = model.ActionType;

                model.DocumentId = Convert.ToInt32(command.ExecuteScalar());

                return model;

            }


            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public CmnDocument Update(CmnDocument model)
        {
            try
            {
                string sqlText = "";

                sqlText = @"

                            Update [dbo].[ACmnDocument]
                                       set FileName = @FileName
                                        ,FileExtension = @FileExtension
                                        ,FileSize = @FileSize
                                        ,FileUniqueName = @FileUniqueName
                                        ,ModuleMasterId = @ModuleMasterId
                                        ,ModuleName = @ModuleName
                                        ,ActionType = @ActionType                                       
                                        Where DocumentId = @DocumentId ";


                var command = CreateCommand(sqlText);

                command.Parameters.Add("@FileName", SqlDbType.NVarChar).Value = model.FileName;
                command.Parameters.Add("@FileExtension", SqlDbType.NVarChar).Value = model.FileExtension;
                command.Parameters.Add("@FileSize", SqlDbType.NVarChar).Value = model.FileSize;
                command.Parameters.Add("@FileUniqueName", SqlDbType.NVarChar).Value = model.FileUniqueName;
                command.Parameters.Add("@ModuleMasterId", SqlDbType.Int).Value = model.ModuleMasterId;
                command.Parameters.Add("@ModuleName", SqlDbType.NVarChar).Value = model.ModuleName;
                command.Parameters.Add("@ActionType", SqlDbType.NVarChar).Value = model.ActionType;

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
