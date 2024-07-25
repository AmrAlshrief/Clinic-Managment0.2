using ClinicBussinessDataLayer;
using NavigationBar.CustomControls;
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
    public partial class FrmSession : Form
    {
        int DistanceUnit = 1;
        List<ctrSessionControl> listOfTableLayout = new List<ctrSessionControl>();

        //ctrSessionControl testEvent = GetComponent<>
        public FrmSession()
        {
            InitializeComponent();
            AddControl();

        }

        private void GenerateCtr()
        {
            ctrSessionControl ctrSessionControl3 = new ctrSessionControl();
            //FrmSession.Controls.Add(ctrSessionControl3);
            ctrSessionControl3.Show();
            

         
            //ctrSessionControl1.Margin = new System.Windows.Forms.Margin(2, 2, 2, 2);

        }

        private void FillControlWithAppointmentData() 
        {
            DataTable dt = clsSessionsBusiness.GetAllActiveAppointment();
            

            foreach(DataRow row in dt.Rows) 
            {
                ctrSessionControl ctrSessionControl3 = new ctrSessionControl();
                ctrSessionControl3.SessionID = row["AppointmentID"].ToString();
                ctrSessionControl3.Name = row["FirstName"].ToString() + " " + row["SecondName"].ToString() + " " + row["LastName"].ToString();
                ctrSessionControl3.OnSessionStart += CtrSessionControl3_OnSessionStart;
                ctrSessionControl3.OnSessionEnd += CtrSessionControl3_OnSessionEnd;
                listOfTableLayout.Add(ctrSessionControl3);
            }
        }

        private void CtrSessionControl3_OnSessionEnd(int obj)
        {
            
            //throw new NotImplementedException();
            //ctrEditDate ctrEdit = new ctrEditDate();
            //ctrEdit.Width = ctrSessionControl3.Width;
            //this.flowLayoutPanel1.Controls.Add(ctrEdit);
            //ctrEdit.Show();
            
            FrmEditSessionDate Frm = new FrmEditSessionDate(obj);
            
            Frm.ShowDialog();

        }

        private void CtrSessionControl3_OnSessionStart(int obj)
        {
            //throw new NotImplementedException();

            FrmSessionDetails frm1 = new FrmSessionDetails(obj);
            MessageBox.Show(Convert.ToString(obj));
            frm1.ShowDialog();    

        }

        private void AddControl() 
        {
            flowLayoutPanel1.Controls.Clear();
            FillControlWithAppointmentData();
            int size = listOfTableLayout.Count;
            label2.Text = size.ToString();
            

            foreach (ctrSessionControl tableLayoutPanel in listOfTableLayout) 
            {
                
                tableLayoutPanel.Top = DistanceUnit * 83;
                tableLayoutPanel.Left = 100;
                DistanceUnit = DistanceUnit + 1;
                this.flowLayoutPanel1.Controls.Add(tableLayoutPanel);
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            //ctrSessionControl ctrSessionControl3 = new ctrSessionControl();
            //ctrSessionControl3 = tableLayoutPanel1;
            //listOfTableLayout.Add(ctrSessionControl3);

        }

 
    }
}
