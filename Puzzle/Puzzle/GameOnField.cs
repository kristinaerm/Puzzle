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
        int verticalCountOfPieces = 0;
        int horisontalCountOfPieces = 0;
        Point currentLocationOfStripZoneTopLeft;
        Point currentLocationOfStripZoneBottomRight;
        PictureBox hint;

        private Point MouseDownLocation;
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
            verticalCountOfPieces = Convert.ToInt32(picture[0]);
            horisontalCountOfPieces = Convert.ToInt32(picture[1]);
            var btm = new List<Bitmap>();
            Image img = System.Drawing.Image.FromFile(path);

            btm = Section.RectangleSection(path, picture[0], picture[1], picture[2], id_picture);//разрезаем картинку на кусочки
            List<PictureBox> pb = new List<PictureBox>();//создаем массив пикчербоксов
            int h = btm[0].Height;
            int w = btm[0].Width;
            // PictureBox p;
            int currH = 0;
            int currW = 0;
            int d = this.Controls.Count;
            int count = btm.Count;
            int countW = Int32.Parse(picture[0]);
            int countH = Int32.Parse(picture[1]);
            if (game_mode == "На поле")
            {
                for (int i = 0; i < count; i++)
                {
                    int W = currW;
                    int H = currH;
                    PictureBox p = new PictureBox();
                    p.Location = new Point(W * (w + 1) + 5, H * (h + 1) + 25);
                    p.Size = new Size(w, h);
                    currW++;
                    if (currW == countW)
                    {
                        currH++;
                        currW = 0;
                    }
                    p.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Image = (Image)btm[i];
                    pb.Add(p);
                    pb[i].Image = btm[i];
                    this.Controls.Add(p);
                    ControlMover.Add(pb[i]); //перемещение кусочков
                }
            }
            else
                if (game_mode == "В куче")
            {

            }
            else
                if (game_mode == "На ленте")
            {
                int newHeightOfForm = this.Size.Height + h + 30;

                buttonLeft.Enabled = true;
                buttonLeft.Visible = true;

                buttonRight.Enabled = true;
                buttonRight.Visible = true;

                this.Size = new Size(this.Width, newHeightOfForm);

                buttonLeft.Location = new Point(buttonLeft.Location.X, this.Size.Height - 15 - (h / 2) - buttonLeft.Height-38);
                buttonRight.Location = new Point(buttonRight.Location.X, this.Size.Height - 15 - (h / 2) - buttonRight.Height-38);

                currentLocationOfStripZoneBottomRight = new Point(this.Width - 50, this.Size.Height - 15 - 38);
                currentLocationOfStripZoneTopLeft = new Point(50, this.Size.Height - h - buttonLeft.Height - 38);

                //столько кусочков уместится на ленте
                int countOfPiecesOnStrip = (currentLocationOfStripZoneBottomRight.X - currentLocationOfStripZoneTopLeft.X-5)/(w+5);
                PictureBox p;


                for (int i = 0; i < countOfPiecesOnStrip; i++)
                {
                    p = new PictureBox();
                    p.Size = new Size(w, h);
                    p.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Image = (Image)btm[i];
                    p.Location = new Point(5+currentLocationOfStripZoneTopLeft.X+i*(w+5), currentLocationOfStripZoneTopLeft.Y);
                    pb.Add(p);
                    this.Controls.Add(p);
                    ControlMover.Add(pb[i]);
                    this.Controls.Add(p);
                }
            }
            hint = new PictureBox();
            hint.SizeMode = PictureBoxSizeMode.StretchImage;
            hint.Size = new Size((btm[0].Width + 1) * verticalCountOfPieces, (btm[0].Height + 1) * horisontalCountOfPieces);
            hint.Location = new Point(5, 25);
            hint.Image = Image.FromFile(path);
            this.Controls.Add(hint);
            hint.Visible = false;
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

        private void button_help_Click(object sender, EventArgs e)
        {
            if (hint.Visible)
            {
                button_help.Text = "Показать подсказку";
            }
            else
            {
                button_help.Text = "Скрыть подсказку";
            }
            hint.BringToFront();
            hint.Visible = !hint.Visible;
        }

        private void GameOnField_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Точно хотите выйти? Для сохранения игры нажмите Отмена и Сохранить", "Уведомление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {

        }

        private void buttonRight_Click(object sender, EventArgs e)
        {

        }
    }


}

