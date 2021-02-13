using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Net;

namespace Chatting_TCP_Server
{
    public partial class ServerForm : Form
    {

        Socket connection;
        Thread readThread;
        NetworkStream socketStream;
        BinaryReader reader;
        BinaryWriter writer;

        public ServerForm()
        {
            InitializeComponent();
            readThread = new Thread(new ThreadStart(runServer));
            readThread.Start();
        }


        public void runServer()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener listener;
            int counter = 1;
            listener = new TcpListener(ip,5000);
            listener.Start();

            while(true)
            {
                connection = listener.AcceptSocket();
                displayTextBox.Text = "Waiting for connection....";
                socketStream = new NetworkStream(connection);
                writer = new BinaryWriter(socketStream);
                reader = new BinaryReader(socketStream);
                displayTextBox.Text += "Connection " + counter + " Recieved\r\n";
                writer.Write("Server :- Connected sucsessfully. ");
                inputTextBox.ReadOnly = false;
                string theReplay = "";

                do
                {
                    theReplay = reader.ReadString();
                    displayTextBox.Text = "\r\n" + theReplay;
                } while (theReplay != "Client :- bye" && connection.Connected);
                displayTextBox.Text += "\r\nUser terminated the connection.";
                inputTextBox.ReadOnly = true;

                writer.Close();
                reader.Close();
                socketStream.Close();
                connection.Close();
                ++counter;
            }
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }

        private void inputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && connection != null)
            {
                writer.Write("Server :- " + inputTextBox.Text);
                displayTextBox.Text = "\r\nServer :- " + inputTextBox.Text;
                if (inputTextBox.Text == "bye")
                    connection.Close();
                inputTextBox.Clear();
            }
        }
    }
}
