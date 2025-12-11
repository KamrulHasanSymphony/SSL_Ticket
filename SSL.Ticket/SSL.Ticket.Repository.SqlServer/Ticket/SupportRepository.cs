using Microsoft.Data.SqlClient;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.ExtentionMethod;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Ticket;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System.Data;

namespace SSL.Ticket.SSL.Ticket.Repository.SqlServer.Support
{
    public class SupportRepository : Repository, ITicketRepository
    {
        private DbConfig _dbConfig;
        public SupportRepository(SqlConnection context, SqlTransaction transaction, DbConfig dbConfig)
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
				WITH RankedTickets AS (
                SELECT
                    h.TicketId,
                    h.Status,
                    h.TotalDurationTime,
                    ROW_NUMBER() OVER (PARTITION BY h.TicketId ORDER BY h.StartDate DESC) AS RowNum
                FROM SP_TicketHistory h
            ),
            LatestTicketHistory AS (
                SELECT
                    TicketId,
                    Status,
                    TotalDurationTime
                FROM RankedTickets
                WHERE RowNum = 1
            )
					Select
                     T.Id,
                     T.TicketCode,
                     T.TicketTitle,
                     T.CreatorEmail,
                     T.CreatorPhone,
                     T.TicketStackHolder,
                     T.TicketStackHolderCC,
                     T.TicketSource,
                     T.TicketTopic,
                     T.DepartmentId,
                     T.ProductId,
                     T.Status,
                     ISNULL(FORMAT(T.CreateDate, 'yyyy-MM-dd HH:mm:ss'), 'Null') AS CreateDate,
                     ISNULL(FORMAT(T.DueDate, 'yyyy-MM-dd HH:mm:ss'), 'Null') AS DueDate,
                     T.Est_EndDate,
                     T.Description,
                     T.Remark,
                     T.File1,
                     T.Organization,
                     T.CreatedBy,
                     sp.LogId,
                     CO.ClientOrOrganization,
                     STRING_AGG(spd.LogId, ', ') AS AssignTo,
                     DATEDIFF(day, T.CreateDate, T.DueDate) AS TotalDays,
                     h.TotalDurationTime,
                     h.Status AS WorkingStatus,
                     Enums.EnumValue
                FROM SP_Ticket T
                LEFT OUTER JOIN [SSLSupportAuthDB].[dbo].[SupportUsers] sp ON sp.ID = T.TicketStackHolder
                LEFT OUTER JOIN [SSLSupportAuthDB].[dbo].[SupportUsers] spc ON spc.ID = T.AssignTo
                LEFT OUTER JOIN SP_ClientOrOrganization CO ON CO.ID = T.Organization
                INNER JOIN SP_AssignTo at ON T.Id = at.TicketId
                INNER JOIN [SSLSupportAuthDB].[dbo].[SupportUsers] spd ON spd.ID = AT.AssignTo
                LEFT JOIN LatestTicketHistory h ON T.Id = h.TicketId
                INNER JOIN Enums ON T.Status = Enums.Id
                WHERE 1=1";
                //sqlText += @" Where T.Id= @Id";
                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue);

