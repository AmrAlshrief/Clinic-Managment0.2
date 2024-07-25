using AccessDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBussinessDataLayer
{
    public class clsSessionsBusiness
    {
        public static DataTable GetAllActiveAppointment()
        {
            DataTable dt = clsSessionAccess.ActiveAppointment();
            DataView Dv = dt.DefaultView;

            

            return dt;
        }
    }
}
