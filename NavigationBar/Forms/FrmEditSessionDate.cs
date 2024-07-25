using ClinicBussinessDataLayer;
using DevExpress.Xpo.Logger;
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
    public partial class FrmEditSessionDate : Form
    {
        private int _AppID;
        public FrmEditSessionDate(int appID)
        {
            InitializeComponent();
            _AppID = appID;
        }

        private bool DateFilter(DateTime date) 
        {
         

            if(date.Date <= DateTime.Today.Date) 
            {
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!DateFilter(dateTimePicker1.Value.Date))
            {
                MessageBox.Show("You Should Pick a Valid Date For Session!", "Error!");
            }
            else
            {
                if (clsAppointmentBusiness._UpdateAppointmentDate(_AppID, dateTimePicker1.Value.Date))
            {
                MessageBox.Show("Session Date Updated Successfully");
                this.Hide();    
            }
                else
                {
                    MessageBox.Show(clsAppointmentBusiness.LogMessage);
                }
            }
        }
    }
}
