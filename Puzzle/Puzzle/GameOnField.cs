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
        private string login = "";
        private bool fromGame = false;

        private int betweenGreatAndNormal;
        private int betweenNormalAndBad;
        private int complexityKoeff;
        private const int GREAT = 10;
        private const int NORMAL = 5;
        private const int BAD = 1;

        private int currentmoves = 0;
        private TimeSpan fromSave = new TimeSpan(0);


        private Stopwatch stopWatch = new Stopwatch();

        private int verticalCountOfPieces = 0;
        private int horisontalCountOfPieces = 0;
        private Point currentLocationOfStripZoneTopLeft;
        private Point currentLocationOfStripZoneBottomRight;
        private PictureBox hint;

        private List<Bitmap> btm = new List<Bitmap>();
        private List<PictureBox> pb = new List<PictureBox>();
        private List<Point> right_location = new List<Point>();//правильные координаты
        private List<int> serial_number = new List<int>();//номера

        //для вывода на ленте
        private int currentFirstElementOnStrip = 0;
        private int countOfPiecesOnStrip = 0;
        private List<char> is_on_strip = new List<char>();

        private static Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        public GameOnField(string id, string game_m, string rec, string log, bool fromSavedGame)
        {
            ConnDatabase bd = new ConnDatabase();
            InitializeComponent();
            id_puzzle = id;
            game_mode = game_m;
            record = rec;
            login = log;
            fromGame = fromSavedGame;
            if (fromGame)
            {
                List<string> saved = bd.selectAllAboutGameByLoginAndIdPuzzle(login, id_puzzle);
                if (bd.cutExcessSpace(record).Equals("На очки"))
                {
                    currentmoves = Convert.ToInt32(saved[2]);
                }
                else
                {
                    string hh = "";
                    hh+= saved[2][0];
                    hh += saved[2][1];
                    string mm = "";
                    mm += saved[2][2];
                    mm += saved[2][3];
                    string ss = "";
                    ss += saved[2][4];
                    ss += saved[2][5];
                    fromSave = new TimeSpan(Convert.ToInt32(hh), Convert.ToInt32(mm), Convert.ToInt32(ss));
                }
            }

            string id_picture = bd.selectIdPictureByIdPuzzle(id_puzzle);
            string path = bd.selectPathByIdPicture(id_picture);
            List<string> picture = bd.selectSizeAndComplexityFromPuzzleByIdPuzzle(id_puzzle);

            verticalCountOfPieces = Convert.ToInt32(picture[0]);
            horisontalCountOfPieces = Convert.ToInt32(picture[1]);

            if (bd.cutExcessSpace(picture[2]).Equals("Легкий"))
            {
                complexityKoeff = 10;
            }
            if (bd.cutExcessSpace(picture[2]).Equals("Средний"))
            {
                complexityKoeff = 50;
            }
            else
            {
                complexityKoeff = 100;
            }
            if (bd.cutExcessSpace(record).Equals("На очки"))
            {
                //в движениях
                betweenGreatAndNormal = 5;
                betweenNormalAndBad = 15;
                //потом при подсчете результата число мувов делю на верт*горизонт и сравниваю и домнажаю на бэднормалгуд и на комплексити
            }
            else
            {
                //в секундах
                betweenGreatAndNormal = 20;
                betweenNormalAndBad = 60;
            }

            btm = new List<Bitmap>();//нормальный список кусочков пазл



            btm = Section.RectangleSection(path, picture[1], picture[0], id_picture);//разрезаем картинку на кусочки


            int h = btm[0].Height;
            int w = btm[0].Width;

            int currH = 0;
            int currW = 0;

            int count = btm.Count;

            PictureBox p;

            for (int i = 0; i < verticalCountOfPieces * horisontalCountOfPieces; i++)
            {
                p = new PictureBox();
                p.Size = new Size(w, h);
                p.SizeMode = PictureBoxSizeMode.StretchImage;
                p.Image = (Image)btm[i];
                pb.Add(p);
                if (game_mode == "На ленте")
                {
                    is_on_strip.Add('n');
                    p.Visible = false;
                }

                right_location.Add(new Point(currW * (w + 1) + 5, currH * (h + 1) + 25));
                serial_number.Add(i);

                this.Controls.Add(pb[i]);
                ControlMover.Add(pb[i]);
                currW++;
                if (currW == horisontalCountOfPieces)
                {
                    currH++;
                    currW = 0;
                }
            }

            //тут шафл массива пикчеров и правильных координат синхронно
            syncShuffle<PictureBox, Point, int>(pb, right_location, serial_number);

            currH = 0;
            currW = 0;

            if (game_mode == "На поле")
            {
                for (int i = 0; i < count; i++)
                {
                    pb[i].Location = new Point(currW * (w + 1) + 5, currH * (h + 1) + 25);
                    currW++;
                    if (currW == horisontalCountOfPieces)
                    {
                        currH++;
                        currW = 0;
                    }
                }
            }
            else if (game_mode == "В куче")
            {
                Random r = new Random();
                for (int i = 0; i < count; i++)
                {
                    pb[i].Location = new Point(r.Next(50, 300), r.Next(50, 300));
                }
            }
            else if (game_mode == "На ленте")
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

                for (int i = 0; i < countOfPiecesOnStrip; i++)
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

            timer1.Enabled = true;
            stopWatch.Start();
        }

        public static void syncShuffle<T, V, P>(List<T> list1, List<V> list2, List<P> list3)
        {
            int n = list1.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value1 = list1[k];
                list1[k] = list1[n];
                list1[n] = value1;

                V value2 = list2[k];
                list2[k] = list2[n];
                list2[n] = value2;

                P value3 = list3[k];
                list3[k] = list3[n];
                list3[n] = value3;
            }
        }

        public void updateStrip(bool right)
        {
            int i = 0;
            int j = 0;
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

            if (right)
            {
                currentFirstElementOnStrip += countOfPiecesOnStrip;
            }
            else
            {
                currentFirstElementOnStrip -= countOfPiecesOnStrip;
                i = currentFirstElementOnStrip;
            }

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

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stopWatch.Stop();
            timer1.Enabled = false;
            this.Close();
        }

        private void menu_top10_Click(object sender, EventArgs e)
        {
            Recorde recordForm = new Recorde();
            recordForm.Show();
        }

        private void button_end_game_Click(object sender, EventArgs e)
        {
            stopWatch.Stop();
            timer1.Enabled = false;
            this.Close();
        }

        private void button_pause_Click(object sender, EventArgs e)
        {
            if (button_pause.Text.Equals("Пауза"))
            {
                button_pause.Text = "Возобновить";
                for (int i = 0; i < pb.Count; i++)
                {
                    pb[i].Enabled = false;
                }
                stopWatch.Stop();
                timer1.Enabled = false;
            }
            else
            {

                button_pause.Text = "Пауза";
                for (int i = 0; i < pb.Count; i++)
                {
                    pb[i].Enabled = true;
                }
                stopWatch.Start();
                timer1.Enabled = true;
            }

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

        private void button1_Click(object sender, EventArgs e)
        {
            ConnDatabase bd = new ConnDatabase();
            stopWatch.Stop();
            timer1.Enabled = false;
            string id_piece ="";
            //проверить,если такая игра уже есть
            //try
            //{
            // for (int j=0;j< piece.Count;j++) { piece[j] = bd.selectIdPiece(id_puzzle); }
            if (record == "На время")
            {
                TimeSpan ts = stopWatch.Elapsed.Add(fromSave);
                bd.insertInGame(id_puzzle, login, game_mode, record, ts.ToString());
                for (int i = 0; i < serial_number.Count; i++)
                {
                    
                    bd.insertInPuzzlePiece(serial_number[i].ToString(), right_location[i].X.ToString(), right_location[i].Y.ToString(), id_puzzle);
                    id_piece = bd.selectIDPiece(serial_number[i].ToString());

                    bd.insertInSave(id_piece, id_puzzle, login, pb[i].Location.X.ToString(), pb[i].Location.Y.ToString());
                }
            }
            else
                {
                    bd.insertInGame(id_puzzle, login, game_mode, record, currentmoves.ToString());
                    for (int i = 0; i < serial_number.Count; i++)
                    {
                   
                    bd.insertInPuzzlePiece(serial_number[i].ToString(), right_location[i].X.ToString(), right_location[i].Y.ToString(), id_puzzle);
                    id_piece = bd.selectIDPiece(serial_number[i].ToString());
                    bd.insertInSave(id_piece, id_puzzle, login, pb[i].Location.X.ToString(), pb[i].Location.Y.ToString());
                    }
                }
            
          //  }
            //catch
            //{
            //    MessageBox.Show("Игра успешно сохранена!");
            //}

            //еали на очки, со сохраняем currentmoves
            //если на время, то TimeSpan ts = stopWatch.Elapsed.Add(fromSave); или что-то в этом роде
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = stopWatch.Elapsed;
            ts.Add(fromSave);
            label1.Text = String.Format("{0:00}:{1:00}:{2:00}",
            ts.Hours, ts.Minutes, ts.Seconds);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}


