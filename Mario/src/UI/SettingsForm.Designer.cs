namespace Global
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.n_players_comboBox = new System.Windows.Forms.ComboBox();
            this.FPS_changing = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Submit_Settings_Game = new System.Windows.Forms.Button();
            this.Sound_Settings = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Players number";
            // 
            // n_players_comboBox
            // 
            this.n_players_comboBox.FormattingEnabled = true;
            this.n_players_comboBox.Items.AddRange(new object[] {
            "1",
            "2"});
            this.n_players_comboBox.Location = new System.Drawing.Point(139, 6);
            this.n_players_comboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.n_players_comboBox.Name = "n_players_comboBox";
            this.n_players_comboBox.Size = new System.Drawing.Size(121, 24);
            this.n_players_comboBox.TabIndex = 1;
            this.n_players_comboBox.SelectedIndexChanged += new System.EventHandler(this.n_players_comboBox_SelectedIndexChanged);
            // 
            // FPS_changing
            // 
            this.FPS_changing.Location = new System.Drawing.Point(139, 38);
            this.FPS_changing.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FPS_changing.Name = "FPS_changing";
            this.FPS_changing.Size = new System.Drawing.Size(120, 22);
            this.FPS_changing.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(9, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Changing FPS\r\n";
            // 
            // Submit_Settings_Game
            // 
            this.Submit_Settings_Game.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Submit_Settings_Game.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Submit_Settings_Game.Location = new System.Drawing.Point(263, 272);
            this.Submit_Settings_Game.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Submit_Settings_Game.Name = "Submit_Settings_Game";
            this.Submit_Settings_Game.Size = new System.Drawing.Size(104, 32);
            this.Submit_Settings_Game.TabIndex = 4;
            this.Submit_Settings_Game.Text = "Submit";
            this.Submit_Settings_Game.UseVisualStyleBackColor = false;
            this.Submit_Settings_Game.Click += new System.EventHandler(this.Submit_Settings_Game_Click);
            // 
            // Sound_Settings
            // 
            this.Sound_Settings.AutoSize = true;
            this.Sound_Settings.BackColor = System.Drawing.Color.Transparent;
            this.Sound_Settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Sound_Settings.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Sound_Settings.Location = new System.Drawing.Point(284, 9);
            this.Sound_Settings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Sound_Settings.Name = "Sound_Settings";
            this.Sound_Settings.Size = new System.Drawing.Size(76, 21);
            this.Sound_Settings.TabIndex = 6;
            this.Sound_Settings.Text = "Sound";
            this.Sound_Settings.UseVisualStyleBackColor = false;
            this.Sound_Settings.CheckedChanged += new System.EventHandler(this.Sound_Settings_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Mario.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(383, 318);
            this.Controls.Add(this.Sound_Settings);
            this.Controls.Add(this.Submit_Settings_Game);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.FPS_changing);
            this.Controls.Add(this.n_players_comboBox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox n_players_comboBox;
        private System.Windows.Forms.TextBox FPS_changing;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Submit_Settings_Game;
        private System.Windows.Forms.CheckBox Sound_Settings;
    }
}