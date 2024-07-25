using ClinicBussinessDataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavigationBar.Forms
{
    public partial class FrmSessionDetails : Form
    {
        private clsAppointmentBusiness _Appointment = new clsAppointmentBusiness();
        private int ID;
        private string _Name;
        public FrmSessionDetails(int ID)
        {
            InitializeComponent();
            _Appointment.AppointmentID = ID;
            _Name =_Appointment.FirstName + _Appointment.LastName;
        }

        private void FrmSessionDetails_Load(object sender, EventArgs e)
        {
            lblPatientID.Text = Convert.ToString(_Appointment.AppointmentID);
            //lblName.Text = _Name;
        }

        
    }
}
