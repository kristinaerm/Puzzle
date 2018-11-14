using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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

        private string id_puzzle = "";
        private string game_mode = "";
        private string record = "";
        private int verticalCountOfPieces = 0;
        private int horisontalCountOfPieces = 0;
        private Point currentLocationOfStripZoneTopLeft;
        private Point currentLocationOfStripZoneBottomRight;
        private PictureBox hint;

        private List<PictureBox> pb = new List<PictureBox>();
        private List<Bitmap> btm = new List<Bitmap>();
        private List<Point> right_location = new List<Point>();
        private List<Point> start_location = new List<Point>();

        //для вывода на ленте
        private int currentFirstElementOnStrip = 0;
        private int countOfPiecesOnStrip = 0;
        private List<char> is_on_strip = new List<char>();

        private static Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

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
            btm = new List<Bitmap>();//нормальный список кусочков пазл
            var btm1 = new List<Bitmap>();//перемешанный список
            Image img = System.Drawing.Image.FromFile(path);
            btm = Section.RectangleSection(path, picture[0], picture[1], picture[2], id_picture);//разрезаем картинку на кусочки
            btm1 = Section.RectangleSection(path, picture[0], picture[1], picture[2], id_picture);//разрезаем картинку на кусочки
            Shuffle<Bitmap>(btm1);//перемешиваем кусочки списка
            pb = new List<PictureBox>();//создаем массив пикчербоксов
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
                    p.Image = (Image)btm1[i];
                    pb.Add(p);
                    pb[i].Image = btm1[i];
                    this.Controls.Add(p);

                    ControlMover.Add(pb[i]); //перемещение кусочков
                }
            }
            else
                if (game_mode == "В куче")
            {
                Random r = new Random();
                for (int i = 0; i < count; i++)
                {
                    PictureBox p = new PictureBox();
                    p.Location = new Point(r.Next(50, 300), r.Next(50, 300));
                    p.Size = new Size(w, h);
                    p.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Image = (Image)btm[i];
                    pb.Add(p);
                    pb[i].Image = btm[i];
                    this.Controls.Add(p);
                    ControlMover.Add(pb[i]); //перемещение кусочков
                }
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

                buttonLeft.Location = new Point(buttonLeft.Location.X, this.Size.Height - 15 - (h / 2) - buttonLeft.Height - 38);
                buttonRight.Location = new Point(buttonRight.Location.X, this.Size.Height - 15 - (h / 2) - buttonRight.Height - 38);

                currentLocationOfStripZoneBottomRight = new Point(this.Width - 50, this.Size.Height - 15 - 38);
                currentLocationOfStripZoneTopLeft = new Point(50, this.Size.Height - h - buttonLeft.Height - 38);

                //столько кусочков уместится на ленте
                countOfPiecesOnStrip = (currentLocationOfStripZoneBottomRight.X - currentLocationOfStripZoneTopLeft.X - 5) / (w + 5);
                PictureBox p;

                int W = 0;
                int H = 0;
                for (int i = 0; i < verticalCountOfPieces * horisontalCountOfPieces; i++)
                {
                    W = currW;
                    H = currH;

                    p = new PictureBox();
                    p.Size = new Size(w, h);
                    p.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Image = (Image)btm[i];
                    pb.Add(p);
                    is_on_strip.Add('n');
                    p.Visible = false;

                    right_location.Add(new Point(W * (w + 1) + 5, H * (h + 1) + 25));

                    this.Controls.Add(pb[i]);
                    ControlMover.Add(pb[i]);
                    currW++;
                    if (currW == countW)
                    {
                        currH++;
                        currW = 0;
                    }
                }

                //тут шафл массива пикчеров и правильных координат синхронно
                syncShuffle<PictureBox, Point>(pb, right_location);

                for (int i=0;  i < countOfPiecesOnStrip; i++)
                {
                    pb[i].Location = new Point(5 + currentLocationOfStripZoneTopLeft.X + i * (w + 5), currentLocationOfStripZoneTopLeft.Y);
                    is_on_strip[i] = 's';
                    pb[i].Visible = true;
                }
                currentFirstElementOnStrip = 0;
            }
            hint = new PictureBox();
            hint.SizeMode = PictureBoxSizeMode.StretchImage;
            hint.Size = new Size((btm[0].Width + 1) * verticalCountOfPieces, (btm[0].Height + 1) * horisontalCountOfPieces);
            hint.Location = new Point(5, 25);
            hint.Image = Image.FromFile(path);
            this.Controls.Add(hint);
            hint.Visible = false;
        }

        //метод, который рандомит элементы списка
        public static void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void syncShuffle<T,V>(List<T> list1, List<V> list2)
        {
            int n = list1.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value1 = list1[k];
                V value2 = list2[k];
                list1[k] = list1[n];
                list2[k] = list2[n];
                list1[n] = value1;
                list2[n] = value2;
            }
        }

        public void updateStrip(bool right)
        {
            int i = 0;
            int j = 0;
            if (right)
            {
                i = currentFirstElementOnStrip;
                while ((i<(verticalCountOfPieces * horisontalCountOfPieces)) && (j < countOfPiecesOnStrip))
                {
                    if (is_on_strip[i]=='s')
                    {
                        j++;
                        is_on_strip[i] = 'n';
                        pb[i].Visible = false;
                    }
                    i++;
                }
                j = 0;
                currentFirstElementOnStrip += countOfPiecesOnStrip;
                while ((i < (verticalCountOfPieces * horisontalCountOfPieces)&&(i< (currentFirstElementOnStrip + countOfPiecesOnStrip)))){
                    if (is_on_strip[i]=='n')
                    {
                        is_on_strip[i] = 's';
                        pb[i].Location = new Point(5 + currentLocationOfStripZoneTopLeft.X + j * (btm[0].Width + 5), currentLocationOfStripZoneTopLeft.Y);
                        pb[i].Visible = true;
                        j++;
                    }
                    i++;
                }
            }
            else
            {
                i = currentFirstElementOnStrip;
                while ((i < (verticalCountOfPieces * horisontalCountOfPieces)) && (j < countOfPiecesOnStrip))
                {
                    if (is_on_strip[i] == 's')
                    {
                        j++;
                        is_on_strip[i] = 'n';
                        pb[i].Visible = false;
                    }
                    i++;
                }
                j = 0;
                currentFirstElementOnStrip -= countOfPiecesOnStrip;
                i = currentFirstElementOnStrip;
                while ((i < (verticalCountOfPieces * horisontalCountOfPieces) && (i < (currentFirstElementOnStrip + countOfPiecesOnStrip))))
                {
                    if (is_on_strip[i] == 'n')
                    {
                        is_on_strip[i] = 's';
                        pb[i].Location = new Point(5 + currentLocationOfStripZoneTopLeft.X + j * (btm[0].Width + 5), currentLocationOfStripZoneTopLeft.Y);
                        pb[i].Visible = true;
                        j++;
                    }
                    i++;
                }
            }
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
            if (currentFirstElementOnStrip != 0)
            {
                updateStrip(false);
            }
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            if ((currentFirstElementOnStrip + countOfPiecesOnStrip) < (verticalCountOfPieces * horisontalCountOfPieces))
            {
                updateStrip(true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}


