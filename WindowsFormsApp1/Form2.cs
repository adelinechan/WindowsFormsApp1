using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using InterfaceLib;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        //程式初始化
        string filePath = Application.StartupPath + @"\" + "config.txt";
        InterfaceLib.IMsg iRemoting = null;
        StructMsg.Pwd _Pwd;
        StructMsg.nc_list _nc_list;
        StructMsg.nc_code _nc_code;
        StructMsg.NcName _NcName;
        StructMsg.alm_current _alm_current;
        string path = "";

        public Form2()
        {
            InitializeComponent();
        }
        #region button
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Conn_Click
        //當程式開始連接機臺網域，並按下Connect鍵時，會將連接訊息變成ONLINE
        private void btnConn_Click(object sender, EventArgs e)
        {
            if (ChannelServices.RegisteredChannels.Length == 0)
                ChannelServices.RegisterChannel(new TcpChannel(), false);
            _Pwd.ConnectionKey = "pmc"; //二次開發密碼
            _Pwd.WritePwd = "pmc";
            iRemoting = (IMsg)Activator.GetObject(typeof(IMsg), "tcp://" + txtIP.Text.Trim() + ":9501" +
            "/RemoteObjectURI9501");
            lblStatus.BackColor = Color.LawnGreen;
            lblStatus.Text = "ONLINE";
            ReflashListView(true);
            ReflashMemView();
        }
        #endregion

        #region ProgramDiscPath
        //讀取本機系統路徑的副程式
        private string programDiscPath()
        {
            if (File.Exists(filePath))
            {
                StreamReader info = File.OpenText(filePath);
                while (info.EndOfStream == false)
                {
                    String[] temp = info.ReadLine().Split('=');
                    if (temp[0].Equals("Path"))
                        path = temp[temp.Length - 1].ToString();
                }
                info.Close();
            }
            return path;
        }
        #endregion

        #region driveListBox
        //顯示所有的Disc
        private void driveListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPath.Text = driveListBox1.Drive[0].ToString() + driveListBox1.Drive[1].ToString() + "\\";
            File.WriteAllText(filePath, "Path=" + txtPath.Text);
            ReflashListView(false);
        }
        #endregion

        #region listLocalView
        //顯示本機所有資料夾裏面的資料
        private void listViewLocal_DoubleClick(object sender, EventArgs e)
        {
            string selItem = listViewLocal.SelectedItems[0].Text;
            string fileOpen = txtPath.Text + selItem;
            if (File.Exists(fileOpen))
            {
                try
                {
                    richTextBox1.Text = File.ReadAllText(fileOpen);
                    richTextBox1.Tag = fileOpen;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                listViewLocal.Items.Clear();
                if (selItem == "..")
                {
                    txtPath.Text = txtPath.Text.Remove(txtPath.Text.Length - 1, 1);
                    DirectoryInfo dirInfo = Directory.GetParent(txtPath.Text);
                    txtPath.Text = dirInfo.FullName;
                    if (txtPath.Text.EndsWith(":\\"))
                    {
                        string[] dirStr = Directory.GetDirectories(txtPath.Text);
                        foreach (string str in dirStr)
                        {
                            FileInfo fullPath = new FileInfo(str);
                            ListViewItem items = new ListViewItem(fullPath.Name, 0);
                            items.SubItems.Add("");
                            items.SubItems.Add(fullPath.CreationTime.ToString());
                            listViewLocal.Items.Add(items);
                        }
                        string[] fileStr = Directory.GetFiles(txtPath.Text);
                        foreach (string str in fileStr)
                        {
                            FileInfo fullPath = new FileInfo(str);
                            ListViewItem items = new ListViewItem(fullPath.Name, 1);
                            items.SubItems.Add(fullPath.Length.ToString());
                            items.SubItems.Add(fullPath.CreationTime.ToString());
                            listViewLocal.Items.Add(items);
                        }
                    }
                    else
                    {
                        txtPath.Text += "\\";
                        string[] dirStr = Directory.GetDirectories(txtPath.Text);
                        ListViewItem itemHead = new ListViewItem("..", 0);
                        itemHead.SubItems.Add("");
                        itemHead.SubItems.Add("");
                        listViewLocal.Items.Add(itemHead);
                        foreach (string str in dirStr)
                        {
                            FileInfo fullPath = new FileInfo(str);
                            ListViewItem items = new ListViewItem(fullPath.Name, 0);
                            items.SubItems.Add("");
                            items.SubItems.Add(fullPath.CreationTime.ToString());
                            listViewLocal.Items.Add(items);
                        }
                        string[] fileStr = Directory.GetFiles(txtPath.Text);
                        foreach (string str in fileStr)
                        {
                            FileInfo fullPath = new FileInfo(str);
                            ListViewItem items = new ListViewItem(fullPath.Name, 1);
                            items.SubItems.Add(fullPath.Length.ToString());
                            items.SubItems.Add(fullPath.CreationTime.ToString());
                            listViewLocal.Items.Add(items);
                        }
                    }
                }
                else
                {
                    try
                    {
                        if (txtPath.Text.EndsWith("\\"))
                            txtPath.Text += selItem + "\\";
                        string[] dirStr = Directory.GetDirectories(txtPath.Text);
                        ListViewItem itemHead = new ListViewItem("..", 0);
                        itemHead.SubItems.Add("");
                        itemHead.SubItems.Add("");
                        listViewLocal.Items.Add(itemHead);
                        foreach (string str in dirStr)
                        {
                            FileInfo fullPath = new FileInfo(str);
                            ListViewItem items = new ListViewItem(fullPath.Name, 0);
                            items.SubItems.Add("");
                            items.SubItems.Add(fullPath.CreationTime.ToString());
                            listViewLocal.Items.Add(items);
                        }
                        string[] fileStr = Directory.GetFiles(txtPath.Text);
                        foreach (string str in fileStr)
                        {
                            FileInfo fullPath = new FileInfo(str);
                            ListViewItem items = new ListViewItem(fullPath.Name, 1);
                            items.SubItems.Add(fullPath.Length.ToString());
                            items.SubItems.Add(fullPath.CreationTime.ToString());
                            listViewLocal.Items.Add(items);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                //將目前的目錄路徑寫入到註冊檔
                File.WriteAllText(filePath, "Path=" + txtPath.Text);
            }
        }
        #endregion

        #region btnDelLocal
        //刪除本機裏的資料
        private void btnDelLocal_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(txtPath.Text + "\\" + listViewLocal.SelectedItems[0].Text);
                MessageBox.Show("刪除成功");
                ReflashListView(false);
                ReflashMemView();
            }
            catch
            {
                MessageBox.Show("刪除失敗");
            }
        }
        #endregion

        #region Upload
        //從本機上傳資料到機臺的系統當中
        private void btnUpload_Click(object sender, EventArgs e)
        {
            string NcName = listViewLocal.SelectedItems[0].Text;
            string Code = File.ReadAllText(txtPath.Text + "\\" + NcName);
            _nc_code.NcName = NcName;
            _nc_code.NcCode = Code;
            short ret = 0;
            ret = iRemoting.UPLOAD_nc_mem(_Pwd, _nc_code);
            if (ret == 0)
            {
                MessageBox.Show("上傳成功");
                ReflashListView(false);
                ReflashMemView();
            }
            else
            {
                MessageBox.Show("上傳失敗");
            }
        }
        #endregion

        #region Download
        //程式下載，從幾臺下載程式至本機路徑中
        private void btnDownload_Click(object sender, EventArgs e)
        {
            short ret = 0;
            _nc_code.NcName = listViewMemory.SelectedItems[0].Text;
            ret = iRemoting.GET_nc_mem_code(_Pwd, ref _nc_code);
            if (ret == 0)
            {
                MessageBox.Show("Download successful");
                File.WriteAllText(txtPath.Text + "\\" + _nc_code.NcName, _nc_code.NcCode);
                ReflashListView(false);
                ReflashMemView();
            }
            else
            {
                MessageBox.Show("Download failed");
            }
        }
        #endregion

        #region DelMemory
        //刪除機臺内的加工程式碼
        private void btnDelMemory_Click(object sender, EventArgs e)
        {
            _NcName.Name = listViewMemory.SelectedItems[0].Text;
            short ret = iRemoting.DEL_nc_mem(_Pwd, _NcName);
            if (ret == 0)
            {
                MessageBox.Show("刪除成功");
                ReflashListView(false);
                ReflashMemView();
            }
            else
            {
                MessageBox.Show("刪除失敗");
            }
        }
        #endregion

        #region ReflashListView
        //更新讀取本機目錄的副程式
        private void ReflashListView(bool loadFlag)
        {
            try
            {
                if (loadFlag == true)
                {
                    txtPath.Text = programDiscPath(); //讀取上次儲存的目錄路徑
                }
                if (txtPath.Text == "")
                {
                    string Drive = driveListBox1.Drive[0].ToString() + driveListBox1.Drive[1].ToString();
                    string strCombo = Drive.ToUpper() + "\\";
                    string oldStr = txtPath.Text;
                    txtPath.Text = strCombo;
                }
                string[] dirStr_Temp;
                dirStr_Temp = Directory.GetDirectories(txtPath.Text);
                string[] dirStr = SortFilesName(dirStr_Temp);
                listViewLocal.Items.Clear();
                if (txtPath.Text.EndsWith(":\\"))
                {
                    if (dirStr != null)
                    {
                        foreach (string str in dirStr)
                        {
                            FileInfo fullPath = new FileInfo(str);
                            ListViewItem items = new ListViewItem(fullPath.Name, 0);
                            items.SubItems.Add("");
                            items.SubItems.Add(fullPath.CreationTime.ToString());
                            listViewLocal.Items.Add(items);
                        }
                    }
                    string[] fileStr_Temp = Directory.GetFiles(txtPath.Text);
                    string[] fileStr = SortFilesName(fileStr_Temp);
                    if (fileStr != null)
                    {
                        foreach (string str in fileStr)
                        {
                            FileInfo fullPath = new FileInfo(str);
                            ListViewItem items = new ListViewItem(fullPath.Name, 1);
                            items.SubItems.Add(fullPath.Length.ToString());
                            items.SubItems.Add(fullPath.CreationTime.ToString());
                            listViewLocal.Items.Add(items);
                        }
                    }
                }
                else
                {
                    ListViewItem itemHead = new ListViewItem("..", 0);
                    itemHead.SubItems.Add("");
                    itemHead.SubItems.Add("");
                    listViewLocal.Items.Add(itemHead);
                    if (dirStr != null)
                    {
                        foreach (string str in dirStr)
                        {
                            FileInfo fullPath = new FileInfo(str);
                            ListViewItem items = new ListViewItem(fullPath.Name, 0);
                            items.SubItems.Add("");
                            items.SubItems.Add(fullPath.CreationTime.ToString());
                            listViewLocal.Items.Add(items);
                        }
                    }
                    string[] fileStr_Temp = Directory.GetFiles(txtPath.Text);
                    string[] fileStr = SortFilesName(fileStr_Temp);
                    if (fileStr != null)
                    {
                        foreach (string str in fileStr)
                        {
                            FileInfo fullPath = new FileInfo(str);
                            ListViewItem items = new ListViewItem(fullPath.Name, 1);
                            items.SubItems.Add(fullPath.Length.ToString());
                            items.SubItems.Add(fullPath.CreationTime.ToString());
                            listViewLocal.Items.Add(items);
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region ReflashMem View
        //更新讀取機台記憶體内目錄的副程式
        private void ReflashMemView()
        {
            listViewMemory.Items.Clear();
            try
            {
                short ret = -1;
                _nc_list.FolderPath = "//CNC_MEM/USER/PATH1/";
                ret = iRemoting.GET_nc_mem_list(_Pwd, ref _nc_list);
                if (ret == 0)
                {
                    for (int i = 0; i <= _nc_list.NcList.Length - 1; i++)
                    {
                        switch (_nc_list.NcList[i][4])
                        {
                            case "file":
                                //NcList[][0]:NC Name
                                //NcList[][1]:Size (byte)
                                //NcList[][2]:DateTime
                                //NcList[][3]:註解
                                //NcList[][4]:file / directory
                                string strProg = _nc_list.NcList[i][0];
                                ListViewItem itemMem = new ListViewItem(strProg, 1);
                                string fileSize = _nc_list.NcList[i][1];
                                itemMem.SubItems.Add(fileSize);
                                string commet = _nc_list.NcList[i][3];
                                itemMem.SubItems.Add(commet);
                                listViewMemory.Items.Add(itemMem);
                                break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(ret.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region SortFilesName
        //資料名稱排序
        public static string[] SortFilesName(string[] arrFiles)
        {
            if (arrFiles == null) return null;
            if (arrFiles.Length == 0) return null;
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("Col", Type.GetType("System.String"));
            int i = 0;
            DataRow dr;
            for (i = 0; i <= arrFiles.Length - 1; i++)
            {
                dr = dtTemp.NewRow();
                dr[0] = arrFiles[i];
                dtTemp.Rows.Add(dr);
            }
            dtTemp.DefaultView.Sort = "Col";
            string[] arrRet = new string[arrFiles.Length];
            for (i = 0; i <= arrFiles.Length - 1; i++)
            {
                arrRet[i] = dtTemp.DefaultView[i][0].ToString();
            }
            dtTemp = null;
            return arrRet;
        }
        #endregion
    }

}
