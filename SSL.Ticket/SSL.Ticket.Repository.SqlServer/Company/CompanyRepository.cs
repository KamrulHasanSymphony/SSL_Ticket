using Microsoft.Data.SqlClient;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.ExtentionMethod;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Company;
using System.Data;

namespace SSL.Ticket.SSL.Ticket.Repository.SqlServer.Company
{
    public class CompanyRepository : Repository, ICompanyRepository
    {
        private CS.SSL.Common.Models.DbConfig _dbConfig;
        public CompanyRepository(SqlConnection context, SqlTransaction transaction, CS.SSL.Common.Models.DbConfig dbConfig)
        {
            this._context = context;
            this._transaction = transaction;
            this._dbConfig = dbConfig;
        }

        public List<Models.CompanyInfo> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {

            try
            {
                
                string sqlText = @"
                SELECT CompanyId
	            ,CompanyName
	            ,CompanyLegalname
	            ,Address
	            ,City
	            ,ZipCode
	            ,TelephoneNo
	            ,FaxNo
	            ,Email
	            ,ContactPerson
	            ,ContactPersonDesignation
	            ,ContactPersonTelephone
	            ,ContactPersonEmail
	            ,TINNo
	            ,BIN
	            ,VatRegistrationNo
	            ,Comments
	            ,ActiveStatus
              FROM SSLSupportDB_DevV2.dbo.[CompanyInfo]

                where 1=1 
                "
                ;
                sqlText = ApplyConditions(sqlText, conditionalFields, conditionalValue);




                SqlCommand objComm = CreateCommand(sqlText);

                objComm = ApplyParameters(objComm, conditionalFields, conditionalValue);

                SqlDataAdapter adapter = new SqlDataAdapter(objComm);
                DataTable dtResutl = new DataTable();
                adapter.Fill(dtResutl);

                string Remarks = null;

                Models.CompanyInfo vmdata = new Models.CompanyInfo();



                List<Models.CompanyInfo> vms = dtResutl.ToList<Models.CompanyInfo>();


                return vms;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Models.CompanyInfo> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public Models.CompanyInfo Insert(Models.CompanyInfo model)
        {
            try
            {
                // Check if there is already a company in the database
                //var countCommand = CreateCommand(@"SELECT COUNT(*) FROM CompanyInfo");
                //int count = (int)countCommand.ExecuteScalar();
                //if (count > 0)
                //{
                //    throw new Exception("Cannot insert another company.");
                //}

                // Proceed with inserting the new company
                string sqlText = "";
                //  string[] retResults = { "Fail", "Fail", Id.ToString(), sqlText, "ex", "Insert" };

                var command = CreateCommand(@" INSERT INTO CompanyInfo(

                                                CompanyName
                                                ,CompanyLegalName
                                                ,Address
                                                ,City
                                                ,ZipCode
                                                ,TelephoneNo
                                                ,FaxNo
                                                ,Email
                                                ,ContactPerson
                                                ,ContactPersonDesignation
                                                ,ContactPersonTelephone
                                                ,ContactPersonEmail
                                                ,Comments
                                                ,ActiveStatus
                                                ,CreatedBy
                                                ,CreatedOn
                                                ) 
                                                VALUES (
                                                @CompanyName
                                                ,@CompanyLegalName
                                                ,@Address
                                                ,@City
                                                ,@ZipCode
                                                ,@TelephoneNo
                                                ,@FaxNo
                                                ,@Email
                                                ,@ContactPerson
                                                ,@ContactPersonDesignation
                                                ,@ContactPersonTelephone
                                                ,@ContactPersonEmail
                                                ,@Comments
                                                ,@ActiveStatus
                                                ,@CreatedBy
                                                ,@CreatedOn
                                                ) SELECT SCOPE_IDENTITY()");

                command.Parameters.AddWithValue("@CompanyName", model.CompanyName);
                command.Parameters.AddWithValue("@CompanyLegalName", model.CompanyLegalName);
                command.Parameters.AddWithValue("@Address", model.Address) ;
                command.Parameters.AddWithValue("@City",model.City);
                command.Parameters.AddWithValue("@ZipCode",model.ZipCode);
                command.Parameters.AddWithValue("@TelephoneNo",model.TelephoneNo);
                command.Parameters.AddWithValue("@FaxNo",model.FaxNo);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@ContactPerson", model.ContactPerson);
                command.Parameters.AddWithValue("@ContactPersonDesignation", model.ContactPersonDesignation);
                command.Parameters.AddWithValue("@ContactPersonTelephone", model.ContactPersonTelephone);
                command.Parameters.AddWithValue("@ContactPersonEmail",model.ContactPersonEmail);
                command.Parameters.AddWithValue("@Comments",model.Comments);
                command.Parameters.AddWithValue("@ActiveStatus", model.ActiveStatus);
                command.Parameters.AddWithValue("@CreatedBy",model.CreatedBy);
                command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);



                model.CompanyID = Convert.ToInt32(command.ExecuteScalar());


                return model;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Models.CompanyInfo InsertActive(Models.CompanyInfo model)
        {
            throw new NotImplementedException();
        }

        public Models.CompanyInfo Update(Models.CompanyInfo model)
        {
            try
            {
                string sqlText = "";
                int count = 0;

                string query = @"  update CompanyInfo set 

 
CompanyName              =@CompanyName
,CompanyLegalName        =@CompanyLegalName 
,Address                 =@Address  
,City                    =@City  
,ZipCode                 =@ZipCode  
,TelephoneNo             =@TelephoneNo  
,FaxNo                   =@FaxNo  
,Email                   =@Email  
,ContactPerson           =@ContactPerson  
,ContactPersonDesignation=@ContactPersonDesignation   
,ContactPersonTelephone  =@ContactPersonTelephone  
,ContactPersonEmail      =@ContactPersonEmail 

,Comments                =@Comments  
,ActiveStatus            =@ActiveStatus  
,LastModifiedBy               =@LastModifiedBy  
,LastModifiedOn               =@LastModifiedOn  

                       
where  CompanyID= @CompanyID ";


                SqlCommand command = CreateCommand(query);

                command.Parameters.Add("@CompanyName", SqlDbType.NChar).Value = model.CompanyName;
                command.Parameters.Add("@CompanyLegalName", SqlDbType.NChar).Value = model.CompanyLegalName;

                command.Parameters.Add("@Address", SqlDbType.NChar).Value = model.Address;
                command.Parameters.Add("@City", SqlDbType.NChar).Value = model.City;

                command.Parameters.Add("@ZipCode", SqlDbType.NChar).Value = model.ZipCode;
                command.Parameters.Add("@TelephoneNo", SqlDbType.NChar).Value = model.TelephoneNo;
                command.Parameters.Add("@FaxNo", SqlDbType.NChar).Value = model.FaxNo;
                //command.Parameters.Add("@CurrencyCode", SqlDbType.NChar).Value = model.CurrencyCode;
                command.Parameters.Add("@Email", SqlDbType.NChar).Value = model.Email;
                command.Parameters.Add("@ContactPerson", SqlDbType.NChar).Value = model.ContactPerson;
                command.Parameters.Add("@ContactPersonDesignation", SqlDbType.NChar).Value = model.ContactPersonDesignation;
                command.Parameters.Add("@ContactPersonEmail", SqlDbType.NChar).Value = model.ContactPersonEmail;
                command.Parameters.Add("@Comments", SqlDbType.NChar).Value = model.Comments;
                command.Parameters.Add("@ActiveStatus", SqlDbType.NChar).Value = model.ActiveStatus;
                command.Parameters.Add("@ContactPersonTelephone", SqlDbType.NChar).Value = model.ContactPersonTelephone;



                command.Parameters.Add("@LastModifiedBy", SqlDbType.NChar).Value = "";

                command.Parameters.Add("@LastModifiedOn", SqlDbType.NChar).Value = DateTime.Now;

                command.Parameters.Add("@CompanyID", SqlDbType.Int).Value = model.CompanyID;


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
