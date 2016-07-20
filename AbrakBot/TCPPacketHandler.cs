using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading;
using System.Globalization;

namespace AbrakBot
{
    class TCPPacketHandler
    {
        private static TcpClient tcpclient ;
        //private static string Data = null;
        
        private static NetworkStream nstream = null;
        private static Queue<string> pck_queue = new Queue<string>();
        private static System.Threading.Thread rcv_th = null;
        private static Config config;

        public static void Handle(string ip_s, int port)
        {
            tcpclient = new TcpClient();
            try
            {
                tcpclient.Connect(ip_s, port);
                if (!tcpclient.Connected)
                {
                    Environment.Exit(0);
                }
                nstream = tcpclient.GetStream();
                rcv_th = new System.Threading.Thread(new System.Threading.ThreadStart(rcv));

                rcv_th.Start();
            }
            catch (Exception expt1)
            {
                Environment.Exit(0);
            }
        }

        public static void rcv()
        {
            while (tcpclient.Connected)
            {
                Thread.Sleep(200);
                Byte[] databyt = new Byte[tcpclient.Available];
                try
                {
                    nstream.Read(databyt, 0, databyt.Length);
                }
                catch
                {
                    Console.WriteLine("Erreur");
                }
                string str00 = Encoding.ASCII.GetString(databyt);
                string[] str01 = null;
                //Console.WriteLine("str00 : " + str00);
                str01 = str00.Split(new char[] { '\0' });
                foreach (string str02 in str01)
                {
                    pck_queue.Enqueue(str02);

                    ReceiveData();
                }
            }
        }

        public static void send(string DataS)
        {

            DataS += "\n\0"; //on rajoute les caractères "0A" et "00" à la fin des données

            //on encode les données en bytes
            byte[] DataToSend = Encoding.UTF8.GetBytes(DataS);
            nstream.Write(DataToSend, 0, DataToSend.Length);

            Globals.writeToDebugBox("snd : " + DataS + "\n", System.Drawing.Color.Violet);
        }

         public static void ReceiveData()
        {
            if (!Globals.isConnected)
            {
                ConnectHandler.ReceiveData(pck_queue);
            } else {
                PacketDispatcher.ReceiveData(pck_queue);
            }
        }

        public static void close()
        {
            if (tcpclient.Connected)
            {
                tcpclient.Close();
                nstream.Close();
            }
        }
    }
}
