using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBussinessDataLayer
{
    public class clsPerson
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        protected clsPerson() 
        {
        }
        protected clsPerson(int ID, string FirstName, string SecondName, string ThirdName, string LastName, DateTime DateOfBirth, 
            string gender, string PhoneNumber, string Email, string Address) 
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = gender;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.Address = Address;

        }

    }
}
