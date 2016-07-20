using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;
using AbrakBot.Forms;
using System.Drawing;

namespace AbrakBot
{
    class Program
    {

        public static Config config;
        static void Main(string[] args)
        {
            Config.load();
            ResourceLoader.load();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Globals.mainForm = new Forms.Home();
            Application.Run(Globals.mainForm);
            //TCPPacketHandler.Handle(Config.serverIp, Config.serverPort);
            //SwfUnpacker.Unpack("C:/Program Files (x86)/Abrak/data/maps/4_0706131721X.swf");
            /*string path = "C:/Users/Hellong/Desktop/PACKETS/Extracts";
            
            if (Directory.Exists(path))
            {
                // This path is a directory
                ProcessDirectory(path, "parent");
            }
            else
            {
                Console.WriteLine("{0} is not a valid file or directory.", path);
            }*/

        }

        /*// Process all files in the directory passed in, recurse on any directories 
        // that are found, and process the files they contain.
        public static void ProcessDirectory(string targetDirectory, string parent)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName, parent);

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory, targetDirectory);
        }

        // Insert logic for processing found files here.
        public static void ProcessFile(string path, string parent)
        {
            string dest = "C:/Users/Hellong/Desktop/PACKETS/Txt";
            System.IO.File.Move(path, dest + "/" + parent.Substring(parent.LastIndexOf('\\')) + ".txt");
            Console.WriteLine("Parent '{0}'.", parent);
        }*/
    }
}

