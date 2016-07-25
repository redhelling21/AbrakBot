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
    public partial class Remote : Form
    {
        public Remote()
        {
            InitializeComponent();
        }

        private void Remote_Load(object sender, EventArgs e)
        {
            upArrowButton.FlatStyle = FlatStyle.Flat;
            upArrowButton.FlatAppearance.BorderSize = 0;
            downArrowButton.FlatStyle = FlatStyle.Flat;
            downArrowButton.FlatAppearance.BorderSize = 0;
            leftArrowButton.FlatStyle = FlatStyle.Flat;
            leftArrowButton.FlatAppearance.BorderSize = 0;
            rightArrowButton.FlatStyle = FlatStyle.Flat;
            rightArrowButton.FlatAppearance.BorderSize = 0;
        }

        private void specificCellButton_Click(object sender, EventArgs e)
        {
            Globals.makeAMove(Int32.Parse(specificCellBox.Text), false);
        }

        private void rightArrowButton_Click(object sender, EventArgs e)
        {
            Globals.makeAMove(Globals.tpDroite, true);
        }

        private void downArrowButton_Click(object sender, EventArgs e)
        {
            Globals.makeAMove(Globals.tpBas, true);
        }

        private void leftArrowButton_Click(object sender, EventArgs e)
        {
            Globals.makeAMove(Globals.tpGauche, true);
        }

        private void upArrowButton_Click(object sender, EventArgs e)
        {
            Globals.makeAMove(Globals.tpHaut, true);
        }
    }
}
