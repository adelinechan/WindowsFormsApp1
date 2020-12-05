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
    public partial class Maintainance : Form
    {
        public Maintainance()
        {
            InitializeComponent();
        }

        string url = "rtsp://admin:abcd12345@192.168.88.203/Streaming/Channels/101";

        //FilterInfoCollection filterInfoCollection;
        //VideoCaptureDevice VideoCaptureDevice;

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Maintainance_Load(object sender, EventArgs e)
        {

        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnPause.Text == "Pause")
            {
                vlcControl.Pause();
                btnPause.Text = "Play";
            }
            else if (btnPause.Text == "Play")
            {
                vlcControl.Play();
                btnPause.Text = "Pause";
            }

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            vlcControl.Stop();
        }

        private void vlcControl1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            vlcControl.Play(new Uri(url));
        }
    }
}
