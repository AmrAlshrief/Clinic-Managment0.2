using System;

namespace ClinicBussinessDataLayer
{
    public interface IclsAppointmentBusiness
    {
        DateTime AppointmentDate { get; set; }
        int AppointmentID { get; set; }
        byte AppointmentStatus { get; set; }
        DateTime AppointmentTime { get; set; }
        int DoctorID { get; set; }
        int MedicalRecordID { get; set; }
        int PatientID { get; set; }
        int PaymentID { get; set; }
        DateTime RegistrationDate { get; set; }



        bool Save();
    }
}