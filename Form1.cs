﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;

namespace socket_client
{
    public partial class Form1 : Form
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            msg("Client Started");
            label1.Text = "Client Socket Program - Server Connected ...";
            clientSocket.Connect("127.0.0.1", 8888);
        }

        public void msg(string mesg)
        {
            textBox1.Text = textBox1.Text + Environment.NewLine + " >> " + mesg;
        }

        private void button1_Click_1(object sender, System.EventArgs e)
        {
            try
            {
                NetworkStream serverStream = clientSocket.GetStream();
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox2.Text + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                byte[] inStream = new byte[10025];
                serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
                string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                msg(returndata);
                textBox2.Text = "";
                textBox2.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Server is Currently Down", "Reverse Replay Http Server", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
