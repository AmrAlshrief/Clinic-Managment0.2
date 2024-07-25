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
    public partial class ctrSessionControl : UserControl
    {
        //public event EventHandler OnSessionEnd;
        public event Action<int> OnSessionStart;
        public event Action <int> OnSessionEnd;
        private string _SessionID;
        private int _SessionIDInt;
        private string _Name;

        public Button btnStart { get; set; }
        public Button btnStop { get; set; }

        public string SessionID
        {
            get { return _SessionID; }
            set { _SessionID = value; lblAppointmentID.Text = value; _SessionIDInt = int.Parse(value); }
        }

        public string Name 
        {
            get { return _Name; }
            set { _Name = value; txtName.Text = value; }
        }
        public ctrSessionControl()
        {
            InitializeComponent();
        }

        protected virtual void SesssionStarted(int sessionID) 
        {
            Action<int> handler = OnSessionStart;
            if(handler != null) 
            {
                handler(sessionID);
            }
        }

        protected virtual void SessionEnd(int sessionID) 
        {
            Action<int> handler = OnSessionEnd;
            if( handler != null) 
            {
                handler(sessionID);
            }
        }

        private void btnEndSession_Click(object sender, EventArgs e)
        {
            if(OnSessionEnd != null) 
            {
                OnSessionEnd(this._SessionIDInt);
            }

            this.Hide();
            
        }

        private void btnStartSession_Click(object sender, EventArgs e)
        {
            //if(OnSessionEnd != null) 
            //{
            //    OnSessionEnd?.Invoke(this, EventArgs.Empty);
            //}

            if(OnSessionStart != null) 
            {
                OnSessionStart(this._SessionIDInt);
            }

        }
    }
}
