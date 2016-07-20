using SwfDotNet.IO;
using SwfDotNet.IO.ByteCode;
using SwfDotNet.IO.ByteCode.Actions;
using SwfDotNet.IO.Tags;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBot
{
    class SwfUnpacker
    {
        public static void Unpack(string filepath)
        {
            SwfReader swfReader = new SwfReader(filepath);
            // Read the completed swf file
            Swf swf = swfReader.ReadSwf();

            IEnumerator tagsEnu = swf.Tags.GetEnumerator();
            while (tagsEnu.MoveNext())
            {
                BaseTag tag = (BaseTag)tagsEnu.Current;
                if (tag.ActionRecCount != 0)
                // The tag contains action script ?
                {
                    IEnumerator byteCodeEnu = tag.GetEnumerator();
                    while (byteCodeEnu.MoveNext())
                    {
                        //Init the action script decompiler
                        Decompiler dc = new Decompiler(swf.Version);
                        //Decompile the current actionscript action from a tag 
                        ArrayList actions = dc.Decompile((byte[])tagsEnu.Current);
                        foreach (BaseAction obj in actions)
                        {
                            //Write action script byte code to the console
                            Console.WriteLine(obj.ToString());
                        }
                    }
                }
            }

            swfReader.Close();
        }
    }
}
