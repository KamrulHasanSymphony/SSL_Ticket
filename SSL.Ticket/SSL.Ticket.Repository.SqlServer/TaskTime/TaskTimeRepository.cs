using Microsoft.Data.SqlClient;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.TaskTime;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using System.Data;

namespace SSL.Ticket.SSL.Ticket.Repository.SqlServer.TaskTime
{
    public class TaskTimeRepository : Repository, ITaskTimeRepository
    {
        private CS.SSL.Common.Models.DbConfig _dbConfig;
        public TaskTimeRepository(SqlConnection context, SqlTransaction transaction, CS.SSL.Common.Models.DbConfig dbConfig)
        {
            this._context = context;
            this._transaction = transaction;

            this._dbConfig = dbConfig;
        }

        public List<T_TaskTimesVM> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public List<T_TaskTimesVM> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public T_TaskTimesVM Insert(T_TaskTimesVM model)
        {
            string sqlText = "";
            int Id = 0;
            int transResult = 0;
            int countId;
            string SlNo;


            try
            {
                sqlText = "";
                sqlText = @" INSERT INTO T_TaskTimes(
                                     T_TaskId
                                    ,StartTime
                                    ,StartedBy
                                    ,StartStatus
                                    ,AssigneeUserId
                                    ,T_TicketId
                                    ) 

                                    VALUES (
                                     @T_TaskId
                                    ,@StartTime
                                    ,@StartedBy
                                    ,@StartStatus
                                    ,@AssigneeUserId
                                    ,@T_TicketId
                                    ) 
                                Update T_Tasks 
                                    SET WorkingStatus = @StartStatus 
                                    Where T_Tasks.Id =  @T_TaskId;
                                SELECT  SCOPE_IDENTITY()";

                var cmdInsert = CreateCommand(sqlText);

                cmdInsert.Parameters.AddWithValue("@T_TaskId", model.T_TaskId);
                cmdInsert.Parameters.AddWithValue("@StartTime", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@StartedBy", model.AssigneeUserId);
                cmdInsert.Parameters.AddWithValue("@StartStatus", model.StartStatus);
                cmdInsert.Parameters.AddWithValue("@AssigneeUserId", model.AssigneeUserId);
                cmdInsert.Parameters.AddWithValue("@T_TicketId", model.T_TicketId);


                model.Id = Convert.ToInt32(cmdInsert.ExecuteScalar());
                return model;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public T_TaskTimesVM InsertActive(T_TaskTimesVM model)
        {
            throw new NotImplementedException();
        }

        public T_TaskTimesVM Update(T_TaskTimesVM model)
        {
            try
            {
                string sqlText = "";
                int count = 0;

                string query = @"  update T_TaskTimes set 

                 StopTime           = @StopTime  
                ,StartStatus       =@StartStatus   
                       
                where  Id= @Id 

         update T_TaskTimes set duration = DATEDIFF(MINUTE, StartTime, StopTime) where id = @Id ;
         Update T_Tasks SET WorkingStatus = @StartStatus Where T_Tasks.Id =  @T_TaskId;";



                SqlCommand command = CreateCommand(query);
                command.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
                command.Parameters.Add("@T_TaskId", SqlDbType.Int).Value = model.T_TaskId;
                command.Parameters.Add("@StopTime", SqlDbType.DateTime).Value = DateTime.Now;
                command.Parameters.Add("@StartStatus", SqlDbType.NChar).Value = model.StartStatus;              


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
    }
}
