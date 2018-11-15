using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle
{
    class Section
    {
        //на вход подается прямоугольник разрезанной картинки
        //на выходе 2 треугольника верхний и нижний в зависимости от параметра flag 
        //прямоуольник делится с верхнего левого угла по правый нижний если flag = true
        //прямоуольник делится с нижнего левого угла по правый верхний если flag = false
        //0- верхний треугольник, 1- нижний треугольник 
        public static Bitmap[] TriangularSection(Bitmap bitmap,bool flag)
        {
            double wight = bitmap.Width;
            double height = bitmap.Height;
            Bitmap[] bitmaps = new Bitmap[2] {new Bitmap((int)wight,(int)height), new Bitmap((int)wight, (int)height) };
            Color white = Color.Transparent;
            double k = wight / height;
            if (flag)
            {
                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < wight; w++)
                    {
                        if (w > h * k)
                        {
                            bitmaps[0].SetPixel(w, h, bitmap.GetPixel(w, h));
                            bitmaps[1].SetPixel(w, h, white);
                        }
                        else
                        {
                            bitmaps[0].SetPixel(w, h, white);
                            bitmaps[1].SetPixel(w, h, bitmap.GetPixel(w, h));
                        }
                    }
                }
            }
            else
            {
                k = -k;
                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < wight; w++)
                    {
                        if (w > h * k + wight)
                        {
                            bitmaps[0].SetPixel(w, h, bitmap.GetPixel(w, h));
                            bitmaps[1].SetPixel(w, h, white);
                        }
                        else
                        {
                            bitmaps[0].SetPixel(w, h, white);
                            bitmaps[1].SetPixel(w, h, bitmap.GetPixel(w, h));
                        }
                    }
                }
            }
            return bitmaps;
        }
        public static List<Bitmap> RectangleSection(string picturePath, string width, string height, string pictureID)
        {
            //генерация кусочков из картинки
            //пока прямоугольные

            Bitmap temp = (Bitmap)Bitmap.FromFile(picturePath);
            Size s = temp.Size;
            s.Width = 460;
            s.Height = 360;
            Bitmap src = new Bitmap(temp, s);

            int o = Convert.ToInt32(height);

            int pieceH = src.Height / Convert.ToInt32(height);
            int pieceW = src.Width / Convert.ToInt32(width);

            Bitmap src1 = new Bitmap(temp, pieceW, pieceH);

            int currX = 0;
            int currY = 0;
            List<Bitmap> btm = new List<Bitmap>();
            for (int i = 1; i <= Convert.ToInt32(height); i++)
            {
                currX = 0;
                for (int j = 1; j <= Convert.ToInt32(width); j++)
                {
                    // Задаем нужную область вырезания (отсчет с верхнего левого угла)
                    Rectangle rect = new Rectangle(new Point(currX,currY), new Size(pieceW, pieceH));
                    
                    Bitmap CuttedImage = CutImage(src, rect);

                    btm.Add(CuttedImage);
                    // btm[] = CuttedImage;//массив кусочков пазл разрезанных
                    currX += pieceW;
                }
                currY += pieceH;
            }
            return btm;
        }
        public static Bitmap CutImage(Bitmap src, Rectangle rect)
        {

            Bitmap bmp = new Bitmap(rect.Width, rect.Height); //создаем битмап

            Graphics g = Graphics.FromImage(bmp);

            g.DrawImage(src, 0, 0, rect, GraphicsUnit.Pixel); //перерисовываем с источника по координатам

            return bmp;
        }





    }
}
