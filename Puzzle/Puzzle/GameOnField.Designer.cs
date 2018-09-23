namespace Puzzle
{
    partial class GameOnField
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
            this.button_pause = new System.Windows.Forms.Button();
            this.button_help = new System.Windows.Forms.Button();
            this.button_end_game = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_pause
            // 
            this.button_pause.Location = new System.Drawing.Point(494, 12);
            this.button_pause.Name = "button_pause";
            this.button_pause.Size = new System.Drawing.Size(75, 23);
            this.button_pause.TabIndex = 0;
            this.button_pause.Text = "Пауза";
            this.button_pause.UseVisualStyleBackColor = true;
            // 
            // button_help
            // 
            this.button_help.Location = new System.Drawing.Point(494, 41);
            this.button_help.Name = "button_help";
            this.button_help.Size = new System.Drawing.Size(75, 23);
            this.button_help.TabIndex = 1;
            this.button_help.Text = "Подсказка";
            this.button_help.UseVisualStyleBackColor = true;
            // 
            // button_end_game
            // 
            this.button_end_game.Location = new System.Drawing.Point(494, 70);
            this.button_end_game.Name = "button_end_game";
            this.button_end_game.Size = new System.Drawing.Size(75, 23);
            this.button_end_game.TabIndex = 2;
            this.button_end_game.Text = "Закончить";
            this.button_end_game.UseVisualStyleBackColor = true;
            // 
            // GameOnField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 405);
            this.Controls.Add(this.button_end_game);
            this.Controls.Add(this.button_help);
            this.Controls.Add(this.button_pause);
            this.Name = "GameOnField";
            this.Text = "GameOnField";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_pause;
        private System.Windows.Forms.Button button_help;
        private System.Windows.Forms.Button button_end_game;
    }
}