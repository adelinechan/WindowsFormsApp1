using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class UC_Info : UserControl
    {
        //Information介面的程式初始化
        public UC_Info()
        {
            InitializeComponent();
        }

        #region showToProgram
        //顯示現階段加工步驟
        public void showToProgram(string richProgram, string activeProg, string nextProg)
        {
            lblActive.Text = activeProg;
            lblNext.Text = nextProg;
            richTextBox1.Text = "";
            richTextBox1.Text = richProgram;
            richTextBox1.Select(0, richTextBox1.GetFirstCharIndexFromLine(1) - 1);
        }
        List<string> info = new List<string>();
        string oldProgName = "";
        #endregion

        #region showToToolNumber
        //顯示加工中所使用的刀具之號碼
        public void showToToolNumber(string strTool, string newProgName)
        {
            if (oldProgName == "")
            {
                oldProgName = newProgName;
            }
            if (oldProgName != newProgName)
            {
                info.Clear();
                oldProgName = newProgName;
            }
            if (!info.Contains(strTool))
            {
                info.Add(strTool);
                listView1.Items.Clear();
                int i = 0;
                foreach (string item in info)
                {
                    i = i + 1;
                    ListViewItem toolItem = new ListViewItem(i.ToString());
                    toolItem.SubItems.Add("T" + item);
                    listView1.Items.Add(toolItem);
                }
            }
        }
        #endregion

        #region showCycleTime
        //顯示加工時間
        public void showCycleTime(int[] timeData, string mainProg)
        {
            lblCycleTime.Text = string.Format("{0:00}", timeData[0]) + "H " +
            string.Format("{0:00}", timeData[1]) + "M " +
            string.Format("{0:00}", timeData[2]) + "S";
            lblProgramName.Text = mainProg;
        }
        #endregion
        private void lblProgramName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
