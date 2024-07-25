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
    public partial class FrmAddEditAppointment : Form
    {
        public static FrmAddEditAppointment FrmAddEditAppointmentInstance;
        public enum enMode { AddNew = 0, Update = 1};
        public enMode _Mode;
        int _AppointmentID;
        //private IclsAppointmentBusiness _Appointment = new clsAppointmentBusiness();
        private clsAppointmentBusiness _Appointment = new clsAppointmentBusiness();
        //public static clsPatientsBusiness _Patient;


        public FrmAddEditAppointment(int AppointmentID)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;

            if (_AppointmentID == -1)
            {
                _Mode = enMode.AddNew;
            }
            else
            {
                _Mode = enMode.Update;
            }
            FrmAddEditAppointmentInstance = this;
            //GetDataOfPatientForm();
            //FrmAddNewPatient.
        }


        private void _FillPatientInfoByData() // Call this method only during Update Mode and after load data of appointment
        {
            
              lblPatientID.Text = _Appointment.PatientID.ToString();
              lblName.Text = _Appointment.FirstName + " " + _Appointment.SecondName + " " + _Appointment.ThirdName + " " + _Appointment.LastName;
              lblPhone.Text = _Appointment.PhoneNumber;


        }

        private void _SaveData() 
        {
            _Appointment.RegistrationDate = DateTime.Now;
            _Appointment.AppointmentDate = dtAppointmentDate.Value.Date;

            if (_Appointment.Save()) 
            {
                MessageBox.Show("Data Saved Successfully!");
            }
            else 
            {
                MessageBox.Show("Error: Data Saving failed!");
                MessageBox.Show(clsAppointmentBusiness.LogMessage);
            }

            
            _Mode = enMode.Update;
            lblMode.Text = "Edit Appointment";
        }

        private void _LoadData() 
        {
            if(_Mode == enMode.AddNew) 
            {
                lblMode.Text = "Add New Appointment";
                _Appointment = new clsAppointmentBusiness();
                return;
            } 
            else 
            {
                _Appointment = clsAppointmentBusiness.Find(_AppointmentID);

                if(_Appointment == null) 
                {
                    MessageBox.Show("this Form Will be closed no Appointment Have this ID Error!");
                    this.Close();
                    return;
                }
                lblMode.Text = "Edit Appointment";
                lblAppointmentID.Text = _AppointmentID.ToString();
                _FillPatientInfoByData();

            }
        }



        private void btnAddNewPatient_Click(object sender, EventArgs e)
        {
            FrmAddNewPatient frm = new FrmAddNewPatient(_Appointment, this);
            frm.DataBack += GetDataOfPatientForm;
            frm.ShowDialog();
           
            
        }

        private void GetDataOfPatientForm(object sender)
        {
            _Appointment = sender as clsAppointmentBusiness;

            if (_Appointment != null)
            {
                lblName.Text = _Appointment.FirstName + " " + _Appointment.SecondName + " " + _Appointment.ThirdName + " " + _Appointment.LastName;
                lblPatientID.Text = "...";
                lblPhone.Text = _Appointment.PhoneNumber;


            }

        }

        private void FrmAddEditAppointment_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _SaveData();
        }
    }
}
