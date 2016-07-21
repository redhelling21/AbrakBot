﻿using AbrakBot.Forms;
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
            Home.appendBox(mainForm.mainBox, text, color);
        }

        public static void writeToDebugBox(string text, Color color)
        {
            Home.appendBox(mainForm.debugBox, text, color);
        }

        public static void updateBars(int pdv, int xp, int pods, int energie)
        {
            if(pdv >= 0)
            {
                Home.updateBar(mainForm.pdvBar, mainForm.pdvLabel, pdv);
            }
            if (xp >= 0)
            {
                Home.updateBar(mainForm.xpBar, mainForm.xpLabel, xp);
            }
            if (pods >= 0)
            {
                Home.updateBar(mainForm.podsBar, mainForm.podsLabel, pods);
            }
            if (energie >= 0)
            {
                Home.updateBar(mainForm.enerBar, mainForm.enerLabel, energie);
            }
        }

        public static void updateCharName(string charName)
        {
            Home.updateTSLabel(mainForm.charNameLabel, charName);
        }

        public static void updateLevel(string lvl)
        {
            Home.updateTSLabel(mainForm.lvlLabel, lvl);
        }

        public static void updateKamas(string kamas)
        {
            Home.updateTSLabel(mainForm.kamasLabel, kamas);
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