using Microsoft.Data.SqlClient;
using SSL.CS.SSL.Common.Models;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Repository.UserProfile;
using SSL.Ticket.SSL.Ticket.Models;
using System;
using System.Data;

namespace SSL.Ticket.SSL.Ticket.Repository.SqlServer.UserProfile
{
    public class UserProfileRepository : Repository, IUserProfileRepository
    {
        private CS.SSL.Common.Models.DbConfig _dbConfig;
        private static Random random = new Random();

        public UserProfileRepository(SqlConnection context, SqlTransaction transaction, CS.SSL.Common.Models.DbConfig dbConfig)
        {
            this._context = context;
            this._transaction = transaction;
            _dbConfig = dbConfig;
        }

        public List<TUserProfile> GetAll(string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public List<TUserProfile> GetIndexData(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public int GetIndexDataCount(IndexModel index, string[] conditionalFields, string[] conditionalValue, PeramModel vm = null)
        {
            throw new NotImplementedException();
        }

        public TUserProfile Insert(TUserProfile model)
        {
            string sqlText = "";
            int Id = 0;
            int transResult = 0;
            int countId;
            string SlNo;
            var oTP = GenerateOTP();

            try
            {
                sqlText = @"
                            INSERT INTO T_UserProfile(
                                FullName, Email, LogId, Password,Address,VerificationCode, AuthId,DepartmentId,ClientId,IsClient,
                                IsActive, IsVerified, IsArchived, CreatedBy, CreatedAt, CreatedFrom
                            ) 
                            VALUES (
                                @FullName, @Email, @LogId, @Password,@Address, @VerificationCode, @AuthId,@DepartmentId,@ClientId,@IsClient,
                                @IsActive, @IsVerified, @IsArchived, @CreatedBy, @CreatedAt, @CreatedFrom
                            );
                            SELECT SCOPE_IDENTITY();
                        ";

                var cmdInsert = CreateCommand(sqlText);

                // Clear any previously added parameters, if applicable


                cmdInsert.Parameters.Clear();

                // Add parameters
                cmdInsert.Parameters.AddWithValue("@FullName", model.FullName);
                cmdInsert.Parameters.AddWithValue("@Email", model.Email);
                cmdInsert.Parameters.AddWithValue("@LogId", model.LogId);
                cmdInsert.Parameters.AddWithValue("@Password", model.Password);
                cmdInsert.Parameters.AddWithValue("@Address", "");
                cmdInsert.Parameters.AddWithValue("@VerificationCode", oTP);
                cmdInsert.Parameters.AddWithValue("@AuthId", (object)model.AuthId ?? DBNull.Value);
                cmdInsert.Parameters.AddWithValue("@DepartmentId", (object)model.DepartmentId ?? DBNull.Value);
                cmdInsert.Parameters.AddWithValue("@ClientId", (object)model.ClientId ?? DBNull.Value);
                cmdInsert.Parameters.AddWithValue("@IsClient", 1);
                cmdInsert.Parameters.AddWithValue("@IsActive", 0);
                cmdInsert.Parameters.AddWithValue("@IsVerified", 0);
                cmdInsert.Parameters.AddWithValue("@IsArchived", 0);
                if (model.CreatedBy != null)
                {
                    cmdInsert.Parameters.AddWithValue("@CreatedBy", model.CreatedBy);
                }
                else
                {
                    cmdInsert.Parameters.AddWithValue("@CreatedBy", model.LogId);
                }
                cmdInsert.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                cmdInsert.Parameters.AddWithValue("@CreatedFrom", "");

                // Execute the command and assign the new Id
                model.Id = Convert.ToInt32(cmdInsert.ExecuteScalar());

                return model;              

            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public static string GenerateOTP(int length = 6)
        {

            // Generate a random number with the specified length
            var otp = random.Next((int)Math.Pow(10, length - 1), (int)Math.Pow(10, length)).ToString();
            return otp;
        }

        public TUserProfile InsertActive(TUserProfile model)
        {
            throw new NotImplementedException();
        }

        public TUserProfile Update(TUserProfile model)
        {
            try
            {
                string sqlText = "";
                int count = 0;

                string query = @"  update T_UserProfile set 

 
                                    FullName              =@FullName                                     
                                    ,Password             =@Password  
                                    ,VerificationCode     =@VerificationCode  
                                    ,AuthId               =@AuthId                                   
                                    ,LastUpdateBy         =@LastUpdateBy   
                                    ,LastUpdateAt         =@LastUpdateAt  
                                    ,LastUpdateFrom       =@LastUpdateFrom  

                       
                                    where  AuthId= @AuthId ";


                SqlCommand command = CreateCommand(query);
                command.Parameters.Add("@FullName", SqlDbType.NChar).Value = model.FullName;                
                command.Parameters.Add("@Password", SqlDbType.NChar).Value = model.Password;
                command.Parameters.Add("@VerificationCode", SqlDbType.NChar).Value = "";
                command.Parameters.Add("@AuthId", SqlDbType.NChar).Value = model.AuthId;               
                command.Parameters.Add("@LastUpdateBy", SqlDbType.NChar).Value = model.LastUpdateBy;
                command.Parameters.Add("@LastUpdateAt", SqlDbType.NChar).Value = DateTime.Now;
                command.Parameters.Add("@LastUpdateFrom", SqlDbType.NChar).Value = "";


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

        public TUserProfile UpdateUserProfile(TUserProfile userProfile)
        {
            try
            {
                string sqlText = "";
                int count = 0;

                string query = @"  update T_UserProfile set 

 
                                    IsActive = @IsActive,
                                    IsVerified = @IsVerified

                       
                                    where  LogId= @LogId ";


                SqlCommand command = CreateCommand(query);
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = 1;
                command.Parameters.Add("@IsVerified", SqlDbType.Bit).Value = 1;
                command.Parameters.Add("@LogId", SqlDbType.NChar).Value = userProfile.LogId;


                int rowcount = command.ExecuteNonQuery();

                if (rowcount <= 0)
                {
                    throw new Exception(MessageModel.UpdateFail);
                }

                return userProfile;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
