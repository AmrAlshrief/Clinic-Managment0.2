using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessDataLayer
{
    public class clsSessionAccess
    {
        public static DataTable ActiveAppointment() 
        {
            DataTable dt = new DataTable();
            var shortDate = DateTime.Now.ToShortDateString();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM AppointmentDetails WHERE AppointmentDateTime = Convert(date, getdate())";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@shortDate", shortDate);

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



    }
}
