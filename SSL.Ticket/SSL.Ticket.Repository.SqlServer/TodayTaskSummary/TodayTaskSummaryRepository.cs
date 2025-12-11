using Microsoft.Data.SqlClient;
using SSL.Common.SSL.Common.Core.ExtentionMethod;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Product;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System.Data;

namespace SSL.Ticket.SSL.Ticket.Repository.SqlServer.TodayTaskSummary
{
    public class TodayTaskSummaryRepository : Repository, ITodayTaskSummaryRepository
    {
        private CS.SSL.Common.Models.DbConfig _dbConfig;
        public TodayTaskSummaryRepository(SqlConnection context, SqlTransaction transaction, CS.SSL.Common.Models.DbConfig dbConfig)
        {
            this._context = context;
            this._transaction = transaction;

            this._dbConfig = dbConfig;
        }

        public List<TodayTaskSummaryVM> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            try
            {
                string sqlText = $@" 
                Select
                Id,Details,Date,IsPost,CreatedBy,CreatedOn,CreatedFrom,PostedBy,PostedOn,LastUpdatedBy,LastUpdatedOn,PostedFrom,LastUpdatedFrom
                from TodayTaskSummary
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

                TodayTaskSummaryVM vmdata = new TodayTaskSummaryVM();



                List<TodayTaskSummaryVM> vms = dtResutl.ToList<TodayTaskSummaryVM>();


                return vms;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public List<TodayTaskSummaryVM> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public TodayTaskSummaryVM Insert(TodayTaskSummaryVM model)
        {
            string sqlText = "";
            int Id = 0;
            int transResult = 0;
            int countId;
            string SlNo;


            try
            {
                sqlText = "";
                sqlText = @" INSERT INTO TodayTaskSummary(                                     
                                    Details
                                    ,Date
                                    ,IsPost
                                    ,CreatedOn
                                    ,CreatedBy
                                    ,CreatedFrom
                                    ) 

                                    VALUES (                                     
                                     @Details
                                    ,@Date
                                    ,@IsPost
                                    ,@CreatedOn   
                                    ,@CreatedBy
                                    ,@CreatedFrom
                                    ) SELECT SCOPE_IDENTITY();";


                var cmdInsert = CreateCommand(sqlText);

                cmdInsert.Parameters.AddWithValue("@Details", model.Details);
                cmdInsert.Parameters.AddWithValue("@Date", model.Date);
                cmdInsert.Parameters.AddWithValue("@IsPost", model.IsPost ?? false);
                cmdInsert.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                cmdInsert.Parameters.AddWithValue("@CreatedFrom", model.CreatedFrom ?? "Admin");


                model.Id = Convert.ToInt32(cmdInsert.ExecuteScalar());
                return model;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public TodayTaskSummaryVM InsertActive(TodayTaskSummaryVM model)
        {
            throw new NotImplementedException();
        }

        public TodayTaskSummaryVM MultiplePost(TodayTaskSummaryVM model)
        {
            if (model.IDs == null || model.IDs.Count == 0)
                throw new ArgumentException("No IDs provided to update.");

            try
            {
                string idsParameterList = string.Join(",", model.IDs.Select((id, index) => "@Id" + index));

                string query = $@"
                                UPDATE TodayTaskSummary
                                SET IsPost = @IsPost,
                                    PostedBy = @PostedBy,
                                    PostedOn = @PostedOn
                                WHERE Id IN ({idsParameterList})
                            ";

                using (SqlCommand command = CreateCommand(query))
                {
                    // Add parameters for IsPost, PostedBy, PostedOn
                    command.Parameters.Add("@IsPost", SqlDbType.Bit).Value = true;
                    command.Parameters.Add("@PostedBy", SqlDbType.NVarChar, 150).Value = model.PostedBy ?? string.Empty;
                    command.Parameters.Add("@PostedOn", SqlDbType.DateTime).Value = model.PostedOn ?? DateTime.Now;

                    // Add parameters for each Id in the list
                    for (int i = 0; i < model.IDs.Count; i++)
                    {
                        command.Parameters.Add("@Id" + i, SqlDbType.Int).Value = int.Parse(model.IDs[i]);
                    }

                    int rowcount = command.ExecuteNonQuery();

                    if (rowcount <= 0)
                    {
                        throw new Exception("Update failed for the selected IDs.");
                    }

                    return model;
                }
            }
            catch (Exception ex)
            {
                // You can log ex.Message if needed
                throw;
            }

        }


        public TodayTaskSummaryVM Update(TodayTaskSummaryVM model)
        {
            try
            {
                string sqlText = "";
                int count = 0;

                string query = @"  update TodayTaskSummary set 
 
                
                 Details          =@Details  
                ,Date      = @Date                
                ,IsPost   = @IsPost
                ,LastUpdatedBy = @LastUpdatedBy
                ,LastUpdatedOn = @LastUpdatedOn
                ,LastUpdatedFrom = @LastUpdatedFrom

                       
                where  Id= @Id ";


                SqlCommand command = CreateCommand(query);
                command.Parameters.Add("@Details", SqlDbType.NChar).Value = model.Details;
                command.Parameters.Add("@Date", SqlDbType.DateTime).Value = model.Date;
                command.Parameters.Add("@IsPost", SqlDbType.NChar).Value = model.IsPost ?? false;
                command.Parameters.Add("@LastUpdatedBy", SqlDbType.NChar).Value = model.LastUpdatedBy;
                command.Parameters.Add("@LastUpdatedOn", SqlDbType.NChar).Value = DateTime.Now;
                command.Parameters.Add("@LastUpdatedFrom", SqlDbType.NChar).Value = model.LastUpdatedFrom;

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
