using AbrakBotWPF.Model.Classes;
using AbrakBotWPF.Model.Messages;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF.Model.Services
{
    public class MapHandler
    {
        public string[] HEX_CHARS = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
        public string ZKARRAY = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
        private Globals globals;
        public MapHandler(Globals glob)
        {
            this.globals = glob;
        }

        //Charge la map ayant l'id donné en parametres
        public void LoadMap(int idMap, string indice, string clef)
        {

            globals.tpBas = -1;
            globals.tpDroite = -1;
            globals.tpGauche = -1;
            globals.tpHaut = -1;
            //Parsing du fichier texte de la map
            StreamReader reader;
            if (clef == "")
            {
                reader = new StreamReader(globals.execPath + "/Resources/txt_maps/" + idMap + "_" + indice + ".txt");
            }
            else
            {
                reader = new StreamReader(globals.execPath + "/Resources/txt_maps/" + idMap + "_" + indice + "X.txt");
            }

            string mapDataText = reader.ReadToEnd();
            string mapData = mapDataText.Split('\"')[1];
            reader.Close();
            string sData;

            if (clef != "")
            {
                //Si la map a besoin d'etre decryptee via une cle
                string preparedKey = prepareKey(clef);
                sData = decypherData(mapData, preparedKey, (int)(Convert.ToInt64(checksum(preparedKey), 16) * 2));
            }
            else
            {
                sData = mapData;
            }

            //Recuperation des donnees de la map
            globals.mapDataActuelle = uncompressMap(sData);
            globals.currentMapId = idMap;
            globals.updateMapCoords("[" + globals.maps[idMap] + "]");
            int num = 0;
            //Recuperation des coordonnees des mapchangers
            do
            {
                if (globals.mapDataActuelle[num].movement == 2)
                {
                    int num2 = getX(num);
                    int num3 = getY(num);
                    if ((num2 - 1) == num3)
                    {
                        globals.tpGauche = num;
                    }
                    else if ((num2 - 0x1b) == num3)
                    {
                        globals.tpDroite = num;
                    }
                    else if ((num2 + num3) == 0x1f)
                    {
                        globals.tpBas = num;
                    }
                    else if (num3 < 0)
                    {
                        num3 = Math.Abs(num3);
                        if ((num2 - num3) == 1)
                        {
                            globals.tpHaut = num;
                        }
                    }
                }
                num++;
            }
            while (globals.mapDataActuelle[num + 1] != null && num <= 500);
            //recuperation avec les movement = 6
            num = 0;
            do
            {
                if (globals.mapDataActuelle[num].movement == 6)
                {
                    int num2 = getX(num);
                    int num3 = getY(num);
                    if ((num2 - 1) == num3 && globals.tpGauche == -1)
                    {
                        globals.tpGauche = num;
                    }
                    else if ((num2 - 0x1b) == num3 && globals.tpDroite == -1)
                    {
                        globals.tpDroite = num;
                    }
                    else if ((num2 + num3) == 0x1f && globals.tpBas == -1)
                    {
                        globals.tpBas = num;
                    }
                    else if (num3 < 0)
                    {
                        num3 = Math.Abs(num3);
                        if ((num2 - num3) == 1 && globals.tpHaut == -1)
                        {
                            globals.tpHaut = num;
                        }
                    }
                }
                num++;
            }
            while (globals.mapDataActuelle[num + 1] != null && num <= 500);

            //Si la map fait partie de la liste des maps avec des mapchangers foireux
            if (globals.mapchangers.ContainsKey(globals.currentMapId))
            {
                globals.tpHaut = globals.mapchangers[globals.currentMapId][0];
                globals.tpBas = globals.mapchangers[globals.currentMapId][1];
                globals.tpGauche = globals.mapchangers[globals.currentMapId][2];
                globals.tpDroite = globals.mapchangers[globals.currentMapId][3];
            }
            LoadRessources(globals.mapDataActuelle);

        }

        //Charge les ressources presentes sur la map
        private void LoadRessources(Cell[] spritesHandler)
        {
            List<int> list = new List<int>();
            List<string> list3 = new List<string>();
            List<bool> list2 = new List<bool>();
            string str = "a";
            //On parse le fichier texte
            StreamReader reader = new StreamReader(globals.execPath + "/Resources/ressources.txt");
            while (str != null)
            {
                str = reader.ReadLine();
                if (str != null && str != "")
                {
                    string[] strArray = str.Split(new char[] { ':' });
                    if (strArray.Length == 3)
                    {
                        int item = Int32.Parse(strArray[0]);
                        string str3 = strArray[1];
                        string str2 = strArray[2];
                        list.Add(item);
                        list3.Add(str3);
                        if (str2 == "o")
                        {
                            list2.Add(true);
                        }
                        else
                        {
                            list2.Add(false);
                        }
                    }
                }
            }
            reader.Close();
            int num = 0;
            globals.actualResources.Clear();
            int num3 = 0;
            //On remplit la liste des ressources sur la map
            do
            {
                if (spritesHandler[num3] == null)
                {
                    break;
                }
                if (list.Contains(spritesHandler[num3].layerObject2Num))
                {
                    if (globals.idResourcesTranslate.ContainsKey(spritesHandler[num3].layerObject2Num))
                    {
                        globals.actualResources.Add(num3, new Ressource(globals.idResourcesTranslate[spritesHandler[num3].layerObject2Num], num3, globals.ressources[spritesHandler[num3].layerObject2Num], true));
                    }
                    
                    
                    //TODO : Ajouter l'etat
                    spritesHandler[num3].object2Movement = list2[list.IndexOf(spritesHandler[num3].layerObject2Num)];
                    num++;
                }
                num3++;
            }
            while (num3 <= 0x3e8);
            List<Ressource> listRes = new List<Ressource>();
            foreach(KeyValuePair<Int32, Ressource> entry in globals.actualResources)
            {
                listRes.Add(entry.Value);
            }
            var msg = new MapResourcesChangedMessage() { ressources =  listRes};
            Messenger.Default.Send<MapResourcesChangedMessage>(msg);
            globals.updateResourceTable();
        }

        //Prepare la cle de decryptage pour l'utiliser pour decrypter les donnees de la map
        private string prepareKey(string key)
        {
            string d = key;

            string _loc3 = "";
            int _loc4 = 0;

            while (_loc4 < d.Length)
            {
                _loc3 = _loc3 + (char)(Convert.ToInt64(d.Substring(_loc4, 2), 16));
                _loc4 += 2;
            }

            _loc3 = Uri.UnescapeDataString(_loc3);

            return _loc3;
        }

        //Decrypte les donnees de la map avec la cle
        private string decypherData(string mapData, string preparedKey, int c)
        {
            string _loc5 = "";
            int _loc6 = preparedKey.Length;
            int _loc7 = 0;
            int _loc9 = 0;

            while (_loc9 < mapData.Length)
            {
                int a = (int)Convert.ToInt64(mapData.Substring(_loc9, 2), 16);
                int b = Encoding.ASCII.GetBytes(preparedKey.Substring((_loc7 + c) % _loc6, 1))[0];
                _loc5 += (char)(a ^ b);
                _loc7 += 1;
                _loc9 += 2;
            }

            _loc5 = Uri.UnescapeDataString(_loc5);

            return _loc5;
        }

        //Checksum
        public string checksum(string s)
        {

            int _loc3 = 0;
            int _loc4 = 0;

            while ((_loc4 < s.Length))
            {
                _loc3 = _loc3 + Encoding.ASCII.GetBytes(s.Substring(_loc4, 1))[0] % 16;
                _loc4 += 1;
            }

            return (HEX_CHARS[_loc3 % 16]);

        }

        //Extrait la map a partir des donnees decryptees
        public Cell[] uncompressMap(string sData)
        {

            Cell[] _loc10 = new Cell[1024];
            int _loc11 = sData.Length;
            int _loc13 = 0;
            int _loc14 = 0;

            while ((_loc14 < _loc11))
            {
                Cell _loc12 = uncompressCell(sData.Substring(_loc14, 10));
                _loc10[_loc13] = _loc12;
                _loc14 += 10;
                _loc13 += 1;
            }

            return _loc10;

        }

        //Extrait les donnees d'une cellule a partir d'une code de 10 caracteres
        //(la string contenant les donnees de la map est un ensemble de codes de 10 caracteres, un par cellule)
        public Cell uncompressCell(string sData)
        {

            Cell _loc5 = new Cell();
            string _loc6 = sData;
            int _loc7 = _loc6.Length - 1;
            int[] _loc8 = new int[5000];

            while ((_loc7 >= 0))
            {
                _loc8[_loc7] = ZKARRAY.IndexOf(_loc6[_loc7]);
                _loc7 -= 1;
            }

            _loc5.movement = ((_loc8[2] & 56) >> 3);
            _loc5.layerObject2Num = ((_loc8[0] & 2) << 12) + ((_loc8[7] & 1) << 12) + (_loc8[8] << 6) + _loc8[9];
            _loc5.layerObject2Interactive = Convert.ToBoolean((_loc8[7] & 2) >> 1);
            return _loc5;

        }

        //Calcule l'abscisse de la case
        public static int getX(int laCase)
        {

            int _loc5 = (int)Math.Floor((float)laCase / (15 * 2 - 1));
            int _loc6 = laCase - _loc5 * (15 * 2 - 1);
            int _loc7 = _loc6 % 15;
            int _loc8 = (laCase - (15 - 1) * (_loc5 - _loc7)) / 15;
            return _loc8;

        }

        //Calcule l'ordonnee de la case
        public static int getY(int laCase)
        {

            int _loc5 = (int)Math.Floor((float)laCase / (15 * 2 - 1));
            int _loc6 = laCase - _loc5 * (15 * 2 - 1);
            int _loc7 = _loc6 % 15;
            int _loc8 = _loc5 - _loc7;
            return _loc8;

        }




    }
}
