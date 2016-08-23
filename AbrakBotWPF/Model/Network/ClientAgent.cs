using AbrakBotWPF.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBotWPF.Model.Network
{
    public class ClientAgent
    {
        TcpListener server;
        TcpClient client;
        Globals globals;
        private NetworkStream nstream = null;
        private Queue<string> pck_queue = new Queue<string>();
        private System.Threading.Thread rcv_th = null;
        public ServerAgent toServ;

        public ClientAgent(string ip, int port, Globals glob)
        {
            globals = glob;
            server = new TcpListener(IPAddress.Parse(ip), port);
            server.Start();
            globals.writeToMainBox("En attente d'une connection du client...", "Green");
            client = server.AcceptTcpClient();
            globals.writeToMainBox("Connection etablie", "Green");
            nstream = client.GetStream();
            rcv_th = new System.Threading.Thread(new System.Threading.ThreadStart(rcv));
            rcv_th.IsBackground = true;
            rcv_th.Name = "PacketThread";
            rcv_th.Start();
        }

        public void rcv()
        {
            while (client.Connected)
            {
                Thread.Sleep(1000);
                if (client.Available != 0)
                {
                    Byte[] databyt = new Byte[client.Available];
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
                    str01 = str00.Split(new char[] { '\0' });
                    foreach (string str02 in str01)
                    {
                        pck_queue.Enqueue(str02);

                        ReceiveData(pck_queue);
                    }
                }

            }
        }

        public void send(string DataS)
        {

            DataS += "\0"; //on rajoute les caractères "0A" et "00" à la fin des données

            //on encode les données en bytes
            byte[] DataToSend = Encoding.UTF8.GetBytes(DataS);
            nstream.Write(DataToSend, 0, DataToSend.Length);
        }

        public void ReceiveData(Queue<string> pck)
        {
            if (pck.Count != 0)
            {
                for (int i = 0; i < pck.Count; i++)
                {
                    string Data = pck.Dequeue();

                    if (Data != "")
                    {
                        //Signifie l'arrivee d'un packet
                        
                        toServ.send(Data);
                    }
                }
            }
        }

        public void close()
        {
            if (client != null && client.Connected)
            {
                client.Close();
                nstream.Close();
            }
        }
    }
}
