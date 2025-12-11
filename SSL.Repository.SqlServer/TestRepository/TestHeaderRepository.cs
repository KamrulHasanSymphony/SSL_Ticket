using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SSL.Core.ExtentionMethod;
using SSL.Core.Interfaces.Repository.TestRepository;
using SSL_ERP.Models;

namespace SSL.Repository.SqlServer.TestRepository
{
    public class TestHeaderRepository : Repository, INewTestHeaderRepository
    {
        public TestHeaderRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction; 
        }

        public List<TestHeaderVM> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm)
        {
            throw new NotImplementedException();
        }

        public List<TestHeaderVM> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm)
        {
            DataTable dt = new DataTable();

            try
            {
                string sqlText = @"
SELECT  [Id]
      ,[Code]
      ,[GLAccount]
      ,[TransDate]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[CreatedFrom]
      ,[LastUpdateBy]
      ,[LastUpdateOn]
      ,[LastUpdateFrom]
      ,[PostedBy]
      ,[PostedOn]
      ,[PostedFrom]
      ,[PushedBy]
      ,[PushedOn]
      ,[PushedFrom]
      ,[BranchId]
      ,[CompanyId]
  FROM [TestHeader] where 1=1 ";


                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue, true);

                // ToDo Escape Sql Injection
                sqlText += @"  order by  " + GetTestColumnName(index.OrderName) + "  " + index.orderDir;
                sqlText += @" OFFSET  " + index.startRec + @" ROWS FETCH NEXT " + index.pageSize + " ROWS ONLY";

                SqlDataAdapter objComm = CreateAdapter(sqlText);

                objComm.SelectCommand = ApplyParameters(objComm.SelectCommand, conditionalFields, conditionalValue);
                objComm.Fill(dt);


                List<TestHeaderVM> testHeaderVms = dt.ToList<TestHeaderVM>();

                return testHeaderVms;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm)
        {
            DataTable dt = new DataTable();

            try
            {
                string sqlText =
                    @"
Select   
count(id)FilteredCount
From TestHeader
Where 1=1
";
                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue);

                SqlDataAdapter objComm = CreateAdapter(sqlText);

                objComm.SelectCommand = ApplyParameters(objComm.SelectCommand, conditionalFields, conditionalValue);

                objComm.Fill(dt);

                return Convert.ToInt32(dt.Rows[0][0]);


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public TestHeaderVM Insert(TestHeaderVM model)
        {
            throw new NotImplementedException();
        }

        public TestHeaderVM Update(TestHeaderVM model)
        {
            throw new NotImplementedException();
        }

        public string TestMethod(string id)
        {
            throw new NotImplementedException();
        }
    }
}
