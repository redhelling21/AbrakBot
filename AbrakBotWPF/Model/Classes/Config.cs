using System;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using AbrakBotWPF.Model.Services;

namespace AbrakBotWPF.Model.Classes
{
    public class Config
    {
        
        [JsonProperty]
        public static String serverIp;
        [JsonProperty]
        public static int serverPort;
        [JsonProperty]
        public static int? defaultServerId;
        [JsonProperty]
        public static int? defaultCharacterId;
        [JsonProperty]
        public static string username;
        [JsonProperty]
        public static string mdp;
        [JsonProperty]
        public static List<SortCombat> sortsCombat;
        [JsonProperty]
        public static int nbMinMonstres;
        [JsonProperty]
        public static int nbMaxMonstres;
        [JsonProperty]
        public static int lvlMinMonstres;
        [JsonProperty]
        public static int lvlMaxMonstres;
        [JsonProperty]
        public static int percentRegen;
        [JsonProperty]
        public static int podsPercentLimit;

        public Config(string serv, int port, int? defserv, int? defchar, string us, string md, List<SortCombat> list, int nbMinM, int nbMaxM, int lvlMinM, int lvlMaxM, int percReg, int percBank)
        {
            serverIp = serv;
            serverPort = port;
            defaultServerId = defserv;
            defaultCharacterId = defchar;
            username = us;
            mdp = md;
            sortsCombat = list;
            nbMinMonstres = nbMinM;
            nbMaxMonstres = nbMaxM;
            lvlMinMonstres = lvlMinM;
            lvlMaxMonstres = lvlMaxM;
            percentRegen = percReg;
            podsPercentLimit = percBank;
        }

        public static void load(string name)
        {
            string uriString = Globals.execPath + "/Configs/" + name;
           
            using (StreamReader r = new StreamReader(uriString))
            {
                string json = r.ReadToEnd();
                Config conf = JsonConvert.DeserializeObject<Config>(json);
            }
        }

        public static void save(string name)
        {
            string json = JsonConvert.SerializeObject(new Config(serverIp, serverPort, defaultServerId, defaultCharacterId, username, mdp, sortsCombat, nbMinMonstres, nbMaxMonstres, lvlMinMonstres, lvlMaxMonstres, percentRegen, podsPercentLimit));

            System.IO.File.WriteAllText(Globals.execPath + "/Configs/" + name, json);
        }
    }
}
