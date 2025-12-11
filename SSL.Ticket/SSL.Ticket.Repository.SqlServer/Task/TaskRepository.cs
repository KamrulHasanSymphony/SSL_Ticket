using Microsoft.Data.SqlClient;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.ExtentionMethod;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Task;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Ticket;
using SSL.Ticket.SSL.Ticket.Models;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Repository.SqlServer.Task
{
    public class TaskRepository : Repository, ITaskRepository
    {
        private CS.SSL.Common.Models.DbConfig _dbConfig;
        public TaskRepository(SqlConnection context, SqlTransaction transaction, CS.SSL.Common.Models.DbConfig dbConfig)
        {
            this._context = context;
            this._transaction = transaction;

            this._dbConfig = dbConfig;
        }

        public int DeleteAttachments(string tableName, string[] conditionalFields, string[] conditionalValue)
        {
            throw new NotImplementedException();
        }

        public string GenerateCode(string CodeGroup, string CodeName, int branchId = 1)
        {
            try
            {
                // ToDo sql injection
                string sqlText = "";

                string NewCode = "";
                string CodePreFix = "";
                string CodeGenerationFormat = "B/N/Y";
                string CodeGenerationMonthYearFormat = "MMYY";
                string BranchCode = "001";
                string CurrentYear = "2020";
                string BranchNumber = "1";
                int CodeLength = 0;
                int nextNumber = 0;

                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataSet ds = new DataSet();

                DateTime TransactionDate = DateTime.Now;
                string year = Convert.ToDateTime(DateTime.Now).ToString("yyyy");

                int BranchId = branchId;


                sqlText += " SELECT   top 1  SettingName FROM Settings";
                sqlText += " WHERE     (SettingGroup ='" + CodeGenerationFormat + "') and   (SettingValue ='Y')  ";

                sqlText += " SELECT   top 1  SettingName FROM Settings";
                sqlText += " WHERE     (SettingGroup ='" + CodeGenerationFormat + "') and   (SettingValue ='Y')  ";

                sqlText += " SELECT   top 1  BranchCode FROM BranchProfiles";
                sqlText += " WHERE     (BranchID ='" + BranchId + "')   ";

                sqlText += " SELECT   count(BranchCode) BranchNumber FROM BranchProfiles where IsArchive='0' and ActiveStatus='1'";

                sqlText += "  SELECT   * from  CodeGenerations where CurrentYear<='2020' ";

                sqlText += "  select YEAR from FiscalYears where '" + Convert.ToDateTime(TransactionDate).ToString("dd/MMM/yyyy") + "' between YearStart and YearEnd ";

                SqlCommand command = CreateCommand(sqlText);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(ds);



                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    CodeGenerationFormat = ds.Tables[0].Rows[0][0].ToString();

                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    CodeGenerationMonthYearFormat = ds.Tables[1].Rows[0][0].ToString();
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    BranchCode = ds.Tables[2].Rows[0][0].ToString();

                if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                    BranchNumber = ds.Tables[3].Rows[0][0].ToString();


                sqlText = "  ";
                sqlText += "  update CodeGenerations set CurrentYear ='2020'  where CurrentYear <='2020'";

                command = CreateCommand(sqlText);
                command.ExecuteNonQuery();

                if (ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
                    CurrentYear = ds.Tables[5].Rows[0][0].ToString();

                sqlText = "  ";

                sqlText += " SELECT     * FROM Codes";
                sqlText += " WHERE     (CodeGroup =@CodeGroup) AND (CodeName = @CodeName)";

                command.CommandText = sqlText;


                command.Parameters.AddWithValue("@CodeGroup", CodeGroup);
                command.Parameters.AddWithValue("@CodeName", CodeName);

                dataAdapter = new SqlDataAdapter(command);


                dataAdapter.Fill(dt1);
                if (dt1 == null || dt1.Rows.Count <= 0)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    CodePreFix = dt1.Rows[0]["prefix"].ToString();
                    CodeLength = Convert.ToInt32(dt1.Rows[0]["Lenth"]);
                }

                sqlText = "  ";
                sqlText += @" 
SELECT top 1 
Id
,CurrentYear
,BranchId
,Prefix
,ISNULL(LastId,0) LastId
FROM CodeGenerations 
WHERE CurrentYear=@CurrentYear AND BranchId=@BranchId AND Prefix=@Prefix order by LastId Desc
";


                command.CommandText = sqlText;


                command.Parameters.AddWithValue("@BranchId", BranchId);
                command.Parameters.AddWithValue("@CurrentYear", CurrentYear);
                command.Parameters.AddWithValue("@Prefix", CodePreFix);


                dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dt2);


                if (dt2 == null || dt2.Rows.Count <= 0)
                {
                    sqlText = "  ";
                    sqlText +=
                        " INSERT INTO CodeGenerations(	CurrentYear,BranchId,Prefix,LastId)";
                    sqlText += " VALUES(";
                    sqlText += " @CurrentYear,";
                    sqlText += " @BranchId,";
                    sqlText += " @Prefix,";
                    sqlText += " 1";
                    sqlText += " )";

                    command.CommandText = sqlText;


                    // command.Parameters.AddWithValue("@CurrentYear", CurrentYear);
                    //  command.Parameters.AddWithValue("@BranchId", BranchId);
                    //  command.Parameters.AddWithValue("@Prefix", CodePreFix);

                    object objfoundId1 = command.ExecuteNonQuery();

                    nextNumber = 1;
                }
                else
                {
                    if (nextNumber != 1)
                    {
                        nextNumber = dt2.Rows[0]["LastId"] == null ? 1 : Convert.ToInt32(dt2.Rows[0]["LastId"]) + 1;
                    }

                    sqlText = "  ";
                    sqlText += " update  CodeGenerations set LastId='" + nextNumber + "'";
                    sqlText += " WHERE CurrentYear=@CurrentYear AND BranchId=@BranchId AND Prefix=@Prefix";


                    command.CommandText = sqlText;

                    // command.Parameters.AddWithValue("@Prefix", CodePreFix);
                    command.ExecuteNonQuery();

                }

                //  NewCode = CodeGenerationMonthYearFormat + "~" + BranchNumber + "~" + CodeGenerationFormat + "~" + BranchCode + "~" + CodeLength + "~" + nextNumber + "~" + CodePreFix + "~" + TransactionDate.ToString("yyyymmdd") + "~" + CurrentYear;
                // DateTime TransactionDate= Convert.ToDateTime(DateTime.Now).ToString("dd/MMM/yyyy");

                NewCode = CodeGeneration1(CodeGenerationMonthYearFormat, BranchNumber, CodeGenerationFormat, BranchCode, CodeLength, nextNumber, CodePreFix, TransactionDate.ToString());
                return NewCode;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int GetLastID()
        {
            try
            {
                string sqlText = "";
                int nextNumber = 0;
                DataTable dt1 = new DataTable();
                int NewCode = 0;
                DateTime TransactionDate = DateTime.Now;
                string year = TransactionDate.ToString("yyyy");
                SqlCommand command;
                SqlDataAdapter dataAdapter;


                sqlText = "UPDATE CodeGenerations SET CurrentYear = @CurrentYear WHERE CurrentYear <= @CurrentYear";
                command = CreateCommand(sqlText);
                command.Parameters.AddWithValue("@CurrentYear", year);
                command.ExecuteNonQuery();


                sqlText = "SELECT TOP (1) Id  FROM T_Tasks order by Id desc";
                command = CreateCommand(sqlText);
                dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dt1);


                if (dt1.Rows.Count > 0)
                {
                    int lastId = Convert.ToInt32(dt1.Rows[0]["Id"]);
                    nextNumber = lastId + 1;

                }


                return nextNumber;
            }
            catch (Exception e)
            {
                throw new Exception("Error while retrieving last ID: " + e.Message);
            }
        }

        public List<T_TasksVM> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            try
            {
                string sqlText = $@" 
				     Select
                     ts.Id,
                     ts.CompanyId,
                     ts.Code,
                     ts.Title,
                     ts.Description,
                     ts.T_TicketId,
                     ts.T_SourceId,
                     ts.T_TopicId,
                     ts.T_PriorityId,
                     ts.T_StatusId,
                     ts.ProgressInPercent,
                     ts.StartDate,
                     ts.StartTime,
                     ts.RequiredTime,tk.Code TicketCode 
                    ,tk.Title TicketTitle
                    ,ts.DepartmentId
                    ,ts.TaskTypeId
                FROM T_Tasks ts
                left outer join T_Tickets tk on ts.T_TicketId=tk.id
                WHERE 1=1";
                //sqlText += @" Where T.Id= @Id";
                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue);

                


                SqlCommand objComm = CreateCommand(sqlText);

                objComm = ApplyParameters(objComm, conditionalFields, conditionalValue);

                SqlDataAdapter adapter = new SqlDataAdapter(objComm);
                DataTable dtResutl = new DataTable();
                adapter.Fill(dtResutl);

                string Remarks = null;

                T_TasksVM vmdata = new T_TasksVM();



                List<T_TasksVM> vms = dtResutl.ToList<T_TasksVM>();


                return vms;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public List<AuditIssueAttachments> GetAllAttachments(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {

            string sqlText = "";
            List<AuditIssueAttachments> VMs = new List<AuditIssueAttachments>();
            DataTable dt = new DataTable();

            try
            {
                sqlText = @"

                    SELECT  [Id]
                          ,[T_TaskId]
                          ,[FileName]
                      FROM T_TaskFiles  

                    where 1=1  

                    ";
                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue);

                SqlDataAdapter objComm = CreateAdapter(sqlText);

                objComm.SelectCommand = ApplyParameters(objComm.SelectCommand, conditionalFields, conditionalValue);

                objComm.Fill(dt);

                VMs = dt.ToList<AuditIssueAttachments>();

                return VMs;


            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public List<T_TasksVM> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public T_TasksVM Insert(T_TasksVM model)
        {
            string sqlText = "";
            int Id = 0;
            int transResult = 0;
            int countId;
            string SlNo;


            try
            {
                sqlText = "";
                sqlText = @" INSERT INTO T_Tasks(
                                     CompanyId
                                    ,Code
                                    ,Title
                                    ,Description
                                    ,T_TicketId
                                    ,T_SourceId
                                    ,T_TopicId
                                    ,T_PriorityId
                                    ,T_StatusId
                                    ,ProgressInPercent
                                    ,StartDate
                                    ,StartTime
                                    ,RequiredTime
                                    ,CreatedOn
                                    ,CreatedBy
                                    ,IsComplete
                                    ,DepartmentId
                                    ,TaskTypeId
                                    ) 

                                    VALUES (
                                     @CompanyId
                                    ,@Code
                                    ,@Title
                                    ,@Description
                                    ,@T_TicketId
                                    ,@T_SourceId
                                    ,@T_TopicId
                                    ,@T_PriorityId
                                    ,@T_StatusId
                                    ,@ProgressInPercent
                                    ,@StartDate
                                    ,@StartTime
                                    ,@RequiredTime
                                    ,@CreatedOn
                                    ,@CreatedBy
                                    ,@IsComplete
                                    ,@DepartmentId
                                    ,@TaskTypeId
                                    ) 
                                    SELECT  SCOPE_IDENTITY()";


                //SqlCommand cmdInsert = new SqlCommand(sqlText, currConn, transaction);
                var cmdInsert = CreateCommand(sqlText);

                cmdInsert.Parameters.AddWithValue("@CompanyId", 1);
                cmdInsert.Parameters.AddWithValue("@Code", model.Code);
                cmdInsert.Parameters.AddWithValue("@Title", model.Title ?? Convert.DBNull);
                cmdInsert.Parameters.Add("@Description", SqlDbType.NVarChar).Value =
                string.IsNullOrEmpty(model.Description) ? "-" : model.Description;
                cmdInsert.Parameters.AddWithValue("@T_TicketId", model.T_TicketId ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@T_SourceId", model.T_SourceId);
                cmdInsert.Parameters.AddWithValue("@T_TopicId", model.T_TopicId ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@T_PriorityId", model.T_PriorityId);
                cmdInsert.Parameters.AddWithValue("@T_StatusId", model.T_StatusId);
                cmdInsert.Parameters.AddWithValue("@ProgressInPercent", 0);
                cmdInsert.Parameters.AddWithValue("@StartDate", model.StartDate);
                cmdInsert.Parameters.AddWithValue("@StartTime", model.StartTime);
                cmdInsert.Parameters.AddWithValue("@RequiredTime", model.RequiredTime);
                cmdInsert.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@CreatedBy", model.CreatedBy ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@IsComplete", model.IsComplete = false);
                cmdInsert.Parameters.AddWithValue("@DepartmentId", model.DepartmentId ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@TaskTypeId", model.TaskTypeId ?? Convert.DBNull);

                model.Id = Convert.ToInt32(cmdInsert.ExecuteScalar());
                return model;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public T_TasksVM InsertActive(T_TasksVM model)
        {
            throw new NotImplementedException();
        }

        public AuditIssueAttachments InsertAttachments(AuditIssueAttachments model)
        {
            try
            {
                string sqlText = "";
                int Id = 0;


                sqlText = @"
                    insert into T_TaskFiles(
                    [T_TaskId]
                    ,[FileName]

                    )
                    values( 
                     @T_TaskId
                    ,@FileName

  
                    )     SELECT SCOPE_IDENTITY() ";

                var command = CreateCommand(sqlText);


                command.Parameters.Add("@T_TaskId", SqlDbType.Int).Value = model.T_TaskId;
                command.Parameters.Add("@FileName", SqlDbType.NVarChar).Value = model.FileName;

                model.Id = Convert.ToInt32(command.ExecuteScalar());

                return model;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public AuditIssueAttachments UpdateAttachments(AuditIssueAttachments model)
        {
            try
            {
                string sqlText = "";
                //int Id = 0;


                sqlText = @"UPDATE T_TaskFiles
                SET
                    [T_TaskId] = @T_TaskId,
                    [FileName] = @FileName
                WHERE
                    [T_TaskId] = @T_TaskId;";

                var command = CreateCommand(sqlText);


                command.Parameters.Add("@T_TaskId", SqlDbType.Int).Value = model.T_TaskId;         
                command.Parameters.Add("@FileName", SqlDbType.NVarChar).Value = model.FileName;

                model.Id = Convert.ToInt32(command.ExecuteScalar());

                return model;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string NewGenerateCode(int ID)
        {
            try
            {

                int lastID = GetLastID();
                string CodePreFix = "TSK-"; // + nextNumber.ToString("D6");
                string NewCode = CodeGeneration(CodePreFix, lastID);
                return NewCode;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string CodeGeneration(string codePrefix, int lastID)
        {
            // Increment the lastID to generate the next code
            int nextNumber = lastID + 1;

            // Format the number to always have 6 digits (padded with leading zeros)
            string formattedNumber = nextNumber.ToString("D6");

            // Concatenate the prefix and the formatted number
            string newCode = codePrefix + formattedNumber;

            return newCode;
        }


        public T_TasksVM Update(T_TasksVM model)
        {

            try
            {
                string query = @"UPDATE T_Tasks SET 
                     CompanyId = @Company, 
                     Title = @Title, 
                     Description = @Description, 
                     T_TicketId = @T_TicketId, 
                     T_SourceId = @T_SourceId, 
                     T_TopicId = @T_TopicId, 
                     T_PriorityId = @T_PriorityId, 
                     T_StatusId = @T_StatusId, 
                     ProgressInPercent = @ProgressInPercent, 
                     StartDate = @StartDate, 
                     StartTime = @StartTime, 
                     RequiredTime = @RequiredTime, 
                     UpdateOn = @UpdatedOn, 
                     UpdateBy = @UpdatedBy, 
                     DepartmentId = @DepartmentId, 
                     TaskTypeId = @TaskTypeId
                     WHERE Id = @Id";

                SqlCommand command = CreateCommand(query);

                // Add parameters
                command.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
                command.Parameters.Add("@Company", SqlDbType.Int).Value = 1;
                command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(model.Title) ? model.Description : model.Title;
                command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = string.IsNullOrEmpty(model.Description) ? "-" : model.Description;
                command.Parameters.Add("@T_TicketId", SqlDbType.Int).Value = model.T_TicketId;
                command.Parameters.Add("@T_SourceId", SqlDbType.Int).Value = model.T_SourceId;
                command.Parameters.Add("@T_TopicId", SqlDbType.Int).Value = model.T_TopicId;
                command.Parameters.Add("@T_PriorityId", SqlDbType.Int).Value = model.T_PriorityId;
                command.Parameters.Add("@T_StatusId", SqlDbType.Int).Value = model.T_StatusId;
                command.Parameters.Add("@ProgressInPercent", SqlDbType.Int).Value = model.ProgressInPercent;
                command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = model.StartDate == DateTime.MinValue ? (object)DBNull.Value : model.StartDate;
                command.Parameters.Add("@StartTime", SqlDbType.VarChar).Value = model.StartTime;
                command.Parameters.Add("@RequiredTime", SqlDbType.Int).Value = model.RequiredTime;
                command.Parameters.Add("@UpdatedOn", SqlDbType.DateTime).Value = model.UpdateOn;
                command.Parameters.Add("@UpdatedBy", SqlDbType.VarChar).Value = model.UpdateBy;
                command.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = model.DepartmentId;
                command.Parameters.Add("@TaskTypeId", SqlDbType.Int).Value = model.TaskTypeId;

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

		public List<AuditIssueAttachments> GetAttachmentsById(int id)
		{
			try
			{
				string sqlText = @" 
                    SELECT [Id], [T_TaskId], [FileName]
                    FROM T_TaskFiles  
                    WHERE T_TaskId = @T_TaskId";  // Use parameterized query to avoid SQL injection

				// Create command object
				SqlCommand objComm = CreateCommand(sqlText);

				// Assuming 'id' is a variable you have somewhere in your code
				objComm.Parameters.AddWithValue("@T_TaskId", id);  // Add the parameter to the command

				// Execute the command and fill the results into a DataTable
				SqlDataAdapter adapter = new SqlDataAdapter(objComm);
				DataTable dtResult = new DataTable();
				adapter.Fill(dtResult);

				// If you want to map the DataTable to a list of objects, you can do something like this
				List<AuditIssueAttachments> vms = dtResult.AsEnumerable().Select(row => new AuditIssueAttachments
				{
					Id = row.Field<int>("Id"),
					T_TaskId = row.Field<int>("T_TaskId"),
					FileName = row.Field<string>("FileName")
				}).ToList();

				// Return the result
				return vms;

			}
			catch (Exception ex)
			{

				throw ex.InnerException;
			}
		}
	}
}
