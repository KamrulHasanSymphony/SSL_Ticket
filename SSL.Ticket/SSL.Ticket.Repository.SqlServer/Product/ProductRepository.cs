using Microsoft.Data.SqlClient;
using SSL.Common.SSL.Common.Core.ExtentionMethod;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Product;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.Topics;
using SSL.Ticket.SSL.Ticket.Models.Tasks;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSL.Ticket.SSL.Ticket.Repository.SqlServer.Product
{
    public class ProductRepository : Repository, IProductRepository
    {
        private CS.SSL.Common.Models.DbConfig _dbConfig;
        public ProductRepository(SqlConnection context, SqlTransaction transaction, CS.SSL.Common.Models.DbConfig dbConfig)
        {
            this._context = context;
            this._transaction = transaction;

            this._dbConfig = dbConfig;
        }

        public List<T_ProductsVM> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            try
            {
                string sqlText = $@" 
                Select Id,Name,IsActive
                From
                T_Products
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

                T_ProductsVM vmdata = new T_ProductsVM();



                List<T_ProductsVM> vms = dtResutl.ToList<T_ProductsVM>();


                return vms;
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }

        public List<T_ProductsVM> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public T_ProductsVM Insert(T_ProductsVM model)
        {
            string sqlText = "";
            int Id = 0;
            int transResult = 0;
            int countId;
            string SlNo;


            try
            {
                sqlText = "";
                sqlText = @" INSERT INTO T_Products(                                     
                                    Name
                                    ,IsActive
                                    ,CreateOn
                                    ,CreateBy
                                    ) 

                                    VALUES (                                     
                                     @Name
                                    ,@IsActive
                                    ,@CreateOn
                                    ,@CreateBy

                                    ) SELECT SCOPE_IDENTITY();";


                var cmdInsert = CreateCommand(sqlText);

                cmdInsert.Parameters.AddWithValue("@Name", model.Name);
                cmdInsert.Parameters.AddWithValue("@IsActive", model.IsActive);
                cmdInsert.Parameters.AddWithValue("@CreateOn", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@CreateBy", model.CreateBy );
                

                model.Id = Convert.ToInt32(cmdInsert.ExecuteScalar());
                return model;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public T_ProductsVM InsertActive(T_ProductsVM model)
        {
            throw new NotImplementedException();
        }

        public T_ProductsVM Update(T_ProductsVM model)
        {
            try
            {
                string sqlText = "";
                int count = 0;

                string query = @"  update T_Products set 

 
                
                 Name          =@Name  
                ,IsActive      =@IsActive                
                ,UpdateOn      =@UpdateOn  
                ,UpdateBy      =@UpdateBy

                       
                where  Id= @Id ";


                SqlCommand command = CreateCommand(query);
                command.Parameters.Add("@Name", SqlDbType.NChar).Value = model.Name;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = model.IsActive;
                command.Parameters.Add("@UpdateOn", SqlDbType.DateTime).Value = DateTime.Now;
                command.Parameters.Add("@UpdateBy", SqlDbType.NChar).Value = model.UpdateBy;

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
