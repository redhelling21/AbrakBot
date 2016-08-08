using AbrakBotWPF.Model.Classes;
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
                    break;
            }
        }
    }
}
