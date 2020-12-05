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
    public partial class XML_file_read_test : Form
    {
        public XML_file_read_test()
        {
            InitializeComponent();
            string exe_path = System.Environment.CurrentDirectory;
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(exe_path + "\\Books.xml");
            dataGridView1.DataSource = dataSet.Tables[0];
            dataGridView1.DataMember = dataSet.Tables[0].ChildRelations[0].RelationName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
