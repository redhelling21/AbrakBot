using AbrakBotWPF.Model.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbrakBotWPF
{
    public partial class ServerAgent
    {
        private void handleGD(string packet)
        {
            Thread.Sleep(100);
            string subcat = packet.Substring(2, 1);
            switch (subcat)
            {
                case "M"://Reception des infos sur la map actuelle
                    if (!globals.isInGame)
                    {
                        globals.isInGame = true;
                        globals.writeToMainBox("En jeu.\n", "Green");
                        globals.writeToDebugBox("En jeu.\n", "Green");
                    }
                    globals.mapLoaded = false;
                    globals.isMoving = false;
                    globals.monsterGroups.Clear();
                    string[] map_datas = packet.Split('|');
                    globals.currentMapId = Int32.Parse(map_datas[1]);
                    string indice = map_datas[2];
                    string clef = map_datas[3];
                    globals.mapHandler.LoadMap(globals.currentMapId, indice, clef);
                    globals.serverGame.send("GI");
                    break;
                case "F"://Reception des infos sur l'etat des ressources de la map
                    string[] res_datas = packet.Split('|');
                    if (res_datas[1] != "")
                    {
                        if (res_datas[1].Split(';')[2] == "0")
                        {
                            if (globals.actualResources.ContainsKey(Int32.Parse(res_datas[1].Split(';')[0])))
                            {
                                globals.actualResources.Remove(Int32.Parse(res_datas[1].Split(';')[0]));
                            }
                        }
                        else
                        {
                            int caseRepop = Int32.Parse(res_datas[1].Split(';')[0]);
                            if (!globals.actualResources.ContainsKey(caseRepop) && globals.idResourcesTranslate.ContainsKey(globals.mapDataActuelle[caseRepop].layerObject2Num))
                            {
                                globals.actualResources.Add(caseRepop, new Ressource(globals.idResourcesTranslate[globals.mapDataActuelle[caseRepop].layerObject2Num], caseRepop, globals.ressources[globals.mapDataActuelle[caseRepop].layerObject2Num], true));
                            }
                        }
                    }
                    break;
                case "K":
                    globals.mapLoaded = true;
                    
                    break;
            }
            toClient.send(packet);
        }
    }
}
