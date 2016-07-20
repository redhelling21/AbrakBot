﻿using System;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace AbrakBot
{
    class Config
    {
        [JsonProperty]
        public static String serverIp;
        [JsonProperty]
        public static int serverPort;
        [JsonProperty]
        public static int? defaultServerId;
        [JsonProperty]
        public static int? defaultCharacterId;
        //public string username;
        //public string mdp;

        public Config(String serv, int port, int? defserv, int? defchar)
        {
            serverIp = serv;
            serverPort = port;
            defaultServerId = defserv;
            defaultCharacterId = defchar;
        }

        public static void load()
        {
            string uriString = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase), "config.json");
            Uri uri = new Uri(uriString);
            using (StreamReader r = new StreamReader(uri.LocalPath))
            {
                string json = r.ReadToEnd();
                Config conf = JsonConvert.DeserializeObject<Config>(json);
            }
        }
    }
}
