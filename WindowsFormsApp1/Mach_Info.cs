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
using System.IO;
using System.Web.UI.WebControls;
using RestSharp.Deserializers;

namespace WindowsFormsApp1
{
    public partial class Mach_Info : Form
    {
        string Skymars_server_ip = "";
        int Skymars_server_port;
        public Mach_Info()
        {
            InitializeComponent();
            get_presetXML();
            Get_Time();
            Speed();
            Codes();
            Status();
            timer1.Enabled = !timer1.Enabled;
        }

        #region Unused elements
        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region GetXML(IP&Port)
        public void get_presetXML()
        {
            //找到XML設定檔位置，
            string exe_path = System.Environment.CurrentDirectory;
            StreamReader str = new StreamReader(exe_path + "\\preset.xml");
            string ReadAll = str.ReadToEnd();
            str.Close();
            //讀取XML文件，//解析XML (Selectnode.innertext)
            XmlDocument _doc = new XmlDocument();
            _doc.LoadXml(ReadAll.Replace(" ", ""));
            var temp = _doc.SelectNodes("catalog/preset/skymars_ip");
            Skymars_server_ip = temp[0].InnerText;// "xxx"
            temp = _doc.SelectNodes("catalog/preset/skymars_port");
            Skymars_server_port = int.Parse(temp[0].InnerText);
        }
        #endregion

        #region Absolute & Relative
        private void Coordinate()
        {
            XmlDocument doc = new XmlDocument();
            doc = _get_data(@"<CNC>
                <API>GET_position</API>
                <ConnKey>pmc</ConnKey>
                <SendId>20110908</SendId>
                </CNC>"); //
            if (doc.HasChildNodes)
            {
                var temps = doc.SelectNodes("CNC/PosData/Abs");
                var pos = new List<string>();
                for (int j = 0; j < temps.Count; j++)
                {
                    pos.Add(temps[j].InnerText);
                }
                label2.Text = pos[0];
                label3.Text = pos[1];
                label5.Text = pos[2];
                //label7.Text = pos[3];
                //label9.Text = pos[4];
                var lbl = new List<System.Windows.Forms.Label>();
                lbl.Add(label2);
                lbl.Add(label3);
                lbl.Add(label5);
                //lbl.Add(label7);
                //lbl.Add(label9);
                int a = 0;
                foreach (var item in pos)
                {
                    lbl[a].Text = item; //item -> pos [0]
                    a += 1;
                }

                //Relative
                var temps_1 = doc.SelectNodes("CNC/PosData/Rel");
                var pos_1 = new List<string>();
                for (int j = 0; j < temps_1.Count; j++)
                {
                    pos_1.Add(temps_1[j].InnerText);
                }
                label19.Text = pos_1[0];
                label17.Text = pos_1[1];
                label15.Text = pos_1[2];
                //label13.Text = pos_1[3];
                //label11.Text = pos_1[4];
                lbl.Add(label19);
                lbl.Add(label17);
                lbl.Add(label15);
                //lbl.Add(label13);
                //lbl.Add(label11);
                int b = 0;
                foreach (var item in pos)
                {
                    lbl[b].Text = item; //item -> pos [0]
                    b += 1;
                }
            }
        }
        #endregion

        #region Get_data
        public XmlDocument _get_data(string xml_cmd)
        {

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml_cmd); //
            TcpClient client = new TcpClient();
            client.Connect(Skymars_server_ip, Skymars_server_port);
            XmlDocument xd = new XmlDocument();
            if (client.Connected)
            {
                //ipconfig
                string receStr = "";
                var ns = client.GetStream();
                var command = Encoding.UTF8.GetBytes(doc.OuterXml);
                if (ns.CanWrite)
                {
                    ns.Write(command, 0, command.Length);
                }
                SpinWait.SpinUntil(() => false, 200);
                //Thread.Sleep(300);
                if (ns.CanRead)
                {
                    //var temp = ns.DataAvailable;
                    while (ns.DataAvailable)
                    {
                        //ByteArrayClear(ref rece);
                        var rece = new byte[1024];
                        ns.Read(rece, 0, rece.Length);
                        receStr += Encoding.UTF8.GetString(rece);
                        //temp = ns.DataAvailable;
                    }

                }
                ns.Close();
                string str = receStr;

                if (receStr != "")
                {
                    xd.LoadXml(receStr.Trim());
                }
            }
            return xd;
        }
        #endregion

