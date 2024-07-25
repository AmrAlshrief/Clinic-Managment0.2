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
    public partial class FrmAddNewPatient : Form
    {

        public delegate void DataBackEventHandler(object sender);
        public event DataBackEventHandler DataBack;

        private FrmAddEditAppointment frmAddEditAppointment1;
        private clsAppointmentBusiness _Appointment;
        public static StData Data1;
        //private static DataTable dt;
        public struct  StData
        {
            public string FirstName;
            public  string LastName;
            public  DateTime DateOfBirth;
            public string Gender;
            public string PhoneNumber;
            public string Email;
            public string Address;
        }

        


       
        public FrmAddNewPatient(clsAppointmentBusiness _Appointment, FrmAddEditAppointment addEditAppointment)
        {
            InitializeComponent();

            frmAddEditAppointment1 = addEditAppointment as FrmAddEditAppointment;
            this._Appointment = _Appointment;
        }

        private  void SaveData() 
        {
            

            Data1.FirstName = txtFirstName.Text;
            Data1.LastName = txtLastName.Text;
            Data1.DateOfBirth = dtDateOfBirth.Value;
            Data1.Gender = txtGender.Text;
            Data1.PhoneNumber = txtPhoneNumber.Text;
            Data1.Email = txtEmail.Text;
            Data1.Address = txtAddress.Text;
        }

        public void LoadDataToPatientObject() 
        {
            if(frmAddEditAppointment1._Mode == FrmAddEditAppointment.enMode.Update) 
            {

                if (_Appointment != null)
                {

                    this.Text = "Edit Patient Info";

                    txtFirstName.Text =_Appointment.FirstName;
                    txtSecondName.Text = _Appointment.SecondName;
                    txtThirdName.Text = _Appointment.ThirdName;
                    txtLastName.Text = _Appointment.LastName;
                    dtDateOfBirth.Value = _Appointment.DateOfBirth;
                    txtGender.Text = _Appointment.Gender;
                    txtPhoneNumber.Text = _Appointment.PhoneNumber;
                    txtEmail.Text = _Appointment.Email;
                    txtAddress.Text = _Appointment.Address;


                }

            }
            
        }

        public  clsAppointmentBusiness AddNewPatient() 
        {

            _Appointment.FirstName = txtFirstName.Text;
            _Appointment.SecondName = txtSecondName.Text;
            _Appointment.ThirdName = txtThirdName.Text;
            _Appointment.LastName = txtLastName.Text;
            _Appointment.DateOfBirth = dtDateOfBirth.Value;
            _Appointment.Gender = txtGender.Text;
            _Appointment.PhoneNumber = txtPhoneNumber.Text;
            _Appointment.Email = txtEmail.Text;
            _Appointment.Address = txtAddress.Text;
             
            return _Appointment;

        }



         

        private void btnAddNew_Click(object sender, EventArgs e)
        {

            //SaveData(); 
            _Appointment = AddNewPatient();
            DataBack?.Invoke(_Appointment);
            this.Close();

        }

      

        private void FrmAddNewPatient_Load(object sender, EventArgs e)
        {
            LoadDataToPatientObject();
        }

       
    }
}