                sqlText +=@"
                GROUP BY 
				T.Id,
                     T.TicketCode,
                     T.TicketTitle,
                     T.CreatorEmail,
                     T.CreatorPhone,
                     T.TicketStackHolder,
                     T.TicketStackHolderCC,
                     T.TicketSource,
                     T.TicketTopic,
                     T.DepartmentId,
                     T.ProductId,
                     T.Status,
					 T.CreateDate,
					 T.DueDate,
					 T.Est_EndDate,
                     T.Description,
                     T.Remark,
                     T.File1,
                     T.Organization,
                     T.CreatedBy,
                     sp.LogId,
					 --spc.LogId,
					 CO.ClientOrOrganization,
					 spd.LogId,
					 h.TotalDurationTime,
                     h.Status,
					 Enums.EnumValue";


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
                                    ,T_ProductId
                                    ,T_SourceId
                                    ,CreateDate
                                    ,DueDate                                    
                                    ,CreatedBy
                                    ,CreateOn
                                    ,IsComplete
                                    ) 

                                    VALUES (
                                     @CompanyId
                                    ,@TicketCode
                                    ,@TicketTitle
                                    ,@Description
                                    ,@ClientId
                                    ,@TicketStackHolder
                                    ,@ProductId
                                    ,@TicketSource
                                    ,@CreateDate
                                    ,@DueDate                                    
                                    ,@CreatedBy
                                    ,@CreatedOn
                                    ,@IsComplete
                                    ) 
                                    SELECT  SCOPE_IDENTITY()";


                    //SqlCommand cmdInsert = new SqlCommand(sqlText, currConn, transaction);
                    var cmdInsert = CreateCommand(sqlText);

                    cmdInsert.Parameters.AddWithValue("@CompanyId", vm.CompanyId);
                    cmdInsert.Parameters.AddWithValue("@TicketCode", vm.Code);
                    cmdInsert.Parameters.AddWithValue("@TicketTitle", vm.Title ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@Description", vm.Description ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@ClientId", vm.T_ClientId ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@TicketStackHolder", vm.StackHolderUserId);
                    cmdInsert.Parameters.AddWithValue("@ProductId", vm.T_ProductId ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@TicketSource", vm.T_SourceId);
                    cmdInsert.Parameters.AddWithValue("@CreateDate", vm.CreateDate ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@DueDate", vm.DueDate ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@CreatedBy", vm.CreatedBy ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@CreatedOn", vm.CreateOn ?? Convert.DBNull);
                    cmdInsert.Parameters.AddWithValue("@IsComplete", vm.IsComplete = false);

                    vm.Id = Convert.ToInt32(cmdInsert.ExecuteScalar());
                return vm;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }            
        }

        public async Task<string[]> InsertAttachmentsAsync(T_TicketVm vm, SqlConnection VcurrConn = null, SqlTransaction Vtransaction = null)
        {
            throw new NotImplementedException();
        }


        //public string GenerateCode(string CodeGroup, string CodeName, int branchId = 1, int Id = 0, string[] conditionFields = null, string[] conditionValues = null, SqlConnection VcurrConn = null, SqlTransaction Vtransaction = null)
        //{
        //    #region Variables
        //    //SqlConnection currConn = null;
        //    //SqlTransaction transaction = null;
        //    List<T_TicketVm> VMs = new List<T_TicketVm>();
        //    T_TicketVm vm;
        //    string NewCode = "";
        //    #endregion


        //    try
        //    {
        //        string sqlText = "";

        //        string CodePreFix = "";
        //        string CodeGenerationFormat = "B/N/Y";
        //        string CodeGenerationMonthYearFormat = "MMYY";
        //        string BranchCode = "001";
        //        string CurrentYear = "2020";
        //        string BranchNumber = "1";
        //        int CodeLength = 0;
        //        int nextNumber = 0;

        //        DataTable dt1 = new DataTable();
        //        DataTable dt2 = new DataTable();
        //        DataSet ds = new DataSet();

        //        DateTime TransactionDate = DateTime.Now;
        //        string year = Convert.ToDateTime(DateTime.Now).ToString("yyyy");

        //        int BranchId = branchId;


        //        sqlText += " SELECT   top 1  SettingName FROM Settings";
        //        sqlText += " WHERE     (SettingGroup ='" + CodeGenerationFormat + "') and   (SettingValue ='Y')  ";

        //        sqlText += " SELECT   top 1  SettingName FROM Settings";
        //        sqlText += " WHERE     (SettingGroup ='" + CodeGenerationFormat + "') and   (SettingValue ='Y')  ";

        //        sqlText += " SELECT   top 1  BranchCode FROM BranchProfiles";
        //        sqlText += " WHERE     (BranchID ='" + BranchId + "')   ";

        //        sqlText += " SELECT   count(BranchCode) BranchNumber FROM BranchProfiles where IsArchive='0' and ActiveStatus='1'";

        //        sqlText += "  SELECT   * from  CodeGenerations where CurrentYear<='2020' ";

        //        sqlText += "  select YEAR from FiscalYears where '" + Convert.ToDateTime(TransactionDate).ToString("dd/MMM/yyyy") + "' between YearStart and YearEnd ";

        //        //SqlCommand command = CreateCommand(sqlText);
        //        var command = new SqlCommand(sqlText);

        //        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
        //        dataAdapter.Fill(ds);



        //        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
        //            CodeGenerationFormat = ds.Tables[0].Rows[0][0].ToString();

        //        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
        //            CodeGenerationMonthYearFormat = ds.Tables[1].Rows[0][0].ToString();
        //        if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
        //            BranchCode = ds.Tables[2].Rows[0][0].ToString();

        //        if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
        //            BranchNumber = ds.Tables[3].Rows[0][0].ToString();


        //        sqlText = "  ";
        //        sqlText += "  update CodeGenerations set CurrentYear ='2020'  where CurrentYear <='2020'";

        //        //command = CreateCommand(sqlText);
        //        command = new SqlCommand(sqlText);

        //        command.ExecuteNonQuery();

        //        if (ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
        //            CurrentYear = ds.Tables[5].Rows[0][0].ToString();

        //        sqlText = "  ";

        //        sqlText += " SELECT     * FROM Codes";
        //        sqlText += " WHERE     (CodeGroup =@CodeGroup) AND (CodeName = @CodeName)";

        //        command.CommandText = sqlText;


        //        command.Parameters.AddWithValue("@CodeGroup", CodeGroup);
        //        command.Parameters.AddWithValue("@CodeName", CodeName);

        //        dataAdapter = new SqlDataAdapter(command);


        //        dataAdapter.Fill(dt1);
        //        if (dt1 == null || dt1.Rows.Count <= 0)
        //        {
        //            throw new ArgumentNullException();
        //        }
        //        else
        //        {
        //            CodePreFix = dt1.Rows[0]["prefix"].ToString();
        //            CodeLength = Convert.ToInt32(dt1.Rows[0]["Lenth"]);
        //        }

        //        sqlText = "  ";
        //        sqlText += @" 
        //                        SELECT top 1 
        //                        Id
        //                        ,CurrentYear
        //                        ,BranchId
        //                        ,Prefix
        //                        ,ISNULL(LastId,0) LastId
        //                        FROM CodeGenerations 
        //                        WHERE CurrentYear=@CurrentYear AND BranchId=@BranchId AND Prefix=@Prefix order by LastId Desc
        //                        ";


        //        command.CommandText = sqlText;


        //        command.Parameters.AddWithValue("@BranchId", BranchId);
        //        command.Parameters.AddWithValue("@CurrentYear", CurrentYear);
        //        command.Parameters.AddWithValue("@Prefix", CodePreFix);


        //        dataAdapter = new SqlDataAdapter(command);
        //        dataAdapter.Fill(dt2);


        //        if (dt2 == null || dt2.Rows.Count <= 0)
        //        {
        //            sqlText = "  ";
        //            sqlText +=
        //                " INSERT INTO CodeGenerations(	CurrentYear,BranchId,Prefix,LastId)";
        //            sqlText += " VALUES(";
        //            sqlText += " @CurrentYear,";
        //            sqlText += " @BranchId,";
        //            sqlText += " @Prefix,";
        //            sqlText += " 1";
        //            sqlText += " )";

        //            command.CommandText = sqlText;

        //            object objfoundId1 = command.ExecuteNonQuery();

        //            nextNumber = 1;
        //        }
        //        else
        //        {
        //            if (nextNumber != 1)
        //            {
        //                nextNumber = dt2.Rows[0]["LastId"] == null ? 1 : Convert.ToInt32(dt2.Rows[0]["LastId"]) + 1;
        //            }

        //            sqlText = "  ";
        //            sqlText += " update  CodeGenerations set LastId='" + nextNumber + "'";
        //            sqlText += " WHERE CurrentYear=@CurrentYear AND BranchId=@BranchId AND Prefix=@Prefix";


        //            command.CommandText = sqlText;


        //            command.ExecuteNonQuery();

        //        }

        //        NewCode = CodeGeneration1(CodeGenerationMonthYearFormat, BranchNumber, CodeGenerationFormat, BranchCode, CodeLength, nextNumber, CodePreFix, TransactionDate.ToString());

        //        //if (transaction != null)
        //        //{
        //        //    transaction.Commit();
        //        //}

        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }           

        //    return NewCode;
        //}

        public T_TicketVm Update(T_TicketVm model)
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

        public T_TicketVm InsertActive(T_TicketVm model)
        {
            throw new NotImplementedException();
        }

        public string NewGenerateCode(int ID)
        {
            throw new NotImplementedException();
        }

        //public AssignToVM AssignToInsert(AssignToVM detail)
        //{
        //    try
        //    {
        //        string sqlText = "";

        //        sqlText = @"

        //                    INSERT INTO dbo.SP_AssignTo
        //                               (TicketId
        //                               ,AssignTo
        //                               ,CreatedBy
        //                               ,CreatedOn)
        //                         VALUES
        //                               (@TicketId                                        
        //                               ,@AssignTo
        //                               ,@LogId                                         
        //                               ,@CreatedOn)  

        //                    SELECT SCOPE_IDENTITY()";


        //        var command = CreateCommand(sqlText);

        //        command.Parameters.Add("@TicketId", SqlDbType.Int).Value = detail.Id;
        //        command.Parameters.Add("@AssignTo", SqlDbType.Int).Value = detail.AssignTo;
        //        command.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = "";                
        //        command.Parameters.Add("@UOMc", SqlDbType.DateTime).Value = DateTime.Now;

        //        detail.Id = "";

        //        return detail;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex.InnerException;
        //    }
        //}
    }
}