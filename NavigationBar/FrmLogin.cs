using ClinicBussinessDataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavigationBar
{
    public partial class FrmLogin : Form
    {

        private static string LoginMessage;
        private static int LoginCounter = 3;
        public static string CurrentUser; 
        FrmMain frm = new FrmMain();


        public FrmLogin()
        {
            InitializeComponent();
        }

       

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            dxErrorProvider1.DataSource = txtUserName;
            if (string.IsNullOrEmpty(txtUserName.Text)) 
            {
                e.Cancel = true;
                txtUserName.Focus();
                btnLogin.Enabled = false;
                errorProvider1.SetError(txtUserName, "UserName Cannot Be Empty Or Null!");
                //dxErrorProvider1.SetError(txtUserName, "Empty Field");
                
            }
            else 
            {
                e.Cancel= false;
                errorProvider1.SetError(txtUserName, "");
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                e.Cancel = true;
                txtPassword.Focus();
                dxErrorProvider1.SetError(txtPassword, "Please Enter a valid Password!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtUserName, "");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool CheckValidation = clsAdminBussiness.checkUserAndPassword(txtUserName.Text, txtPassword.Text);

            if (CheckValidation) 
            {
                CurrentUser = txtUserName.Text; 
                frm.Show();
                this.Hide();
                frm.FormClosed += (s, args) => this.Close(); 
                
            }
             else
            {
                LoginCounter--;
                lblNumberOfFailsLogin.Text = "Wrong UserName or Password! Try Again you have " + LoginCounter.ToString() + " Tries Left";
                MessageBox.Show(clsAdminBussiness.LogMessage);
            }
            

            
        }

        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
           this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            txtUserName.Validating -= txtUserName_Validating;

            if (txtUserName.Focused)
            {
                this.ActiveControl = null;
                
            }
            //txtUserName.
        }
    }
}
