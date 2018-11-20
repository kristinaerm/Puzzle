using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle
{
    public partial class CreateGame : Form
    {
        private bool fromGallery = false;

        public CreateGame()
        {
            InitializeComponent();
            radio_level1.Checked = true;
        }

        public void setSelectedPic(string path)
        {
            text_picture_id.Text = path;
            Bitmap MyImage;
            picture_pazzle.SizeMode = PictureBoxSizeMode.StretchImage;
            MyImage = new Bitmap(path);
            picture_pazzle.Image = (Image)MyImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string formOfPuzzle = "";
            string width = "";
            string height = "";
            string complexity = "";
            string pictureID = "";
            string pictureID1 = "";

            if (!((radio_triangle.Checked) | (radio_square.Checked))) MessageBox.Show("Выберите форму пазла");
            else
            {
                if (radio_square.Checked) formOfPuzzle = "прямоугольник";
                else formOfPuzzle = "треугольник";

                height = numeric_height.Value.ToString();
                width = numeric_width.Value.ToString();

                if (!((radio_level3.Checked) | (radio_level2.Checked) | (radio_level1.Checked))) MessageBox.Show("Выберите сложность пазла");
                else
                {
                    if (radio_level3.Checked) complexity = "3";
                    else if (radio_level2.Checked) complexity = "2";
                    else complexity = "1";

                    if (text_picture_id.Text.Equals("")) MessageBox.Show("Выберите картинку");
                    else
                    {
                        ConnDatabase bd = new ConnDatabase();
                        pictureID = bd.selectIdByPathPicture(text_picture_id.Text);
                        pictureID1 = bd.cutExcessSpace(pictureID);                       
                        string puzzleID = bd.insertInPuzzle(complexity, formOfPuzzle, pictureID1, height, width);
                        if (puzzleID.Equals("")) MessageBox.Show("Не удалось создать такой пазл!");
                        else
                        {
                            MessageBox.Show("Пазл успешно создан!");
                            numeric_height.Value = 3;
                            numeric_width.Value = 3;
                            radio_level1.Checked = true;
                            radio_level2.Checked = false;
                            radio_level3.Checked = false;
                            radio_triangle.Checked = false;
                            radio_square.Checked = true;
                            text_picture_id.Text = "";
                            picture_pazzle.Image = null;

                        }
                    }
                }
            }
        }

        private void button_find_picture_Click(object sender, EventArgs e)
        {
            Gallery galleryForm = new Gallery(true, this);
            galleryForm.Show();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadyPuzzles readyPuzzlesForm = new ReadyPuzzles();
            readyPuzzlesForm.Show();
        }

        private void справкаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Profiles profilesForm = new Profiles();
            profilesForm.Show();
        }

        private void CreateGame_Load(object sender, EventArgs e)
        {

        }

        private void оРазработчикахToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Лабороторный практикум по дисциплине: Технология программирования\n Разработчики:\n студентки группы 6104-090301D\n Глотова П.А.\n Катиркина К.И.\n Самарский университет 2018");
        }

        private void CreateGame_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void обИгреToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
