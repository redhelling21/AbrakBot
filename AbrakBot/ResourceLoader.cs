﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBot
{
    class ResourceLoader
    {
        public static void load()
        {
            string line;
            string respath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "/Resources/";
            Uri uri = new Uri(respath);
            StreamReader file;
            file = new StreamReader(uri.LocalPath + "objets.txt");
            while ((line = file.ReadLine()) != null)
            {
                Globals.objects.Add(Int32.Parse(line.Split(':')[0]), line.Split(':')[1]);
            }
            file.Close();

            file = new StreamReader(uri.LocalPath + "ressources.txt");
            while ((line = file.ReadLine()) != null)
            {
                Globals.ressources.Add(Int32.Parse(line.Split(':')[0]), line.Split(':')[1]);
            }
            file.Close();

            file = new StreamReader(uri.LocalPath + "maps.txt");
            while ((line = file.ReadLine()) != null)
            {
                Globals.maps.Add(Int32.Parse(line.Split(':')[0]), line.Split(':')[1]);
            }
            file.Close();

            file = new StreamReader(uri.LocalPath + "sorts.txt");
            while ((line = file.ReadLine()) != null)
            {
                Globals.sorts.Add(Int32.Parse(line.Split(':')[0]), line.Split(':')[1]);
            }
            file.Close();

            file = new StreamReader(uri.LocalPath + "mapchangers.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] splitLine = line.Split(';');
                Globals.mapchangers.Add(Int32.Parse(splitLine[0]), new Int32[] { Int32.Parse(splitLine[1]), Int32.Parse(splitLine[2]), Int32.Parse(splitLine[3]), Int32.Parse(splitLine[4]) });
            }
            file.Close();

            file = new StreamReader(uri.LocalPath + "harvestEquals.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] splitLine = line.Split(':');
                Globals.idResourcesTranslate.Add(Int32.Parse(splitLine[0]), Int32.Parse(splitLine[1]));
            }
            file.Close();
        }
    }
}
