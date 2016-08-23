using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading;
using System.Globalization;
using AbrakBotWPF.Model.Classes;

namespace AbrakBotWPF.Model.Services
{
    public class TCPPacketHandler
    {
        /*private Globals globals;
        private TcpClient tcpclient ;
        //private static string Data = null;
        public bool shouldStop = false;
        private NetworkStream nstream = null;
        private Queue<string> pck_queue = new Queue<string>();
        private System.Threading.Thread rcv_th = null;
        private Config config;
        public ConnectHandler connectHandler;
        private PacketDispatcher dispatcher;

        public TCPPacketHandler(Globals glob, Player player)
        {
            this.globals = glob;
            connectHandler = new ConnectHandler(globals, player);
            dispatcher = new PacketDispatcher(globals, player);
        }

        public void Handle(string ip_s, int port)
        {
            tcpclient = new TcpClient();
            try
            {
                tcpclient.Connect(ip_s, port);
                if (!tcpclient.Connected)
                {
                    Environment.Exit(0);
                }
                shouldStop = false;
                nstream = tcpclient.GetStream();
                rcv_th = new System.Threading.Thread(new System.Threading.ThreadStart(rcv));
                rcv_th.IsBackground = true;
                rcv_th.Name = "PacketThread";
                rcv_th.Start();
            }
            catch (Exception expt1)
            {
                Environment.Exit(0);
            }
        }

        public void rcv()
        {
            while (tcpclient.Connected)
            {
                Thread.Sleep(200);
                if (shouldStop)
                {
                    break;
                }
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

        public void send(string DataS)
        {

            DataS += "\n\0"; //on rajoute les caractères "0A" et "00" à la fin des données

            //on encode les données en bytes
            byte[] DataToSend = Encoding.UTF8.GetBytes(DataS);
            nstream.Write(DataToSend, 0, DataToSend.Length);

            globals.writeToDebugBox("snd : " + DataS + "\n", "Violet");
        }

         public void ReceiveData()
        {
            if (!globals.isConnected)
            {
                connectHandler.ReceiveData(pck_queue);
            } else {
                dispatcher.ReceiveData(pck_queue);
            }
        }

        public void close()
        {
            if (tcpclient != null && tcpclient.Connected)
            {
                tcpclient.Close();
                nstream.Close();
            }
        }*/
    }
}
