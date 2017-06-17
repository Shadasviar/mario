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
            // if((Settings.Default.fps != null) && (Settings.Default.fps != < 0)) something gets wrong или не нужно было проверять так как в Parse уже занесены 3 исключения ?
            Settings.Default.fps = Int32.Parse(this.FPS_changing.SelectedText.ToString());
        }
    }
}
