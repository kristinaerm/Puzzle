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
            this.components = new System.ComponentModel.Container();
            this.button_pause = new System.Windows.Forms.Button();
            this.button_help = new System.Windows.Forms.Button();
            this.button_end_game = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menu_top10 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_about_game = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonRight = new System.Windows.Forms.Button();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_time = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_pause
            // 
            this.button_pause.Location = new System.Drawing.Point(535, 42);
            this.button_pause.Name = "button_pause";
            this.button_pause.Size = new System.Drawing.Size(91, 23);
            this.button_pause.TabIndex = 0;
            this.button_pause.Text = "Пауза";
            this.button_pause.UseVisualStyleBackColor = true;
            this.button_pause.Click += new System.EventHandler(this.button_pause_Click);
            // 
            // button_help
            // 
            this.button_help.Location = new System.Drawing.Point(535, 156);
            this.button_help.Name = "button_help";
            this.button_help.Size = new System.Drawing.Size(91, 23);
            this.button_help.TabIndex = 1;
            this.button_help.Text = "Подсказка";
            this.button_help.UseVisualStyleBackColor = true;
            this.button_help.Click += new System.EventHandler(this.button_help_Click);
            // 
            // button_end_game
            // 
            this.button_end_game.Location = new System.Drawing.Point(535, 100);
            this.button_end_game.Name = "button_end_game";
            this.button_end_game.Size = new System.Drawing.Size(91, 23);
            this.button_end_game.TabIndex = 2;
            this.button_end_game.Text = "Закончить";
            this.button_end_game.UseVisualStyleBackColor = true;
            this.button_end_game.Click += new System.EventHandler(this.button_end_game_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(535, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_top10,
            this.menu_about_game,
            this.menu_exit,
            this.выходToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(638, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menu_top10
            // 
            this.menu_top10.Name = "menu_top10";
            this.menu_top10.Size = new System.Drawing.Size(63, 20);
            this.menu_top10.Text = "Рейтинг";
            this.menu_top10.Click += new System.EventHandler(this.menu_top10_Click);
            // 
            // menu_about_game
            // 
            this.menu_about_game.Name = "menu_about_game";
            this.menu_about_game.Size = new System.Drawing.Size(63, 20);
            this.menu_about_game.Text = "Об игре";
            // 
            // menu_exit
            // 
            this.menu_exit.Name = "menu_exit";
            this.menu_exit.Size = new System.Drawing.Size(112, 20);
            this.menu_exit.Text = "О разработчиках";
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // buttonRight
            // 
            this.buttonRight.Enabled = false;
            this.buttonRight.Location = new System.Drawing.Point(592, 365);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(31, 32);
            this.buttonRight.TabIndex = 5;
            this.buttonRight.Text = ">";
            this.buttonRight.UseVisualStyleBackColor = true;
            this.buttonRight.Visible = false;
            this.buttonRight.Click += new System.EventHandler(this.buttonRight_Click);
            // 
            // buttonLeft
            // 
            this.buttonLeft.Enabled = false;
            this.buttonLeft.Location = new System.Drawing.Point(15, 365);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(31, 32);
            this.buttonLeft.TabIndex = 6;
            this.buttonLeft.Text = "<";
            this.buttonLeft.UseVisualStyleBackColor = true;
            this.buttonLeft.Visible = false;
            this.buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_time
            // 
            this.label_time.AutoSize = true;
            this.label_time.Location = new System.Drawing.Point(550, 133);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(0, 13);
            this.label_time.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(539, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 8;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // GameOnField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(638, 422);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_time);
            this.Controls.Add(this.buttonLeft);
            this.Controls.Add(this.buttonRight);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_end_game);
            this.Controls.Add(this.button_help);
            this.Controls.Add(this.button_pause);
            this.MaximizeBox = false;
            this.Name = "GameOnField";
            this.Text = "Игра";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameOnField_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_pause;
        private System.Windows.Forms.Button button_help;
        private System.Windows.Forms.Button button_end_game;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu_top10;
        private System.Windows.Forms.ToolStripMenuItem menu_about_game;
        private System.Windows.Forms.ToolStripMenuItem menu_exit;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.Label label1;
    }
}