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
    public partial class CreateGame : Form
    {
        public CreateGame()
        {
            InitializeComponent();
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

                        if (text_picture_path.Text.Equals("")) MessageBox.Show("Выберите картинку");
                        else
                        {
                            picturePath = text_picture_path.Text;

                            //запись пазла в базу
                            ConnDatabase bd = new ConnDatabase();
                            bd.InsertInPuzzle(complexity, formOfPuzzle, pictureID, height, width);

                            //генерация кусочков из картинки
                            //пока прямоугольные
                            Image temp = Image.FromFile(picturePath);
                            Bitmap src = new Bitmap(temp, temp.Width, temp.Height);

                            for (int i = 1; i <= numeric_height.Value; i++)
                            {
                                for (int j = 1; j <= numeric_width.Value; j++)
                                {
                                    // Задаем нужную область вырезания (отсчет с верхнего левого угла)
                                    Rectangle rect = new Rectangle(new Point(0, 0), new Size(pictureBox1.Width / 2, pictureBox1.Height / 2));
                                    // передаем в нашу функцию   
                                    Bitmap CuttedImage = CutImage(src, rect);
                                    // результат изображение передаем на форму 
                                    pictureBox1.Image = CuttedImage;
                                    //запись кусочков в базу

                                }
                            }
                        }
                    }
                }
            }


        }

        private void button_find_picture_Click(object sender, EventArgs e)
        {

        }
    }
}
