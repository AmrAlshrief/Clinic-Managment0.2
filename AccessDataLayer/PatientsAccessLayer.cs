using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AccessDataLayer
{
    public class clsPatientDataAccess
    {
        public static bool GetPatientInfoByID(int ID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
            ref string Gender, ref string PhoneNumber, ref string Email, ref string Address)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM PatientsInfo Where PatientID = @PatientID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PatientID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    ThirdName = (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (string)reader["Gender"];

                    if (reader["PhoneNumber"] != DBNull.Value)
                    {
                        PhoneNumber = (string)reader["PhoneNumber"];


                    }
                    else
                    {
                        PhoneNumber = "";
                    }

                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];

                    }
                    else
                    {
                        Email = "";
                    }

                    if (reader["Address"] != DBNull.Value)
                    {
                        Address = (string)reader["Address"];
                    }
                    else
                    {
                        Address = "";
                    }
                }
                else
                {
                    isFound = false;
                }

                reader.Close();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

       
        public static int AddNewPatient(string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            string Gender, string PhoneNumber, string Email, string Address)
        {
            int PatientID = -1;
            int PersonID = clsPersonAccess.AddNewPerson(FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, PhoneNumber, Email, Address);
            if(PersonID != -1) 
            {
                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"INSERT INTO Patients (PersonID)
                                 VALUES (@PersonID)
                                 SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@PersonID", PersonID);

                try 
                {
                    connection.Open();

                    object result = command.ExecuteScalar();
                    connection.Close();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        PatientID = insertedID;

                    }
                }
                catch (Exception ex) 
                {

                }
                finally { connection.Close(); }
 
            }

            return PatientID;
        }


        public static bool DeletePatient(int PatientID, ref string Message) 
        {
            int PersonID = GetPersonIDFromPatient(PatientID);
            int rowsAffected = 0;
            bool DeletePerson = false;


            if (PersonID != -1) 
            {
                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"Delete Patients WHERE PatientID = @PatientID AND PersonID = @PersonID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@PatientID", PatientID);
                command.Parameters.AddWithValue("@PersonID", PersonID);

                try
                {
                    connection.Open();

                    rowsAffected = command.ExecuteNonQuery();


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

                if(rowsAffected > 0) 
                {
                    DeletePerson = clsPersonAccess.DeletePerson(PersonID, ref Message);
                }
                else 
                {
                    return false;
                }              
            }

            return DeletePerson;
        }

        public static bool UpdatePatient(int PatientID, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, 
            string Gender, string PhoneNumber, string Email, string Address, ref string Message) 
        {

            bool isPatientUpdated = false; 
            int PersonID = GetPersonIDFromPatient(PatientID);
          
            if(PersonID != -1) 
            {
                isPatientUpdated = clsPersonAccess.UpdatePerson(PersonID, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, PhoneNumber, Email, Address,ref Message);
            } 

            return isPatientUpdated;
            
        }

        public static int GetPersonIDFromPatient(int PatientID) 
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT PersonID FROM Patients WHERE PatientID = @PatientID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PatientID", PatientID);

            try 
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) 
                {
                    PersonID = (int)reader["PersonID"];
                }

                reader.Close();
            }
            catch (Exception ex) 
            { 
            }finally 
            { 
                connection.Close(); 
            }

            return PersonID;
        }

        public static bool isPatientExist(int ID) 
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found = 1 FROM Patients WHERE PatientID = @PatientID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PatientID", ID);

            try 
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception ex) 
            { 
            }
            finally { connection.Close(); }

            return isFound;
        }

    }
}
