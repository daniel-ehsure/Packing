using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;

namespace Packing
{
    /// <summary>
    /// 条码打印
    /// </summary>
    public class Barcode
    {
        static object o = new object();
        static PrintDocument pd = new PrintDocument();
        static Image printImage;

        static Barcode()
        {
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
        }

        public static void Print(string bar, string name)
        {
            lock (o)
            {
                //整个图片的大小
                printImage = getPrintImage(bar, name, 550, 200);
                pd.Print();
            }
        }

        static void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            using (Graphics g = e.Graphics)
            {
                g.DrawImage(printImage, 0, 0);
            }
        }

        /// <summary>
        /// 获得要打印的图片
        /// 具体位置需要调整
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="name"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        private static Image getPrintImage(string barcode, string name, int w, int h)
        {
            Image im = new Bitmap(w, h);
            int fontSize = 15;
            Font fon = new System.Drawing.Font("宋体", fontSize);
            Code128 code = new Code128();
            code.Height = 40;//这里可以控制条码的高度
            code.Magnify = 0;//这里可以控制条码的宽度
            code.ValueFont = fon;
            Image iim = code.GetCodeImage(barcode, Code128.Encode.Code128B);

            Graphics g = Graphics.FromImage(im);
            SolidBrush wbrush = new SolidBrush(Color.White);
            g.FillRectangle(wbrush, 0, 0, w, h);
            Pen pen = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(Color.Black);
            g.DrawRectangle(pen, 5, 5, w - 10, h - 10);

            g.DrawImage(iim, 10, 10);//从10，10开始打印条码

            g.DrawString(name, fon, brush, 10, iim.Height + 10);//在条码下面打印内容

            return im;
        }
    }
}
