using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kool_game_idea_try_1._2021_b4_summer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
//            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartMenue());
        }
    }
}




// // // // // // I disabled Application.VisualStyles() for the progress bar. make sure to enable it if it makes trouble!!
// // // // // // ... it also makes everything look different. hmmmmm...