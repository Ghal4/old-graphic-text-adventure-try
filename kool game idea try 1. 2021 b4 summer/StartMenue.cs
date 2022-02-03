using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace kool_game_idea_try_1._2021_b4_summer
{
    public partial class StartMenue : Form
    {
        public StartMenue()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Load_BuildCharacter(object sender, EventArgs e)
        {
            BuildCharacter buildCharacter = new BuildCharacter();
            buildCharacter.Show();

            //         StartMenue startMenue = new StartMenue();
            //          startMenue.Hide();

            //  Close();   

        }


    }

    
}

