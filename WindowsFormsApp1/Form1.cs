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
    public partial class Form1 : Form
    {
        InterfaceLib.IMsg iRemoting = null;
        int panelWidth;
        bool Hidden;
        public Form1()
        {
            InitializeComponent();
            //this.Size = new Size(50,50);
            panelWidth = panel3.Width;
            Hidden = false;
            timer2.Enabled = !timer2.Enabled;

            //Random ab = new Random();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            //timer1.Start();
        }
        #region button & unused elements
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        #endregion
        private void 虎虎智能_Load(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Hidden)
            {
                panel3.Width = panel3.Width + 10;
                if (panel3.Width >= panelWidth)
                {
                    timer1.Stop();
                    Hidden = false;
                    this.Refresh();
                }
            }
            else
            {
                panel3.Width = panel3.Width - 10;
                if (panel3.Width <= 0)
                {
                    timer1.Stop();
                    Hidden = true;
                    this.Refresh();
                }
            }

        }
        #region button & unused elements

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            //xd.SelectSingleNode();

        }
        /*public void ByteArrayClear(ref byte[] data) //ref= receive callback info
        {
            for(var i=0; i< data.Length; i++)
            {
                data[i] = 0;
            }
        }*/
        #endregion

        private void button11_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(@"<CNC>
                <API>GET_othercode</API>
                <ConnKey>123</ConnKey>
                <SendId>20110908</SendId>
                </CNC>"); //
            TcpClient client = new TcpClient();
            client.Connect("192.168.1.168", 9701);
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
                //SpinWait.SpinUntil(() => false, 200);
                Thread.Sleep(200);
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

                XmlDocument xd = new XmlDocument();
                xd.LoadXml(str);
                //string xd = "";
                //var temps = xd.SelectNodes("CNC");
                label40.Text = xd.SelectSingleNode("CNC/HCode").InnerText;//H
                label41.Text = xd.SelectSingleNode("CNC/FCode").InnerText;//F
                label42.Text = xd.SelectSingleNode("CNC/TCode").InnerText;//T
                label43.Text = xd.SelectSingleNode("CNC/DCode").InnerText;//D
                label44.Text = xd.SelectSingleNode("CNC/SCode").InnerText;//S
                label45.Text = xd.SelectSingleNode("CNC/MCode").InnerText;//M*/

                for(int i=0; i<101; i++) 
                {
                    SpinWait.SpinUntil(() => false, 200);
                    circleprogressBar1.updateProgress(i);
                    Application.DoEvents();
                    
                }
                
            }
        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            abs();
            timer2.Enabled = true;
        }
        private void abs()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(@"<CNC>
                <API>GET_position</API>
                <ConnKey>123</ConnKey>
                <SendId>20110908</SendId>
                </CNC>"); //
            TcpClient client = new TcpClient();
            client.Connect("192.168.1.168", 9701);
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
                //Thread.Sleep(200);
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

                XmlDocument xd = new XmlDocument();
                xd.LoadXml(str);
                var temps = xd.SelectNodes("CNC/PosData/Abs");
                var pos = new List<string>();
                for (int j = 0; j < temps.Count; j++)
                {
                    pos.Add(temps[j].InnerText);
                }
                label30.Text = pos[0];
                label31.Text = pos[1];
                label32.Text = pos[2];
                var lbl = new List<Label>();
                lbl.Add(label30);
                lbl.Add(label31);
                lbl.Add(label32);
                int a = 0;
                foreach (var item in pos)
                {
                    lbl[a].Text = item; //item -> pos [0]
                    a += 1;
                }
            }
       
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
        }

        private void circleprogressBar1_Load(object sender, EventArgs e)
        {
        }
    }
}
