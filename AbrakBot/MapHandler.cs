using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBot
{
    class MapHandler
    {
        public static string[] HEX_CHARS = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
        public static string ZKARRAY = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
        public static void LoadMap(int idMap, string indice, string clef)
        {
            
            Globals.tpBas = -1;
            Globals.tpDroite = -1;
            Globals.tpGauche = -1;
            Globals.tpHaut = -1;
            StreamReader reader;
            if (clef == "")
            {
                reader = new StreamReader(Globals.execPath + "/Resources/txt_maps/" + idMap + "_" + indice + ".txt");
            }
            else
            {
                reader = new StreamReader(Globals.execPath + "/Resources/txt_maps/" + idMap + "_" + indice + "X.txt");
            }
            
            string mapDataText = reader.ReadToEnd();
            string mapData = mapDataText.Split('\"')[1];
            reader.Close();
            string sData;
            if (clef != "")
            {
                string preparedKey = prepareKey(clef);
                sData = decypherData(mapData, preparedKey, (int)(Convert.ToInt64(checksum(preparedKey), 16) * 2));
            }else
            {
                sData = mapData;
            }


            Globals.mapDataActuelle = uncompressMap(sData);
                Globals.currentMapId = idMap;
                Globals.updateMapCoords("[" + Globals.maps[idMap] + "]");
                int num = 0;
                do
                {
                    if (Globals.mapDataActuelle[num].movement == 2)
                    {
                        int num2 = getX(num);
                        int num3 = getY(num);
                        if ((num2 - 1) == num3)
                        {
                            Globals.tpGauche = num;
                        }
                        else if ((num2 - 0x1b) == num3)
                        {
                            Globals.tpDroite = num;
                        }
                        else if ((num2 + num3) == 0x1f)
                        {
                            Globals.tpBas = num;
                        }
                        else if (num3 < 0)
                        {
                            num3 = Math.Abs(num3);
                            if ((num2 - num3) == 1)
                            {
                                Globals.tpHaut = num;
                            }
                        }
                    }
                    num++;
                }
                while (Globals.mapDataActuelle[num+1] != null && num <= 500);
                //LoadRessources(Globals.mapDataActuelle);
            
        }

        private static void LoadRessources(Cell[] spritesHandler)
        {
            List<int> list = new List<int>();
            List<string> list3 = new List<string>();
            List<bool> list2 = new List<bool>();
            string str = "a";
            StreamReader reader = new StreamReader(Globals.execPath + "/Resources/ressources.txt");
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
            //perso.TabUtilisateur.ListeRessources.Items.Clear();
            int num3 = 0;
            do
            {
                if (list.Contains(spritesHandler[num3].layerObject2Num))
                {
                    //Affichage des ressources
                    /*perso.TabUtilisateur.ListeRessources.Items.Add(list3[list.IndexOf(spritesHandler[num3].layerObject2Num)]);
                    perso.TabUtilisateur.ListeRessources.Items[num].SubItems.Add(num3.ToString());
                    perso.TabUtilisateur.ListeRessources.Items[num].SubItems.Add("Non coup\x00e9");*/
                    spritesHandler[num3].object2Movement = list2[list.IndexOf(spritesHandler[num3].layerObject2Num)-1];
                    num++;
                }
                num3++;
            }
            while (num3 <= 0x3e8);
        }

        private static string prepareKey(string key)
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

        private static string decypherData(string mapData, string preparedKey, int c)
        {
            string _loc5 = "";
            int _loc6 = preparedKey.Length;
            int _loc7 = 0;
            int _loc9 = 0;

            while(_loc9 < mapData.Length)
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

        public static string checksum(string s)
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

        public static Cell[] uncompressMap(string sData)
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


        public static Cell uncompressCell(string sData)
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


        public static int getX(int laCase)
        {

            int _loc5 = (int)Math.Floor((float)laCase / (15 * 2 - 1));
            int _loc6 = laCase - _loc5 * (15 * 2 - 1);
            int _loc7 = _loc6 % 15;
            int _loc8 = (laCase - (15 - 1) * (_loc5 - _loc7)) / 15;
            return _loc8;

        }

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
