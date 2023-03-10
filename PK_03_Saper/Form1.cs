using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PK_03_Saper
{
    public partial class FormSaper : Form
    {
        private PanelGame myGame;
        public FormSaper()
        {
            InitializeComponent();
            małaToolStripMenuItem_Click(null, null);
        }

        private void małaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myGame != null)
            {
                this.Controls.Remove(myGame);
            }
            myGame = new PanelGame(8, 8, 5);
            //aby nie było wsunięte pod menu
            myGame.Location = new Point(0, menuStrip1.Height);
            this.Controls.Add(myGame);
        }

        private void dużaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myGame != null)
            {
                this.Controls.Remove(myGame);
            }
            myGame = new PanelGame(15, 10, 20);
            //aby nie było wsunięte pod menu
            myGame.Location = new Point(0, menuStrip1.Height);
            this.Controls.Add(myGame);
        }
    }
}
