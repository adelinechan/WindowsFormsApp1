using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class UC_ToolManagement : UserControl
    {
        //刀具管理者頁面功能設計的程式之初始化
        DataTable dtTool;
        string xmlFileName = "ToolSet.xml";
        string xmlFilePath = "";
        MemoryStream imgOK = null;
        MemoryStream imgAlarm = null;
        frmToolAdd toolAdd = null;
        Dictionary<string, int> codeData = new Dictionary<string, int>(); //刀號與使用次數
        public UC_ToolManagement()
        {
            InitializeComponent();
        }

        #region Load
        //最初載入刀具管理畫面的設定
        private void UC_ToolManagement_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            byte[] bytes = File.ReadAllBytes(Application.StartupPath + "\\Images\\green_ball.png");
            imgOK = new MemoryStream(bytes);
            bytes = File.ReadAllBytes(Application.StartupPath + "\\Images\\red_ball.png");
            imgAlarm = new MemoryStream(bytes);
            LoadToolData();
        }
        #endregion


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        #region ToolSet
        //判斷表格式，並寫入XML格式的檔案中做暫存
        private void dgvToolSet_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                dgvToolSet["UseQty", e.RowIndex].Value = 0;
                dgvToolSet["Status", e.RowIndex].Value = "OK";
                dgvToolSet["Image", e.RowIndex].Value = Image.FromStream(imgOK);
                writeXML();
            }
        }
        #endregion

        #region Modify
        //設定Modify按鍵的功能，讓用戶可以修改（如刪除）資料庫的資料
        private void btnModify_Click(object sender, EventArgs e)
        {
            btnModify.Enabled = false;
            btnSave.Enabled = true;
            dgvToolSet.ReadOnly = false;
            dgvToolSet.Columns["Reset"].ReadOnly = true;
            dgvToolSet.Columns["Image"].ReadOnly = true;
            dgvToolSet.Columns["ID"].ReadOnly = true;
            dgvToolSet.Columns["Status"].ReadOnly = true;
            dgvToolSet.Columns["UseQty"].ReadOnly = true;
            dgvToolSet.AllowUserToDeleteRows = true;
            dgvToolSet.Columns["Product"].DefaultCellStyle.BackColor = Color.LightBlue;
            dgvToolSet.Columns["Process"].DefaultCellStyle.BackColor = Color.LightBlue;
            dgvToolSet.Columns["IdentifyNumber"].DefaultCellStyle.BackColor = Color.LightBlue;
            dgvToolSet.Columns["Type"].DefaultCellStyle.BackColor = Color.LightBlue;
            dgvToolSet.Columns["ToolNumber"].DefaultCellStyle.BackColor = Color.LightBlue;
            dgvToolSet.Columns["ToolLife"].DefaultCellStyle.BackColor = Color.LightBlue;
        }

        #endregion

        #region Save
        //設定Save按鍵的功能，讓用戶可以儲存所修改的資料
        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnModify.Enabled = true;
            dgvToolSet.ReadOnly = true;
            dgvToolSet.AllowUserToDeleteRows = false;
            writeXML();
        }
        #endregion

        #region Add
        //設定Add按鍵的功能，讓用戶可以增加刀具資料庫的資料，增加時將跳出新視窗
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (toolAdd == null || toolAdd.IsDisposed)
            {
                openNewForm();
            }
            else
            {
                toolAdd.Close();
                openNewForm();
            }
        }
        #endregion

        #region Load Tool Data
        //建立刀具資料表格
        private void LoadToolData()
        {
            dtTool = new DataTable();
            xmlFilePath = System.Environment.CurrentDirectory + @"\" + xmlFileName;
            DataSet ds = new DataSet("info");
            ds.ReadXml(xmlFilePath);
            dtTool = ds.Tables[0];
            dgvToolSet.DataSource = dtTool;
            dgvToolSet.Columns["ID"].HeaderText = "序號";
            dgvToolSet.Columns["Status"].HeaderText = "狀態";
            dgvToolSet.Columns["Product"].HeaderText = "料號";
            dgvToolSet.Columns["Process"].HeaderText = "製程";
            dgvToolSet.Columns["UseQty"].HeaderText = "用量";
            dgvToolSet.Columns["IdentifyNumber"].HeaderText = "刀片編號";
            dgvToolSet.Columns["Type"].HeaderText = "刀片規格";
            dgvToolSet.Columns["ToolNumber"].HeaderText = "刀號";
            dgvToolSet.Columns["ToolLife"].HeaderText = "刀具壽命";
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "歸零";
            btn.Text = "Reset";
            btn.Name = "Reset";
            btn.UseColumnTextForButtonValue = true;
            dgvToolSet.Columns.Insert(0, btn); //加入歸零按鈕欄位
            DataGridViewImageColumn img = new DataGridViewImageColumn();
            img.HeaderText = "燈號";
            img.Name = "Image";
            img.Image = Image.FromStream(imgOK);
            img.ReadOnly = false;
            dgvToolSet.Columns.Insert(1, img); //加入燈號圖片欄位
            RefreshToolCount(dtTool);
            RefreshStatusImage();
        }

        #endregion

        #region Refresh Tool Count
        //更新刀號使用次數
        private void RefreshToolCount(DataTable dt)
        {
            codeData.Clear();
            foreach (DataRow row in dt.Rows)
            {
                string tCode = row["ToolNumber"].ToString();
                int tQty = Convert.ToInt32(row["UseQty"]);
                if (!codeData.ContainsKey(tCode))
                    codeData.Add(tCode, tQty);
            }
        }
        #endregion

        #region Refresh Status Image
        //更新顯示的警示圖案
        public void RefreshStatusImage()
        {
            for (int i = 0; i < dgvToolSet.Rows.Count; i++)
            {
                string Status = dgvToolSet.Rows[i].Cells["Status"].Value.ToString();
                if (Status == "OK")
                    dgvToolSet.Rows[i].Cells["Image"].Value = Image.FromStream(imgOK);
                else
                    dgvToolSet.Rows[i].Cells["Image"].Value = Image.FromStream(imgAlarm);
            }
            dgvToolSet.Refresh();
        }
        #endregion

        #region Refresh All Data
        //更新全部資料並寫入XML檔案中
        public void RefreshAllData(DataTable dt)
        {
            dgvToolSet.DataSource = dt;
            writeXML();
        }
        #endregion

        #region GetStatus
        //Textbox 中顯示機臺狀態
        public void getStatus(string status)
        {
            textBox1.Text = status;
        }
        #endregion

        #region GetTCode
        //抓取刀具的T-code
        public void getTCode(string tCode, string oldTCode)
        {
            if (codeData.ContainsKey(tCode))
            {
                if (tCode != oldTCode)
                {
                    int temp = codeData[tCode] + 1; //刀號使用次數
                    codeData[tCode] = temp;
                    DataTable dtTemp = new DataTable();
                    dtTemp = (DataTable)dgvToolSet.DataSource;
                    foreach (DataRow row in dtTemp.Rows)
                    {
                        if (row["ToolNumber"].ToString() == tCode)
                        {
                            row["UseQty"] = codeData[tCode];
                            if (codeData[tCode] >= Convert.ToInt32(row["ToolLife"]))
                                row["Status"] = "Alarm";
                            else
                                row["Status"] = "OK";
                        }
                    }
                    RefreshStatusImage();
                }
            }
        }
        #endregion

        #region WriteXML
        //匯出成XML格式
        public void writeXML()
        {
            DataTable dtSave = new DataTable("info");
            DataColumn col;
            //dgvToolSet 0為按鈕欄;1為燈號欄;2之後為xml內容資料
            for (int i = 2; i < dgvToolSet.Columns.Count; i++)
            {
                col = new DataColumn();
                col.ColumnName = dgvToolSet.Columns[i].Name.ToString();
                dtSave.Columns.Add(col);
            }
            for (int j = 0; j < dgvToolSet.Rows.Count; j++)
            {
                DataRow row = dtSave.NewRow();
                for (int r = 2; r < dgvToolSet.Columns.Count; r++)
                {
                    row[r - 2] = dgvToolSet.Rows[j].Cells[r].Value;
                }
                dtSave.Rows.Add(row);
            }
            dtSave.WriteXml(xmlFilePath);
            dgvToolSet.DataSource = null;
            dgvToolSet.DataSource = dtSave;
            RefreshToolCount(dtSave);
            RefreshStatusImage();
            dtTool = dtSave;
        }
        #endregion

        #region openNewForm
        //指定增加刀具時所要顯示的Form
        private void openNewForm()
        {
            toolAdd = new frmToolAdd(dtTool, this);
            toolAdd.Show();
        }
        #endregion

       
    }
}
