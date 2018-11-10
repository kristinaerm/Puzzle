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
                s += file[1]+", "+ file[2] + ", " + file[4] + " x " + file[5];
                
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
            string levelComplexity = "";
            string modeGame = "";

            //*********
            if (radioButton1.Checked) {
                bildOfPuzzle = "На поле";
            }
            else
                if (radioButton2.Checked) { bildOfPuzzle = "В куче"; }
            else
            {
                bildOfPuzzle = "На ленте";
            }
        
            if (radioButton7.Checked) { modeGame = "На время"; }
            else
                if (radioButton8.Checked) { modeGame = "На очки"; }

            GameOnField gameOnFieldForm = new GameOnField(id_puzzle_curr, bildOfPuzzle, modeGame);
            gameOnFieldForm.Show();
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
            string[] selected = puzz.ElementAt(listBox1.SelectedIndex);
            textBox1.Text = selected[1] + ", " + selected[2] + ", " + selected[4] + " x " + selected[5];
            string path = bd.selectPicture(selected[3]);
            pictureBox1.Image = new Bitmap(path);
            id_puzzle_curr = selected[0];
        }
    }
}
