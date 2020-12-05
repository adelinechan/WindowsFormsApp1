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

using System.Xml;
using System.Net.Sockets;
using System.Threading;


namespace WindowsFormsApp1
{
    
    public partial class Maincs : Form
    {
        InterfaceLib.IMsg iRemoting = null;
        string Skymars_server_ip = "";
        string Skymars_server_port="";
        string port2 = "";
        public Maincs()
        {
            InitializeComponent(); 
            

            //get_presetXML();
            design();
            //Status();
        }

        #region SubMenu
        private void design()
        {
            panelSubMenu.Visible = false;
        }
        private void hideSubmenu()
        {
            if (panelSubMenu.Visible == true)
                panelSubMenu.Visible = false;
        }
        private void showSubmenu()
        {
            if (panelSubMenu.Visible == false)
            {
                hideSubmenu();
                panelSubMenu.Visible = true;
            }
            else
                panelSubMenu.Visible = false;
        }
        #endregion


        private void btnMenu_Click(object sender, EventArgs e)
        {
            showSubmenu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openChildForm(new Mach_Info());
            hideSubmenu();
        }

        private void btnProgram_Click(object sender, EventArgs e)
        {
            openChildForm(new Form2());
            hideSubmenu();
        }

        #region ChildForm
        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Controls.Clear();
            activeForm = childForm;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }
        #endregion

        #region Elements
        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTool_Click(object sender, EventArgs e)
        {
            openChildForm(new ToolManagement());
            hideSubmenu();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        #endregion

        //#region IP&Port
        //public void get_presetXML()
        //{
        //    //找到XML設定檔位置，
        //    string exe_path = System.Environment.CurrentDirectory;
        //    StreamReader str = new StreamReader(exe_path + "\\preset.xml");
        //    string ReadAll = str.ReadToEnd();
        //    str.Close();
        //    //讀取XML文件，//解析XML (Selectnode.innertext)
        //    XmlDocument _doc = new XmlDocument();
        //    _doc.LoadXml(ReadAll.Replace(" ", ""));
        //    var temp = _doc.SelectNodes("catalog/preset/skymars_ip");
        //    Skymars_server_ip = temp[0].InnerText;// "xxx"
        //    temp = _doc.SelectNodes("catalog/preset/skymars_port");
        //    Skymars_server_port = int.Parse(temp[0].InnerText);
        //}
        //#endregion

        //#region GetData
        //public XmlDocument _get_data(string xml_cmd)
        //{

        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(xml_cmd); //
        //    TcpClient client = new TcpClient();
        //    client.Connect(Skymars_server_ip, Skymars_server_port);
        //    XmlDocument xd = new XmlDocument();
        //    if (client.Connected)
        //    {
        //        //ipconfig
        //        string receStr = "";
        //        var ns = client.GetStream();
        //        var command = Encoding.UTF8.GetBytes(doc.OuterXml);
        //        if (ns.CanWrite)
        //        {
        //            ns.Write(command, 0, command.Length);
        //        }
        //        SpinWait.SpinUntil(() => false, 200);
        //        //Thread.Sleep(200);
        //        if (ns.CanRead)
        //        {
        //            //var temp = ns.DataAvailable;
        //            while (ns.DataAvailable)
        //            {
        //                //ByteArrayClear(ref rece);
        //                var rece = new byte[1024];
        //                ns.Read(rece, 0, rece.Length);
        //                receStr += Encoding.UTF8.GetString(rece);
        //                //temp = ns.DataAvailable;
        //            }

        //        }
        //        ns.Close();
        //        string str = receStr;

        //        if (receStr != "")
        //        {
        //            xd.LoadXml(receStr.Trim());
        //        }
        //    }
        //    return xd;
        //}
        //#endregion

      

        //#region Alarm & CNC Status
        //private void Status()
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc = _get_data(@"<CNC>
        //        <API>GET_status</API>
        //        <ConnKey>pmc</ConnKey>
        //        <SendId>20110908</SendId>
        //        </CNC>"); //
        //    if (doc.HasChildNodes)
        //    {
        //        //Initialised.Text = doc.SelectSingleNode("CNC/CncStatus").InnerText;//F%    
        //        //lblAlarmMsg.Text = doc.SelectSingleNode("CNC/Alarm").InnerText;
        //    }
        //}
        //#endregion
        private void lblAlarmMsg_Click(object sender, EventArgs e)
        {

        }

        private void btnAlarm_Click(object sender, EventArgs e)
        {
            openChildForm(new Alarm());
            hideSubmenu();
        }

        private void btnMaintainance_Click(object sender, EventArgs e)
        {
            openChildForm(new Maintainance());
            hideSubmenu();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Skymars_server_port = "9501" ;
            //Maincs uc = new Maincs(Skymars_server_ip, port2);

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Maincs_Load(object sender, EventArgs e)
        {
            string ip="";
            string port="";
            Skymars_server_ip = ip;
            Skymars_server_port = port;
        }
    }
}
