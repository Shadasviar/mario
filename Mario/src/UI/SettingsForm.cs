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
            this.ActiveControl = Submit_Settings_Game;
            this.n_players_comboBox.Text = Settings.Default.players_number.ToString();
            this.FPS_changing.Text = Settings.Default.fps.ToString();
            this.Sound_Settings.Checked = !(Settings.Default.Sound_Off);
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

        private void Submit_Settings_Game_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void Sound_Settings_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.Sound_Off = !Sound_Settings.Checked;
        }
    }
}
