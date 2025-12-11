using Microsoft.Data.SqlClient;
using System.Data;
//using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SSL.Sample.SSL.Sample.Repository.SqlServer
{
    public abstract class Repository
    {
        protected SqlConnection _context;
        protected SqlTransaction _transaction;




        protected SqlCommand CreateCommand(string query)
        {
            return new SqlCommand(query, _context, _transaction);
        }

        protected SqlDataAdapter CreateAdapter(string query)
        {
            var cmd =  new SqlCommand(query, _context, _transaction);

            return new SqlDataAdapter(cmd);
        }

        protected SqlDataAdapter CreateAdapter(SqlCommand cmd)
        {
            return new SqlDataAdapter(cmd);
        }

        //change
        public string GetSettingsValue(string[] conditionalFields, string[] conditionalValue)
        {

            try
            {
                string sqlText = @"select SettingValue from Settings where 1=1";

                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue);


                SqlCommand objComm = CreateCommand(sqlText);

                objComm = ApplyParameters(objComm, conditionalFields, conditionalValue);

                SqlDataAdapter adapter = new SqlDataAdapter(objComm);
                DataTable dtResutl = new DataTable();




                adapter.Fill(dtResutl);

                string settingValue = "2";

                if (dtResutl.Rows.Count > 0)
                {
                    DataRow row = dtResutl.Rows[0];
                    settingValue = row["SettingValue"].ToString();
                }




                return settingValue;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public string StringReplacing(string stringToReplace)
        {
            string newString = stringToReplace;
            if (stringToReplace.Contains("."))
            {
                newString = Regex.Replace(stringToReplace, @"^[^.]*.", "", RegexOptions.IgnorePatternWhitespace);
            }
            newString = newString.Replace(">", "From");
            newString = newString.Replace("<", "To");
            newString = newString.Replace("!", "");
            newString = newString.Replace("[", "");
            newString = newString.Replace("]", "");
          //  newString = newString.Replace("IN", "");
            return newString;
        }

        public  string ApplyConditions(string sqlText, string[] conditionalFields,string[] conditionalValue, bool orOperator = false)
        {
            string cField = "";
            bool conditionFlag = true;
            var checkValueExist = conditionalValue==null?false: conditionalValue.ToList().Any(x => !string.IsNullOrEmpty(x));
            var checkConditioanlValue = conditionalValue==null?false: conditionalValue.ToList().Any(x => !string.IsNullOrEmpty(x));
            
            if (checkValueExist && orOperator && checkConditioanlValue)
            {
                sqlText += " and (";
            }

            if (conditionalFields != null && conditionalValue != null && conditionalFields.Length == conditionalValue.Length)
            {
                for (int i = 0; i < conditionalFields.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(conditionalFields[i]) || string.IsNullOrWhiteSpace(conditionalValue[i]))
                    {
                        continue;
                    }
                    cField = conditionalFields[i].ToString();
                    cField = StringReplacing(cField);
                    string operand = " AND ";

                    if (orOperator)
                    {
                        operand = " OR ";

                        if (conditionFlag)
                        {
                            operand = "  ";
                            conditionFlag = false;
                        }
                    }

                   
                    if (conditionalFields[i].ToLower().Contains("like"))
                    {
                        sqlText += operand +conditionalFields[i] + " '%'+ " + " @" + cField.Replace("like", "").Trim() + " +'%'";
                    }
                    else if (conditionalFields[i].Contains(">") || conditionalFields[i].Contains("<"))
                    {
                        sqlText += operand + conditionalFields[i] + " @" + cField;
                    }
                    else if (conditionalFields[i].Contains("in",StringComparison.OrdinalIgnoreCase))
                    {
                         
                        //  such as invoice then work it , to avoid this type 

                      var test= conditionalFields[i].Split(" in");

                        if (test.Length > 1)
                        {
                            sqlText += operand + conditionalFields[i] + "(" + conditionalValue[i] + ")";
                        }    else
                        {
                            sqlText += operand + conditionalFields[i] + "= '" + Convert.ToString( conditionalValue[i]) + "'";
                        }

                    }                   
                    else
                    {
                        sqlText += operand + conditionalFields[i] + "= @" + cField;
                    }
                }
            }

            if (checkValueExist && orOperator && checkConditioanlValue)
            {
                sqlText += " )";
            }

            return sqlText;
        }
        
        public  SqlCommand ApplyParameters(SqlCommand objComm, string[] conditionalFields,string[] conditionalValue)
        {
            string cField = "";
            string tst = "";
            if (conditionalFields != null && conditionalValue != null && conditionalFields.Length == conditionalValue.Length)
            {
                for (int j = 0; j < conditionalFields.Length; j++)
                {
                    if (string.IsNullOrWhiteSpace(conditionalFields[j]) || string.IsNullOrWhiteSpace(conditionalValue[j]))
                    {
                        continue;
                    }
                    cField = conditionalFields[j].ToString();
                    cField = StringReplacing(cField);

                    var test = conditionalFields[j].ToLower().Contains("in");

                    if (conditionalFields[j].ToLower().Contains("like"))
                    {
                        objComm.Parameters.AddWithValue("@" + cField.Replace("like", "").Trim(), conditionalValue[j]);
                    }
                    else if (conditionalFields[j].ToLower().Contains("in",StringComparison.OrdinalIgnoreCase))
                    {
                    }
                    else
                    {
                        objComm.Parameters.AddWithValue("@" + cField, conditionalValue[j]);
                    }
                }
            }

            return objComm;
        }

        public int GetCount(string tableName, string fieldName, string[] conditionalFields, string[] conditionalValue)
        {
            try
            {
                // ToDo sql injection
                string sqlText = "SELECT COUNT(" + fieldName + ") TotalRecords from " + tableName + " where 1=1  ";

                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue,false);

                SqlCommand command = CreateCommand(sqlText);

                command = ApplyParameters(command, conditionalFields, conditionalValue);

                int totalRecords = Convert.ToInt32(command.ExecuteScalar());

                return totalRecords;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int Delete(string tableName, string[] conditionalFields, string[] conditionalValue)
        {
            try
            {
                // ToDo sql injection
                string sqlText = " update   " + tableName + " set IsArchive=1 where 1=1 ";

                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue);

                SqlCommand command = CreateCommand(sqlText);

                command = ApplyParameters(command, conditionalFields, conditionalValue);

                int totalRecords = Convert.ToInt32(command.ExecuteNonQuery());

                return totalRecords;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int DetailsDelete(string tableName, string[] conditionalFields, string[] conditionalValue)
        {
            try
            {
                // ToDo sql injection
                string sqlText = " delete   " + tableName + "  where 1=1 ";

                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue);

                SqlCommand command = CreateCommand(sqlText);

                command = ApplyParameters(command, conditionalFields, conditionalValue);

                int totalRecords = Convert.ToInt32(command.ExecuteNonQuery());

                return totalRecords;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        // Todo add audit
        public int Archive(string tableName, string[] conditionalFields, string[] conditionalValue)
        {
            try
            {
                // ToDo sql injection
                string sqlText = "update from " + tableName + " set IsArchive=1 where 1=1 ";

                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue);

                SqlCommand command = CreateCommand(sqlText);

                command = ApplyParameters(command, conditionalFields, conditionalValue);

                int totalRecords = Convert.ToInt32(command.ExecuteScalar());

                return totalRecords;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CheckExists(string tableName, string[] conditionalFields, string[] conditionalValue)
        {
            try
            {
                // ToDo sql injection
                string sqlText = "select count(*)  from " + tableName + " where 1=1 ";

                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue);

                SqlCommand command = CreateCommand(sqlText);

                command = ApplyParameters(command, conditionalFields, conditionalValue);

                int totalRecords = Convert.ToInt32(command.ExecuteScalar());

                return totalRecords > 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string GenerateCode(string CodeGroup, string CodeName, string EntryDate, int branchId = 1)
        {
            try
            {
                // ToDo sql injection
                string sqlText = "";

                string NewCode = "";
                string CodePreFix = "";
                string CodeGenerationFormat = "Branch/Number/Year";
                string CodeGenerationMonthYearFormat = "MMYY";
                string BranchCode = "001";
                string CurrentYear = "2020";
                string BranchNumber = "1";
                int CodeLength = 0;
                int nextNumber = 0;
              
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataSet ds = new DataSet();

                DateTime TransactionDate = Convert.ToDateTime(EntryDate);
                string year = Convert.ToDateTime(DateTime.Now).ToString("yyyy");

                int BranchId = branchId;


                sqlText += " SELECT   top 1  SettingName FROM Settings";
                sqlText += " WHERE     (SettingGroup ='" + CodeGenerationFormat + "') and   (SettingValue ='Y')  ";

                sqlText += " SELECT   top 1  SettingName FROM Settings";
                sqlText += " WHERE     (SettingGroup ='"+ CodeGenerationFormat + "') and   (SettingValue ='Y')  ";

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

                    BranchNumber = BranchId.ToString();


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

              NewCode=   CodeGeneration1(CodeGenerationMonthYearFormat, BranchNumber, CodeGenerationFormat, BranchCode.Trim(), CodeLength, nextNumber, CodePreFix, TransactionDate.ToString());
                return NewCode;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }



        public string CodeGeneration1(string CodeGenerationMonthYearFormat, string BranchNumber, string CodeGenerationFormat, string BranchCode, int CodeLength
                   , int nextNumber, string CodePreFix, string TransactionDate)
        {
            string NewCode = "";

            #region try

            try
            {
                CodeGenerationMonthYearFormat = CodeGenerationMonthYearFormat.Replace("Y", "y");
                if (Convert.ToInt32(BranchNumber) < 1)
                {
                    CodeGenerationFormat = CodeGenerationFormat.Replace("B/", "");
                }
                
                var my = Convert.ToDateTime(TransactionDate).ToString(CodeGenerationMonthYearFormat);
                var nextNumb = nextNumber.ToString().PadLeft(CodeLength, '0');
                CodeGenerationFormat = CodeGenerationFormat.Replace("Branch", BranchCode);
                CodeGenerationFormat = CodeGenerationFormat.Replace("Number", nextNumb);
                CodeGenerationFormat = CodeGenerationFormat.Replace("Year", my);

                NewCode = CodePreFix + "-" + CodeGenerationFormat;
            }
            #endregion

            #region catch

            catch (Exception ex)
            {
               // FileLogger.Log("CommonDAL", "CodeGeneration1", ex.ToString());

                throw ex;
            }
            #endregion

            return NewCode;


        }


        public string GetSingleValeByID(string tableName,string ReturnFields, string[] conditionalFields, string[] conditionalValue)
        {
            try
            {
                // ToDo sql injection
                string sqlText = "select "+ ReturnFields + "  from " + tableName + " where 1=1  ";

                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue);

                SqlCommand command = CreateCommand(sqlText);

                command = ApplyParameters(command, conditionalFields, conditionalValue);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dtResutl = new DataTable();
                adapter.Fill(dtResutl);
                if(dtResutl.Rows.Count>0)
                {
                    return dtResutl.Rows[0][ReturnFields].ToString();
                }
                else
                {
                    return ""; 
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CheckPostStatus(string tableName, string[] conditionalFields, string[] conditionalValue)
        {
            try
            {
                bool ÌsPost = false;
                string Post = "";

                DataTable dt = new DataTable();

                // ToDo sql injection
                string sqlText = "select IsPost  from " + tableName + " where 1=1 ";

                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue);

                SqlCommand command = CreateCommand(sqlText);

                command = ApplyParameters(command, conditionalFields, conditionalValue);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Post = dt.Rows[0]["IsPost"].ToString();
                    return (Post == "Y");
                }


                return ÌsPost;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CheckPushStatus(string tableName, string[] conditionalFields, string[] conditionalValue)
        {
            try
            {
                bool ÌsPush=false;
                string Push="";

                DataTable dt = new DataTable();

                // ToDo sql injection
                string sqlText = "select IsPush  from " + tableName + " where 1=1 ";

                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue);

                SqlCommand command = CreateCommand(sqlText);

                command = ApplyParameters(command, conditionalFields, conditionalValue);



                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string push = dt.Rows[0]["IsPush"].ToString();
                    return (push == "Y");
                }


                return ÌsPush ;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



    }
}


   
