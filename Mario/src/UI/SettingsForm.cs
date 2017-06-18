using System;
using Mario.Properties;
using System.Windows.Forms;

namespace Global
{
    public partial class SettingsForm : Form
    {
        MainMenu parent;
        public SettingsForm(MainMenu parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        private void n_players_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.players_number = Int32.Parse(this.n_players_comboBox.SelectedItem.ToString());
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.Show();
        }

        private void FPS_changing_TextChanged(object sender, EventArgs e)
        {
            if (Int32.Parse(this.FPS_changing.Text.ToString()) <= 0)
            {
                return;
            }
            Settings.Default.fps = Int32.Parse(this.FPS_changing.Text.ToString());
        }
    }
}
