using Microsoft.Data.SqlClient;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.ExtentionMethod;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Ticket;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Topics;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Repository.SqlServer.Topics
{
    public class TopicsRepository : Repository, ITopicsRepository
    {
        private CS.SSL.Common.Models.DbConfig _dbConfig;
        public TopicsRepository(SqlConnection context, SqlTransaction transaction, CS.SSL.Common.Models.DbConfig dbConfig)
        {
            this._context = context;
            this._transaction = transaction;

            this._dbConfig = dbConfig;
        }

        public List<T_TopicsVM> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            try
            {
                string sqlText = $@" 
                Select Id,T_ProductId,Name,IsActive 
                       from
                     T_Topics
                Where 1=1";
                //sqlText += @" Where T.Id= @Id";
                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue);


                //sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue);


                SqlCommand objComm = CreateCommand(sqlText);

                objComm = ApplyParameters(objComm, conditionalFields, conditionalValue);

                SqlDataAdapter adapter = new SqlDataAdapter(objComm);
                DataTable dtResutl = new DataTable();
                adapter.Fill(dtResutl);

                string Remarks = null;

                T_TopicsVM vmdata = new T_TopicsVM();



                List<T_TopicsVM> vms = dtResutl.ToList<T_TopicsVM>();


                return vms;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public List<T_TopicsVM> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public T_TopicsVM Insert(T_TopicsVM model)
        {
            string sqlText = "";
            int Id = 0;
            int transResult = 0;
            int countId;
            string SlNo;


            try
            {
                sqlText = "";
                sqlText = @" INSERT INTO T_Topics(
                                     T_ProductId
                                    ,Name
                                    ,CreateOn
                                    ,CreateBy                                    
                                    ,IsActive
                                    
                                    ) 

                                    VALUES (
                                     @T_ProductId
                                    ,@Name
                                    ,@CreateOn
                                    ,@CreateBy                                    
                                    ,@IsActive
                                    ) SELECT SCOPE_IDENTITY();";


                var cmdInsert = CreateCommand(sqlText);

                cmdInsert.Parameters.AddWithValue("@T_ProductId", model.T_ProductId);
                cmdInsert.Parameters.AddWithValue("@Name", model.Name);
                cmdInsert.Parameters.AddWithValue("@CreateOn", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@CreateBy", model.CreateBy ?? Convert.DBNull);                
                cmdInsert.Parameters.AddWithValue("@IsActive", model.IsActive);

                model.Id = Convert.ToInt32(cmdInsert.ExecuteScalar());
                return model;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public T_TopicsVM InsertActive(T_TopicsVM model)
        {
            throw new NotImplementedException();
        }

        public T_TopicsVM Update(T_TopicsVM model)
        {
            try
            {
                string sqlText = "";
                int count = 0;

                string query = @"  update T_Topics set 

 
                T_ProductId    =@T_ProductId
                ,Name          =@Name  
                ,UpdateOn      =@UpdateOn  
                ,UpdateBy      =@UpdateBy 
                ,IsActive      =@IsActive

                       
                where  Id= @Id ";


                SqlCommand command = CreateCommand(query);
                command.Parameters.Add("T_ProductId", SqlDbType.Int).Value = model.T_ProductId;
                command.Parameters.Add("@Name", SqlDbType.NChar).Value = model.Name;
                command.Parameters.Add("@UpdateOn", SqlDbType.DateTime).Value = DateTime.Now;
                command.Parameters.Add("@UpdateBy", SqlDbType.NChar).Value = model.UpdateBy;                
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;

                // Add parameter for WHERE clause
                command.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;

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
