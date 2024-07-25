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
    public partial class FrmAppointments : Form
    {
        public FrmAppointments()
        {
            InitializeComponent();
            _RefreshAppointmentsList();
        }

        private void _RefreshAppointmentsList() 
        {
            DataView dv = clsAppointmentBusiness.GetAllAppointment();
            //string Name = dv.Table.Columns[0].ColumnName;
            dgvAllAppointments.DataSource = clsAppointmentBusiness.GetAllAppointment();
            ChangeDataGridLanguage();
           // MessageBox.Show(Name);
        }

        private void ChangeDataGridLanguage() 
        {
            dgvAllAppointments.RightToLeft = RightToLeft.Yes;
            dgvAllAppointments.Columns[0].HeaderCell.Value = "كود الحجز";
            dgvAllAppointments.Columns[1].HeaderCell.Value = "كود المريض";
            dgvAllAppointments.Columns[2].HeaderCell.Value = "كود الطبيب";
            dgvAllAppointments.Columns[3].HeaderCell.Value = "الاسم الأول";
            dgvAllAppointments.Columns[4].HeaderCell.Value = "اسم الأب";
            dgvAllAppointments.Columns[5].HeaderCell.Value = "اسم الجد";
            dgvAllAppointments.Columns[6].HeaderCell.Value = "اسم العائلة";
            dgvAllAppointments.Columns[7].HeaderCell.Value = "رقم الهاتف";
            dgvAllAppointments.Columns[8].HeaderCell.Value = "تاريخ تسجيل الزيارة";
            dgvAllAppointments.Columns[9].HeaderCell.Value = "موعد الزيارة";
            dgvAllAppointments.Columns[10].HeaderCell.Value = "حالة الزيارة";
            dgvAllAppointments.Columns[11].HeaderCell.Value = "كود العلاج";
            //dgvAllAppointments.ColumnHeadersVisible = false;
            //dgvAllAppointments.Width = 200;
            //dgvAllAppointments



        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddApp_Click(object sender, EventArgs e)
        {
            FrmAddEditAppointment frm = new FrmAddEditAppointment(-1);

            frm.ShowDialog();
            _RefreshAppointmentsList();

        }

        private void Edit_Click(object sender, EventArgs e)
        {
            FrmAddEditAppointment frm = new FrmAddEditAppointment((int)dgvAllAppointments.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshAppointmentsList();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete appointment [" + dgvAllAppointments.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK) 
            {
                if (clsAppointmentBusiness.DeleteAppointment((int)dgvAllAppointments.CurrentRow.Cells[0].Value)) 
                {
                    MessageBox.Show("Appointment Deleted Succesfully");
                    _RefreshAppointmentsList();
                }
                else 
                {
                    MessageBox.Show("Failed To Delete!");
                    MessageBox.Show(clsAppointmentBusiness.LogMessage);
                }
            }
        }
    }
}
