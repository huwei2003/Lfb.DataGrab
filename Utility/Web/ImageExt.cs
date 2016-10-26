using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Comm.Tools.Utility.Web
{
    public static class ImageExt
    {
        /// <summary>
        /// 彩色图片转化为黑白
        /// </summary>
        /// <returns></returns>
        public static Bitmap ToGrayscale(this Bitmap bitmap)
        {
            var bm = new Bitmap(bitmap.Width, bitmap.Height);
            for (var y = 0; y < bm.Height; y++)
            {
                for (var x = 0; x < bm.Width; x++)
                {
                    var c = bitmap.GetPixel(x, y);
                    var rgb = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
                    bm.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            }
            return bm;
        }

        /// <summary>
        /// 放大缩小图片尺寸
        /// </summary>
        /// <param name="imgPath">原图片地址</param>
        /// <param name="savePath">处理后图片的地址</param>
        /// <param name="iSize">倍数</param>
        public static void ResetSized(string imgPath, string savePath, double iSize)
        {
            using (var img = new Bitmap(imgPath))
            {
                using (var bmp = img.ResetRectangle(new Size((int)(img.Width * iSize), (int)(img.Height * iSize))))
                {
                    bmp.Save(savePath);
                }
            }
        }

        /// <summary>
        /// 放大缩小图片尺寸
        /// </summary>
        /// <param name="imgPath">原图片地址</param>
        /// <param name="savePath">处理后图片的地址</param>
        /// <param name="func">处理后的图片位置图片大小</param>
        public static void ResetRectangle(string imgPath, string savePath, Func<Image, Size> func)
        {
            using (var img = new Bitmap(imgPath))
            {
                using (var bmp = img.ResetRectangle(func(img)))
                {
                    bmp.Save(savePath);
                }
            }
        }

        /// <summary>
        /// 重置图片宽高
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="size">处理后的图片位置图片大小</param>
        /// <param name="isCut">false可能会变形;true不变形,但超出部分会被裁剪</param>
        public static Image ResetRectangle(this Image img, Size size, bool isCut = false)
        {
            var srcRect = new Rectangle(0, 0, img.Width, img.Height);
            if (isCut)
            {
                var rate = 1d * size.Width / size.Height;
                var initRate = 1d * srcRect.Width / srcRect.Height;
                if (rate > initRate)
                {
                    srcRect.Width = initRate > 1 ? img.Height : img.Width;
                    srcRect.Height = (int)(srcRect.Width / rate);
                    srcRect.Y = (img.Height - srcRect.Height) / 2;
                }
                else
                {
                    srcRect.Height = initRate < 1 ? img.Width : img.Height;
                    srcRect.Width = (int)(img.Height * rate);
                    srcRect.X = (img.Width - srcRect.Width) / 2;
                }
            }
            return img.ResetRectangle(new Rectangle(0, 0, size.Width, size.Height), srcRect);
        }

        /// <summary>
        /// 重置图片宽高
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="destRect">将图像进行缩放以适合该矩形</param>
        /// <param name="srcRect">Image对象中要绘制的部分</param>
        public static Image ResetRectangle(this Image img, Rectangle destRect, Rectangle srcRect)
        {
            var bmp = new Bitmap(destRect.Width, destRect.Height);
            using (var g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.High;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(img, destRect, srcRect, GraphicsUnit.Pixel);
            }
            return bmp;
        }
    }
}