        #region Codes
        private void Codes()
        {
            XmlDocument doc = new XmlDocument();
            doc = _get_data(@"<CNC>
                <API>GET_othercode</API>
                <ConnKey>pmc</ConnKey>
                <SendId>20110908</SendId>
                </CNC>"); //
            if (doc.HasChildNodes)
            {
                var temps = doc.SelectNodes("CNC");
                label23.Text = temps[0].SelectSingleNode("HCode").InnerText;//H
                label25.Text = temps[0].SelectSingleNode("FCode").InnerText;//F
                label33.Text = temps[0].SelectSingleNode("TCode").InnerText;//T
                label37.Text = temps[0].SelectSingleNode("DCode").InnerText;//D
                label27.Text = temps[0].SelectSingleNode("SCode").InnerText;//S
                label29.Text = temps[0].SelectSingleNode("MCode").InnerText;//M*/
            }
        }
        #endregion

        #region Speed
        private void Speed()
        {
            XmlDocument doc = new XmlDocument();
            doc = _get_data(@"<CNC>
                    <API>GET_feed_spindle</API>
                    <ConnKey>pmc</ConnKey>
                    <SendId>20110908</SendId>
                    </CNC>"); //
            if (doc.HasChildNodes)
            {
                label31.Text = doc.SelectSingleNode("CNC/OvFeed").InnerText;//F%
                label35.Text = doc.SelectSingleNode("CNC/OvSpindle").InnerText;//S%
                label39.Text = doc.SelectSingleNode("CNC/ActFeed").InnerText;//Feed
                label43.Text = doc.SelectSingleNode("CNC/ActSpindle").InnerText;//Speed
            }
        }
        #endregion

        #region Time Settings
        private void Get_Time ()
        {
            XmlDocument doc = new XmlDocument();
            doc = _get_data(@"<CNC>
                <API>GET_time</API>
                <ConnKey>pmc</ConnKey>
                <SendId>20110908</SendId>
                </CNC>"); //
            if (doc.HasChildNodes)
            {
                var temps = doc.SelectNodes("CNC/Power");
                label51.Text = temps[0].SelectSingleNode("Hour").InnerText + " hours "
                    + temps[0].SelectSingleNode("Minuite").InnerText + " minutes "
                    + temps[0].SelectSingleNode("Second").InnerText + " seconds";//Power
                temps = doc.SelectNodes("CNC/Cycle");
                label53.Text = temps[0].SelectSingleNode("Hour").InnerText + " hours "
                    + temps[0].SelectSingleNode("Minuite").InnerText + " minutes "
                    + temps[0].SelectSingleNode("Second").InnerText + " seconds";//Cycle
                temps = doc.SelectNodes("CNC/Operation");
                label55.Text = temps[0].SelectSingleNode("Hour").InnerText + " hours "
                    + temps[0].SelectSingleNode("Minuite").InnerText + " minutes "
                    + temps[0].SelectSingleNode("Second").InnerText + " seconds";//Operation
                temps = doc.SelectNodes("CNC/Cutting");
                label57.Text = temps[0].SelectSingleNode("Hour").InnerText + " hours "
                    + temps[0].SelectSingleNode("Minuite").InnerText + " minutes "
                    + temps[0].SelectSingleNode("Second").InnerText + " seconds";//Cutting
            }
        }
        #endregion

        #region Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Coordinate();
            timer1.Enabled = true;
        }
        #endregion

        #region Status
        private void Status()
        {
            XmlDocument doc = new XmlDocument();
            doc = _get_data(@"<CNC>
                <API>GET_status</API>
                <ConnKey>pmc</ConnKey>
                <SendId>20110908</SendId>
                </CNC>"); //
            if (doc.HasChildNodes)
            {
                var temps = doc.SelectNodes("CNC");
                label21.Text = temps[0].SelectSingleNode("MainProg").InnerText;//Main Prog Name
                label22.Text = "BC" + temps[0].SelectSingleNode("CurSeq").InnerText;//Current Sequence
            }

            Thread.Sleep(200);
            doc = _get_data(@"<CNC>
                <API>GET_nc_mem_code</API>
                <ConnKey>pmc</ConnKey>
                <Para>
                <FolderPath></FolderPath>
                <NcName>" + label21.Text + @"</NcName>
                </Para>
                <SendId>20110908</SendId>
                </CNC>"); //
            if (doc.HasChildNodes)
            {
                var temps = doc.SelectNodes("CNC /NcCode");
                richTextBox2.Text = temps[0].InnerText;
            }

            doc = _get_data(@"<CNC>
            <API>GET_nc_current_block</API>
            <ConnKey>pmc</ConnKey>
            <SendId>20110908</SendId>
            </CNC>");
            if (doc.HasChildNodes)
            {
                var temps = doc.SelectNodes("CNC /NcBlock");
                richTextBox2.Text = "";
                for (int i = 0; i < temps.Count; i++)
                {
                    richTextBox2.Text += temps[0].InnerText + Environment.NewLine;//Nc Code
                }
            }
        }
            #endregion

            #region Unused Label
            private void label51_Click(object sender, EventArgs e)
        {

        }

        private void label54_Click(object sender, EventArgs e)
        {

        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void label52_Click(object sender, EventArgs e)
        {

        }

        private void label56_Click(object sender, EventArgs e)
        {

        }

        private void label55_Click(object sender, EventArgs e)
        {

        }

        private void label58_Click(object sender, EventArgs e)
        {

        }

        private void label57_Click(object sender, EventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void label50_Click(object sender, EventArgs e)
        {

        }

        private void label49_Click(object sender, EventArgs e)
        {

        }

        private void label48_Click(object sender, EventArgs e)
        {

        }

        private void label47_Click(object sender, EventArgs e)
        {

        }

        private void label53_Click_1(object sender, EventArgs e)
        {

        }

        private void label55_Click_1(object sender, EventArgs e)
        {

        }

        private void label57_Click_1(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
