namespace Global
{
    partial class MainMenu
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
            this.new_game_button = new System.Windows.Forms.Button();
            this.settings_button = new System.Windows.Forms.Button();
            this.gameoverLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // new_game_button
            // 
            this.new_game_button.BackgroundImage = global::Mario.Properties.Resources.cegla;
            this.new_game_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.new_game_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.new_game_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.8F);
            this.new_game_button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.new_game_button.Location = new System.Drawing.Point(12, 12);
            this.new_game_button.Name = "new_game_button";
            this.new_game_button.Size = new System.Drawing.Size(316, 52);
            this.new_game_button.TabIndex = 0;
            this.new_game_button.Text = "NEW GAME";
            this.new_game_button.UseVisualStyleBackColor = true;
            this.new_game_button.Click += new System.EventHandler(this.new_game_button_Click);
            // 
            // settings_button
            // 
            this.settings_button.BackgroundImage = global::Mario.Properties.Resources.cegla;
            this.settings_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.8F);
            this.settings_button.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.settings_button.Location = new System.Drawing.Point(12, 70);
            this.settings_button.Name = "settings_button";
            this.settings_button.Size = new System.Drawing.Size(316, 52);
            this.settings_button.TabIndex = 1;
            this.settings_button.Text = "SETTINGS";
            this.settings_button.UseVisualStyleBackColor = true;
            this.settings_button.Click += new System.EventHandler(this.settings_button_Click);
            // 
            // gameoverLabel
            // 
            this.gameoverLabel.AutoSize = true;
            this.gameoverLabel.BackColor = System.Drawing.Color.Transparent;
            this.gameoverLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gameoverLabel.ForeColor = System.Drawing.Color.Snow;
            this.gameoverLabel.Location = new System.Drawing.Point(12, 231);
            this.gameoverLabel.Name = "gameoverLabel";
            this.gameoverLabel.Size = new System.Drawing.Size(331, 57);
            this.gameoverLabel.TabIndex = 2;
            this.gameoverLabel.Text = "GAME OVER";
            this.gameoverLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Mario.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(340, 295);
            this.Controls.Add(this.gameoverLabel);
            this.Controls.Add(this.settings_button);
            this.Controls.Add(this.new_game_button);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button new_game_button;
        private System.Windows.Forms.Button settings_button;
        private System.Windows.Forms.Label gameoverLabel;
    }
}