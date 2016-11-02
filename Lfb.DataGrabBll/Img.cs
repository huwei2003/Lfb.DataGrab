using System;
using System.Drawing;
using System.IO;
using System.Text;
using Lib.Csharp.Tools;

namespace Lfb.DataGrabBll
{
    public class Img
    {

        
        /// <summary>
        /// 网络图片另存到本地路径
        /// </summary>
        /// <param name="picUrl">网络图片地址</param>
        /// <param name="savePath">本地存放地址</param>
        /// <returns></returns>
        public static string NetImgSaveAs(string picUrl,string savePath)
        {
            
            var fileName = "";
            try
            {
                var stream = HttpHelper.GetStream(picUrl, Encoding.UTF8);
                if (stream != null)
                {
                    var img = new Bitmap(stream); //获取图片流
                    fileName = GetImgFileName(picUrl,img.Width,img.Height);
                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }
                    img.Save(savePath + fileName); //随机名
                    
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
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
        public static string GetImgFileName(string picUrl,int width,int height)
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
                Log.Error(ex.Message + ex.StackTrace);
            }
            fileName = rnd.Next(10000, 1000000).ToString() + "_" + DateTime.Now.ToString("yyMMddHHmmss") + "_w" + width + "_h" + height + ".png";
            return fileName;
        }
    }
}
