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
        private string level = "";
        private string login = "";
        private string id_puzzle_curr = "";
        private bool exit = true;
        private string forma = "";

        public UserFindGame()
        {
            InitializeComponent();
            update_list();
        }


        public UserFindGame(string login)
        {
            InitializeComponent();
            update_list();
            this.login = login;
        }

        private void update_list()
        {
            ConnDatabase bd = new ConnDatabase();
            List<string[]> path = bd.selectPuzzlesByComplexity(level);
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

            if (button2.Text.Equals("Начать заново"))
            {
                ConnDatabase bd = new ConnDatabase();
                bd.deleteSaveByIdPuzzleAndLogin(id_puzzle_curr, login);
                bd.deleteGameByIdPuzzleAndLogin(id_puzzle_curr, login);
            }

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

                    GameOnField gameOnFieldForm = new GameOnField(id_puzzle_curr, bildOfPuzzle, modeGame, login, forma, false);
                    gameOnFieldForm.Show();
                    exit = false;
                    this.Close();
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



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //добавить обработку сейвов

            ConnDatabase bd = new ConnDatabase();
            List<string[]> puzz = bd.selectPuzzlesByComplexity(level);
            if ((listBox1.SelectedIndex < puzz.Count)&& (listBox1.SelectedIndex > -1))
            {
                string[] selected = puzz.ElementAt(listBox1.SelectedIndex);
                forma = bd.cutExcessSpace(selected[2]);
                textBox1.Text = "";
                if (selected[1][0] == '1') textBox1.Text += "Лёгкий";
                else if (selected[1][0] == '2') textBox1.Text += "Средний";
                else textBox1.Text += "Сложный";
                textBox1.Text += " \r\n" + bd.cutExcessSpace(selected[2]) + " \r\n" + bd.cutExcessSpace(selected[4]) + " x " + bd.cutExcessSpace(selected[5]);
                string path = bd.selectPathByIdPicture(selected[3]);
                Bitmap MyImage;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                MyImage = new Bitmap(path);
                pictureBox1.Image = (Image)MyImage;
                id_puzzle_curr = selected[0];
                button2.Enabled = true;
                List<string> saved_game = bd.selectAllAboutGameByLoginAndIdPuzzle(login, selected[0]);
                if (saved_game.Count > 0)
                {
                    button1.Enabled = true;
                    button1.Visible = true;
                    textBox1.Text += " \r\n" + bd.cutExcessSpace(saved_game[0]);
                    textBox1.Text += " \r\n" + bd.cutExcessSpace(saved_game[1]);
                    textBox1.Text += " \r\nРезультат:" + bd.cutExcessSpace(saved_game[2]);
                    button2.Text = "Начать заново";
                }
                else
                {
                    button1.Enabled = false;
                    button1.Visible = false;
                    button2.Text = "Играть!";
                }
            }
            else button2.Enabled = false;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            level = "";
            update_list();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void UserFindGame_Load(object sender, EventArgs e)
        {

        }

        private void UserFindGame_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (exit)
            {
                Application.Exit();
            }

        }

        //из сейва
        private void button1_Click(object sender, EventArgs e)
        {
            ConnDatabase bd = new ConnDatabase();
            List<string> saved_game = bd.selectAllAboutGameByLoginAndIdPuzzle(login, id_puzzle_curr);
            string bildOfPuzzle = saved_game[0];
            string modeGame = saved_game[1];
            GameOnField gameOnFieldForm = new GameOnField(id_puzzle_curr, bildOfPuzzle, modeGame, login, forma, true);
            gameOnFieldForm.Show();
            exit = false;
            this.Close();
        }

        private void menu_exit_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Лабороторный практикум по дисциплине: Технология программирования\n Разработчики:\n студентки группы 6104-090301D\n Глотова П.А.\n Катиркина К.И.\n Самарский университет 2018");

        }
    }
}

