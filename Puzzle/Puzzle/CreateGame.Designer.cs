namespace Puzzle
{
    partial class CreateGame
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radio_triangle = new System.Windows.Forms.RadioButton();
            this.radio_square = new System.Windows.Forms.RadioButton();
            this.numeric_height = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numeric_width = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.text_picture_name = new System.Windows.Forms.TextBox();
            this.button_find_picture = new System.Windows.Forms.Button();
            this.picture_pazzle = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radio_level3 = new System.Windows.Forms.RadioButton();
            this.radio_level2 = new System.Windows.Forms.RadioButton();
            this.radio_level1 = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_height)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_width)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_pazzle)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radio_triangle);
            this.groupBox1.Controls.Add(this.radio_square);
            this.groupBox1.Location = new System.Drawing.Point(21, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 59);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Форма пазлов";
            // 
            // radio_triangle
            // 
            this.radio_triangle.AutoSize = true;
            this.radio_triangle.Location = new System.Drawing.Point(141, 27);
            this.radio_triangle.Name = "radio_triangle";
            this.radio_triangle.Size = new System.Drawing.Size(90, 17);
            this.radio_triangle.TabIndex = 1;
            this.radio_triangle.TabStop = true;
            this.radio_triangle.Text = "Треугольная";
            this.radio_triangle.UseVisualStyleBackColor = true;
            // 
            // radio_square
            // 
            this.radio_square.AutoSize = true;
            this.radio_square.Location = new System.Drawing.Point(19, 27);
            this.radio_square.Name = "radio_square";
            this.radio_square.Size = new System.Drawing.Size(105, 17);
            this.radio_square.TabIndex = 0;
            this.radio_square.TabStop = true;
            this.radio_square.Text = "Прямоугольная";
            this.radio_square.UseVisualStyleBackColor = true;
            // 
            // numeric_height
            // 
            this.numeric_height.Location = new System.Drawing.Point(67, 20);
            this.numeric_height.Name = "numeric_height";
            this.numeric_height.Size = new System.Drawing.Size(34, 20);
            this.numeric_height.TabIndex = 1;
            this.numeric_height.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numeric_width);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.numeric_height);
            this.groupBox2.Location = new System.Drawing.Point(21, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 59);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Размеры пазла";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(138, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ширина";
            // 
            // numeric_width
            // 
            this.numeric_width.Location = new System.Drawing.Point(189, 20);
            this.numeric_width.Name = "numeric_width";
            this.numeric_width.Size = new System.Drawing.Size(34, 20);
            this.numeric_width.TabIndex = 3;
            this.numeric_width.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Высота";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.text_picture_name);
            this.groupBox3.Controls.Add(this.button_find_picture);
            this.groupBox3.Controls.Add(this.picture_pazzle);
            this.groupBox3.Location = new System.Drawing.Point(21, 149);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(276, 184);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Выбор картинки";
            // 
            // text_picture_name
            // 
            this.text_picture_name.Location = new System.Drawing.Point(99, 19);
            this.text_picture_name.Name = "text_picture_name";
            this.text_picture_name.ReadOnly = true;
            this.text_picture_name.Size = new System.Drawing.Size(170, 20);
            this.text_picture_name.TabIndex = 2;
            // 
            // button_find_picture
            // 
            this.button_find_picture.Location = new System.Drawing.Point(6, 19);
            this.button_find_picture.Name = "button_find_picture";
            this.button_find_picture.Size = new System.Drawing.Size(87, 21);
            this.button_find_picture.TabIndex = 1;
            this.button_find_picture.Text = "Выбрать";
            this.button_find_picture.UseVisualStyleBackColor = true;
            // 
            // picture_pazzle
            // 
            this.picture_pazzle.Location = new System.Drawing.Point(6, 46);
            this.picture_pazzle.Name = "picture_pazzle";
            this.picture_pazzle.Size = new System.Drawing.Size(264, 132);
            this.picture_pazzle.TabIndex = 0;
            this.picture_pazzle.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radio_level3);
            this.groupBox4.Controls.Add(this.radio_level2);
            this.groupBox4.Controls.Add(this.radio_level1);
            this.groupBox4.Location = new System.Drawing.Point(21, 339);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(275, 57);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Уровень сложности";
            // 
            // radio_level3
            // 
            this.radio_level3.AutoSize = true;
            this.radio_level3.Location = new System.Drawing.Point(151, 24);
            this.radio_level3.Name = "radio_level3";
            this.radio_level3.Size = new System.Drawing.Size(31, 17);
            this.radio_level3.TabIndex = 2;
            this.radio_level3.TabStop = true;
            this.radio_level3.Text = "3";
            this.radio_level3.UseVisualStyleBackColor = true;
            // 
            // radio_level2
            // 
            this.radio_level2.AutoSize = true;
            this.radio_level2.Location = new System.Drawing.Point(84, 24);
            this.radio_level2.Name = "radio_level2";
            this.radio_level2.Size = new System.Drawing.Size(31, 17);
            this.radio_level2.TabIndex = 1;
            this.radio_level2.TabStop = true;
            this.radio_level2.Text = "2";
            this.radio_level2.UseVisualStyleBackColor = true;
            // 
            // radio_level1
            // 
            this.radio_level1.AutoSize = true;
            this.radio_level1.Location = new System.Drawing.Point(19, 24);
            this.radio_level1.Name = "radio_level1";
            this.radio_level1.Size = new System.Drawing.Size(31, 17);
            this.radio_level1.TabIndex = 0;
            this.radio_level1.TabStop = true;
            this.radio_level1.Text = "1";
            this.radio_level1.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(360, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // CreateGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 408);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CreateGame";
            this.Text = "CreateGame";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_height)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_width)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_pazzle)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radio_triangle;
        private System.Windows.Forms.RadioButton radio_square;
        private System.Windows.Forms.NumericUpDown numeric_height;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numeric_width;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox text_picture_name;
        private System.Windows.Forms.Button button_find_picture;
        private System.Windows.Forms.PictureBox picture_pazzle;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radio_level3;
        private System.Windows.Forms.RadioButton radio_level2;
        private System.Windows.Forms.RadioButton radio_level1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
    }
}