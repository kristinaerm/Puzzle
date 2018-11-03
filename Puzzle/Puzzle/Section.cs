using System;
using System.Collections.Generic;
using System.Drawing;
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
        //нулевой верхний треугольник первый нижний треугольник 
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
    }
}
