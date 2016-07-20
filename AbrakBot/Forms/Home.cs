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
    }
}
