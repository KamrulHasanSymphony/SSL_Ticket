using Microsoft.Data.SqlClient;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.ExtentionMethod;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Ticket;
using SSL.Ticket.SSL.Ticket.Models;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Repository.SqlServer.Ticket
{
    public class TicketRepository : Repository, ITicketRepository
    {
        private CS.SSL.Common.Models.DbConfig _dbConfig;
        public TicketRepository(SqlConnection context, SqlTransaction transaction, CS.SSL.Common.Models.DbConfig dbConfig)
        {
            this._context = context;
            this._transaction = transaction;

            this._dbConfig = dbConfig;
        }
        public List<T_TicketVm> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            try
            {
                string sqlText = $@" 
                SELECT
				Id, Code, Title, Description, T_ClientId, StackHolderUserId, 
                     T_ProductId,T_TopicId,T_RatingId,T_PriorityId,T_StatusId, T_SourceId,DepartmentId,TicketTypeId, CreateDate, DueDate, CreatedBy
                FROM T_Tickets
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

                T_TicketVm vmdata = new T_TicketVm();



                List<T_TicketVm> vms = dtResutl.ToList<T_TicketVm>();


                return vms;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public List<T_TicketVm> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public T_TicketVm Insert(T_TicketVm vm)
        {
            string sqlText = "";
            int Id = 0;
            int transResult = 0;
            int countId;
            string SlNo;


            try
            {
                sqlText = "";
                sqlText = @" INSERT INTO T_Tickets(
                                     CompanyId
                                    ,Code
                                    ,Title
                                    ,Description
                                    ,T_ClientId
                                    ,StackHolderUserId
                                    ,T_RatingId
                                    ,T_TopicId
                                    ,T_PriorityId
                                    ,T_StatusId
                                    ,T_ProductId
                                    ,T_SourceId
                                    ,CreateDate
                                    ,DueDate                                    
                                    ,CreatedBy
                                    ,CreateOn
                                    ,IsComplete
                                    ,DepartmentId
                                    ,TicketTypeId
                                    ) 

                                    VALUES (
                                     @CompanyId
                                    ,@TicketCode
                                    ,@TicketTitle
                                    ,@Description
                                    ,@ClientId
                                    ,@TicketStackHolder
                                    ,@T_RatingId
                                    ,@T_TopicId
                                    ,@T_PriorityId
                                    ,@T_StatusId
                                    ,@ProductId
                                    ,@TicketSource
                                    ,@CreateDate
                                    ,@DueDate                                    
                                    ,@CreatedBy
                                    ,@CreatedOn
                                    ,@IsComplete
                                    ,@DepartmentId
                                    ,@TicketTypeId
                                    );

                                    DECLARE @TicketId INT = SCOPE_IDENTITY();

                                    INSERT INTO T_Tasks(
                                    CompanyId
                                    ,Code
                                    ,Title
                                    ,T_TicketId
                                    ,T_SourceId
                                    ,T_TopicId
                                    ,T_PriorityId
                                    ,T_StatusId
                                    ,StartDate
                                    ,DepartmentId
                                    ,TaskTypeId
                                    ,CreatedOn
                                    ,CreatedBy)

                                    VALUES(
                                    @CompanyId 
                                    ,@TaskCode
                                    ,@TicketTitle
                                    ,@TicketId
                                    ,@TicketSource
                                    ,@T_TopicId
                                    ,@T_PriorityId
                                    ,@T_StatusId
                                    ,@CreatedOn
                                    ,@DepartmentId
                                    ,@TicketTypeId
                                    ,@CreatedOn
                                    ,@CreatedBy);

                                    SELECT @TicketId;";


                var cmdInsert = CreateCommand(sqlText);

                cmdInsert.Parameters.AddWithValue("@CompanyId", 1);
                cmdInsert.Parameters.AddWithValue("@TicketCode", vm.Code);
                cmdInsert.Parameters.AddWithValue("@TaskCode", vm.TaskCode);
                cmdInsert.Parameters.AddWithValue("@TicketTitle", vm.Title ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@Description", vm.Description ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@ClientId", vm.T_ClientId ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@TicketStackHolder", vm.StackHolderUserId ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@T_RatingId", vm.T_RatingId ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@T_TopicId", vm.T_TopicId ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@T_PriorityId", vm.T_PriorityId ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@T_StatusId", vm.T_StatusId ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@ProductId", vm.T_ProductId ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@TicketSource", vm.T_SourceId ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@CreateDate", vm.CreateDate ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@DueDate", vm.DueDate ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@CreatedBy", vm.CreatedBy ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@CreatedOn", vm.CreateOn = DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@IsComplete", vm.IsComplete = false);
                cmdInsert.Parameters.AddWithValue("@DepartmentId", vm.DepartmentId);
                cmdInsert.Parameters.AddWithValue("@TicketTypeId", vm.TicketTypeId);

                vm.Id = Convert.ToInt32(cmdInsert.ExecuteScalar());
                return vm;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        //public T_TicketVm Insert(T_TicketVm vm)
        //{
        //    try
        //    {

        //        int TicketId = InsertTicket(vm);

        //        //int TaskId =InsertTask(vm, TicketId);
        //        //InsertTaskAssignes(TicketId, TaskId, vm);
        //        vm.Id = TicketId;
        //        return vm;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex.InnerException;
        //    }
        //}
        //public int InsertTicket(T_TicketVm vm)
        //{
        //    string sqlText = @"INSERT INTO T_Tickets(
        //                     CompanyId, Code, Title, Description, T_ClientId, StackHolderUserId, 
        //                     T_RatingId, T_TopicId, T_PriorityId, T_StatusId, T_ProductId, 
        //                     T_SourceId, CreateDate, DueDate, CreatedBy, CreateOn, IsComplete, 
        //                     DepartmentId, TicketTypeId
        //                     ) 
        //               VALUES (
        //                     @CompanyId, @TicketCode, @TicketTitle, @Description, @ClientId, 
        //                     @TicketStackHolder, @T_RatingId, @T_TopicId, @T_PriorityId, 
        //                     @T_StatusId, @ProductId, @TicketSource, @CreateDate, @DueDate, 
        //                     @CreatedBy, @CreatedOn, @IsComplete, @DepartmentId, @TicketTypeId
        //                     );
        //               SELECT SCOPE_IDENTITY();";

        //    var cmdInsert = CreateCommand(sqlText);


        //    cmdInsert.Parameters.AddWithValue("@CompanyId", 1);
        //    cmdInsert.Parameters.AddWithValue("@TicketCode", vm.Code);
        //    cmdInsert.Parameters.AddWithValue("@TicketTitle", vm.Title ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@Description", vm.Description ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@ClientId", vm.T_ClientId ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@TicketStackHolder", vm.StackHolderUserId ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@T_RatingId", vm.T_RatingId ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@T_TopicId", vm.T_TopicId ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@T_PriorityId", vm.T_PriorityId ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@T_StatusId", vm.T_StatusId ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@ProductId", vm.T_ProductId ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@TicketSource", vm.T_SourceId ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@CreateDate", vm.CreateDate ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@DueDate", vm.DueDate ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@CreatedBy", vm.CreatedBy ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@CreatedOn", vm.CreateOn = DateTime.Now);
        //    cmdInsert.Parameters.AddWithValue("@IsComplete", vm.IsComplete = false);
        //    cmdInsert.Parameters.AddWithValue("@DepartmentId", vm.DepartmentId);
        //    cmdInsert.Parameters.AddWithValue("@TicketTypeId", vm.TicketTypeId);


        //    return Convert.ToInt32(cmdInsert.ExecuteScalar());
        //}

        //public int InsertTask(T_TicketVm vm, int TicketId)
        //{
        //    string sqlText = @"INSERT INTO T_Tasks(
        //                     CompanyId, Title, T_TicketId, T_SourceId, T_TopicId, 
        //                     T_PriorityId, T_StatusId, StartDate, DepartmentId, 
        //                     TaskTypeId, CreatedOn, CreatedBy
        //                     ) 
        //               VALUES (
        //                     @CompanyId, @TaskTitle, @TicketId, @TaskSource, 
        //                     @T_TopicId, @T_PriorityId, @T_StatusId, 
        //                     @CreatedOn, @DepartmentId, @TaskTypeId, @CreatedOn, @CreatedBy
        //                     )
        //                      SELECT SCOPE_IDENTITY();;";

        //    var cmdInsert = CreateCommand(sqlText);


        //    cmdInsert.Parameters.AddWithValue("@CompanyId", 1);
        //    cmdInsert.Parameters.AddWithValue("@TaskTitle", vm.Title ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@TicketId", TicketId);
        //    cmdInsert.Parameters.AddWithValue("@TaskSource", vm.T_SourceId ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@T_TopicId", vm.T_TopicId ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@T_PriorityId", vm.T_PriorityId ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@T_StatusId", vm.T_StatusId ?? Convert.DBNull);
        //    cmdInsert.Parameters.AddWithValue("@CreatedOn", vm.CreateOn = DateTime.Now);
        //    cmdInsert.Parameters.AddWithValue("@DepartmentId", vm.DepartmentId);
        //    cmdInsert.Parameters.AddWithValue("@TaskTypeId", vm.TicketTypeId);
        //    cmdInsert.Parameters.AddWithValue("@CreatedBy", vm.CreatedBy ?? Convert.DBNull);


        //    return Convert.ToInt32(cmdInsert.ExecuteScalar());
        //}
        public async Task<string[]> InsertAttachmentsAsync(T_TicketVm vm, SqlConnection VcurrConn = null, SqlTransaction Vtransaction = null)
        {
            throw new NotImplementedException();
        }

        public T_TicketVm Update(T_TicketVm model)
        {
            try
            {
                string sqlText = "";
                int count = 0;

                string query = @"  update T_Tickets set 

                CompanyId           = @Company  
                ,Title              =@Title  
                ,Description        =@Description 
                ,T_ClientId         =@T_ClientId  
                ,StackHolderUserId  =@StackHolderUserId  
                ,T_ProductId        =@T_ProductId  
                ,T_SourceId         =@T_SourceId  
                ,CreateDate         =@CreateDate  
                ,DueDate            =@DueDate  
                ,UpdateBy           =@UpdateBy  
                ,UpdateOn           =@UpdateOn
                ,T_TopicId          =@T_TopicId
                ,T_PriorityId       =@T_PriorityId
                ,T_StatusId         =@T_StatusId
                ,DepartmentId       =@DepartmentId
                ,TicketTypeId       =@TicketTypeId
                       
                where  Id= @Id ";


                SqlCommand command = CreateCommand(query);
                command.Parameters.Add("@Id", SqlDbType.Int).Value = model.Id;
                command.Parameters.Add("@Company", SqlDbType.Int).Value = 1;
                command.Parameters.Add("@Title", SqlDbType.NChar).Value = model.Title;
                command.Parameters.Add("@Description", SqlDbType.NChar).Value = model.Description;
                command.Parameters.Add("@T_ClientId", SqlDbType.Int).Value = model.T_ClientId;
                command.Parameters.Add("@StackHolderUserId", SqlDbType.NChar).Value = model.StackHolderUserId;
                command.Parameters.Add("@T_ProductId", SqlDbType.Int).Value = model.T_ProductId;
                command.Parameters.Add("@T_SourceId", SqlDbType.Int).Value = model.T_SourceId;
                command.Parameters.Add("@CreateDate", SqlDbType.NChar).Value = string.IsNullOrEmpty(model.CreateDate.ToString()) ? (object)DBNull.Value : model.CreateDate.ToString();
                command.Parameters.Add("@DueDate", SqlDbType.NChar).Value = string.IsNullOrEmpty(model.DueDate.ToString()) ? (object)DBNull.Value : model.DueDate.ToString();
                command.Parameters.Add("@UpdateBy", SqlDbType.NChar).Value = model.UpdateBy;
                command.Parameters.Add("@UpdateOn", SqlDbType.DateTime).Value = DateTime.Now;
                command.Parameters.Add("@T_TopicId", SqlDbType.Int).Value = model.T_TopicId;
                command.Parameters.Add("@T_PriorityId", SqlDbType.Int).Value = model.T_PriorityId;
                command.Parameters.Add("@T_StatusId", SqlDbType.Int).Value = model.T_StatusId;
                command.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = model.DepartmentId;
                command.Parameters.Add("@TicketTypeId", SqlDbType.Int).Value = model.TicketTypeId;


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

        public string GenerateCode(string CodeGroup, string CodeName, int branchId = 1)
        {
            try
            {
                // ToDo sql injection
                string sqlText = "";

                string NewCode = "";
                string CodePreFix = "";
                string CodeGenerationFormat = "";
                string CodeGenerationMonthYearFormat = "MMYY";
                string BranchCode = "001";
                string CurrentYear = "2024";
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
                sqlText += "  update CodeGenerations set CurrentYear ='2024'  where CurrentYear <='2024'";

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

                    command.ExecuteNonQuery();

                }

                NewCode = CodeGeneration1(CodeGenerationMonthYearFormat, BranchNumber, CodeGenerationFormat, BranchCode, CodeLength, nextNumber, CodePreFix, TransactionDate.ToString());
                return NewCode;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public T_TicketVm InsertActive(T_TicketVm model)
        {
            throw new NotImplementedException();
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


                sqlText = "SELECT TOP (1) Id FROM T_Tickets ORDER BY Id DESC";
                command = CreateCommand(sqlText);
                dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dt1);


                if (dt1.Rows.Count > 0)
                {
                    int lastId = Convert.ToInt32(dt1.Rows[0]["Id"]);
                    nextNumber = lastId + 1;

                }
                //else
                //{

                //    NewCode = "TIC-000001";  
                //}

                return nextNumber;
            }
            catch (Exception e)
            {
                throw new Exception("Error while retrieving last ID: " + e.Message);
            }
        }
        public string NewGenerateCode(int ID)
        {
            try
            {

                int lastID = GetLastID();
                string CodePreFix = "TIC-"; // + nextNumber.ToString("D6");
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
    }
}
