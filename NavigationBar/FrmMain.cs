using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavigationBar
{
    public partial class FrmMain : Form
    {
        private Button CurrentButton;
        private Form activeForm = null;
        
        public FrmMain()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }

        private Color SelectThemeColor(byte index) 
        {
            string color = ThemeColor.ColorList[index];

            return ColorTranslator.FromHtml(color);
        }

        private void ActiveButton(object btnSender) 
        {
            if(btnSender != null) 
            {
                if(CurrentButton != (Button)btnSender) 
                {
                    DisableButton();
                    Color color = SelectThemeColor(2);
                    CurrentButton = (Button)btnSender;
                    CurrentButton.BackColor = color;
                    CurrentButton.ForeColor = Color.White;
                    CurrentButton.Font = new System.Drawing.Font("Segoe UI", 12.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    pnTitleBar.BackColor = color;
                    //lblTitle.Text = CurrentButton.Text;
                    lblUser.Text = FrmLogin.CurrentUser;
                }
                


            }
        }

        private void OpenChildForm(Form childForm, object btnSender) 
        {
            if(activeForm != null) 
            {
                activeForm.Close();
            }
            ActiveButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.pnDesktopPanel.Controls.Add(childForm);
            this.pnDesktopPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }

        private void DisableButton() 
        {
            foreach(Control PreviousBtn in panelMenu.Controls) 
            {
                if(PreviousBtn.GetType()== typeof(Button)) 
                {
                    PreviousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    PreviousBtn.ForeColor = Color.Gainsboro;
                    PreviousBtn.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void btnAdminDashboard_Click(object sender, EventArgs e)
        {
            //ActiveButton(sender);
            OpenChildForm(new Forms.FrmAdminDashboard(), sender);
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            //ActiveButton(sender);
            OpenChildForm(new Forms.FrmEmployees(), sender);
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            //ActiveButton(sender);
            OpenChildForm(new Forms.FrmPatients(), sender);

        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FrmAppointments(), sender);
        }

        private void btnSession_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FrmSession(), sender);
        }


        private void btnSettings_Click(object sender, EventArgs e)
        {
            ActiveButton(sender);


        }

       
        private void btnClose_Click(object sender, EventArgs e)
        {
            FrmLogin frm = new FrmLogin();
            this.FormClosed -= (s, args) => this.Close();
            this.Hide();
            frm.FormClosed += (s, args) => this.Close();
            frm.Show();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblWatch.Text = DateTime.Now.ToString();
        }

       
    }
}
