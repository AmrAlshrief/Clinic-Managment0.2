using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AccessDataLayer
{
    public class clsAppointmentAccess
    {

        public static int AddNewAppointment(int DoctorID, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            string Gender, string PhoneNumber, string Email, string Address,
            DateTime RegistrationDate, DateTime AppointmentDateTime, ref string Message)
        {
            int AppointmentID = -1;
            int PatientID = clsPatientDataAccess.AddNewPatient(FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, PhoneNumber, Email, Address);

            if (PatientID != -1)
            {
                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"INSERT INTO Appointments (PatientID, DoctorID, RegistrationDate, AppointmentStatus, AppointmentDateTime)
                                 VALUES (@PatientID, @DoctorID, @RegistrationDate, @AppointmentStatus, @AppointmentDateTime)
                                 SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@PatientID", PatientID);
                command.Parameters.AddWithValue("@DoctorID", DoctorID);
                command.Parameters.AddWithValue("@RegistrationDate", RegistrationDate);

                if (AppointmentDateTime != null)
                {
                    command.Parameters.AddWithValue("@AppointmentDateTime", AppointmentDateTime);
                    if(AppointmentDateTime == DateTime.Today.Date) 
                    {
                        command.Parameters.AddWithValue("@AppointmentStatus", 1);
                    }
                }
                else
                {
                    command.Parameters.AddWithValue("@AppointmentDateTime", System.DBNull.Value);

                }

                if(AppointmentDateTime != DateTime.Today.Date) 
                {
                    command.Parameters.AddWithValue("@AppointmentStatus", 0);
                }
              

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    connection.Close();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        AppointmentID = insertedID;
                    }
                }
                catch (Exception ex)
                {

                    Message = ex.Message;
                    
                } finally { connection.Close(); }

            }

            return AppointmentID;
        }

        public static bool UpdateAppointment(int AppointmentID, int PatientID, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            string Gender, string PhoneNumber, string Email, string Address,
            DateTime RegistrationDate, DateTime AppointmentDateTime, ref string Message)
        {
            bool isPatientUpdated = false;
            if (PatientID != -1) 
            {
                isPatientUpdated = clsPatientDataAccess.UpdatePatient(PatientID, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, PhoneNumber, Email, Address, ref Message);
                if(isPatientUpdated == false)
                {
                   // Message = "Patient Data Updating Failed!";
                }
            }
            int rowAffected = 0;
            if(isPatientUpdated) 
            {
                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"Update Appointments
                                set RegistrationDate = @RegistrationDate,
                                    AppointmentStatus = @AppointmentStatus,
                                    AppointmentDateTime = @AppointmentDateTime
                                    WHERE AppointmentID = @AppointmentID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                command.Parameters.AddWithValue("@RegistrationDate", RegistrationDate);
                

                if (AppointmentDateTime != null)
                {
                    command.Parameters.AddWithValue("@AppointmentDateTime", AppointmentDateTime);
                    if (AppointmentDateTime.Date == DateTime.Today.Date)
                    {
                        command.Parameters.AddWithValue("@AppointmentStatus", 1);
                    }
                }
                else
                {
                    command.Parameters.AddWithValue("@AppointmentDateTime", System.DBNull.Value);

                }

                if (AppointmentDateTime.Date != DateTime.Today.Date)
                {
                    command.Parameters.AddWithValue("@AppointmentStatus", 0);
                   
                    
                }

                try 
                {
                    connection.Open();
                    rowAffected = command.ExecuteNonQuery();
                }catch (Exception ex) 
                {

                    Message = ex.Message;
                    return false;
                }
                finally { connection.Close(); }
            }

            return (rowAffected > 0);
        }

        public static bool UpdateAppointmentDate(int AppointmentID, DateTime AppointmentDateTime, ref string Message)
        {
            //bool isPatientUpdated = false;
            //if (PatientID != -1)
            //{
            //    isPatientUpdated = clsPatientDataAccess.UpdatePatient(PatientID, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, PhoneNumber, Email, Address, ref Message);
            //    if (isPatientUpdated == false)
            //    {
            //        // Message = "Patient Data Updating Failed!";
            //    }
            //}
            int rowAffected = 0;
            
            
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Appointments
                             set AppointmentDateTime = @AppointmentDateTime
                                 WHERE AppointmentID = @AppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

            if (AppointmentDateTime != null)
            {
                command.Parameters.AddWithValue("@AppointmentDateTime", AppointmentDateTime);
              if (AppointmentDateTime.Date == DateTime.Today.Date)
              {
                        command.Parameters.AddWithValue("@AppointmentStatus", 1);
              }
            }
            else
            {
              command.Parameters.AddWithValue("@AppointmentDateTime", System.DBNull.Value);

            }

            if (AppointmentDateTime.Date != DateTime.Today.Date)
            {
                command.Parameters.AddWithValue("@AppointmentStatus", 0);

            }

            try
            {
                connection.Open();
                rowAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

               Message = ex.Message;
               return false;
            }
            finally { connection.Close(); }
         

            return (rowAffected > 0);
        }


        public static bool DeleteAppointment(int AppointmentID, ref string Message)
        {
            int PatientID = GetPatientIDFromAppointment(AppointmentID, ref Message);
            int rowsAffected = 0;
            bool DeletePatient = false;

            Message += PatientID;

            if (PatientID != -1)
            {
                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

                string query = @"Delete Appointments WHERE AppointmentID = @AppointmentID AND PatientID = @PatientID";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                command.Parameters.AddWithValue("@PatientID", PatientID);
                

                try
                {
                    connection.Open();

                    rowsAffected = command.ExecuteNonQuery();
                    Message += PatientID + " " + AppointmentID;


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

                if (rowsAffected > 0)
                {
                    DeletePatient = clsPatientDataAccess.DeletePatient(PatientID, ref Message);
                }
                else
                {
                    return false;
                }
            }

            return DeletePatient;
        }


        public static DataTable GetAllAppointments()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM AppointmentDetails";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    dt.Load(reader);
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

            return dt;
        }

        //public static bool GetAppointmentByID(int AppointmentID, ref int PatientID, ref int DoctorID, ref string FirstName, ref string LastName, ref DateTime DateOfBirth, ref DateTime RegistrationDate, ref DateTime AppointmentDate,
        //                                      ref DateTime AppointmentTime, ref byte AppointmentStatus, ref int MedicalRecordID, ref int PaymentID)
        //{

        //    bool isFound = false;

        //    SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

        //    string query = @"SELECT * FROM AppointmentDetails Where AppointmentID = @AppointmentID";

        //    SqlCommand command = new SqlCommand(query, connection);

        //    command.Parameters.AddWithValue("@AppointmentID", AppointmentID);


        //    try
        //    {
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();

        //        if (reader.Read())
        //        {
        //            isFound = true;

        //            PatientID = (int)reader["PatientID"];
        //            DoctorID = (int)reader["DoctorID"];
        //            FirstName = (string)reader["FirstName"];
        //            LastName = (string)reader["LastName"];
        //            RegistrationDate = (DateTime)reader["RegistrationTime"];
        //            AppointmentDate = (DateTime)reader["AppointmentDate"];

        //            if (reader["AppointmentTime"] != DBNull.Value)
        //            {
        //                AppointmentTime = (DateTime)reader["AppointmentTime"];

        //            }
        //            else
        //            {
        //                AppointmentTime = DateTime.Now;
        //            }

        //            if (reader["AppointmentTime"] != DBNull.Value)
        //            {
        //                AppointmentTime = (DateTime)reader["AppointmentTime"];

        //            }
        //            else
        //            {
        //                AppointmentTime = DateTime.Now;
        //            }

        //            if (reader["AppointmentStatus"] != DBNull.Value)
        //            {
        //                AppointmentStatus = (byte)reader["AppointmentStatus"];

        //            }
        //            else
        //            {
        //                AppointmentStatus = 0;
        //            }

        //            if (reader["MedicalRecord"] != DBNull.Value)
        //            {
        //                MedicalRecordID = (int)reader["MedicalRecord"];

        //            }
        //            else
        //            {
        //                MedicalRecordID = -1;
        //            }

        //            if (reader["PaymentID"] != DBNull.Value)
        //            {
        //                PaymentID = (int)reader["PaymentID"];

        //            }
        //            else
        //            {
        //                PaymentID = -1;
        //            }
        //        }

        //        reader.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }

        //    return isFound; 
        //}
        

        public static bool GetAppointmentByID(int AppointmentID, ref int PatientID, ref int DoctorID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref string Gender, ref string Phone, ref string Email, ref string Address, ref DateTime DateOfBirth, ref DateTime RegistrationDate, ref DateTime AppointmentDate,
                                          ref DateTime AppointmentTime, ref byte AppointmentStatus, ref int MedicalRecordID, ref int PaymentID)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM AppointmentDetails2 Where AppointmentID = @AppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppointmentID", AppointmentID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    PatientID = (int)reader["PatientID"];
                    DoctorID = (int)reader["DoctorID"];
                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gender = (string)reader["Gender"];
                    Phone = (string)reader["PhoneNumber"];
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
                    RegistrationDate = (DateTime)reader["RegistrationTime"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];

                    if (reader["AppointmentTime"] != DBNull.Value)
                    {
                        AppointmentTime = (DateTime)reader["AppointmentTime"];

                    }
                    else
                    {
                        AppointmentTime = DateTime.Now;
                    }

                    if (reader["AppointmentTime"] != DBNull.Value)
                    {
                        AppointmentTime = (DateTime)reader["AppointmentTime"];

                    }
                    else
                    {
                        AppointmentTime = DateTime.Now;
                    }

                    if (reader["AppointmentStatus"] != DBNull.Value)
                    {
                        AppointmentStatus = (byte)reader["AppointmentStatus"];

                    }
                    else
                    {
                        AppointmentStatus = 0;
                    }

                    if (reader["MedicalRecord"] != DBNull.Value)
                    {
                        MedicalRecordID = (int)reader["MedicalRecordID"];

                    }
                    else
                    {
                        MedicalRecordID = -1;
                    }

                    if (reader["PaymentID"] != DBNull.Value)
                    {
                        PaymentID = (int)reader["PaymentID"];

                    }
                    else
                    {
                        PaymentID = -1;
                    }
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



        public static int GetPatientIDFromAppointment(int AppointmentID, ref string Message)
        {
            int PatientID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT PatientID FROM AppointmentDetails WHERE AppointmentID = @AppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@AppointmentID", AppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    PatientID = (int)reader["PatientID"];
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            finally
            {
                connection.Close();
            }

            return PatientID;
        }   

    }



}

