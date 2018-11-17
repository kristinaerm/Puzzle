﻿using System;
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
        private string id_picture = "";

        private int h = 0;
        private int w = 0;

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
            ControlMover.Owner = this;

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
                    hh += saved[2][0];
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

            id_picture = bd.selectIdPictureByIdPuzzle(id_puzzle);
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

            h = btm[0].Height;
            w = btm[0].Width;

            int currH = 0;
            int currW = 0;

            int count = btm.Count;

            PictureBox p;
            object[] obj;

            for (int i = 0; i < verticalCountOfPieces * horisontalCountOfPieces; i++)
            {
                p = new PictureBox();
                p.Size = new Size(w, h);
                p.SizeMode = PictureBoxSizeMode.StretchImage;
                p.Image = (Image)btm[i];
                pb.Add(p);
                obj = new object[2];
                if (game_mode == "На ленте")
                {
                    is_on_strip.Add('n');
                    p.Visible = false;
                    obj[1] = 'n';
                }
                else
                {
                    obj[1] = ' ';
                }
                obj[0] = new Point(currW * (w + 1) + 5, currH * (h + 1) + 25);
                p.Tag = obj;
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

            if (fromGame)
            {
                List<string[]> num_x_y = bd.selectPuzzlePiecesByPuzzleId(id, log);
                for (int i = 0; i < verticalCountOfPieces * horisontalCountOfPieces; i++)
                {
                    int j = 0;
                    while ((j < num_x_y.Count) && (!bd.cutExcessSpace(num_x_y[j][0]).Equals(j.ToString())))
                    {
                        j++;
                    }
                    pb[i].Location = new Point(Convert.ToInt32(num_x_y[j][1]), Convert.ToInt32(num_x_y[j][2]));
                }
            }
            else
            {
                //тут шафл массива пикчеров и правильных координат синхронно
                syncShuffle<PictureBox, int>(pb, serial_number);

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

                        object[] o = new object[2];
                        o[0] = ((Point)((object[])pb[i].Tag)[0]);
                        o[1] = 's';
                        pb[i].Tag = o;

                        pb[i].Visible = true;
                    }
                    currentFirstElementOnStrip = 0;
                }
            }

            hint = new PictureBox();
            hint.SizeMode = PictureBoxSizeMode.StretchImage;
            hint.Size = new Size((btm[0].Width + 1) * horisontalCountOfPieces, (btm[0].Height + 1) * verticalCountOfPieces);
            hint.Location = new Point(5, 25);
            hint.Image = Image.FromFile(path);
            this.Controls.Add(hint);
            hint.Visible = false;

            timer1.Enabled = true;
            stopWatch.Start();
        }

        public static void syncShuffle<T, V>(List<T> list1, List<V> list2)
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
            }
        }

        public void updateStrip()
        {
            //те шо были убрать в n
            int i = 0;
            int j = 0;
            while (i < (verticalCountOfPieces * horisontalCountOfPieces))
            {
                if ((char)(((object[])pb[i].Tag)[1]) == 's')
                {
                    j++;
                    object[] o = new object[2];
                    o[0] = ((Point)((object[])pb[i].Tag)[0]);
                    o[1] = 'n';
                    pb[i].Tag = o;
                    pb[i].Visible = false;
                }
                i++;
            }

            i = 0;
            j = 0;

            while ((i < (verticalCountOfPieces * horisontalCountOfPieces) && (j < countOfPiecesOnStrip)))
            {
                if ((char)(((object[])pb[i].Tag)[1]) == 'n')
                {
                    object[] o = new object[2];
                    o[0] = ((Point)((object[])pb[i].Tag)[0]);
                    o[1] = 's';
                    pb[i].Tag = o;
                    pb[i].Location = new Point(5 + currentLocationOfStripZoneTopLeft.X + j * (btm[0].Width + 5), currentLocationOfStripZoneTopLeft.Y);
                    pb[i].Visible = true;
                    j++;
                }
                i++;
            }
            i = 0;
            while (!((char)(((object[])pb[i].Tag)[1]) == 's')) i++;
            currentFirstElementOnStrip = i;
        }

        public void updateStrip(bool right)
        {
            int i = 0;
            int j = 0;
            int curr_last_strip = verticalCountOfPieces * horisontalCountOfPieces - 1;

            while ((curr_last_strip > -1) && !((char)(((object[])pb[curr_last_strip].Tag)[1]) == 's')) curr_last_strip--;

            //i = currentFirstElementOnStrip;
            while (i < (verticalCountOfPieces * horisontalCountOfPieces))
            {
                if ((char)(((object[])pb[i].Tag)[1]) == 's')
                {
                    object[] o = new object[2];
                    o[0] = ((Point)((object[])pb[i].Tag)[0]);
                    o[1] = 'n';
                    pb[i].Tag = o;
                    pb[i].Visible = false;
                    curr_last_strip = i;
                }
                i++;
            }

            j = 0;
            if (right)
            {
                if (!(curr_last_strip == (verticalCountOfPieces * horisontalCountOfPieces - 1)))
                {
                    curr_last_strip++;
                    while ((!((char)(((object[])pb[curr_last_strip].Tag)[1]) == 'n')) && (curr_last_strip < verticalCountOfPieces * horisontalCountOfPieces - 1))
                    {
                        curr_last_strip++;
                    }
                    if (!(curr_last_strip == verticalCountOfPieces * horisontalCountOfPieces - 1))
                    {
                        currentFirstElementOnStrip = curr_last_strip;
                    }
                }
            }
            else
            {
                i = currentFirstElementOnStrip;
                i--;
                while ((i > -1) && (!((char)(((object[])pb[i].Tag)[1]) == 'n')))
                {
                    i--;
                }
                if (!(i == 0))
                {
                    while ((j < countOfPiecesOnStrip) && ((i > -1)))
                    {
                        if (((char)(((object[])pb[i].Tag)[1]) == 'n'))
                        {
                            j++;
                        }
                        i--;
                    }
                    currentFirstElementOnStrip = i + 1;
                }
            }

            i = currentFirstElementOnStrip;
            j = 0;

            while ((i < (verticalCountOfPieces * horisontalCountOfPieces) && (j < countOfPiecesOnStrip)))
            {
                if ((char)(((object[])pb[i].Tag)[1]) == 'n')
                {
                    object[] o = new object[2];
                    o[0] = ((Point)((object[])pb[i].Tag)[0]);
                    o[1] = 's';
                    pb[i].Tag = o;
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
            string id_piece = "";
            List<string> game = new List<string>();

            try
            {
                game = bd.selectAllAboutGameByLoginAndIdPuzzle(login, id_puzzle);
                if (game.Count != 0)
                {
                    bd.deleteGameByIdPuzzleAndLogin(id_puzzle, login);
                    for (int i = 0; i < serial_number.Count; i++)
                    {
                        id_piece = bd.selectIDPiece(serial_number[i].ToString(), id_puzzle);
                        bd.deletePiecePuzzleByIdPuzzleAndOrIdPuzzle(id_puzzle, id_piece);
                    }
                    bd.deleteSaveByIdPuzzleAndLogin(id_puzzle, login);
                    saveGame(record);
                }
                else
                {
                    saveGame(record);
                }
            }
            catch
            {

            }

            //еали на очки, со сохраняем currentmoves
            //если на время, то TimeSpan ts = stopWatch.Elapsed.Add(fromSave); или что-то в этом роде
        }
        private void saveGame(string rec)
        {
            ConnDatabase bd = new ConnDatabase();
            string id_piece = "";
            if (rec == "На время")
            {
                TimeSpan ts = stopWatch.Elapsed.Add(fromSave);
                string formatts = ts.ToString(@"hh\:mm\:ss");
                bd.insertInGame(id_puzzle, login, game_mode, record, formatts);
                for (int i = 0; i < serial_number.Count; i++)
                {

                    bd.insertInPuzzlePiece(serial_number[i].ToString(), ((Point)((object[])pb[i].Tag)[1]).X.ToString(), ((Point)((object[])pb[i].Tag)[1]).Y.ToString(), id_puzzle);
                    id_piece = bd.selectIDPiece(serial_number[i].ToString(), id_puzzle);
                    bd.insertInSave(id_piece, id_puzzle, login, pb[i].Location.X.ToString(), pb[i].Location.Y.ToString());
                }
                MessageBox.Show("Игра успешно сохранена!");
            }
            else
            {
                bd.insertInGame(id_puzzle, login, game_mode, record, currentmoves.ToString());
                for (int i = 0; i < serial_number.Count; i++)
                {

                    bd.insertInPuzzlePiece(serial_number[i].ToString(), ((Point)((object[])pb[i].Tag)[1]).X.ToString(), ((Point)((object[])pb[i].Tag)[1]).Y.ToString(), id_puzzle);
                    id_piece = bd.selectIDPiece(serial_number[i].ToString(), id_puzzle);
                    bd.insertInSave(id_piece, id_puzzle, login, pb[i].Location.X.ToString(), pb[i].Location.Y.ToString());
                }
                MessageBox.Show("Игра успешно сохранена!");
            }
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

        public void setPieceIfOnRightLocation(object pic)
        {
            currentmoves++;
            PictureBox picture = (PictureBox)pic;
            char place = (char)((object[])picture.Tag)[1];
            Point rightxy = (Point)((object[])picture.Tag)[0];
            bool need_to_update_strip = false;
            
            if ((picture.Location.X < (rightxy.X + 5)) && (picture.Location.X > (rightxy.X - 5)))
            {
                if ((picture.Location.Y < (rightxy.Y + 5)) && (picture.Location.Y > (rightxy.Y - 5)))
                {
                    picture.Location = rightxy;
                    picture.Enabled = false;
                    if (!place.Equals(' '))
                    {
                        object[] o = new object[2];
                        o[0] = rightxy;
                        o[1] = 'f';
                        picture.Tag = o;
                        if (place == 's') need_to_update_strip = true;
                    }
                }
            }

            //лента
            if (!place.Equals(' '))
            {
                if ((currentLocationOfStripZoneTopLeft.X< picture.Location.X) &&(picture.Location.X< currentLocationOfStripZoneBottomRight.X))
                {
                    if ((currentLocationOfStripZoneTopLeft.Y < picture.Location.Y) && (picture.Location.Y < currentLocationOfStripZoneBottomRight.Y))
                    {
                        //значит в зоне ленты
                        if (!(place == 's'))
                        {
                            need_to_update_strip = true;
                            object[] o = new object[2];
                            o[0] = rightxy;
                            o[1] = 'n';
                            picture.Tag = o;
                        }
                    }
                }
                if (place == 's')
                {
                    need_to_update_strip = true;
                    object[] o = new object[2];
                    o[0] = rightxy;
                    o[1] = 'f';
                    picture.Tag = o;
                }
            }

            int i = 0;
            while ((i < pb.Count) && (pb[i].Enabled == false))
            {
                i++;
            }
            if (i == pb.Count)
            {
                string res = "";
                int points = 0;
                TimeSpan ts = stopWatch.Elapsed;
                ts.Add(fromSave);
                int sec = ts.Hours * 60 * 60 + ts.Minutes * 60 + ts.Seconds;
                if (record.Equals("На очки"))
                {
                    points = currentmoves / (verticalCountOfPieces * horisontalCountOfPieces);
                }
                else
                {
                    points = sec / (verticalCountOfPieces * horisontalCountOfPieces);
                }
                if (points > betweenGreatAndNormal) points *= GREAT;
                else if (points < betweenNormalAndBad) points *= BAD;
                else points *= NORMAL;
                points *= complexityKoeff;
                res = points.ToString();
                MessageBox.Show("Победа! Ваш результат: " + res);
                //удалить сейвы игры, если они были
                //записать результат к сумме в его логин в базу
                //значит в базе надо прописать сет сумма баллов у юзера с логином
            }
            else if(need_to_update_strip) updateStrip();
        }
    }
}


