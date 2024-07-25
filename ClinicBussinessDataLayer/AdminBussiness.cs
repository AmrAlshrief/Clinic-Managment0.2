using AccessDataLayer;
using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace ClinicBussinessDataLayer
{
    public class clsAdminBussiness
    {
        private enum enMode {UpdateUserName = 0, UpdatePassword = 1, UpdateUserNameAndPassWord = 2, CheckPassword = 3}
        private string _UserName { get; set; }
        private string _Password { get; set; }

        public static string LogMessage { get; set; }


        private static bool isAdminExist(string UserName, string Password)
        {
            string Message = "";

            bool Result = clsAdminAccess.IsAdminFound(UserName, Password, ref Message);
            // Message2 = Message;
            LogMessage = Message;

            return Result;
        }

        public static bool checkUserAndPassword(string UserName, string Password) 
        {
            return (isAdminExist(UserName, Password));
            
        }

        static void Main(string[] args)
        {
        }

    }
}
