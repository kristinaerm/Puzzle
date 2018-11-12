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
    public partial class GameOnField : Form
    {
        public GameOnField()
        {
            InitializeComponent();
        }

        string id_puzzle = "";
        string game_mode = "";
        string record = "";

        //***********
        public GameOnField(string id_puzzle, string game_mode, string record)
        {
            ConnDatabase bd = new ConnDatabase();
            InitializeComponent();
            this.id_puzzle = id_puzzle;
            this.game_mode = game_mode;
            this.record = record;
            string id_picture = bd.selectIdPicture(id_puzzle);
            string path = bd.SelectPathPicture(id_picture);
            List<string> picture = bd.SelectInPuzzle(id_puzzle);
            var btm = new List<Bitmap>();
<<<<<<< HEAD
            Image img = System.Drawing.Image.FromFile(path);
            btm = Section.RectangleSection(path, picture[0], picture[1], picture[2], id_picture);//разрезаем картинку на кусочки
            List<PictureBox> pb = new List<PictureBox>();//создаем список пикчербоксов
            for (int i = 0; i < btm.Count; i++)
            {
                PictureBox a = new PictureBox();
                a.Height = btm[i].Height;
                a.Width = btm[i].Width;
                Point xy = new Point(12+(i* btm[i].Width+1), 27 + (i * btm[i].Height+1));
                a.Location = xy;
                a.SizeMode = PictureBoxSizeMode.StretchImage;
                        a.Image = btm[0];  
                flowLayoutPanel1.Controls.Add(a);
                pb.Add(a);
                //pb.Add(new PictureBox());
                //pb[i].Image = btm[i];

                // pictureBox1.Image = pb[i].Image;
            }
            //pictureBox1.Image = pb[1].Image;
            //тут резать выволить и тд
          
=======
            btm=Section.RectangleSection(path, picture[0], picture[1], picture[2], id_picture);//разрезаем картинку на кусочки
            List<PictureBox> pb = new List<PictureBox>();//создаем массив пикчербоксов
            int h = btm[0].Height;
            int w = btm[0].Width;
            PictureBox p;
            int currh = 0;
            int currw = 0;

            for (int i = 0; i < btm.Count; i++)
            {
                p = new PictureBox();
                p.Size = new Size(w, h);
                //p.Location = new Point(i*(w+5), i*(h+5));
                //p.Location = new Point(currw, currh);
                //if (currw < (flowLayoutPanel1.Size.Width - w - 5 * Convert.ToInt32(picture[0])))
                //{
                //    currw += w + 5;
                //}
                //else
                //{
                //    currw = 0;
                //    currh += h + 5;
                //}
                //впвап

                p.SizeMode = PictureBoxSizeMode.StretchImage;
                p.Image = (Image)btm[i];
                pb.Add(p);
                pb[i].Image = btm[i];
                flowLayoutPanel1.Controls.Add(p);
            }
>>>>>>> 15d667cab3ddcfa72de181025217b23805b4f292
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menu_top10_Click(object sender, EventArgs e)
        {
            Recorde recordForm = new Recorde();
            recordForm.Show();
        }

        private void button_end_game_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_pause_Click(object sender, EventArgs e)
        {

        }

        private void GameOnField_Load(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
