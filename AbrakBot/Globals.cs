using AbrakBot.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbrakBot
{
    static class Globals
    {
        public static bool isConnected = false;
        public static bool isFighting = false;
        public static bool isInGame = false;
        public static int currentMapId = 0;
        public static Dictionary<Int32, string> objects = new Dictionary<Int32, string>();
        public static Dictionary<Int32, string> ressources = new Dictionary<Int32, string>();
        public static Dictionary<Int32, string> sorts = new Dictionary<Int32, string>();
        public static Dictionary<Int32, string> maps = new Dictionary<Int32, string>();
        public static Home mainForm;

        public static void writeToMainBox(string text, Color color)
        {
            mainForm.mainBox.AppendText(text, color);
        }

        public static void writeToDebugBox(string text, Color color)
        {
            mainForm.debugBox.AppendText(text, color);
        }
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
