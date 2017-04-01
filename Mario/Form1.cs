using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mario
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GameAPI game = new Game();
            List<Coordinates> crd = game.getAllUnitsCoordinates();
            int x = crd.Count;
            Console.WriteLine("x = {0}", x);
        }
    }
}
