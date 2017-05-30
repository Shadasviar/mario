using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;

namespace Global
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            List<int> keys = new List<int>(new int[8]);
            GameAPI game = new Game(ref keys);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new Form1(game, ref keys, 0).Show();
            new Form1(game, ref keys, 1).Show();
            Application.Run();
        }
    }
}
