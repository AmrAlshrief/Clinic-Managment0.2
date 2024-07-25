using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NavigationBar.CustomControls
{
    public partial class ctrEditDate : UserControl
    {
        public event Action<DateTime> OnEditSessionDate;
        public ctrEditDate()
        {
            InitializeComponent();
        }

        protected virtual void EditSessionDate(DateTime SessionDate)
        {
            Action<DateTime> handler = OnEditSessionDate;
            if (handler != null)
            {
                handler(SessionDate);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(OnEditSessionDate != null) 
            {
                OnEditSessionDate(dateTimePicker1.Value.Date);
            }
        }
    }
}
