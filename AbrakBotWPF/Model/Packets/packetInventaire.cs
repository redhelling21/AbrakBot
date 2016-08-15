using AbrakBotWPF.Model.Classes;
using AbrakBotWPF.Model.Messages;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBotWPF
{
    public partial class PacketDispatcher
    {
        private void handleInventaire(string packet)
        {
            switch(packet.Substring(0, 2))
            {
                case "Ow"://Infos sur les pods
                    Thread.Sleep(100);
                    string[] elems = packet.Substring(2).Split('|');
                    player.pods_max = Int32.Parse(elems[1]);
                    player.pods = Int32.Parse(elems[0]);
                    if (Math.Round(((float)(player.pods) / player.pods_max) * 100) > globals.podsPercentLimit)
                    {
                        globals.writeToMainBox("Inventaire plein à plus de " + globals.podsPercentLimit + "%. Retour à la banque.\n", "FireBrick");
                        globals.needsBank = true;
                    }
                    break;
                case "OR"://item remove
                    string idUnique = packet.Substring(2);
                    Item toDelete = null;
                    foreach (Item item in player.inventaire)
                    {
                        if (item.uniqueID == idUnique)
                        {
                            toDelete = item;
                        }
                    }
                    player.inventaire.Remove(toDelete);
                    globals.removingItem = false;
                    var msg = new InventoryChangedMessage() { inventory = player.inventaire };
                    Messenger.Default.Send<InventoryChangedMessage>(msg);
                    break;
                case "OQ":
                    string[] qteList = packet.Split('|');
                    string idQte = Int32.Parse(qteList[0].Substring(2)).ToString("X").ToLower();
                    foreach (Item item in player.inventaire)
                    {
                        if (item.uniqueID == idQte)
                        {
                            item.quantite = Int32.Parse(qteList[1]);
                        }
                    }
                    var msg2 = new InventoryChangedMessage() { inventory = player.inventaire };
                    Messenger.Default.Send<InventoryChangedMessage>(msg2);
                    break;
                case "OA":
                    if(packet.Substring(3, 1) == "O")
                    {
                        string[] onAddList = packet.Substring(4).Split('*');
                        foreach(string str in onAddList)
                        {
                            string[] oaoList = str.Split('~');
                            bool found = false;
                            foreach (Item item in player.inventaire)
                            {
                                if (item.uniqueID == oaoList[0])
                                {
                                    item.quantite = item.quantite + Int32.Parse(oaoList[2]);
                                }
                            }
                            if (found)
                            {
                                player.inventaire.Add(new Item(oaoList[0], int.Parse(oaoList[1], System.Globalization.NumberStyles.HexNumber), globals.objects[int.Parse(oaoList[1], System.Globalization.NumberStyles.HexNumber)], Int32.Parse(oaoList[2]), false));
                            }
                        }
                    }
                    break;
            }
        }
    }
}
