using Microsoft.Data.SqlClient;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.ExtentionMethod;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Clients;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.TktEnternalNote;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Repository.SqlServer.Clients
{
    public class ClientsRepository : Repository, IClientsRepository
    {
        private CS.SSL.Common.Models.DbConfig _dbConfig;
        public ClientsRepository(SqlConnection context, SqlTransaction transaction, CS.SSL.Common.Models.DbConfig dbConfig)
        {
            this._context = context;
            this._transaction = transaction;
            this._dbConfig = dbConfig;
        }

        public List<T_ClientsVm> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            try
            {
                string sqlText = $@" 
                SELECT
				        Id,
						Name,
						Address,
						City,
						District,
						CompanyContactNumber,
						CompanyEmail,
						Website,
						ConcernPerson,
						ContactPersonNumber,
						ContactPersonEmail,
						CreateOn,
						CreateBy,
						UpdateOn,
						UpdateBy,
						IsActive
                FROM T_Clients
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

                T_ClientsVm vmdata = new T_ClientsVm();



                List<T_ClientsVm> vms = dtResutl.ToList<T_ClientsVm>();


                return vms;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public List<T_ClientsVm> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public T_ClientsVm Insert(T_ClientsVm model)
        {
            string sqlText = "";
            int Id = 0;
            int transResult = 0;
            int countId;
            string SlNo;


            try
            {
                sqlText = "";
                sqlText = @" INSERT INTO T_Clients(
                                     Name
                                    ,Address
                                    ,City
                                    ,District
                                    ,CompanyContactNumber
                                    ,CompanyEmail
                                    ,Website
                                    ,ConcernPerson
                                    ,ContactPersonNumber
                                    ,ContactPersonEmail
                                    ,CreateOn
                                    ,CreateBy
                                    ,IsActive
                                    
                                    ) 

                                    VALUES (
                                     @Name
                                    ,@Address
                                    ,@City
                                    ,@District
                                    ,@CompanyContactNumber
                                    ,@CompanyEmail
                                    ,@Website
                                    ,@ConcernPerson
                                    ,@ContactPersonNumber
                                    ,@ContactPersonEmail
                                    ,@CreateOn
                                    ,@CreateBy
                                    ,@IsActive
                                    ) SELECT SCOPE_IDENTITY();";


                var cmdInsert = CreateCommand(sqlText);

                cmdInsert.Parameters.AddWithValue("@Name", model.Name);
                cmdInsert.Parameters.AddWithValue("@Address", model.Address);
                cmdInsert.Parameters.AddWithValue("@City", model.City);
                cmdInsert.Parameters.AddWithValue("@District", model.District ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@CompanyContactNumber", model.CompanyContactNumber ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@CompanyEmail", model.CompanyEmail ?? Convert.DBNull);

                cmdInsert.Parameters.AddWithValue("@Website", model.Website ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@ConcernPerson", model.ConcernPerson ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@ContactPersonNumber", model.ContactPersonNumber ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@ContactPersonEmail", model.ContactPersonEmail ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@CreateOn", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@CreateBy", model.CreateBy ?? Convert.DBNull);
                cmdInsert.Parameters.AddWithValue("@IsActive", model.IsActive );

                model.Id = Convert.ToInt32(cmdInsert.ExecuteScalar());
                return model;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public T_ClientsVm InsertActive(T_ClientsVm model)
        {
            throw new NotImplementedException();
        }

        public T_ClientsVm Update(T_ClientsVm model)
        {
            try
            {
                string sqlText = "";
                int count = 0;

                string query = @"  update T_Clients set 

 
                Name              =@Name
                ,Address          =@Address  
                ,City             =@City  
                ,District         =@District  
                ,CompanyContactNumber =@CompanyContactNumber  
                ,CompanyEmail     =@CompanyEmail  
                ,Website          =@Website  
                ,ConcernPerson    =@ConcernPerson  
                ,ContactPersonNumber =@ContactPersonNumber   
                ,ContactPersonEmail =@ContactPersonEmail
                ,UpdateOn = @UpdateOn
                ,UpdateBy = @UpdateBy
                ,IsActive = @IsActive

                       
                where  Id= @Id ";


                SqlCommand command = CreateCommand(query);
                command.Parameters.Add("@Name", SqlDbType.NChar).Value = model.Name;
                command.Parameters.Add("@Address", SqlDbType.NChar).Value = model.Address;
                command.Parameters.Add("@City", SqlDbType.NChar).Value = model.City;
                command.Parameters.Add("@District", SqlDbType.NChar).Value = model.District;
                command.Parameters.Add("@CompanyContactNumber", SqlDbType.NChar).Value = model.CompanyContactNumber;
                command.Parameters.Add("@CompanyEmail", SqlDbType.NChar).Value = model.CompanyEmail;
                command.Parameters.Add("@Website", SqlDbType.NChar).Value = model.Website;
                command.Parameters.Add("@ConcernPerson", SqlDbType.NChar).Value = model.ConcernPerson;
                command.Parameters.Add("@ContactPersonNumber", SqlDbType.NChar).Value = model.ContactPersonNumber;
                command.Parameters.Add("@ContactPersonEmail", SqlDbType.NChar).Value = model.ContactPersonEmail;
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
