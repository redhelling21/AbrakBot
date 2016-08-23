using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF
{
    public partial class ServerAgent
    {
        private void handleEchange(string packet)
        {
            switch(packet.Substring(0, 2))
            {
                case "EC":
                    //Type echange
                    break;
                case "Es"://Echange d'un item ok
                          //
                    break;
                case "EL"://Contenu banque
                          //Inutile pour l'instant
                    break;
                case "EV"://fin echange
                    globals.isInExchange = false;
                    break;
            }
            toClient.send(packet);
        }
    }
}
