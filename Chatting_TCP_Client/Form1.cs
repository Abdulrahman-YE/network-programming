using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chatting_TCP_Client
{
    public partial class ClientForm : Form
    {
        TcpClient client;
        Thread readThread;
        NetworkStream myStream;
        BinaryReader reader;
        BinaryWriter writer;

        String message;
        public ClientForm()
        {
            InitializeComponent();
            readThread = new Thread(new ThreadStart(runClient));
            readThread.Start();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {

        }

        public void runClient()
        {
            client = new TcpClient();
            client.Connect("licalhost", 5000);
            myStream = client.GetStream();
            writer = new BinaryWriter(myStream);
            reader = new BinaryReader(myStream);
            displayTextBox.Text += "\r\nGet I/O Streams\r\n";
            do
            {
                message = reader.ReadString();
                displayTextBox.Text = "\r\n" + message;
            } while (message != "Server :- bye");

            writer.Close();
            reader.Close();
            myStream.Close();
            Application.Exit();


        }

        private void inputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                writer.Write("Client :- " + inputTextBox.Text);
                displayTextBox.Text = "\r\nClient :- " + inputTextBox.Text;
                inputTextBox.Clear();
            }
        }

        private void ClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }
    }
}
