using Microsoft.Data.SqlClient;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Collaboration;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.EnternalNote;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Repository.SqlServer.Collaboration
{
    public class CollaborationRepository : Repository, ICollaborationRepository
    {
        private CS.SSL.Common.Models.DbConfig _dbConfig;
        public CollaborationRepository(SqlConnection context, SqlTransaction transaction, CS.SSL.Common.Models.DbConfig dbConfig)
        {
            this._context = context;
            this._transaction = transaction;

            this._dbConfig = dbConfig;
        }

        public List<T_TaskCollaborationsVM> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public List<T_TaskCollaborationsVM> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public T_TaskCollaborationsVM Insert(T_TaskCollaborationsVM model)
        {
            string sqlText = "";
            int Id = 0;
            int transResult = 0;
            int countId;
            string SlNo;


            try
            {
                sqlText = "";
                sqlText = @" INSERT INTO T_TaskCollaborations(
                                     T_TaskId
                                    ,ShortNote
                                    ,Description
                                    ,UserId
                                    ,CreatedAt
                                    ,IsActive
                                    ) 

                                    VALUES (
                                     @T_TaskId
                                    ,@ShortNote
                                    ,@Description
                                    ,@AssigneeUserId
                                    ,@CreatedAt
                                    ,@IsActive                                    
                                    ) 
                                    SELECT  SCOPE_IDENTITY()";


                //SqlCommand cmdInsert = new SqlCommand(sqlText, currConn, transaction);
                var cmdInsert = CreateCommand(sqlText);

                cmdInsert.Parameters.AddWithValue("@T_TaskId", model.T_TaskId);
                cmdInsert.Parameters.AddWithValue("@ShortNote", model.ShortNote);
                cmdInsert.Parameters.AddWithValue("@Description", model.Description ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@AssigneeUserId", model.UserId);
                cmdInsert.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@IsActive", model.IsActive = true);

                model.Id = Convert.ToInt32(cmdInsert.ExecuteScalar());
                return model;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public T_TaskCollaborationsVM InsertActive(T_TaskCollaborationsVM model)
        {
            try
            {
                string sqlText = "";
                int count = 0;
                sqlText = "";
                sqlText = @" Update T_TaskCollaborations set
                                    IsActive = @IsActive
                                    Where Id = @Id "
                ;


                SqlCommand command = CreateCommand(sqlText);
                command.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = false;


                int rowcount = command.ExecuteNonQuery();

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

        public T_TaskCollaborationsVM Update(T_TaskCollaborationsVM model)
        {
            throw new NotImplementedException();
        }
    }
}
