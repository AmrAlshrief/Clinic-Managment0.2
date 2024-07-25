using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;

namespace AccessDataLayer
{
    public class clsPersonAccess
    {
        public static int AddNewPerson(string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            string Gender, string PhoneNumber, string Email, string Address)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO Persons (FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, PhoneNumber, Email, Address)
                             VALUES (@FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gender, @PhoneNumber, @Email, @Address)
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if(ThirdName != null) 
            {
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }
            else 
            {
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
            }
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);

            if (Email != null)
            {
                command.Parameters.AddWithValue("@Email", Email);
            }
            else
            {
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            }

            if (Address != null)
            {
                command.Parameters.AddWithValue("@Address", Address);
            }
            else
            {
                command.Parameters.AddWithValue("@Address", System.DBNull.Value);

            }

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();
                connection.Close();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    PersonID = insertedID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return PersonID;
        }

        public static bool DeletePerson(int ID,ref string Message) 
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete Persons WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", ID);

            try 
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex) 
            {
                Message = ex.Message;
                return false;
            }
            finally { connection.Close(); }

            return RowsAffected > 0;
        }

        public static bool UpdatePerson(int PersonID, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            string Gender, string PhoneNumber, string Email, string Address, ref string Message) 
        {
            int RowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Persons
                            set FirstName = @FirstName,
                                SecondName = @SecondName,
                                ThirdName = @ThirdName,
                                LastName = @LastName,
                                DateOfBirth = @DateOfBirth,
                                Gender = @Gender, 
                                PhoneNumber = @PhoneNumber,
                                Email = @Email,
                                Address = @Address
                                WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != null)
            {
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }
            else
            {
                command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);

            }
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
            if (Email != null)
            {
                command.Parameters.AddWithValue("@Email", Email);
            }
            else
            {
                command.Parameters.AddWithValue("@Email", System.DBNull.Value);

            }

            if (Address != null)
            {
                command.Parameters.AddWithValue("@Address", Address);
            }
            else
            {
                command.Parameters.AddWithValue("@Address", System.DBNull.Value);

            }

            try
            {
                connection.Open();

                RowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex) 
            {
                Message = ex.Message;
                return false;
            }
            finally 
            { 
                connection.Close(); 
            }

            return (RowsAffected > 0);  
        }
    }
}
