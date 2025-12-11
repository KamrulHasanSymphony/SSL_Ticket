using Microsoft.Data.SqlClient;
using SSL.Common.SSL.Common.Core.ExtentionMethod;
using SSL.CS.SSL.Common.Core.Interfaces.Repository.Company;
using SSL.CS.SSL.Common.Models;
using System.Data;

namespace SSL.CS.SSL.Common.Repository.SqlServer.Company
{
    public class CompanyInfoRepository: Repository, ICompanyInfoRepository
    {
        private DbConfig _dbConfig;

        public CompanyInfoRepository(SqlConnection context, SqlTransaction transaction, DbConfig dbConfig)
        {
            this._context = context;
            this._transaction = transaction;

            this._dbConfig = dbConfig;

        }

        public List<CompanyInfo> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            string sqlText = "";
            List<CompanyInfo> VMs = new List<CompanyInfo>();
            DataTable dt = new DataTable();

            try
            {
                sqlText = @"
SELECT  [Id]
      ,[CompanyId]
      ,[CompanyName]
      ,[CompanyDataBase]
      ,[SerialNo]
  FROM [CompanyInfo]

where 1=1 
";
                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue, true);

                SqlDataAdapter objComm = CreateAdapter(sqlText);

                objComm.SelectCommand = ApplyParameters(objComm.SelectCommand, conditionalFields, conditionalValue);

                objComm.Fill(dt);

                VMs = dt.ToList<CompanyInfo>();

                return VMs;


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CompanyInfo> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public CompanyInfo Insert(CompanyInfo model)
        {
            throw new NotImplementedException();
        }

        public CompanyInfo Update(CompanyInfo model)
        {
            throw new NotImplementedException();
        }

        public List<BranchProfile> GetBranches(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            string sqlText = "";
            List<BranchProfile> branchProfiles = new List<BranchProfile>();
            DataTable dt = new DataTable();

            try
            {
                sqlText = @"
SELECT bp.[BranchID]
      ,bp.[BranchCode]
      ,bp.[BranchName]
      ,bp.[BranchLegalName]
      ,bp.[Address]
      ,bp.[City]
      ,bp.[ZipCode]
      ,bp.[TelephoneNo]
      ,bp.[FaxNo]
      ,bp.[Email]
      ,bp.[ContactPerson]
      ,bp.[ContactPersonDesignation]
      ,bp.[ContactPersonTelephone]
      ,bp.[ContactPersonEmail]
      
      
      ,bp.[TINNo]
      ,bp.[Comments]
      ,bp.[ActiveStatus]
      ,bp.[CreatedBy]
      ,bp.[CreatedOn]
      ,bp.[LastModifiedBy]
      ,bp.[LastModifiedOn]
      ,isnull(bp.[IsArchive],0)IsArchive
    
     
      ,bp.[Id]
  
      
      ,bp.[IsWCF]
      
      ,bp.[IsCentral]
  FROM [BranchProfiles] bp left outer join UserBranchMap um on bp.BranchID = um.BranchId

where 1=1 


";
                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue, true);

                SqlDataAdapter objComm = CreateAdapter(sqlText);

                objComm.SelectCommand = ApplyParameters(objComm.SelectCommand, conditionalFields, conditionalValue);

                objComm.Fill(dt);

                branchProfiles = dt.ToList<BranchProfile>();

                return branchProfiles;


            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
