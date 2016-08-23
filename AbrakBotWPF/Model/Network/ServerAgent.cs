using AbrakBotWPF.Model.Classes;
using AbrakBotWPF.Model.Network;
using AbrakBotWPF.Model.Messages;
using AbrakBotWPF.Model.Services;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBotWPF
{
    public partial class ServerAgent
    {
        private TcpClient tcpclient;
        //private static string Data = null;
        public bool shouldStop = false;
        private NetworkStream nstream = null;
        private Queue<string> pck_queue = new Queue<string>();
        private System.Threading.Thread rcv_th = null;
        public string GUID = "";
        public ClientAgent toClient;
        private string server_id = Config.defaultServerId.HasValue ? Config.defaultServerId.Value.ToString(CultureInfo.InvariantCulture) : "";
        private string character_id = Config.defaultCharacterId.HasValue ? Config.defaultCharacterId.Value.ToString(CultureInfo.InvariantCulture) : "";
        private Globals globals;
        private Player player;

        public ServerAgent(Globals globals, Player player)
        {
            this.globals = globals;
            this.player = player;
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

        public void ReceiveData(Queue<string> pck_queue)
        {
            if (pck_queue.Count != 0)
            {
                for (int i = 0; i < pck_queue.Count; i++)
                {
                    string Data = pck_queue.Dequeue();
                    string donnee = "";
                    if (Data != null && Data != "")
                    {
                        //On affiche pas les packets am, il y en a trop
                        if (Data.Substring(0, 2) != "am")
                        {
                            globals.writeToDebugBox("(FROM SERVER) rcv : ", "Red");
                            globals.writeToDebugBox(Data + "\n", "Black");
                        }
                        donnee = Data.Substring(0, 2);
                        if (Data.Substring(0, 2) == "GD") { handleGD(Data); }
                        else if (Data.Substring(0, 2) == "GM") { handleGM(Data); }
                        else if (Data.Substring(0, 2) == "GA") { handleGA(Data); }
                        else if (Data.Substring(0, 1) == "J") { handleJob(Data); }
                        else if (Data.Substring(0, 1) == "E") { handleEchange(Data); }
                        else if (Data.Substring(0, 2) == "cM") { handleMessage(Data); }//TODO : gestion de l'insertion d'items
                        else if (Data.Substring(0, 1) == "O") { handleInventaire(Data); }
                        else if (Data.Substring(0, 1) == "G") { handleCombat(Data); }
                        else if (Data.Substring(0, 1) == "A" && globals.isConnected == false) { handleConnect(Data);}
                        else { handleDivers(Data); }
                    }
                    else
                    {
                        //globals.writeToDebugBox("Aucune données reçues\n", "Firebrick");
                    }
                }
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
                    ReceiveData(pck_queue);
                }
            }
        }

        public void send(string DataS)
        {

            DataS += "\0"; //on rajoute les caractères "0A" et "00" à la fin des données
            globals.writeToDebugBox("(FROM CLIENT) snd : " + DataS, "Black");
            //on encode les données en bytes
            byte[] DataToSend = Encoding.UTF8.GetBytes(DataS);
            nstream.Write(DataToSend, 0, DataToSend.Length);

            //Console.WriteLine("snd : " + DataS);
        }

        public void close()
        {
            if (tcpclient != null && tcpclient.Connected)
            {
                tcpclient.Close();
                nstream.Close();
            }
        }
    }
}
