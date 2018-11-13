﻿namespace Puzzle
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
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menu_top10 = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_about_game = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_pause
            // 
            this.button_pause.Location = new System.Drawing.Point(546, 42);
            this.button_pause.Name = "button_pause";
            this.button_pause.Size = new System.Drawing.Size(75, 23);
            this.button_pause.TabIndex = 0;
            this.button_pause.Text = "Пауза";
            this.button_pause.UseVisualStyleBackColor = true;
            this.button_pause.Click += new System.EventHandler(this.button_pause_Click);
            // 
            // button_help
            // 
            this.button_help.Location = new System.Drawing.Point(546, 312);
            this.button_help.Name = "button_help";
            this.button_help.Size = new System.Drawing.Size(75, 23);
            this.button_help.TabIndex = 1;
            this.button_help.Text = "Подсказка";
            this.button_help.UseVisualStyleBackColor = true;
            // 
            // button_end_game
            // 
            this.button_end_game.Location = new System.Drawing.Point(546, 71);
            this.button_end_game.Name = "button_end_game";
            this.button_end_game.Size = new System.Drawing.Size(75, 23);
            this.button_end_game.TabIndex = 2;
            this.button_end_game.Text = "Закончить";
            this.button_end_game.UseVisualStyleBackColor = true;
            this.button_end_game.Click += new System.EventHandler(this.button_end_game_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(546, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
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
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 27);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(510, 383);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // GameOnField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 422);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_end_game);
            this.Controls.Add(this.button_help);
            this.Controls.Add(this.button_pause);
            this.Name = "GameOnField";
            this.Text = "Игра";
            this.Load += new System.EventHandler(this.GameOnField_Load);
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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}