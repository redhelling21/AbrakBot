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
        delegate void updateTSLabelDelegate(ToolStrip ts, ToolStripStatusLabel label, string valeur);
        delegate void updateLabelDelegate(Label label, string valeur);
        delegate void updateTSBarDelegate(ToolStrip ts, ToolStripProgressBar bar, ToolStripStatusLabel label, int valeur);
        delegate void updateTSButtonTextDelegate(ToolStrip ts, ToolStripButton button, string text);
        delegate void updateBarDelegate(ProgressBar bar, int valeur);
        public Home()
        {
            InitializeComponent();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if(connectButton.Text == "Connexion")
            {
                connectButton.Text = "Déconnexion";
                Globals.connect = new TCPPacketHandler();
                Globals.connect.Handle(Config.serverIp, Config.serverPort);
            }
            else
            {
                connectButton.Text = "Connexion";
                Globals.connect.close();
                Globals.connect.shouldStop = true;
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            trajetsList.Items.AddRange(Globals.getTrajetList());
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Globals.connect.close();
            Globals.connect.shouldStop = true;
            Environment.Exit(0);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public void appendBox(RichTextBox box, string text, Color color)
        {
            
            if (box.InvokeRequired)
            {
                box.Invoke(new appendBoxDelegate(appendBox), new object[] { box, text, color });
                return;
            }
            box.AppendText(text, color);
            box.ScrollToCaret();
        }

        public static void updateTSButtonText(ToolStrip ts, ToolStripButton button, string text)
        {
            if (ts.InvokeRequired)
            {
                ts.Invoke(new updateTSButtonTextDelegate(updateTSButtonText), new object[] { ts, button, text });
                return;
            }
        }

        public static void updateTSBar(ToolStrip ts, ToolStripProgressBar bar, ToolStripStatusLabel label, int valeur)
        {
            if (ts.InvokeRequired)
            {
                ts.Invoke(new updateTSBarDelegate(updateTSBar), new object[] { ts, bar, label, valeur });
                return;
            }
            bar.Value = valeur;
            label.Text = valeur + "%";

        }

        public static void updateTSLabel(ToolStrip ts, ToolStripStatusLabel label, string valeur)
        {
            if (ts.InvokeRequired)
            {
                ts.Invoke(new updateTSLabelDelegate(updateTSLabel), new object[] { ts, label, valeur });
                return;
            }
            label.Text = valeur;
        }

        public static void updateLabel(Label label, string valeur)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new updateLabelDelegate(updateLabel), new object[] { label, valeur });
                return;
            }
            label.Text = valeur;
        }

        public static void updateBar(ProgressBar bar, int valeur)
        {
            if (bar.InvokeRequired)
            {
                bar.Invoke(new updateBarDelegate(updateBar), new object[] { bar, valeur });
                return;
            }
            bar.Value = valeur;
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void sendMessageBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                Globals.sendMessage(sendMessageBox.Text);
                sendMessageBox.Clear();
            }
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<Int32, Int32> entry in Globals.actualResources)
            {
                HarvestHandler.Recolter(entry.Key);
            }
        }

        private void trajetsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.setActiveTrajet((string)trajetsList.SelectedItem);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (startButton.Text == "Lancer")
            {
                startButton.Text = "Arrêter";
                Globals.isRunning = true;
                ThreadTrajet.handleTrajet();
            }
            else
            {
                startButton.Text = "Lancer";
                Globals.isRunning = false;
            }
        }

        private void remoteControlButton_Click(object sender, EventArgs e)
        {
            Remote rem= new Forms.Remote();
            rem.Show();
        }
    }
}
