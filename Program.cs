using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            TcpListener serverSocket = new TcpListener(8888);
            int requestCount = 0;
            TcpClient clientSocket = default(TcpClient);
            serverSocket.Start();
            Console.WriteLine(" >> Server Started");
            clientSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine(" >> Accept connection from client");
            requestCount = 0;
            

            while ((true))
            {
                try
                {
                    requestCount = requestCount + 1;


                    NetworkStream networkStream = clientSocket.GetStream();
                    byte[] bytesFrom = new byte[10025];
                    try
                    {
                        if (!clientSocket.Connected)
                        {
                            Console.WriteLine("Client is not Online!");
                            break;
                        }
                        networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Client is not Online!");
                        break;
                    }
                    string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine(" >> Data from client - " + dataFromClient);

                    string mesg, rev = "";
                    mesg = dataFromClient;
                    int len = mesg.Length - 1;
                    for (; len >= 0; len--)
                    {
                        rev = rev + mesg[len];
                    }

                    string serverResponse = "Last Message from client:- " + rev;
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    Console.WriteLine(" >> " + serverResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine(" >> exit");
            Console.ReadLine();
        }

        //public static bool IsConnected(this Socket socket)
        //{
        //    try
        //    {
        //        return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
        //    }
        //    catch (SocketException) { return false; }
        //}

    }
}

