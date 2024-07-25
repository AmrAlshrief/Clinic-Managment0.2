using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
namespace AccessDataLayer
{
    public class clsAdminAccess
    {
        public static bool IsAdminFound(string UserName, string Password, ref string Message) 
        {
            bool IsAdminFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = " SELECT Found = 1 FROM Admin WHERE UserName = @UserName and Password = @Password";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            try 
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                IsAdminFound = reader.HasRows;
                reader.Close();

            }
            catch (Exception ex) 
            {
                Message = ex.Message;
                return false;
            }
            finally { connection.Close(); } 

            return IsAdminFound;
        }

        public static bool UpdatePassword(string UserName, string OldPassword, string NewPassword) 
        {
            //bool IsOldPasswordTrue= false;
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection( clsDataAccessSettings.ConnectionString);

            string query = @"Update Admin
                            set UserName = @UserName,
                            set Password = @NewPassword,
                            WHERE Password = @OldPassword";

            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@OldPassword", OldPassword);
            command.Parameters.AddWithValue("@NewPassword", NewPassword);
            command.Parameters.AddWithValue("@UserName", UserName);

            try 
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) 
            {
                return false;    
            }
            finally { connection.Close(); } 

            return rowsAffected > 0;
        }

        static void Main(string[] args)
        {
        }
    }
}
