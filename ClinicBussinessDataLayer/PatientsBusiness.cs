using AccessDataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBussinessDataLayer
{
    public class clsPatientsBusiness : clsPerson
    {

        public enum enMode { AddNew= 0, Update =1}
        public enMode Mode = enMode.AddNew;
        private clsPatientsBusiness(int ID, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth,
            string Gender, string PhoneNumber, string Email, string Address) : base(ID, FirstName, SecondName, ThirdName, LastName, DateOfBirth,
                Gender, PhoneNumber, Email, Address)
        {


        }

        public clsPatientsBusiness() : base()
        {
            this.ID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gender = "";
            this.PhoneNumber = "";
            this.Email = "";
            this.Address = "";

            Mode = enMode.AddNew;
        }

        public static clsPatientsBusiness Find(int ID) 
        {
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Gender = "", PhoneNumber = "", Email = "", Address = "";
            DateTime DateOfBirth = DateTime.Now;

            if(clsPatientDataAccess.GetPatientInfoByID(ID, ref FirstName, ref SecondName, ref ThirdName,ref LastName, ref DateOfBirth, ref Gender, ref PhoneNumber, ref Email, ref Address)) 
            {
                return new clsPatientsBusiness(ID, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gender, PhoneNumber, Email, Address);
            }
            else 
            {
                return null;
            }

        }


        private bool _AddNewPatient() 
        {
            this.ID = clsPatientDataAccess.AddNewPatient(this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gender, this.PhoneNumber, this.Email, this.Address);

            return (this.ID != -1);
        }

        public static bool DeletePatient(int ID) 
        {
            return false;
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPatient())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

            }

            return false;
        }

    }
}
