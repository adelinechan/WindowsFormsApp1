using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApp1
{
    public partial class frmToolAdd : Form
    {
        DataTable dtXmlInfo = null;
        UC_ToolManagement returnToolMan = null;
        ArrayList codeList = new ArrayList();

        public frmToolAdd(DataTable dt, UC_ToolManagement temp)
        {
            InitializeComponent();
            dtXmlInfo = dt;
            returnToolMan = temp;
        }
        private void frmToolAdd_Load(object sender, EventArgs e)
        {
            foreach (DataRow row in dtXmlInfo.Rows)
            {
                if (!codeList.Contains(row["ToolNumber"]))
                    codeList.Add(row["ToolNumber"]);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            int oldCount = dtXmlInfo.Rows.Count;
            DataRow dr = dtXmlInfo.NewRow();
            dr["ID"] = (oldCount + 1).ToString().PadLeft(2, '0');
            dr["Status"] = txtStatus.Text;
            dr["Product"] = txtProduct.Text;
            dr["Process"] = txtProcess.Text;
            dr["UseQty"] = txtUseQty.Text;
            dr["IdentifyNumber"] = txtIdentifyNumber.Text;
            dr["Type"] = txtType.Text;
            dr["ToolNumber"] = txtToolNumber.Text;
            dr["ToolLife"] = txtToolLife.Text;

            if (!codeList.Contains(txtToolNumber.Text))
            {
                dtXmlInfo.Rows.Add(dr);
                returnToolMan.RefreshAllData(dtXmlInfo);
                this.Close();
            }
            else
            {
                MessageBox.Show("aa" + txtToolNumber.Text + "aa");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
