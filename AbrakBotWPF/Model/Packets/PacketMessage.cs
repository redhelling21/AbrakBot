using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF
{
    public partial class ServerAgent
    {
        private void handleMessage(string packet)
        {
            switch (packet.Substring(2, 1))
            {
                /*case "K":
                    string sub = packet.Substring(3, 1);
                    string[] parts = packet.Split('|');
                    switch (sub)
                    {
                        case ":":
                            globals.writeToMainBox("[" + DateTime.Now.ToString("h:mm") + "] (Commerce) " + parts[2] + " : " + parts[3] + "\n", "SaddleBrown");
                            break;
                        case "?":
                            globals.writeToMainBox("[" + DateTime.Now.ToString("h:mm") + "] (Recrutement) " + parts[2] + " : " + parts[3] + "\n", "LightGray");
                            break;
                        case "F":
                            globals.writeToMainBox("[" + DateTime.Now.ToString("h:mm") + "] de " + parts[2] + " : " + parts[3] + "\n", "DeepSkyBlue");
                            break;
                        case "T":
                            globals.writeToMainBox("[" + DateTime.Now.ToString("h:mm") + "] a " + parts[2] + " : " + parts[3] + "\n", "DeepSkyBlue");
                            break;
                        default:
                            globals.writeToMainBox("[" + DateTime.Now.ToString("h:mm") + "] " + parts[2] + " : " + parts[3] + "\n", "Black");
                            break;
                    }
                    break;
                case "E":
                    if (packet.Substring(3, 1) == "f")
                    {
                        globals.writeToMainBox("Le joueur n'existe pas ou n'est pas en ligne", "Firebrick");
                    }
                    break;*/
            }
            toClient.send(packet);
        }
    }
}
