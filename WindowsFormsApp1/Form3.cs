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
using System.Net.Http;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public partial class ToolManagement : Form
    {
        //初始化
        InterfaceLib.IMsg iRemoting;
        StructMsg.Pwd _Pwd;
        StructMsg.SkyConn_status _SkyConn_status;
        StructMsg.SkyNc_filename _SkyNc_filename;
        StructMsg.nc_current_block _nc_current_block;
        StructMsg.time _time;
        StructMsg.othercode _othercode;
        StructMsg.alm_current _alm_current;

        string currentStatus = "";
        string currentProgName = "";
        string currentProg = "";
        string currentTCode = "";
        int[] cycleTime = new int[3];

        string oldStatus = "";
        string oldTCode = "";
        string oldProg = "";

        UC_Info uc_info = null;
        UC_ToolManagement uc_tool = null;
        public ToolManagement()
        {
            InitializeComponent();
            lblConnStatus.Image = Image.FromFile(Application.StartupPath + "\\images\\offline.ico");
        }

        #region VOID
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Connect
        //按下Connect鍵之後所要做的事和判斷的訊息
        private void btnConn_Click(object sender, EventArgs e)
        {
            if (ChannelServices.RegisteredChannels.Length == 0)
                ChannelServices.RegisterChannel(new TcpChannel(), false);
            string IP = txtIP.Text.Trim();
            string Port = "9501";
            iRemoting = (IMsg)Activator.GetObject(typeof(IMsg), "tcp://" + IP + ":" + Port + "/RemoteObjectURI" + Port);
            _Pwd.ConnectionKey = "pmc";
            _Pwd.WritePwd = "pmc";
            short ret = iRemoting.SKY_conn_status(_Pwd, ref _SkyConn_status);
            if (ret == 0)
            {
                MessageBox.Show("Successful!");
                lblConnStatus.Image = Image.FromFile(Application.StartupPath + "\\images\\online.ico");
                timer1.Enabled = true;
                timer2.Enabled = true;
            }
        }
        #endregion

        #region Disconnect
        //將機臺斷開連線
        private void btnDisconn_Click(object sender, EventArgs e)
        {
            lblConnStatus.Image = Image.FromFile(Application.StartupPath + "\\images\\offline.ico");
            timer1.Enabled = false;
        }

        #endregion


        #region Information
        //按下"Information"之後會顯示的版面狀態
        private void btnInfo_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            uc_info = new UC_Info();
            uc_info.Location = new Point(0, 0);
            uc_info.Name = "UC_Info";
            uc_info.MaximumSize = new System.Drawing.Size(0, 0);
            uc_info.MinimumSize = new System.Drawing.Size(0, 0);
            uc_info.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc_info);
        }
        #endregion

        #region Timer
        //讓程式一直呼叫回傳機臺訊息的程式
        private void timer1_Tick(object sender, EventArgs e)
        {
            getCNCInfo();
            //UC Info
            if (uc_info != null)
            {
                if (currentStatus == "RUN")
                {
                    uc_info.showToProgram(currentProg, _nc_current_block.Block[0], _nc_current_block.Block[1]);
                    uc_info.showToToolNumber(currentTCode, currentProgName);
                    uc_info.showCycleTime(cycleTime, currentProgName);
                }
            }

            //UC Tool
            //設定狀態初始值
            if (oldStatus == "")
                oldStatus = currentStatus;
            //設定刀號初始值
            if (oldTCode == "")
                oldTCode = currentTCode;
            //設定程式名稱初始值
            if (oldProg == "")
                oldProg = currentProgName;
            if (currentStatus != "RUN")
                currentTCode = "none";
            if (uc_tool != null)
            {
                if (oldTCode != currentTCode)
                {
                    uc_tool.getTCode(currentTCode, oldTCode);
                }
                uc_tool.getStatus(currentStatus);
                if (oldStatus == "RUN" && oldStatus != currentStatus)
                    uc_tool.writeXML();
            }
            if (oldStatus != currentStatus)
                oldStatus = currentStatus;
            if (oldTCode != currentTCode)
                oldTCode = currentTCode;
        }
        #endregion

        #region getCNCInfo
        //取得機臺狀態
        private void getCNCInfo()
        {
            string eqpStatus = "";
            short ret = iRemoting.SKY_conn_status(_Pwd, ref _SkyConn_status);
            if (ret == 0)
            {
                switch (_SkyConn_status.Status[0])
                {
                    case 0:
                        eqpStatus = "OFF";
                        break;
                    case 1:
                        eqpStatus = "RUN";
                        break;
                    case 2:
                        eqpStatus = "IDLE";
                        break;
                    case 3:
                        eqpStatus = "Alarm";
                        break;
                }
            }
            currentStatus = eqpStatus;
            //取得程式名稱
            ret = iRemoting.SKY_nc_filename(_Pwd, ref _SkyNc_filename);
            if (ret == 0)
            {
                currentProgName = _SkyNc_filename.MainProg[0];
            }
            //取得程式內容
            ret = iRemoting.GET_nc_current_block(_Pwd, ref _nc_current_block);
            if (ret == 0)
            {
                currentProg = "";
                for (int i = 0; i <= _nc_current_block.Block.Length - 1; i++)
                {
                    currentProg += _nc_current_block.Block[i] + "\r\n";
                }
            }
            //取得循環時間
            ret = iRemoting.GET_time(_Pwd, ref _time);
            if (ret == 0)
            {
                cycleTime = _time.Cycle;
            }
            //取得刀號
            ret = iRemoting.GET_othercode(_Pwd, ref _othercode);
            if (ret == 0)
            {
                currentTCode = _othercode.TCode.ToString();
            }
        }
        #endregion

        #region Tool_Click
        //按下Tool Button 之後會顯示的版面狀態
        private void btnTool_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            uc_tool = new UC_ToolManagement();
            uc_tool.Location = new Point(0, 0);
            uc_tool.Name = "UC_ToolManagement";
            uc_tool.MaximumSize = new System.Drawing.Size(0, 0);
            uc_tool.MinimumSize = new System.Drawing.Size(0, 0);
            uc_tool.Dock = DockStyle.Fill;
            panelMain.Controls.Add(uc_tool);
        }
        #endregion

        #region LINE
        private void timer2_Tick(object sender, EventArgs e)
        {
            getCNCInfo();
            if (uc_info != null)
            {
                if (currentStatus == "RUN")
                {
                    short ret = iRemoting.GET_alm_current(_Pwd, ref _alm_current);
                    if (ret == 0 && _alm_current.IsAlarm)
                    {
                        if (_alm_current.IsAlarm == true)
                        {
                            //做line推撥
                            //KeyValuePair //json

                        }
                        else
                        {
                            //什麼都不錯
                        }
                    }
                }

            }
        }
        #endregion
    }
}






