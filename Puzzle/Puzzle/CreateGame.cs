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
        }
        public CreateGame(bool fromGal)
        {
            InitializeComponent();
            fromGallery = fromGal;
        }

        public Bitmap CutImage(Bitmap src, Rectangle rect)
        {

            Bitmap bmp = new Bitmap(src.Width, src.Height); //создаем битмап

            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(src, 0, 0, rect, GraphicsUnit.Pixel); //перерисовываем с источника по координатам

            return bmp;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string formOfPuzzle = "";
            string width = "";
            string height = "";
            string complexity = "";
            string pictureID = "";
            string picturePath = "";

            if (!((radio_triangle.Checked) | (radio_square.Checked))) MessageBox.Show("Выберите форму пазла");
            else
            {
                if (radio_square.Checked) formOfPuzzle = "прямоугольник";
                else formOfPuzzle = "треугольник";

                height = numeric_height.Value.ToString();
                width = numeric_width.Value.ToString();

                if (!((radio_level3.Checked) | (radio_level3.Checked) | (radio_level3.Checked))) MessageBox.Show("Выберите сложность пазла");
                else
                {
                    if (radio_level3.Checked) complexity = "3";
                    else if (radio_level2.Checked) complexity = "2";
                    else complexity = "1";

                    if (text_picture_id.Text.Equals("")) MessageBox.Show("Выберите картинку");
                    else
                    {
                        pictureID = text_picture_id.Text;

                        //запись пазла в базу
                        ConnDatabase bd = new ConnDatabase();
                        string puzzleID = bd.InsertInPuzzle(complexity, formOfPuzzle, pictureID, height, width);
                        
                    }
                }
            }
        }

        private void button_find_picture_Click(object sender, EventArgs e)
        {
            Gallery galleryForm = new Gallery(true);
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

        private void picture_pazzle_Click(object sender, EventArgs e)
        {


        }
        //принятие и отображение выбранной картинки в picture_pazzle из галлереи
        private void CreateGame_Load(object sender, EventArgs e)
        {
            if (fromGallery == true)
            {
                Gallery gal = (Gallery)this.Owner;
                picture_pazzle.Image = new Bitmap(gal.DataForm());
            }
        }
    }
}
