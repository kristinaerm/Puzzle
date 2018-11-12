using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class UserFindGame : Form
    {
        string level = "";
        public UserFindGame()
        {
            InitializeComponent();
            update_list();
        }

        private void update_list()
        {
            ConnDatabase bd = new ConnDatabase();
            List<string[]> path = bd.SelectPuzzles(level);
            string s = "";
            // заполняем список изображениями
            listBox1.Items.Clear();
            foreach (string[] file in path)
            {
                s = "";
                if (file[1][0] == '1') s += "Лёгкий";
                else if (file[1][0] == '2') s += "Средний";
                else s += "Сложный";
                s += ", " + bd.cutExcessSpace(file[2]) + ", " + bd.cutExcessSpace(file[4]) + " x " + bd.cutExcessSpace(file[5]);

                listBox1.Items.Add(s);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void menu_top10_Click(object sender, EventArgs e)
        {
            Recorde recordForm = new Recorde();
            recordForm.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string bildOfPuzzle = "";
            string modeGame = "";

            if ((radioButton1.Checked) || (radioButton2.Checked) || (radioButton3.Checked))
            {
                if (radioButton1.Checked)
                {
                    bildOfPuzzle = "На поле";
                }
                else if (radioButton2.Checked) { bildOfPuzzle = "В куче"; }
                else if (radioButton3.Checked)
                {
                    bildOfPuzzle = "На ленте";
                }

                if ((radioButton7.Checked) || (radioButton8.Checked))
                {
                    if (radioButton7.Checked) { modeGame = "На время"; }
                    else if (radioButton8.Checked) { modeGame = "На очки"; }

                    GameOnField gameOnFieldForm = new GameOnField(id_puzzle_curr, bildOfPuzzle, modeGame);
                    gameOnFieldForm.Show();
                }
                else
                {
                    MessageBox.Show("Выберите режим игры!");
                }
            }
            else
            {
                MessageBox.Show("Выберите режим сборки!");
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            level = "3";
            update_list();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            level = "2";
            update_list();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            level = "1";
            update_list();
        }

        private string id_puzzle_curr = "";

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //вывод картинки в пиксербокс и текста в текстбокс
            ConnDatabase bd = new ConnDatabase();
            List<string[]> puzz = bd.SelectPuzzles(level);
            if (listBox1.SelectedIndex< puzz.Count)
            {
                string[] selected = puzz.ElementAt(listBox1.SelectedIndex);
                textBox1.Text = "";
                if (selected[1][0] == '1') textBox1.Text += "Лёгкий";
                else if (selected[1][0] == '2') textBox1.Text += "Средний";
                else textBox1.Text += "Сложный";
                textBox1.Text += " \r\n" + bd.cutExcessSpace(selected[2]) + " \r\n" + bd.cutExcessSpace(selected[4]) + " x " + bd.cutExcessSpace(selected[5]);
                string path = bd.selectPicture(selected[3]);
                Bitmap MyImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                MyImage = new Bitmap(path);
                pictureBox1.Image = (Image)MyImage;
                id_puzzle_curr = selected[0];
            }            
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            level = "";
            update_list();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
