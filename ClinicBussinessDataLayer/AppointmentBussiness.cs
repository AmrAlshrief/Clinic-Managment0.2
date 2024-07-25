using AccessDataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBussinessDataLayer
{
    public class clsAppointmentBusiness : clsPerson
    {

        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int AppointmentID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime AppointmentTime { get; set; }
        public byte AppointmentStatus { get; set; }
        public int MedicalRecordID { get; set; }
        public int PaymentID { get; set; }
        public static string LogMessage { get; set; }

        public clsAppointmentBusiness(int AppointmentID, int PatientID, int DoctorID, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, string Gender, string Phone, string Email, string Address,
                                      DateTime RegistrationDate, DateTime AppointmentDate,
                                      DateTime AppointmentTime, byte AppointmentStatus, int MedicalRecordID, int PaymentID)
        {
            this.AppointmentID = AppointmentID;
            this.PatientID = PatientID;
            this.DoctorID = DoctorID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.PhoneNumber = Phone;
            this.Email = Email;
            this.Address = Address;
            this.RegistrationDate = RegistrationDate;
            this.AppointmentDate = AppointmentDate;
            this.AppointmentTime = AppointmentTime;
            this.AppointmentStatus = AppointmentStatus;
            this.MedicalRecordID = MedicalRecordID;
            this.PaymentID = PaymentID;
            Mode = enMode.Update;
        }

        public clsAppointmentBusiness()
        {
            this.AppointmentID = -1;
            this.PatientID = -1;
            this.DoctorID = 1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gender = "";
            this.PhoneNumber = "";
            this.Email = "";
            this.Address = "";
            this.RegistrationDate = DateTime.Now;
            this.AppointmentDate = DateTime.Now.Date;
            this.AppointmentTime = DateTime.Now;
            this.AppointmentStatus = 0;
            this.MedicalRecordID = -1;
            this.PaymentID = -1;

            Mode = enMode.AddNew;
        }

        private bool _AddNewAppointment()
        {
            string Message = "";
            this.AppointmentID = clsAppointmentAccess.AddNewAppointment(this.DoctorID, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gender,
                                                                        this.PhoneNumber, this.Email, this.Address, this.RegistrationDate, this.AppointmentDate, ref Message);
            LogMessage = Message;
            return (this.AppointmentID != -1);
        }

        private bool _UpdateAppointment()
        {
            string Message = "";
            bool isAppointmentUpdated = false;
            isAppointmentUpdated = clsAppointmentAccess.UpdateAppointment(this.AppointmentID, this.PatientID, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gender,
                                                          this.PhoneNumber, this.Email, this.Address, this.RegistrationDate, this.AppointmentDate, ref Message);
            LogMessage = Message;
            return isAppointmentUpdated;

        }

        public static bool _UpdateAppointmentDate(int AppointmentID, DateTime AppointmentDate)
        {
            string Message = "";
            bool isAppointmentUpdated = false;
            isAppointmentUpdated = clsAppointmentAccess.UpdateAppointmentDate(AppointmentID, AppointmentDate, ref Message);
            LogMessage = Message;

            return isAppointmentUpdated;
        }

        public static bool DeleteAppointment(int AppointmentID)
        {
            string Message = "";
            bool isDeleted = clsAppointmentAccess.DeleteAppointment(AppointmentID, ref Message);
            LogMessage = Message;
            return isDeleted;
        }


        public static DataView GetAllAppointment()
        {
            DataTable dt1 = clsAppointmentAccess.GetAllAppointments();

            DataView DV1 = dt1.DefaultView;

            return DV1;
        }

        public static clsAppointmentBusiness Find(int ID)
        {
            int PatientID = -1, DoctorID = -1, MedicalRecordID = -1, PaymentID = -1;
            byte AppointmentStatus = 0;
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Gender = "", Phone = "", Email = "", Address = "";
            DateTime DateOfBirth = DateTime.Now, RegistrationDate = DateTime.Now, AppointmentDate = DateTime.Now, AppointmentTime = DateTime.Now;

            if (clsAppointmentAccess.GetAppointmentByID(ID, ref PatientID, ref DoctorID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref Gender, ref Phone, ref Email, ref Address,
                ref DateOfBirth, ref RegistrationDate, ref AppointmentDate, ref AppointmentTime, ref AppointmentStatus, ref MedicalRecordID, ref PaymentID))
            {
                if (Gender == "")
                {
                    return null;
                }
                return new clsAppointmentBusiness(ID, PatientID, DoctorID, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, Phone, Email, Address, RegistrationDate, AppointmentDate, AppointmentTime, AppointmentStatus, MedicalRecordID, PaymentID);

            }
            else
            {
                return null;
            }

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewAppointment())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    if (_UpdateAppointment())
                    {
                        return true;

                    }
                    //LogMessage = "Update Appointment Return false!";
                    return false;
            }

            return false;
        }
    }
}
