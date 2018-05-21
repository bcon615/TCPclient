using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using System.Net.Sockets;


namespace TCPclient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            Thread mThread = new Thread(new ThreadStart(ConnectAsClient));
            mThread.Start();
        }

        private void ConnectAsClient()
        {
            TcpClient client = new TcpClient();
            //client.Connect(IPAddress.Parse("10.10.10.149"), 5004);
            client.Connect(IPAddress.Parse("192.168.10.37"), 5004);
            UpdateUI("Connected");
            NetworkStream stream = client.GetStream();
            string s = "Hello from client";
            byte[] message = Encoding.ASCII.GetBytes(s);
            stream.Write(message, 0, message.Length);
            UpdateUI("Message Sent");
            stream.Close();
            client.Close();
        }

        private void UpdateUI(string s)
        {
            Func<int> del = delegate ()
        {
            textBox1.AppendText(s + System.Environment.NewLine);
            return 0;
        };
            Invoke(del); 
        }
    }
}
