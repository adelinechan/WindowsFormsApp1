using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Alarm : Form
    {
        AlarmHistory almhis = null;
        public Alarm()
        {
            InitializeComponent();
        }

        private void btnAlarm_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            almhis = new AlarmHistory();
            almhis.Location = new Point(0, 0);
            almhis.Name = "UC_ToolManagement";
            almhis.MaximumSize = new System.Drawing.Size(0, 0);
            almhis.MinimumSize = new System.Drawing.Size(0, 0);
            almhis.Dock = DockStyle.Fill;
            panel1.Controls.Add(almhis);
        }
    }
}
