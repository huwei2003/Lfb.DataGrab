using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using Lib.Csharp.Tools.Dto;

namespace Lib.Csharp.Tools
{
    public class ImageHelper
    {
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式 HW:指定高宽缩放（可能变形） W:指定宽，高按比例 H:指定高，宽按比例 Cut:指定高宽裁减（不变形）</param>    
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

            var towidth = width;
            var toheight = height;

            var x = 0;
            var y = 0;
            var ow = originalImage.Width;
            var oh = originalImage.Height;

            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）                
                    break;
                case "W"://指定宽，高按比例                    
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）                
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }

            //新建一个bmp图片
            var bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            var g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        /// 在图片上增加文字水印
        /// </summary>
        /// <param name="path">原服务器图片路径</param>
        /// <param name="pathSy">生成的带文字水印的图片路径</param>
        /// <param name="addText">生成的文字</param>
        /// <param name="isDeleteOld">是否删除旧图片</param>
        public static void AddWater(string path, string pathSy, string addText, bool isDeleteOld)
        {
            var image = System.Drawing.Image.FromFile(path);
            var g = System.Drawing.Graphics.FromImage(image);
            g.DrawImage(image, 0, 0, image.Width, image.Height);
            var x = image.Width / 2 - 50;
            var y = image.Height / 2 + 80;

            var gp = new GraphicsPath();
            var rec = new Rectangle(new Point(x, y), new Size(90, 20));
            gp.AddRectangle(rec);
            //g.FillRectangle(new Pen(Color.Gold), rec);
            g.FillRectangle(Brushes.White, rec);

            var f = new System.Drawing.Font("Verdana", 18);
            var b = new System.Drawing.SolidBrush(System.Drawing.Color.Gold);

            var rng = new System.Drawing.RectangleF(x, y, 90, 20);

            g.DrawString(addText, f, b, x, y);

            g.Dispose();

            image.Save(pathSy);
            image.Dispose();

            if (isDeleteOld && File.Exists(path))
                File.Delete(path);

        }

        /// <summary>
        /// 在图片上生成图片水印
        /// </summary>
        /// <param name="path">原服务器图片路径</param>
        /// <param name="pathSyp">生成的带图片水印的图片路径</param>
        /// <param name="pathSypf">水印图片路径</param>
        /// <param name="isDeleteOld">是否删除旧图片</param>
        public static void AddWaterPic(string path, string pathSyp, string pathSypf, bool isDeleteOld)
        {
            var image = System.Drawing.Image.FromFile(path);
            var copyImage = System.Drawing.Image.FromFile(pathSypf);
            var g = System.Drawing.Graphics.FromImage(image);
            var x = (image.Width - copyImage.Width) / 2;
            var y = (image.Width - copyImage.Width) / 2;
            var height = copyImage.Height;
            var width = copyImage.Width;
            g.DrawImage(copyImage, new System.Drawing.Rectangle(x, y, height, width), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();

            image.Save(pathSyp);
            image.Dispose();

            if (isDeleteOld && File.Exists(path))
                File.Delete(path);
        }

        ///<summary>
        ///检查文件类型是否允许上传
        ///</summary>
        public static bool IsAllowedExtension(string filePath)
        {
            var fileConfig = new string[] { ".jpg", ".bmp", ".png", ".gif" };

            if (!string.IsNullOrEmpty(filePath))
            {
                var fileExtension = System.IO.Path.GetExtension(filePath);
                for (var i = 0; i < fileConfig.Length; i++)
                {
                    if (fileExtension.Equals(fileConfig[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 判断上传文件大小是否超过设置的最大值
        /// </summary>
        public static bool IsAllowedLength(System.Web.UI.WebControls.FileUpload Controls)
        {
            var maxLength = 100;
            try
            {
                maxLength = 10 * 1024 * 1024;
            }
            catch
            {
                maxLength = 100;
            }

            maxLength = maxLength * 1024;

            if (Controls.PostedFile.ContentLength > maxLength)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取指定图片的属性信息
        /// </summary>
        /// <param name="originalImagePath"></param>
        /// <returns></returns>
        public static DtoImageInfo GetPicInfo(string originalImagePath)
        {
            var retObj = new DtoImageInfo();
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            try
            {
                retObj.Width = originalImage.Width;
                retObj.Height = originalImage.Height;
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();

            }
            return retObj;
        }

        /// <summary>
        /// 网络图片另存到本地路径
        /// </summary>
        /// <param name="picUrl">网络图片地址</param>
        /// <param name="savePath">本地存放地址</param>
        /// <returns></returns>
        public static string NetImgSaveAs(string picUrl, string savePath)
        {

            var fileName = "";
            try
            {
                var stream = HttpHelper.GetStream(picUrl, Encoding.UTF8);
                if (stream != null)
                {
                    var img = new Bitmap(stream); //获取图片流
                    fileName = GetImgFileName(picUrl, img.Width, img.Height);
                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }
                    img.Save(savePath + fileName); //随机名

                }
            }
            catch (Exception ex)
            {
                //Log.Error(ex.Message + ex.StackTrace);
            }
            finally
            {

            }
            return fileName;
        }

        /// <summary>
        /// 生成带width,height信息的图片文件名
        /// </summary>
        /// <param name="picUrl"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string GetImgFileName(string picUrl, int width, int height)
        {
            var rnd = new Random();
            var fileName = "";
            try
            {

                //http://p0.ifengimg.com/a/2016_37/3102d9151fdd675_size64_w410_h272.jpg
                var arrStr = picUrl.Split('/');
                if (arrStr.Length == 0)
                {
                    arrStr = picUrl.Split('\\');
                }
                var tempName = arrStr[arrStr.Length - 1];
                var extName = tempName.Split(',')[1];

                fileName = rnd.Next(10000, 1000000).ToString() + "_" + DateTime.Now.ToString("yyMMddHHmmss") + "_w" + width + "_h" + height + "." + extName;
                return fileName;
            }
            catch (Exception ex)
            {
                //Log.Error(ex.Message + ex.StackTrace);
            }
            fileName = rnd.Next(10000, 1000000).ToString() + "_" + DateTime.Now.ToString("yyMMddHHmmss") + "_w" + width + "_h" + height + ".png";
            return fileName;
        }
    }
}
