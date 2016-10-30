using System;
using System.Collections.Generic;
using System.Threading;
using Comm.Global.DTO.News;
using Lfb.DataGrab.Model;
using Lib.Csharp.Tools;

namespace Lfb.DataGrab
{
    public class ImgDeal
    {
        

        public void ImgDealing()
        {
            try
            {
                Log.Info("图片处理开始");
                var listNews = DalNews.GetNoDealNewsList();

                if (listNews==null || listNews.Count<1)
                {
                    Log.Info("没有要处理的图片"+DateTime.Now);
                    return;
                }

                if (listNews.Count > 0)
                {
                    foreach (var item in listNews)
                    {
                        try
                        {
                            Log.Info("===== newsid= " + item.Id + " 处理开始 =====");
                            var newsId = item.Id;
                            var savePath = Global.ImgSavePrex + Global.ImgSaveSuffix + newsId + "\\";
                            
                            #region === 处理logo图 ===

                            Log.Info("处理logo图:"+item.LogoUrl);
                            var logUrl = item.LogoUrl;
                            var fileName = Img.NetImgSaveAs(logUrl, savePath);
                            //处理成功更新字段
                            if (!string.IsNullOrWhiteSpace(fileName))
                            {
                                var dbSavePath = Global.ImgSaveSuffix + newsId + "\\" + fileName;
                                var model = new T_News()
                                {
                                    LogoUrl = dbSavePath,
                                    //ImgFlag =1,
                                    Id = newsId
                                };
                                //model.Update();
                            }
                            #endregion
                            
                            #region === 处理内容中的图 ===

                            //暂不处理内容区，内容区的处理后用了相对地址，取出时无法加上域名前缀（不方便）

                            //Log.Info("处理内容区:" + item.LogoUrl);
                            //var content = item.Contents;
                            //var imgList = StrHelper.GetAttrStrListByXPath(content, "//img","src");
                            //if (imgList != null && imgList.Count > 0)
                            //{
                            //    foreach (var img in imgList)
                            //    {
                            //        try
                            //        {
                            //            if (img.Length < 1)
                            //                continue;
                            //            var nfileName = Img.NetImgSaveAs(img, savePath);
                            //            //替换内容区中的该图片链接
                            //            if (!string.IsNullOrWhiteSpace(nfileName))
                            //            {
                            //                var dbSavePath = Global.ImgSaveSuffix + newsId + "\\" + nfileName;
                            //                content = content.Replace(img, dbSavePath);

                            //            }
                            //        }
                            //        catch (Exception ex)
                            //        {
                            //            Log.Error("处理异常: " + img);
                            //            Log.Error(ex.Message + ex.StackTrace);
                            //        }
                            //    }
                            //    //处理成功更新字段
                            //    var model = new T_News()
                            //    {
                            //        Contents = content,
                            //        Id = newsId
                            //    };
                            //    model.Update();
                            //}

                            #endregion

                            #region === 处理图片类型下的多图部分 ===

                            if (item.NewsMedia != null && item.NewsMedia.Count > 0)
                            {
                                foreach (var subItem in item.NewsMedia)
                                {
                                    var picUrl = subItem.PicUrl;
                                    var nfileName = Img.NetImgSaveAs(picUrl, savePath);
                                    //处理成功更新字段
                                    if (!string.IsNullOrWhiteSpace(nfileName))
                                    {
                                        var dbSavePath = Global.ImgSaveSuffix + newsId + "\\" + nfileName;
                                        //var model = new T_NewsMedia()
                                        //{
                                        //    PicUrl = dbSavePath,
                                        //    Id = subItem.Id
                                        //};
                                        //model.Update();
                                    }
                                }
                            }

                            #endregion

                            Log.Info("===== newsid= "+item.Id+" 处理完成 =====");

                            //休眠 控制抓取的频率
                            Random rnd = new Random();
                            var sleepSeconds = rnd.Next(30, 90);
                            Thread.Sleep(sleepSeconds * 1000);
                        }
                        catch (Exception ex)
                        {
                            Log.Error("内容: "+item);
                            Log.Error(ex.Message + ex.StackTrace);
                        }
                    }
                }
                Log.Info("图片处理结束");
                
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
            
        }

        /// <summary>
        /// 主要处理内容里广告性质的文字及样式
        /// </summary>
        /// <param name="contents"></param>
        /// <returns></returns>
        public static List<DtoNewsMedia> GetImgList(string contents)
        {
            var str = contents;
            var list = new List<DtoNewsMedia>();
            try
            {
                //提取内容中的图片链接
                var imgList = XpathHelper.GetAttrValueListByXPath(str, "//img", "src");
                if (imgList != null && imgList.Count > 0)
                {
                    var i = 0;
                    foreach (var img in imgList)
                    {
                        try
                        {
                            var model = new DtoNewsMedia()
                            {
                                Description = "",
                                IsShow = 1,
                                NewsId = 0,
                                Orders = i,
                                PicOriginalUrl = img,
                                PicUrl = img,
                                ThumbnailUrl = img
                            };
                            list.Add(model);
                        }
                        catch (Exception ex)
                        {
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }
    }
}
