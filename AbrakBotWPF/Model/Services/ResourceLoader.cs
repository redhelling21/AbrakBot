using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF.Model.Services
{
    class ResourceLoader
    {
        public static void load(Globals globals)
        {
            string line;
            string respath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "/Resources/";
            Uri uri = new Uri(respath);
            StreamReader file;
            file = new StreamReader(uri.LocalPath + "objets.txt");
            while ((line = file.ReadLine()) != null)
            {
                globals.objects.Add(Int32.Parse(line.Split(':')[0]), line.Split(':')[1]);
            }
            file.Close();

            file = new StreamReader(uri.LocalPath + "ressources.txt");
            while ((line = file.ReadLine()) != null)
            {
                globals.ressources.Add(Int32.Parse(line.Split(':')[0]), line.Split(':')[1]);
            }
            file.Close();

            file = new StreamReader(uri.LocalPath + "maps.txt");
            while ((line = file.ReadLine()) != null)
            {
                globals.maps.Add(Int32.Parse(line.Split(':')[0]), line.Split(':')[1]);
            }
            file.Close();

            file = new StreamReader(uri.LocalPath + "sorts.txt");
            while ((line = file.ReadLine()) != null)
            {
                globals.sorts.Add(Int32.Parse(line.Split(':')[0]), line.Split(':')[1]);
            }
            file.Close();

            file = new StreamReader(uri.LocalPath + "mapchangers.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] splitLine = line.Split(';');
                globals.mapchangers.Add(Int32.Parse(splitLine[0]), new Int32[] { Int32.Parse(splitLine[1]), Int32.Parse(splitLine[2]), Int32.Parse(splitLine[3]), Int32.Parse(splitLine[4]) });
            }
            file.Close();

            file = new StreamReader(uri.LocalPath + "harvestEquals.txt");
            while ((line = file.ReadLine()) != null)
            {
                string[] splitLine = line.Split(':');
                globals.idResourcesTranslate.Add(Int32.Parse(splitLine[0]), Int32.Parse(splitLine[1]));
            }
            file.Close();

            for (int i = 0; i < 1000; i++)
            {
                globals.sortsMin[i] = -1;
                globals.sortsMax[i] = -1;
            }

            file = new StreamReader(uri.LocalPath + "sorts.txt");
            while ((line = file.ReadLine()) != null)
            {
                string id = line.Split(':')[0];
                string sort = line.Split(':')[1];
                if (line.Split(':').Length == 4)
                {
                    globals.sortsMin[Int32.Parse(id)] = Int32.Parse(line.Split(':')[2]);
                    globals.sortsMax[Int32.Parse(id)] = Int32.Parse(line.Split(':')[3]);
                }

            }
            file.Close();


        }
    }
}
