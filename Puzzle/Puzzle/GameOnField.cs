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
        private string id_picture = "";
        private string form = "";
        private bool close_without_ask = false;

        private int h = 0;
        private int w = 0;
        private bool first_move = true;

        private int betweenGreatAndNormal;
        private int betweenNormalAndBad;
        private int complexityKoeff;
        private const int GREAT = 10;
        private const int NORMAL = 5;
        private const int BAD = 1;

        private int currentmoves = 0;
        private TimeSpan fromSave = new TimeSpan(0);
        private List<string[]> pieces;

        private Stopwatch stopWatch = new Stopwatch();

        private int verticalCountOfPieces = 0;
        private int horisontalCountOfPieces = 0;
        private Point currentLocationOfStripZoneTopLeft;
        private Point currentLocationOfStripZoneBottomRight;
        private Hint hhh;
        PicBox hint;

        private List<Bitmap> btm = new List<Bitmap>();
        private List<Bitmap> btm1 = new List<Bitmap>();
        private List<PicBox> pb = new List<PicBox>();
        private List<int> serial_number = new List<int>();

        //для вывода на ленте
        private int currentFirstElementOnStrip = 0;
        private int countOfPiecesOnStrip = 0;

        //для пятнашек
        private Point oldLocation;

        public static bool triangle = false;

        private static Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Brushes.Gray, 1);
            for (int i = 1; i < verticalCountOfPieces; i++)
            {
                e.Graphics.DrawLine(p, 5, (i * (h + 1) + 25), (5 + (w + 1) * horisontalCountOfPieces), (i * (h + 1) + 25));
            }
            for (int i = 1; i < horisontalCountOfPieces; i++)
            {
                e.Graphics.DrawLine(p, i*(w+1), 25, i * (w + 1), 25+(h+1)*verticalCountOfPieces);
            }

        }


        public GameOnField(string id, string game_m, string rec, string log, string form, bool fromSavedGame)
        {
            ConnDatabase bd = new ConnDatabase();
            InitializeComponent();
            ControlMover.Owner = this;

            //верхние пикчербоксы
            List<Bitmap> top = new List<Bitmap>();
            //нижние пикчербоксы
            List<Bitmap> bottom = new List<Bitmap>();
            //верхние пикчербоксы
            List<PicBox> top_pic = new List<PicBox>();
            //нижние пикчербоксы
            List<PicBox> bottom_pic = new List<PicBox>();
            //верхние номера
            List<int> top_num = new List<int>();
            //нижние номера
            List<int> bottom_num = new List<int>();


            if (form.Equals("треугольник"))
            {
                triangle = true;
            }

            id_puzzle = id;
            game_mode = game_m;
            record = rec;
            login = log;
            fromGame = fromSavedGame;
            this.form = form;

            id_picture = bd.selectIdPictureByIdPuzzle(id_puzzle);
            string path = bd.selectPathByIdPicture(id_picture);
            List<string> picture = bd.selectSizeAndComplexityFromPuzzleByIdPuzzle(id_puzzle);

            verticalCountOfPieces = Convert.ToInt32(picture[0]);
            horisontalCountOfPieces = Convert.ToInt32(picture[1]);

            if (bd.cutExcessSpace(picture[2]).Equals("1"))
            {
                complexityKoeff = 10;
            }
            else if (bd.cutExcessSpace(picture[2]).Equals("2"))
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

            int count = 0;

            btm1 = new List<Bitmap>();//нормальный список кусочков пазл
            btm1 = Section.RectangleSection(path, picture[1], picture[0], id_picture);//разрезаем картинку на кусочки
            if (triangle)
            {
                btm = new List<Bitmap>();
                Bitmap[] mass = new Bitmap[2];
                for (int i = 0; i < horisontalCountOfPieces * verticalCountOfPieces; i++)
                {
                    mass = Section.TriangularSection(btm1[i], false);
                    top.Add(mass[0]);
                    bottom.Add(mass[1]);
                }
                count = horisontalCountOfPieces * verticalCountOfPieces * 2;
            }
            else
            {
                btm = btm1;
                count = horisontalCountOfPieces * verticalCountOfPieces;
            }

            if (fromGame)
            {
                try
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
                        mm += saved[2][3];
                        mm += saved[2][4];
                        string ss = "";
                        ss += saved[2][6];
                        ss += saved[2][7];
                        fromSave = new TimeSpan(Convert.ToInt32(hh), Convert.ToInt32(mm), Convert.ToInt32(ss));
                    }
                    //загрузить все кусочки в виде номер - координаты текущие
                    pieces = bd.selectPuzzlePiecesByPuzzleIdAndLogin(id_puzzle, login);
                    string[] buff;
                    //отсортировать по номеру
                    for (int i = 0; i < count; i++)
                    {
                        for (int j = 0; j < count - 1; j++)
                        {
                            if (Convert.ToInt32(pieces[j][0]) > Convert.ToInt32(pieces[j + 1][0]))
                            {
                                buff = pieces[j];
                                pieces[j] = pieces[j + 1];
                                pieces[j + 1] = buff;
                            }
                        }
                    }
                }
                catch
                {
                    if (MessageBox.Show("Не удалось загрузить пазл!") == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
            }
            h = btm1[0].Height;
            w = btm1[0].Width;

            int newHeightOfForm = this.Size.Height + h + 30;

            buttonLeft.Location = new Point(buttonLeft.Location.X, newHeightOfForm - 15 - (h / 2) - buttonLeft.Height - 38);
            buttonRight.Location = new Point(buttonRight.Location.X, newHeightOfForm - 15 - (h / 2) - buttonRight.Height - 38);

            currentLocationOfStripZoneBottomRight = new Point(this.Width - 50, newHeightOfForm - 15 - 38);
            currentLocationOfStripZoneTopLeft = new Point(50, newHeightOfForm - h - buttonLeft.Height - 38);



            int currH = 0;
            int currW = 0;

            PicBox p = new PicBox();
            PicBox p1 = new PicBox();
            object[] obj;
            object[] obj1;
            Graphics gr = this.CreateGraphics();
            gr.DrawEllipse(Pens.Black, currW * (w + 1) + 5, currH * (h + 1) + 25, 10, 10);
            Point pp;

            for (int i = 0; i < verticalCountOfPieces * horisontalCountOfPieces; i++)
            {


                if (triangle)
                {
                    p = new PicBox();
                    p.Size = new Size(w, h);
                    p.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Image = (Image)top[i];
                    top_pic.Add(p);

                    p1 = new PicBox();
                    p1.Size = new Size(w, h);
                    p1.SizeMode = PictureBoxSizeMode.StretchImage;
                    p1.Image = (Image)bottom[i];
                    bottom_pic.Add(p1);
                }
                else
                {
                    p = new PicBox();
                    p.Size = new Size(w, h);
                    p.SizeMode = PictureBoxSizeMode.StretchImage;
                    p.Image = (Image)btm[i];
                    pb.Add(p);
                }

                obj = new object[2];
                obj1 = new object[2];
                if (game_mode == "На ленте")
                {
                    if (fromGame)
                    {
                        if (triangle)
                        {
                            if (((Convert.ToInt32(pieces[i * 2][1]) == 0) && (Convert.ToInt32(pieces[i * 2][2]) == 0)) || (currentLocationOfStripZoneTopLeft.X < Convert.ToInt32(pieces[i * 2][1])) && (Convert.ToInt32(pieces[i * 2][1]) < currentLocationOfStripZoneBottomRight.X) && (currentLocationOfStripZoneTopLeft.Y < Convert.ToInt32(pieces[i * 2][2])) && (Convert.ToInt32(pieces[i * 2][2]) < currentLocationOfStripZoneBottomRight.Y))
                            {
                                //в ленте
                                p.Visible = false;
                                obj[1] = 'n';
                            }
                            else
                            {
                                p.Visible = true;
                                obj[1] = 'f';
                            }
                            if (((Convert.ToInt32(pieces[i + i + 1][1]) == 0) && (Convert.ToInt32(pieces[i + i + 1][2]) == 0)) || (currentLocationOfStripZoneTopLeft.X < Convert.ToInt32(pieces[i + i + 1][1])) && (Convert.ToInt32(pieces[i + i + 1][1]) < currentLocationOfStripZoneBottomRight.X) && (currentLocationOfStripZoneTopLeft.Y < Convert.ToInt32(pieces[i + i + 1][2])) && (Convert.ToInt32(pieces[i + i + 1][2]) < currentLocationOfStripZoneBottomRight.Y))
                            {
                                //в ленте
                                p1.Visible = false;
                                obj1[1] = 'n';
                            }
                            else
                            {
                                p1.Visible = true;
                                obj1[1] = 'f';
                            }
                        }
                        else
                        {
                            if (((Convert.ToInt32(pieces[i][1]) == 0) && (Convert.ToInt32(pieces[i][2]) == 0)) || (currentLocationOfStripZoneTopLeft.X < Convert.ToInt32(pieces[i][1])) && (Convert.ToInt32(pieces[i][1]) < currentLocationOfStripZoneBottomRight.X) && (currentLocationOfStripZoneTopLeft.Y < Convert.ToInt32(pieces[i][2])) && (Convert.ToInt32(pieces[i][2]) < currentLocationOfStripZoneBottomRight.Y))
                            {
                                //в ленте
                                p.Visible = false;
                                obj[1] = 'n';
                            }
                            else
                            {
                                p.Visible = true;
                                obj[1] = 'f';
                            }
                        }


                    }
                    else
                    {
                        if (triangle)
                        {
                            p.Visible = false;
                            p1.Visible = false;
                        }
                        else
                        {
                            p.Visible = false;
                        }
                        obj[1] = 'n';
                    }
                }
                else
                {
                    if (triangle)
                    {
                        obj[1] = ' ';
                        obj1[1] = ' ';
                    }
                    else
                    {
                        obj[1] = ' ';
                    }

                }
                pp = new Point(currW * (w + 1) + 5, currH * (h + 1) + 25);
                obj[0] = pp;
                obj1[0] = pp;
                if (triangle)
                {
                    p.Tag = obj;
                    p1.Tag = obj1;
                }
                else
                {
                    p.Tag = obj;
                    serial_number.Add(i);
                }

                if (fromGame)
                {
                    if (triangle)
                    {
                        p.Location = new Point(Convert.ToInt32(pieces[i * 2][1]), Convert.ToInt32(pieces[i * 2][2]));
                        if ((pp.X == p.Location.X) && (pp.Y == p.Location.Y))
                        {
                            p.BorderStyle = BorderStyle.Fixed3D;
                            p.Enabled = false;
                        }

                        p1.Location = new Point(Convert.ToInt32(pieces[i + i + 1][1]), Convert.ToInt32(pieces[i + i + 1][2]));
                        if ((pp.X == p1.Location.X) && (pp.Y == p1.Location.Y))
                        {
                            p1.BorderStyle = BorderStyle.Fixed3D;
                            p1.Enabled = false;
                        }
                    }
                    else
                    {
                        p.Location = new Point(Convert.ToInt32(pieces[i][1]), Convert.ToInt32(pieces[i][2]));
                        if ((pp.X == p.Location.X) && (pp.Y == p.Location.Y))
                        {
                            p.BorderStyle = BorderStyle.Fixed3D;
                            p.Enabled = false;
                        }
                    }

                }

                if (triangle)
                {
                    this.Controls.Add(p);
                    ControlMover.Add(p);
                    this.Controls.Add(p1);
                    ControlMover.Add(p1);
                }
                else
                {
                    this.Controls.Add(pb[i]);
                    ControlMover.Add(pb[i]);
                }
                currW++;
                if (currW == horisontalCountOfPieces)
                {
                    currH++;
                    currW = 0;
                }
            }



            if (!fromGame)
            {
                //тут шафл массива пикчеров и номеров синхронно
                if (triangle)
                {
                    for (int i = 0; i < 2 * verticalCountOfPieces * horisontalCountOfPieces; i += 2)
                    {
                        top_num.Add(i);
                        bottom_num.Add(i + 1);
                    }
                    //перемешивание верхних кусочков
                    syncShuffle<PicBox, int>(top_pic, top_num);
                    //нижние
                    syncShuffle<PicBox, int>(bottom_pic, bottom_num);
                    //объединение 

                }
                else
                {
                    syncShuffle<PicBox, int>(pb, serial_number);
                }


                currH = 0;
                currW = 0;

                if (game_mode == "На поле")
                {
                    for (int i = 0; i < verticalCountOfPieces * horisontalCountOfPieces; i++)
                    {
                        if (triangle)
                        {
                            top_pic[i].Location = new Point(currW * (w + 1) + 5, currH * (h + 1) + 25);
                            bottom_pic[i].Location = new Point(currW * (w + 1) + 5, currH * (h + 1) + 25);
                        }
                        else
                        {
                            pb[i].Location = new Point(currW * (w + 1) + 5, currH * (h + 1) + 25);
                        }

                        currW++;
                        if (currW == horisontalCountOfPieces)
                        {
                            currH++;
                            currW = 0;
                        }
                    }
                    pb.AddRange(top_pic);
                    pb.AddRange(bottom_pic);
                    serial_number.AddRange(top_num);
                    serial_number.AddRange(bottom_num);
                }
                else if (game_mode == "В куче")
                {


                    if (triangle)
                    {

                        for (int i = 0; i < verticalCountOfPieces * horisontalCountOfPieces; i++)
                        {

                            top_pic[i].Location = new Point(currW * (w + 1) + 5, currH * (h + 1) + 25);
                            bottom_pic[i].Location = new Point(currW * (w + 1) + 5, currH * (h + 1) + 25);


                            currW++;
                            if (currW == horisontalCountOfPieces)
                            {
                                currH++;
                                currW = 0;
                            }
                        }
                        pb.AddRange(top_pic);
                        pb.AddRange(bottom_pic);
                        serial_number.AddRange(top_num);
                        serial_number.AddRange(bottom_num);

                    }
                    else
                    {
                        Random r = new Random();
                        for (int i = 0; i < verticalCountOfPieces * horisontalCountOfPieces; i++)
                        {
                            if (triangle)
                            {
                                top_pic[i].Location = new Point(r.Next(50, 300), r.Next(50, 300));
                                bottom_pic[i].Location = new Point(r.Next(50, 300), r.Next(50, 300));
                            }
                            else
                            {
                                pb[i].Location = new Point(r.Next(50, 300), r.Next(50, 300));
                            }

                        }
                        pb.AddRange(top_pic);
                        pb.AddRange(bottom_pic);
                        serial_number.AddRange(top_num);
                        serial_number.AddRange(bottom_num);
                    }


                }
                else if (game_mode == "На ленте")
                {
                    pb.AddRange(top_pic);
                    pb.AddRange(bottom_pic);
                    serial_number.AddRange(top_num);
                    serial_number.AddRange(bottom_num);

                    buttonLeft.Enabled = true;
                    buttonLeft.Visible = true;

                    buttonRight.Enabled = true;
                    buttonRight.Visible = true;

                    this.Size = new Size(this.Width, newHeightOfForm);

                    //столько кусочков уместится на ленте
                    countOfPiecesOnStrip = (currentLocationOfStripZoneBottomRight.X - currentLocationOfStripZoneTopLeft.X - 5) / (w + 5);

                    for (int i = 0; i < countOfPiecesOnStrip; i++)
                    {
                        pb[i].Location = new Point(5 + currentLocationOfStripZoneTopLeft.X + i * (w + 5), currentLocationOfStripZoneTopLeft.Y);

                        object[] o = new object[2];
                        o[0] = ((Point)((object[])pb[i].Tag)[0]);
                        o[1] = 's';
                        pb[i].Tag = o;

                        pb[i].Visible = true;
                    }
                    currentFirstElementOnStrip = 0;
                }
            }
            else
            {
                if (game_mode == "На ленте")
                {
                    if (triangle)
                    {
                        for (int i = 0; i < 2 * verticalCountOfPieces * horisontalCountOfPieces; i += 2)
                        {
                            top_num.Add(i);
                            bottom_num.Add(i + 1);
                        }
                    }
                    pb.AddRange(top_pic);
                    pb.AddRange(bottom_pic);
                    serial_number.AddRange(top_num);
                    serial_number.AddRange(bottom_num);
                    syncShuffle<PicBox, int>(pb, serial_number);

                    buttonLeft.Enabled = true;
                    buttonLeft.Visible = true;

                    buttonRight.Enabled = true;
                    buttonRight.Visible = true;

                    this.Size = new Size(this.Width, newHeightOfForm);

                    //столько кусочков уместится на ленте
                    countOfPiecesOnStrip = (currentLocationOfStripZoneBottomRight.X - currentLocationOfStripZoneTopLeft.X - 5) / (w + 5);

                    updateStrip();
                }
            }

            //ЗАПУТАЛАСЬ С РАЗМЕТКОЙ ШО ДЕЛАТЬ
            this.Paint += new PaintEventHandler(Form1_Paint);
            hint = new PicBox();
            hint.SizeMode = PictureBoxSizeMode.StretchImage;
            hint.Size = new Size((btm1[0].Width + 1) * horisontalCountOfPieces, (btm1[0].Height + 1) * verticalCountOfPieces);
            hint.Location = new Point(5, 25);
            hint.Image = Image.FromFile(path);
            hhh = new Hint(hint);


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
            int i = 0;
            int j = 0;
            int kolichestvo = 0;
            if (triangle)
            {
                kolichestvo = 2 * verticalCountOfPieces * horisontalCountOfPieces;
            }
            else
            {
                kolichestvo = verticalCountOfPieces * horisontalCountOfPieces;
            }
            while (i < (kolichestvo))
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

            while ((i < (kolichestvo) && (j < countOfPiecesOnStrip)))
            {
                if ((char)(((object[])pb[i].Tag)[1]) == 'n')
                {
                    object[] o = new object[2];
                    o[0] = ((Point)((object[])pb[i].Tag)[0]);
                    o[1] = 's';
                    pb[i].Tag = o;
                    pb[i].Location = new Point(5 + currentLocationOfStripZoneTopLeft.X + j * (btm1[0].Width + 5), currentLocationOfStripZoneTopLeft.Y);
                    pb[i].Visible = true;
                    j++;
                }
                i++;
            }
            i = 0;
            while ((i < (kolichestvo)) && (!((char)(((object[])pb[i].Tag)[1]) == 's')))
            {
                i++;
            }
            currentFirstElementOnStrip = i;
        }

        public void updateStrip(bool right)
        {
            int i = 0;
            int j = 0;
            int kolichestvo = 0;
            if (triangle)
            {
                kolichestvo = 2 * verticalCountOfPieces * horisontalCountOfPieces;
            }
            else
            {
                kolichestvo = verticalCountOfPieces * horisontalCountOfPieces;
            }
            int curr_last_strip = kolichestvo - 1;

            while ((curr_last_strip > -1) && !((char)(((object[])pb[curr_last_strip].Tag)[1]) == 's')) curr_last_strip--;

            while (i < (kolichestvo))
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
                if (!(curr_last_strip == (kolichestvo - 1)))
                {
                    curr_last_strip++;
                    while ((!((char)(((object[])pb[curr_last_strip].Tag)[1]) == 'n')) && (curr_last_strip < kolichestvo - 1))
                    {
                        curr_last_strip++;
                    }
                    if (!(curr_last_strip == kolichestvo - 1))
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

            while ((i < (kolichestvo) && (j < countOfPiecesOnStrip)))
            {
                if ((char)(((object[])pb[i].Tag)[1]) == 'n')
                {
                    object[] o = new object[2];
                    o[0] = ((Point)((object[])pb[i].Tag)[0]);
                    o[1] = 's';
                    pb[i].Tag = o;
                    pb[i].Location = new Point(5 + currentLocationOfStripZoneTopLeft.X + j * (btm1[0].Width + 5), currentLocationOfStripZoneTopLeft.Y);
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
            hhh.Close();

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
            hhh.Close();
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
            hhh = new Hint(hint);
            hhh.Show();
        }

        private void GameOnField_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close_without_ask)
            {
                UserFindGame u = new UserFindGame(login);
                u.Show();
            }
            else
            {
                DialogResult result = MessageBox.Show("Точно хотите выйти? Для сохранения игры нажмите Отмена и Сохранить", "Уведомление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    UserFindGame u = new UserFindGame(login);
                    u.Show();
                }
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
            int l = verticalCountOfPieces * horisontalCountOfPieces;
            if (triangle) l *= 2;
            if ((currentFirstElementOnStrip + countOfPiecesOnStrip) < (l))
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

        }
        private void saveGame(string rec)
        {
            ConnDatabase bd = new ConnDatabase();
            string id_piece = "";
            int l = verticalCountOfPieces * horisontalCountOfPieces;
            if (triangle)
            {
                l *= 2;
            }
            if (rec == "На время")
            {
                TimeSpan ts = stopWatch.Elapsed.Add(fromSave);
                string formatts = ts.ToString(@"hh\:mm\:ss");
                bd.insertInGame(id_puzzle, login, game_mode, record, formatts);

                for (int i = 0; i < l; i++)
                {
                    bd.insertInPuzzlePiece(serial_number[i].ToString(), ((Point)((object[])pb[i].Tag)[0]).X.ToString(), ((Point)((object[])pb[i].Tag)[0]).Y.ToString(), id_puzzle);
                    id_piece = bd.selectIDPiece(serial_number[i].ToString(), id_puzzle);
                    if ((game_mode.Equals("На ленте")) && (((char)((object[])pb[i].Tag)[1] == 'n') || ((char)((object[])pb[i].Tag)[1] == 's')))
                    {
                        bd.insertInSave(id_piece, id_puzzle, login, (55).ToString(), (this.Size.Height - h - buttonLeft.Height - 38 + 5).ToString());
                    }
                    else
                    {
                        bd.insertInSave(id_piece, id_puzzle, login, pb[i].Location.X.ToString(), pb[i].Location.Y.ToString());
                    }

                }
                MessageBox.Show("Игра успешно сохранена!");
            }
            else
            {
                bd.insertInGame(id_puzzle, login, game_mode, record, currentmoves.ToString());
                for (int i = 0; i < l; i++)
                {

                    bd.insertInPuzzlePiece(i.ToString(), ((Point)((object[])pb[i].Tag)[1]).X.ToString(), ((Point)((object[])pb[i].Tag)[1]).Y.ToString(), id_puzzle);
                    id_piece = bd.selectIDPiece(i.ToString(), id_puzzle);
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
            if (first_move)
            {
                timer1.Enabled = true;
                stopWatch.Start();
                first_move = false;
            }
            currentmoves++;
            PicBox picture = (PicBox)pic;

            bool is_moved_top_piece = true;
            bool is_this_top_piece = true;
            char place = (char)((object[])picture.Tag)[1];
            Point rightxy = (Point)((object[])picture.Tag)[0];
            bool need_to_update_strip = false;
            ConnDatabase bd = new ConnDatabase();
            string id_piece = "";
            List<string> game = new List<string>();
            picture.Invalidate();

            if (triangle)
            {
                Color iii = ((Bitmap)picture.Image).GetPixel(5, 5);
                is_moved_top_piece = !(iii.ToArgb() == Color.Transparent.ToArgb());
            }

            if ((picture.Location.X < (rightxy.X + (btm1[0].Width / 2))) && (picture.Location.X > (rightxy.X - (btm1[0].Width / 2))) && (picture.Location.Y < (rightxy.Y + (btm1[0].Height / 2))) && (picture.Location.Y > (rightxy.Y - (btm1[0].Height / 2))))
            {
                picture.Location = rightxy;
                picture.Invalidate();
                picture.Enabled = false;
                picture.BorderStyle = BorderStyle.Fixed3D;
                if (game_mode.Equals("На ленте"))
                {
                    object[] o = new object[2];
                    o[0] = rightxy;
                    o[1] = 'f';
                    picture.Tag = o;
                    if (place == 's') need_to_update_strip = true;
                }
            }

            if (game_mode.Equals("На поле"))
            {
                //найти ячейку на которую стал пазл
                int r = 0;
                int old_num = 0;
                bool www = false;

                int l = verticalCountOfPieces * horisontalCountOfPieces;
                if (triangle) l *= 2;


                //как-то не проверить себя самого
                while ((old_num < l) && !((char)(((object[])pb[old_num].Tag)[1]) == 'o'))
                {
                    old_num++;
                }



                while ((r < l) && (!www))
                {
                    Color iii = ((Bitmap)pb[r].Image).GetPixel(5, 5);
                    is_this_top_piece = !(iii.ToArgb() == Color.Transparent.ToArgb());

                    if (r != old_num)
                    {
                        if ((picture.Location.X > (pb[r].Location.X - (btm1[0].Width / 2))) && (picture.Location.X < (pb[r].Location.X + (btm1[0].Width / 2))) && (picture.Location.Y < pb[r].Location.Y + (btm1[0].Height / 2)) && (picture.Location.Y > (pb[r].Location.Y - (btm1[0].Height / 2))))
                        {
                            if (pb[r].Enabled)
                            {
                                if (is_this_top_piece == is_moved_top_piece)
                                {
                                    www = true;
                                    //устанавливаю туда, куда он стал
                                    picture.Location = pb[r].Location;
                                    picture.BringToFront();
                                    picture.Invalidate();
                                    pb[r].Location = oldLocation;
                                    pb[r].BringToFront();
                                    pb[r].Invalidate();
                                    if (pb[r].Location.X == ((Point)((object[])pb[r].Tag)[0]).X)
                                    {
                                        if (pb[r].Location.Y == ((Point)((object[])pb[r].Tag)[0]).Y)
                                        {
                                            pb[r].Enabled = false;
                                            pb[r].BorderStyle = BorderStyle.Fixed3D;
                                        }
                                    }
                                }
                            }

                        }
                    }

                    r++;
                }
                if (!www)
                {
                    picture.Location = oldLocation;
                    picture.Invalidate();
                }
                object[] o = new object[2];
                o[0] = (Point)(((object[])picture.Tag)[0]);
                o[1] = ' ';
                picture.Tag = o;
            }


            //лента
            if (game_mode.Equals("На ленте"))
            {
                if ((currentLocationOfStripZoneTopLeft.X < picture.Location.X) && (picture.Location.X < currentLocationOfStripZoneBottomRight.X))
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
            if (need_to_update_strip) updateStrip();

            int i = 0;
            while ((i < pb.Count) && (pb[i].Enabled == false))
            {
                i++;
            }
            if (i == pb.Count)
            {
                string res = "";
                double points = 0;
                int l = verticalCountOfPieces * horisontalCountOfPieces;
                if (triangle)
                {
                    l *= 2;
                }
                TimeSpan ts = stopWatch.Elapsed;
                ts.Add(fromSave);
                int sec = ts.Hours * 60 * 60 + ts.Minutes * 60 + ts.Seconds;
                if (record.Equals("На очки"))
                {
                    points = currentmoves / (l);
                }
                else
                {
                    points = sec / (l);
                }
                if (points < betweenGreatAndNormal) points = GREAT / points;
                else if (points > betweenNormalAndBad) points = BAD / points;
                else points = NORMAL / points;
                points *= complexityKoeff;
                res = ((int)points).ToString();

                //проверка сохраненной игры 
                game = bd.selectAllAboutGameByLoginAndIdPuzzle(login, id_puzzle);
                if (game.Count != 0)
                {
                    bd.deleteGameByIdPuzzleAndLogin(id_puzzle, login);
                    for (int k = 0; k < l; k++)
                    {
                        id_piece = bd.selectIDPiece(k.ToString(), id_puzzle);
                        bd.deletePiecePuzzleByIdPuzzleAndOrIdPuzzle(id_puzzle, id_piece);
                    }
                    bd.deleteSaveByIdPuzzleAndLogin(id_puzzle, login);

                }
                string result = bd.selectResults(login);
                try
                {
                    points += Int32.Parse(result);
                }
                catch
                {
                    points += 0;
                }
                bd.setResults(login, ((int)points).ToString());
                if (MessageBox.Show("Победа!Ваш результат: " + res) == DialogResult.OK)
                {
                    stopWatch.Stop();
                    timer1.Enabled = false;
                    close_without_ask = true;
                    hhh.Close();
                    this.Close();
                }
            }
        }

        public void setOldLocation(object pic)
        {
            PicBox p = (PicBox)pic;
            oldLocation = p.Location;
            if (game_mode.Equals("На поле"))
            {
                object[] o = new object[2];
                o[0] = (Point)(((object[])p.Tag)[0]);
                o[1] = 'o';
                p.Tag = o;
            }
        }
    }
}


