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
            this.text_picture_id = new System.Windows.Forms.TextBox();
            this.button_find_picture = new System.Windows.Forms.Button();
            this.picture_pazzle = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radio_level3 = new System.Windows.Forms.RadioButton();
            this.radio_level2 = new System.Windows.Forms.RadioButton();
            this.radio_level1 = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.обИгреToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оРазработчикахToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
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
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Форма пазлов";
            // 
            // radio_triangle
            // 
            this.radio_triangle.AutoSize = true;
            this.radio_triangle.Location = new System.Drawing.Point(8, 42);
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
            this.radio_square.Location = new System.Drawing.Point(8, 19);
            this.radio_square.Name = "radio_square";
            this.radio_square.Size = new System.Drawing.Size(105, 17);
            this.radio_square.TabIndex = 0;
            this.radio_square.TabStop = true;
            this.radio_square.Text = "Прямоугольная";
            this.radio_square.UseVisualStyleBackColor = true;
            // 
            // numeric_height
            // 
            this.numeric_height.Location = new System.Drawing.Point(97, 19);
            this.numeric_height.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numeric_height.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
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
            this.groupBox2.Location = new System.Drawing.Point(193, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(184, 71);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Размеры пазла";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ширина";
            // 
            // numeric_width
            // 
            this.numeric_width.Location = new System.Drawing.Point(97, 39);
            this.numeric_width.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numeric_width.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
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
            this.label1.Location = new System.Drawing.Point(17, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Высота";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.text_picture_id);
            this.groupBox3.Controls.Add(this.button_find_picture);
            this.groupBox3.Controls.Add(this.picture_pazzle);
            this.groupBox3.Location = new System.Drawing.Point(12, 104);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(277, 217);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Выбор картинки";
            // 
            // text_picture_id
            // 
            this.text_picture_id.Location = new System.Drawing.Point(99, 19);
            this.text_picture_id.Name = "text_picture_id";
            this.text_picture_id.ReadOnly = true;
            this.text_picture_id.Size = new System.Drawing.Size(170, 20);
            this.text_picture_id.TabIndex = 2;
            // 
            // button_find_picture
            // 
            this.button_find_picture.Location = new System.Drawing.Point(6, 19);
            this.button_find_picture.Name = "button_find_picture";
            this.button_find_picture.Size = new System.Drawing.Size(87, 21);
            this.button_find_picture.TabIndex = 1;
            this.button_find_picture.Text = "Выбрать";
            this.button_find_picture.UseVisualStyleBackColor = true;
            this.button_find_picture.Click += new System.EventHandler(this.button_find_picture_Click);
            // 
            // picture_pazzle
            // 
            this.picture_pazzle.Location = new System.Drawing.Point(6, 46);
            this.picture_pazzle.Name = "picture_pazzle";
            this.picture_pazzle.Size = new System.Drawing.Size(263, 165);
            this.picture_pazzle.TabIndex = 0;
            this.picture_pazzle.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radio_level3);
            this.groupBox4.Controls.Add(this.radio_level2);
            this.groupBox4.Controls.Add(this.radio_level1);
            this.groupBox4.Location = new System.Drawing.Point(295, 104);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(82, 175);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Сложность";
            // 
            // radio_level3
            // 
            this.radio_level3.AutoSize = true;
            this.radio_level3.Location = new System.Drawing.Point(23, 38);
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
            this.radio_level2.Location = new System.Drawing.Point(23, 79);
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
            this.radio_level1.Location = new System.Drawing.Point(23, 129);
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
            this.справкаToolStripMenuItem,
            this.справкаToolStripMenuItem1,
            this.обИгреToolStripMenuItem,
            this.оРазработчикахToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(392, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.справкаToolStripMenuItem.Text = "Готовые пазлы";
            this.справкаToolStripMenuItem.Click += new System.EventHandler(this.справкаToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem1
            // 
            this.справкаToolStripMenuItem1.Name = "справкаToolStripMenuItem1";
            this.справкаToolStripMenuItem1.Size = new System.Drawing.Size(107, 20);
            this.справкаToolStripMenuItem1.Text = "Учетные записи";
            this.справкаToolStripMenuItem1.Click += new System.EventHandler(this.справкаToolStripMenuItem1_Click);
            // 
            // обИгреToolStripMenuItem
            // 
            this.обИгреToolStripMenuItem.Name = "обИгреToolStripMenuItem";
            this.обИгреToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.обИгреToolStripMenuItem.Text = "Об игре";
            this.обИгреToolStripMenuItem.Click += new System.EventHandler(this.обИгреToolStripMenuItem_Click);
            // 
            // оРазработчикахToolStripMenuItem
            // 
            this.оРазработчикахToolStripMenuItem.Name = "оРазработчикахToolStripMenuItem";
            this.оРазработчикахToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
            this.оРазработчикахToolStripMenuItem.Text = "О разработчиках";
            this.оРазработчикахToolStripMenuItem.Click += new System.EventHandler(this.оРазработчикахToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(295, 285);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Создать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CreateGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 333);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "CreateGame";
            this.Text = "Создать новую игру";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateGame_FormClosing_1);
            this.Load += new System.EventHandler(this.CreateGame_Load);
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
        private System.Windows.Forms.TextBox text_picture_id;
        private System.Windows.Forms.Button button_find_picture;
        private System.Windows.Forms.PictureBox picture_pazzle;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radio_level3;
        private System.Windows.Forms.RadioButton radio_level2;
        private System.Windows.Forms.RadioButton radio_level1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem обИгреToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оРазработчикахToolStripMenuItem;
    }
}