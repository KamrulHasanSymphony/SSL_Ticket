using Microsoft.Data.SqlClient;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Assignee;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Collaboration;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Repository.SqlServer.Assignee
{
    public class AssigneeRepository : Repository, IAssigneeRepository
    {
        private CS.SSL.Common.Models.DbConfig _dbConfig;
        public AssigneeRepository(SqlConnection context, SqlTransaction transaction, CS.SSL.Common.Models.DbConfig dbConfig)
        {
            this._context = context;
            this._transaction = transaction;

            this._dbConfig = dbConfig;
        }

        public List<T_TaskAssignesVM> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public List<T_TaskAssignesVM> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public T_TaskAssignesVM Insert(T_TaskAssignesVM model)
        {
            string sqlText = "";
            int Id = 0;
            int transResult = 0;
            int countId;
            string SlNo;


            try
            {
                sqlText = "";
                sqlText = @" INSERT INTO T_TaskAssignes(
                                     T_TaskId
                                    ,T_TicketId
                                    ,AssigneeUserId
                                    ,CreatedBy
                                    ,CreatedOn
                                    ,IsActive
                                    ) 

                                    VALUES (
                                     @T_TaskId
                                    ,@T_TicketId
                                    ,@AssigneeUserId
                                    ,@CreatedBy
                                    ,@CreatedOn
                                    ,@IsActive                                   
                                    ) 
                                    SELECT  SCOPE_IDENTITY()";


                //SqlCommand cmdInsert = new SqlCommand(sqlText, currConn, transaction);
                var cmdInsert = CreateCommand(sqlText);

                cmdInsert.Parameters.AddWithValue("@T_TaskId", model.T_TaskId);
                cmdInsert.Parameters.AddWithValue("@T_TicketId", model.T_TicketId);
                cmdInsert.Parameters.AddWithValue("@AssigneeUserId", model.AssigneeUserId);
                cmdInsert.Parameters.AddWithValue("@CreatedBy", 1);
                cmdInsert.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@IsActive", true);

                model.Id = Convert.ToInt32(cmdInsert.ExecuteScalar());
                return model;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public T_TaskAssignesVM InsertActive(T_TaskAssignesVM model)
        {
            try
            {
                string sqlText = "";
                int count = 0;
                sqlText = "";
                sqlText = @" Update T_TaskAssignes set
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

        public T_TaskAssignesVM Update(T_TaskAssignesVM model)
        {
            throw new NotImplementedException();
        }
    }
}
