using AbrakBotWPF.Model.Classes;
using AbrakBotWPF.Model.Messages;
using AbrakBotWPF.Model.Services;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBotWPF
{
    public partial class PacketDispatcher
    {
        public Globals globals;
        private Player player;

        public PacketDispatcher(Globals globals, Player player)
        {
            this.globals = globals;
            this.player = player;
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
                            globals.writeToDebugBox("rcv : ", "Red");
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
                        else { handleDivers(Data); }
                    }
                    else
                    {
                        globals.writeToDebugBox("Aucune données reçues\n", "Firebrick");
                    }
                }
            }
        }
    }
}
