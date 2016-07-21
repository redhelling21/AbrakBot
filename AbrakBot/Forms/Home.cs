using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbrakBot.Forms
{
    public partial class Home : Form
    {
        delegate void appendBoxDelegate(RichTextBox box, string text, Color color);
        public Home()
        {
            InitializeComponent();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if(connectButton.Text == "Connexion")
            {
                connectButton.Text = "Déconnexion";
                TCPPacketHandler.Handle(Config.serverIp, Config.serverPort);
            }
            else
            {
                connectButton.Text = "Connexion";
                TCPPacketHandler.close();
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            TCPPacketHandler.close();
            Environment.Exit(0);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public static void appendBox(RichTextBox box, string text, Color color)
        {
            if (box.InvokeRequired)
            {
                box.Invoke(new appendBoxDelegate(appendBox), new object[] { box, text, color });
                return;
            }
            box.AppendText(text, color);
        }

        public static void updateBar(ToolStripProgressBar bar, ToolStripStatusLabel label, int valeur)
        {
            bar.Value = valeur;
            label.Text = valeur + "%";

        }

        public static void updateTSLabel(ToolStripStatusLabel label, string valeur)
        {
            label.Text = valeur;
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
