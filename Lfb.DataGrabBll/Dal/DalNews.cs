using System;
using System.Collections.Generic;
using System.Linq;
using Comm.Cloud.RDS;
using Comm.Cloud.RDS.DTO;
using Comm.Global.DTO.News;
using Comm.Global.Enum.Sys;
using Comm.Tools.Utility;
using Lfb.DataGrabBll.Model;
using Lib.Csharp.Tools;
using Lfb.DataGrabBll.Dto;
using Log = Lib.Csharp.Tools.Log;

namespace Lfb.DataGrabBll
{
    public class DalNews
    {
        private static RdsNew Sql; //注意在TestFixtureSetUp后初始化
        private static object lockObj1 = new object();
        private static object lockObj1News = new object();
        private static object lockObj1Author = new object();

        static DalNews()
        {
            Sql = new RdsNew(RdsConfig, DbType.MySql);
        }

        public static PublicCloudRdsConfig RdsConfig
        {
            get
            {
                var sqlInfo = Global.MySql1;
                var server = "localhost";
                var db = "News";
                var uid = "root";
                var pwd = "";
                var port = 3306;
                if (!string.IsNullOrWhiteSpace(sqlInfo))
                {
                    var arrStr = sqlInfo.Split(';');
                    if (arrStr != null && arrStr.Length > 0)
                    {
                        foreach (var item in arrStr)
                        {
                            //Server=localhost;Database=News;Uid=root;Pwd=;Port=3306
                            if (item.Contains("Server="))
                            {
                                server = item.Replace("Server=", "");
                            }
                            if (item.Contains("Database="))
                            {
                                db = item.Replace("Database=", "");
                            }
                            if (item.Contains("Uid="))
                            {
                                uid = item.Replace("Uid=", "");
                            }
                            if (item.Contains("Pwd="))
                            {
                                pwd = item.Replace("Pwd=", "");
                            }
                            if (item.Contains("Port="))
                            {
                                port = StrHelper.ToInt32(item.Replace("Port=", ""));
                            }
                        }
                    }
                }
                var rdsConfig = new PublicCloudRdsConfig
                {
                    Server = server,
                    Database = db,
                    Uid = uid,
                    Pwd = pwd,
                    Port = port,
                };
                return rdsConfig;
            }
        }

        #region === news deal ===
        /// <summary>
        /// 添加一条新闻
        /// </summary>
        /// <param name="model">新闻实体</param>
        /// <returns></returns>
        public static int Insert(DtoNews model)
        {
            try
            {
                if (model.Author == null)
                    model.Author = "";
                if (model.Contents == null)
                    model.Contents = "";
                if (model.FromSiteName == null)
                    model.FromSiteName = "";
                if (model.PubTime == null)
                    model.PubTime = DateTime.Now;
                if (model.FromUrl == null)
                    model.FromUrl = "";
                if (model.LogoOriginalUrl == null)
                    model.LogoOriginalUrl = "";
                if (model.LogoUrl == null)
                    model.LogoUrl = "";
                if (model.Title == null)
                    model.Title = "";

                ////非图片的，且内容小于100的不入库
                //if (model.NewsTypeId != NewsTypeEnum.图片  && model.Contents.Length < 100)
                //{
                //    return -1;
                //}
                //if(string.IsNullOrWhiteSpace(model.Title.Trim()))
                //{
                //    return -1;
                //}

                var item = new T_News()
                {
                    Author = model.Author,
                    Contents = model.Contents,
                    //CreateTime = model.CreateTime,
                    FromSiteName = model.FromSiteName,
                    FromUrl = model.FromUrl,
                    IsShow = 0,
                    LogoOriginalUrl = model.LogoOriginalUrl,
                    LogoUrl = model.LogoUrl,
                    NewsTypeId = (int)model.NewsTypeId,
                    PubTime = model.PubTime,
                    Title = model.Title,
                    AuthorId = model.AuthorId,
                    TotalComments = model.TotalComments,
                    Tags = model.Tags,
                    NewsHotClass = model.NewsHotClass,
                    LastReadTimes = model.LastReadTimes,
                    LastDealTime = DateTime.Now,
                    IsHot = model.IsHot,
                    IsDeal = model.IsDeal,
                    IntervalMinutes = model.IntervalMinutes,
                    CurReadTimes = model.CurReadTimes,
                    CreateTime = DateTime.Now,
                    GroupId = model.GroupId,
                };


                var id = Sql.InsertId<T_News>(item);

                return id;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
                return -1;
            }
        }

        public static int UpdateNews(DtoNews model)
        {
            try
            {
                var news = new T_News()
                {
                    Id = model.Id,
                    CurReadTimes = model.CurReadTimes,
                    LastDealTime = DateTime.Now,
                    LastReadTimes = model.LastReadTimes,
                    IsHot = model.IsHot,
                    IsDeal = 1,
                    TotalComments = model.TotalComments,
                    NewsHotClass = model.NewsHotClass,
                    IntervalMinutes = model.IntervalMinutes,
                    GroupId = model.GroupId,
                };
                return Sql.Update(news, "Id={0}".Formats(model.Id));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
            return 1;
        }
        /// <summary>
        /// 判断某条新闻是否已存在
        /// </summary>
        /// <param name="title">新闻标题</param>
        /// <returns></returns>
        public static bool IsExistsNews(string title)
        {
            try
            {
                var sql = "select Id from T_News where Title=?";

                var id = Sql.ExecuteScalar(0, sql, title);

                return id > 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 判断某条新闻是否已存在
        /// </summary>
        /// <param name="authorId">新闻作者</param>
        /// <param name="title">新闻标题</param>
        /// <returns></returns>
        public static int IsExistsNews(string authorId, string title)
        {
            try
            {
                var sql = "select Id from T_News where AuthorId=? and Title=?";

                var id = Sql.ExecuteScalar(0, sql, authorId, title);

                return id;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return 0;
        }

        public static bool DelNews(int id)
        {
            try
            {
                var sql = string.Format("delete from T_News where Id={0}", id);

                var result = Sql.ExecuteSql(sql);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 添加一条新闻
        /// </summary>
        /// <param name="model">新闻实体</param>
        /// <returns></returns>
        public static int InsertMedia(DtoNewsMedia model)
        {
            try
            {
                if (model.Description == null)
                    model.Description = "";
                if (model.PicOriginalUrl == null)
                    model.PicOriginalUrl = "";
                if (model.PicUrl == null)
                    return -1;
                if (model.NewsId < 1)
                    return -1;

                //var item = new T_NewsMedia()
                //{
                //    Description = model.Description,
                //    IsShow = 1,
                //    NewsId = model.NewsId,
                //    Orders = model.Orders,
                //    PicOriginalUrl = model.PicOriginalUrl,
                //    PicUrl = model.PicUrl,
                //    //ThumbnailUrl = model.ThumbnailUrl,

                //};

                //var id = item.InsertIdNoTrans();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
                return -1;
            }
        }

        /// <summary>
        /// 获取30条图片未处理的记录
        /// </summary>
        /// <returns></returns>
        public static List<DtoNewsAll> GetNoDealNewsList()
        {
            try
            {
                var sql = "select top 30 * from T_News where ImgFlag=0 order By Id DESC";
                var list = Sql.Select<DtoNewsAll>(sql);

                if (list != null && list.Count > 0)
                {
                    var ids = list.Select(p => p.Id).Join(",");
                    if (ids.Length == 0)
                    {
                        ids = "0";
                    }
                    var list2 = GetNewsMedia(ids);

                    #region === 循环取每个回复 ===

                    foreach (var item in list)
                    {

                        if (list2 == null || list2.Count <= 0) continue;
                        var medias = list2.Where(p => p.NewsId == item.Id).ToList();
                        item.NewsMedia = medias;

                    }

                    #endregion
                }

                return list;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// 获取指定id的图片信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        private static List<DtoNewsMedia> GetNewsMedia(string ids)
        {
            try
            {
                var sql = @"SELECT * FROM dbo.T_NewsMedia WHERE NewsId in({0})".Formats(ids);

                var list = Sql.Select<DtoNewsMedia>(sql);
                return list;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 获取某一个新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T_News GetNews(int id)
        {
            try
            {
                var sql = @"SELECT  * FROM T_News WHERE Id={0}".Formats(id);

                var list = Sql.Select<T_News>(sql);
                if (list != null && list.Count > 0)
                {
                    return list[0];
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// 更新新闻的图片处理状态
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <param name="imgFlag">图片处理标识 0=no,1=yes</param>
        /// <returns></returns>
        public static bool UpdateImgFlag(int id, int imgFlag)
        {
            try
            {
                if (imgFlag > 1)
                    imgFlag = 1;
                if (imgFlag < 0)
                    imgFlag = 0;

                var sql = string.Format("update T_News set ImgFlag={0} where id={1}", imgFlag, id);

                var result = Sql.ExecuteSql(sql);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 获取100条未处理的新闻记录 未处理是指没有抓取该新闻详细页，从中取出作者url
        /// </summary>
        /// <returns></returns>
        public static List<DtoNews> GetNoGatherAuthorUrlNewsList()
        {
            try
            {
                lock (lockObj1News)
                {
                    var sql = "select * from T_News where (IsShow<=1) order By Id DESC limit 0,100";
                    var list = Sql.Select<DtoNews>(sql);

                    if (list != null && list.Count > 0)
                    {
                        var ids = list.Select(p => p.Id).Join(",");
                        if (ids.Length == 0)
                        {
                            ids = "0";
                        }
                        //取出后置位IsShow 正在处理状态　IsShow=2
                        sql = "update T_News set IsShow=2 where Id in({0})".Formats(ids);
                        Sql.ExecuteSql(sql);
                    }
                    else
                    {
                        //全部执行完后统一回位 isshow=1
                        sql = "update T_News set IsShow=1";
                        Sql.ExecuteSql(sql);
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return null;
        }


        /// <summary>
        /// 更新新闻的关键字字段
        /// </summary>
        /// <returns></returns>
        public static bool UpdateNewsTags(int newsId, string tags)
        {
            try
            {
                var sql = "update T_News set Tags='{0}' where Id={1}".Formats(tags, newsId);
                Sql.ExecuteSql(sql);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return true;
        }

        #endregion

        #region === author deal ===
        /// <summary>
        /// 添加一条新闻
        /// </summary>
        /// <param name="model">新闻实体</param>
        /// <returns></returns>
        public static int Insert(DtoAuthor model)
        {
            try
            {
                if (model.Author == null)
                    model.Author = "";
                if (model.AuthorId == null)
                    return -1;
                if (model.Url == null)
                    return -1;

                var item = new T_Author()
                {
                    Author = model.Author,
                    CreateTime = DateTime.Now,
                    AuthorId = model.AuthorId,
                    IsDeal = model.IsDeal,
                    LastDealTime = DateTime.Now,
                    Url = model.Url,
                    IsShow = 0,
                    GroupId = model.GroupId
                };


                var id = Sql.InsertId<T_Author>(item);

                return id;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
                return -1;
            }
        }

        /// <summary>
        /// 判断某个作者是否已存在
        /// </summary>
        /// <param name="authorId">作者id</param>
        /// <returns></returns>
        public static bool IsExistsAuthor(string authorId)
        {
            try
            {
                var sql = "select Id from T_Author where AuthorId=?";

                var id = Sql.ExecuteScalar(0, sql, authorId);

                return id > 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 更新作者的处理状态
        /// </summary>
        /// <param name="id">作者表id</param>
        /// <param name="isDeal">处理标识 0=no,1=yes</param>
        /// <returns></returns>
        public static bool UpdateAuthorIsDeal(int id, int isDeal)
        {
            try
            {
                if (isDeal > 1)
                    isDeal = 1;
                if (isDeal < 0)
                    isDeal = 0;

                var sql = string.Format("update T_Author set IsDeal={0} where Id={1}", isDeal, id);

                var result = Sql.ExecuteSql(sql);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 更新作者的groupid
        /// </summary>
        /// <param name="authroId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static bool UpdateAuthorGroupId(string authroId, string groupId)
        {
            try
            {
                lock (lockObj1Author)
                {
                    var sql = "";
                    if (string.IsNullOrWhiteSpace(groupId))
                    {
                        sql = string.Format("update T_Author set RefreshTimes=RefreshTimes+1 where AuthorId='{0}'",
                            authroId);
                        //return true;
                    }
                    else
                    {
                        sql =
                            string.Format(
                                "update T_Author set GroupId='{0}',RefreshTimes=RefreshTimes+1 where AuthorId='{1}' and (GroupId='0' or GroupId='')",
                                groupId, authroId);
                    }

                    var result = Sql.ExecuteSql(sql);
                    return result;
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 更新作者的处理状态
        /// </summary>
        /// <param name="id">作者表id</param>
        /// <param name="isShow">处理标识 0=no,1=yes</param>
        /// <returns></returns>
        public static bool UpdateAuthorIsShow(int id, int isShow)
        {
            try
            {
                if (isShow > 1)
                    isShow = 1;
                if (isShow < 0)
                    isShow = 0;

                var sql = string.Format("update T_Author set IsShow={0} where Id={1}", isShow, id);

                var result = Sql.ExecuteSql(sql);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return false;
            }
        }
        public static bool UpdateAuthorInterval(string authorId, int intervalMinutes)
        {
            try
            {
                var sql = string.Format("update T_Author set IntervalMinutes={0} where AuthorId='{1}'", intervalMinutes, authorId);

                var result = Sql.ExecuteSql(sql);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return false;
            }
        }
        public static bool UpdateAuthorIsDeal(string authorId, int isDeal)
        {
            try
            {
                var sql = string.Format("update T_Author set IsDeal={0} where AuthorId='{1}'", isDeal, authorId);

                var result = Sql.ExecuteSql(sql);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// 获取100条未处理的作者记录
        /// </summary>
        /// <returns></returns>
        public static List<DtoAuthor> GetNoDealAuthorList()
        {
            try
            {
                lock (lockObj1Author)
                {
                    var sql = "select * from T_Author where (IsDeal<=1) order By Id DESC limit 0,100";
                    var list = Sql.Select<DtoAuthor>(sql);

                    if (list != null && list.Count > 0)
                    {
                        var ids = list.Select(p => p.Id).Join(",");
                        if (ids.Length == 0)
                        {
                            ids = "0";
                        }
                        //取出后置位isdeal 正在处理状态　isdeal=2
                        sql = "update T_Author set IsDeal=2,RefreshTimes=RefreshTimes+1 where Id in({0})".Formats(ids);
                        Sql.ExecuteSql(sql);
                    }
                    else
                    {
                        //当isdeal=0 =1的没有时，全部置位
                        sql = "update T_Author set IsDeal=1";
                        Sql.ExecuteSql(sql);
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// 获取100条未处理的作者记录
        /// </summary>
        /// <returns></returns>
        public static List<DtoAuthor> GetNoRefreshAuthorList()
        {
            try
            {
                lock (lockObj1Author)
                {
                    var sql = "select * from T_Author where (IsShow<=1) order By Id asc limit 0,100";
                    var list = Sql.Select<DtoAuthor>(sql);

                    if (list != null && list.Count > 0)
                    {
                        var ids = list.Select(p => p.Id).Join(",");
                        if (ids.Length == 0)
                        {
                            ids = "0";
                        }
                        //取出后置位isdeal 正在处理状态　IsShow=2
                        sql = "update T_Author set IsShow=2,RefreshTimes=RefreshTimes+1 where Id in({0})".Formats(ids);
                        Sql.ExecuteSql(sql);
                    }
                    else
                    {
                        //全部处理完后置位IsShow=1
                        sql = "update T_Author set IsShow=1";
                        Sql.ExecuteSql(sql);
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// 获取需要刷新的作者记录
        /// </summary>
        /// <returns></returns>
        public static List<DtoAuthor> GetWaitRefreshAuthorList()
        {
            try
            {
                //因为此处有多个线程同时执行
                lock (lockObj1)
                {
                    var curTime = DateTime.Now;
                    //取一个月内抓取且已到刷新时间的新闻的作者列表
                    var sql =
                        "select * from t_author where (IsDeal=0 or IsDeal=1) and AuthorId in(SELECT DISTINCT AuthorId from t_news WHERE  DATE_ADD(PubTime,INTERVAL 30 DAY)>'{0}' and DATE_ADD(LastDealTime,INTERVAL IntervalMinutes MINUTE)>'{0}' and (IsDeal<=1)) order By Id DESC limit 0,1000"
                            .Formats(curTime);


                    var list = Sql.Select<DtoAuthor>(sql);

                    //取过的新闻置位isdeal=1
                    var sql2 =
                        "update T_News set IsDeal=2,RefreshTimes=RefreshTimes+1 where DATE_ADD(PubTime,INTERVAL 30 DAY)>'{0}' and DATE_ADD(LastDealTime,INTERVAL IntervalMinutes MINUTE)>'{0}' and (IsDeal<=1)"
                            .Formats(curTime);
                    Sql.ExecuteSql(sql2);

                    if (list != null && list.Count > 0)
                    {
                        var ids = list.Select(p => p.Id).Join(",");
                        if (ids.Length == 0)
                        {
                            ids = "0";
                        }
                        //取出后置位isdeal 正在处理状态　isdeal=2
                        sql = "update T_Author set IsDeal=2,RefreshTimes=RefreshTimes+1 where Id in({0})".Formats(ids);
                        Sql.ExecuteSql(sql);

                    }
                    else
                    {
                        //全部执行完后统一回位 isdeal=1
                        sql = "update T_Author set IsDeal=1";
                        Sql.ExecuteSql(sql);

                        //全部执行完后统一回位 isdeal=1
                        sql = "update T_News set IsDeal=1";
                        Sql.ExecuteSql(sql);
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return null;
        }


        #endregion

        #region === t_news_bjh deal ===
        /// <summary>
        /// 添加一条新闻
        /// </summary>
        /// <param name="model">新闻实体</param>
        /// <returns></returns>
        public static int Insert_News_Bjh(DtoNews model)
        {
            try
            {
                if (model.Author == null)
                    model.Author = "";
                if (model.Contents == null)
                    model.Contents = "";
                if (model.FromSiteName == null)
                    model.FromSiteName = "";
                if (model.PubTime == null)
                    model.PubTime = DateTime.Now;
                if (model.FromUrl == null)
                    model.FromUrl = "";
                if (model.LogoOriginalUrl == null)
                    model.LogoOriginalUrl = "";
                if (model.LogoUrl == null)
                    model.LogoUrl = "";
                if (model.Title == null)
                    model.Title = "";
                if (model.FeedId == null)
                    model.FeedId = "";

                ////非图片的，且内容小于100的不入库
                //if (model.NewsTypeId != NewsTypeEnum.图片  && model.Contents.Length < 100)
                //{
                //    return -1;
                //}
                //if(string.IsNullOrWhiteSpace(model.Title.Trim()))
                //{
                //    return -1;
                //}

                var item = new T_News_Bjh()
                {
                    Author = model.Author,
                    Contents = model.Contents,
                    //CreateTime = model.CreateTime,
                    FromSiteName = model.FromSiteName,
                    FromUrl = model.FromUrl,
                    IsShow = 0,
                    LogoOriginalUrl = model.LogoOriginalUrl,
                    LogoUrl = model.LogoUrl,
                    NewsTypeId = (int)model.NewsTypeId,
                    PubTime = model.PubTime,
                    Title = model.Title,
                    AuthorId = model.AuthorId,
                    TotalComments = model.TotalComments,
                    Tags = model.Tags,
                    NewsHotClass = model.NewsHotClass,
                    LastReadTimes = model.LastReadTimes,
                    LastDealTime = DateTime.Now,
                    IsHot = model.IsHot,
                    IsDeal = model.IsDeal,
                    IntervalMinutes = model.IntervalMinutes,
                    CurReadTimes = model.CurReadTimes,
                    CreateTime = DateTime.Now,
                    GroupId = model.GroupId,
                    FeedId = model.FeedId,
                };


                var id = Sql.InsertId<T_News_Bjh>(item);

                return id;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                //Log.Error(ex.Message + ex.StackTrace);
                return -1;
            }
        }

        /// <summary>
        /// 添加一条新闻 用于特殊作者处理
        /// </summary>
        /// <param name="model">新闻实体</param>
        /// <returns></returns>
        public static int Insert_News_Bjh_ForClient(DtoNews model)
        {
            try
            {
                if (model.Author == null)
                    model.Author = "";
                if (model.Contents == null)
                    model.Contents = "";
                if (model.FromSiteName == null)
                    model.FromSiteName = "";
                if (model.PubTime == null)
                    model.PubTime = DateTime.Now;
                if (model.FromUrl == null)
                    model.FromUrl = "";
                if (model.LogoOriginalUrl == null)
                    model.LogoOriginalUrl = "";
                if (model.LogoUrl == null)
                    model.LogoUrl = "";
                if (model.Title == null)
                    model.Title = "";
                if (model.FeedId == null)
                    model.FeedId = "";

                ////非图片的，且内容小于100的不入库
                //if (model.NewsTypeId != NewsTypeEnum.图片  && model.Contents.Length < 100)
                //{
                //    return -1;
                //}
                //if(string.IsNullOrWhiteSpace(model.Title.Trim()))
                //{
                //    return -1;
                //}

                var item = new T_News_Bjh_Client()
                {
                    Author = model.Author,
                    Contents = model.Contents,
                    //CreateTime = model.CreateTime,
                    FromSiteName = model.FromSiteName,
                    FromUrl = model.FromUrl,
                    IsShow = 0,
                    LogoOriginalUrl = model.LogoOriginalUrl,
                    LogoUrl = model.LogoUrl,
                    NewsTypeId = (int)model.NewsTypeId,
                    PubTime = model.PubTime,
                    Title = model.Title,
                    AuthorId = model.AuthorId,
                    TotalComments = model.TotalComments,
                    Tags = model.Tags,
                    NewsHotClass = model.NewsHotClass,
                    LastReadTimes = model.LastReadTimes,
                    LastDealTime = DateTime.Now,
                    IsHot = model.IsHot,
                    IsDeal = model.IsDeal,
                    IntervalMinutes = model.IntervalMinutes,
                    CurReadTimes = model.CurReadTimes,
                    CreateTime = DateTime.Now,
                    GroupId = model.GroupId,
                    FeedId = model.FeedId,
                };


                var id = Sql.InsertId<T_News_Bjh_Client>(item);

                return id;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                //Log.Error(ex.Message + ex.StackTrace);
                return -1;
            }
        }

        public static int UpdateNews_Bjh(DtoNews model)
        {
            try
            {
                var news = new T_News_Bjh()
                {
                    Id = model.Id,
                    CurReadTimes = model.CurReadTimes,
                    LastDealTime = DateTime.Now,
                    LastReadTimes = model.LastReadTimes,
                    IsHot = model.IsHot,
                    IsDeal = 1,
                    TotalComments = model.TotalComments,
                    NewsHotClass = model.NewsHotClass,
                    IntervalMinutes = model.IntervalMinutes,
                    GroupId = model.GroupId,
                };
                return Sql.Update(news, "Id={0}".Formats(model.Id));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
            return 1;
        }

        public static int UpdateNews_Bjh_ForClient(DtoNews model)
        {
            try
            {
                var news = new T_News_Bjh_Client()
                {
                    Id = model.Id,
                    CurReadTimes = model.CurReadTimes,
                    LastDealTime = DateTime.Now,
                    LastReadTimes = model.LastReadTimes,
                    IsHot = model.IsHot,
                    IsDeal = 1,
                    TotalComments = model.TotalComments,
                    NewsHotClass = model.NewsHotClass,
                    IntervalMinutes = model.IntervalMinutes,
                    GroupId = model.GroupId,
                    PubTime = model.PubTime
                };
                return Sql.Update(news, "Id={0}".Formats(model.Id));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
            return 1;
        }
        public static bool Del_News_Bjh_ForClient(int id)
        {
            try
            {
                var sql = "delete from T_News_Bjh_Client where Id={0}".Formats(id);

                var result = Sql.ExecuteSql(sql);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return false;
        }
        /// <summary>
        /// 判断某条新闻是否已存在
        /// </summary>
        /// <param name="title">新闻标题</param>
        /// <returns></returns>
        public static bool IsExistsNews_Bjh(string title)
        {
            try
            {
                var sql = "select Id from T_News_Bjh where Title=?";

                var id = Sql.ExecuteScalar(0, sql, title);

                return id > 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return false;
        }
 

        /// <summary>
        /// 判断某条新闻是否已存在
        /// </summary>
        /// <param name="authorId">新闻作者</param>
        /// <param name="title">新闻标题</param>
        /// <returns></returns>
        public static int IsExistsNews_Bjh(string authorId, string title)
        {
            try
            {
                var sql = "select Id from T_News_Bjh where AuthorId=? and Title=?";

                var id = Sql.ExecuteScalar(0, sql, authorId, title);

                return id;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return 0;
        }
        /// <summary>
        /// 判断某条新闻是否已存在
        /// </summary>
        /// <param name="authorId">新闻作者</param>
        /// <param name="title">新闻标题</param>
        /// <returns></returns>
        public static int IsExistsNews_Bjh_ForClient(string authorId, string title)
        {
            try
            {
                var sql = "select Id from T_News_Bjh_client where AuthorId=? and Title=?";

                var id = Sql.ExecuteScalar(0, sql, authorId, title);

                return id;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return 0;
        }

        public static bool DelNews_Bjh(int id)
        {
            try
            {
                var sql = string.Format("delete from T_News_Bjh where Id={0}", id);

                var result = Sql.ExecuteSql(sql);

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return false;
        }


        /// <summary>
        /// 获取某一个新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T_News_Bjh GetNews_Bjh(int id)
        {
            try
            {
                var sql = @"SELECT  * FROM T_News_Bjh WHERE Id={0}".Formats(id);

                var list = Sql.Select<T_News_Bjh>(sql);
                if (list != null && list.Count > 0)
                {
                    return list[0];
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return null;
        }


        /// <summary>
        /// 获取某一个新闻
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T_News_Bjh_Client GetNews_Bjh_ForClient(int id)
        {
            try
            {
                var sql = @"SELECT  * FROM T_News_Bjh_Client WHERE Id={0}".Formats(id);

                var list = Sql.Select<T_News_Bjh_Client>(sql);
                if (list != null && list.Count > 0)
                {
                    return list[0];
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return null;
        }
        /// <summary>
        /// 获取100条未处理的新闻记录 未处理是指没有抓取该新闻详细页，从中取出作者url
        /// </summary>
        /// <returns></returns>
        public static List<DtoNews> GetNoGatherAuthorUrlNewsList_Bjh()
        {
            try
            {
                lock (lockObj1Author)
                {
                    var sql = "select * from T_News_Bjh where (IsShow<=1) order By Id DESC limit 0,100";
                    var list = Sql.Select<DtoNews>(sql);

                    if (list != null && list.Count > 0)
                    {
                        var ids = list.Select(p => p.Id).Join(",");
                        if (ids.Length == 0)
                        {
                            ids = "0";
                        }
                        //取出后置位IsShow 正在处理状态　IsShow=2
                        sql = "update T_News_Bjh set IsShow=2 where Id in({0})".Formats(ids);
                        Sql.ExecuteSql(sql);
                    }
                    else
                    {
                        //全部执行完后统一回位 isshow=1
                        sql = "update T_News_Bjh set IsShow=1";
                        Sql.ExecuteSql(sql);
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return null;
        }


        /// <summary>
        /// 更新新闻的关键字字段
        /// </summary>
        /// <returns></returns>
        public static bool UpdateNewsTags_Bjh(int newsId, string tags)
        {
            try
            {
                var sql = "update T_News_Bjh set Tags='{0}' where Id={1}".Formats(tags, newsId);
                Sql.ExecuteSql(sql);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return true;
        }

        #endregion

        #region === author_bjh deal ===
        /// <summary>
        /// 添加一条新闻
        /// </summary>
        /// <param name="model">新闻实体</param>
        /// <returns></returns>
        public static int Insert_Author_Bjh(DtoAuthor model)
        {
            try
            {
                if (model.Author == null)
                    model.Author = "";
                if (model.AuthorId == null)
                    return -1;
                if (model.Url == null)
                    return -1;

                var item = new T_Author_Bjh()
                {
                    Author = model.Author,
                    CreateTime = DateTime.Now,
                    AuthorId = model.AuthorId,
                    IsDeal = model.IsDeal,
                    LastDealTime = DateTime.Now,
                    Url = model.Url,
                    IsShow = 0,
                    GroupId = model.GroupId
                };


                var id = Sql.InsertId<T_Author_Bjh>(item);

                return id;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
                return -1;
            }
        }

        /// <summary>
        /// 判断某个作者是否已存在
        /// </summary>
        /// <param name="authorId">作者id</param>
        /// <returns></returns>
        public static bool IsExistsAuthor_Bjh(string authorId)
        {
            try
            {
                var sql = "select Id from T_Author_Bjh where AuthorId=?";

                var id = Sql.ExecuteScalar(0, sql, authorId);

                return id > 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 更新作者的处理状态
        /// </summary>
        /// <param name="id">作者表id</param>
        /// <param name="isDeal">处理标识 0=no,1=yes</param>
        /// <returns></returns>
        public static bool UpdateAuthorIsDeal_Bjh(int id, int isDeal)
        {
            try
            {
                if (isDeal > 1)
                    isDeal = 1;
                if (isDeal < 0)
                    isDeal = 0;

                var sql = string.Format("update T_Author_Bjh set IsDeal={0} where Id={1}", isDeal, id);

                var result = Sql.ExecuteSql(sql);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 更新作者的groupid
        /// </summary>
        /// <param name="authroId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static bool UpdateAuthorGroupId_Bjh(string authroId, string groupId)
        {
            try
            {
                var sql = "";
                if (string.IsNullOrWhiteSpace(groupId))
                {
                    sql = string.Format("update T_Author_Bjh set RefreshTimes=RefreshTimes+1 where AuthorId='{0}'", authroId);
                    //return true;
                }
                else
                {
                    sql =
                        string.Format(
                            "update T_Author_Bjh set GroupId='{0}',RefreshTimes=RefreshTimes+1 where AuthorId='{1}' and (GroupId='0' or GroupId='')",
                            groupId, authroId);
                }

                var result = Sql.ExecuteSql(sql);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 更新作者的处理状态
        /// </summary>
        /// <param name="id">作者表id</param>
        /// <param name="isShow">处理标识 0=no,1=yes</param>
        /// <returns></returns>
        public static bool UpdateAuthorIsShow_Bjh(int id, int isShow)
        {
            try
            {
                if (isShow > 1)
                    isShow = 1;
                if (isShow < 0)
                    isShow = 0;

                var sql = string.Format("update T_Author_Bjh set IsShow={0} where Id={1}", isShow, id);

                var result = Sql.ExecuteSql(sql);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return false;
            }
        }
        public static bool UpdateAuthorInterval_Bjh(string authorId, int intervalMinutes)
        {
            try
            {
                var sql = string.Format("update T_Author_Bjh set IntervalMinutes={0} where AuthorId='{1}'", intervalMinutes, authorId);

                var result = Sql.ExecuteSql(sql);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return false;
            }
        }
        public static bool UpdateAuthorIsDeal_Bjh(string authorId, int isDeal)
        {
            try
            {
                var sql = string.Format("update T_Author_Bjh set IsDeal={0} where AuthorId='{1}'", isDeal, authorId);

                var result = Sql.ExecuteSql(sql);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// 获取100条未处理的作者记录
        /// </summary>
        /// <returns></returns>
        public static List<DtoAuthor> GetNoDealAuthorList_Bjh()
        {
            try
            {
                lock (lockObj1Author)
                {
                    var sql = "select * from T_Author_Bjh where (IsDeal<=1) order By Id asc limit 0,10";
                    var list = Sql.Select<DtoAuthor>(sql);

                    if (list != null && list.Count > 0)
                    {
                        var ids = list.Select(p => p.Id).Join(",");
                        if (ids.Length == 0)
                        {
                            ids = "0";
                        }
                        //取出后置位isdeal 正在处理状态　isdeal=2
                        sql =
                            "update T_Author_Bjh set IsDeal=2,RefreshTimes=RefreshTimes+1,LastDealTime='{0}' where Id in({1})"
                                .Formats(DateTime.Now, ids);
                        Sql.ExecuteSql(sql);
                    }
                    else
                    {
                        //当isdeal=0 =1的没有时，全部置位
                        sql = "update T_Author_Bjh set IsDeal=1";
                        Sql.ExecuteSql(sql);
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// 获取1条未处理的作者记录(用于特殊作者处理)
        /// </summary>
        /// <returns></returns>
        public static List<DtoAuthor> GetNoDealAuthorList_BjhForClient()
        {
            try
            {
                lock (lockObj1Author)
                {
                    var sql = "select * from T_Author_Bjh_client where (IsDeal<=1) order By Id asc limit 0,1";
                    var list = Sql.Select<DtoAuthor>(sql);

                    if (list != null && list.Count > 0)
                    {
                        var ids = list.Select(p => p.Id).Join(",");
                        if (ids.Length == 0)
                        {
                            ids = "0";
                        }
                        //取出后置位isdeal 正在处理状态　isdeal=2
                        sql =
                            "update T_Author_Bjh_client set IsDeal=2,RefreshTimes=RefreshTimes+1,LastDealTime='{0}' where Id in({1})"
                                .Formats(DateTime.Now, ids);
                        Sql.ExecuteSql(sql);
                    }
                    else
                    {
                        //当isdeal=0 =1的没有时，全部置位
                        sql = "update T_Author_Bjh_client set IsDeal=1";
                        Sql.ExecuteSql(sql);
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// 获取100条未处理的作者记录
        /// </summary>
        /// <returns></returns>
        public static List<DtoAuthor> GetNoRefreshAuthorList_Bjh()
        {
            try
            {
                lock (lockObj1Author)
                {
                    var sql = "select * from T_Author_Bjh where (IsShow<=1) order By Id asc limit 0,100";
                    var list = Sql.Select<DtoAuthor>(sql);

                    if (list != null && list.Count > 0)
                    {
                        var ids = list.Select(p => p.Id).Join(",");
                        if (ids.Length == 0)
                        {
                            ids = "0";
                        }
                        //取出后置位isdeal 正在处理状态　IsShow=2
                        sql =
                            "update T_Author_Bjh set IsShow=2,RefreshTimes=RefreshTimes+1 where Id in({0})".Formats(ids);
                        Sql.ExecuteSql(sql);
                    }
                    else
                    {
                        //全部处理完后置位IsShow=1
                        sql = "update T_Author_Bjh set IsShow=1";
                        Sql.ExecuteSql(sql);
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// 获取需要刷新的作者记录
        /// </summary>
        /// <returns></returns>
        public static List<DtoAuthor> GetWaitRefreshAuthorList_Bjh()
        {
            try
            {
                //因为此处有多个线程同时执行
                lock (lockObj1)
                {
                    var curTime = DateTime.Now;
                    //取一个月内抓取且已到刷新时间的新闻的作者列表
                    var sql =
                        "select * from T_Author_Bjh where (IsDeal=0 or IsDeal=1) and AuthorId in(SELECT DISTINCT AuthorId from t_news WHERE  DATE_ADD(CreateTime,INTERVAL 30 DAY)>'{0}' and DATE_ADD(LastDealTime,INTERVAL IntervalMinutes MINUTE)>'{0}' and (IsDeal<=1)) order By Id DESC limit 0,1000"
                            .Formats(curTime);


                    var list = Sql.Select<DtoAuthor>(sql);

                    //取过的新闻置位isdeal=1
                    var sql2 =
                        "update T_News_Bjh set IsDeal=2,RefreshTimes=RefreshTimes+1 where DATE_ADD(CreateTime,INTERVAL 30 DAY)>'{0}' and DATE_ADD(LastDealTime,INTERVAL IntervalMinutes MINUTE)>'{0}' and (IsDeal<=1)"
                            .Formats(curTime);
                    Sql.ExecuteSql(sql2);

                    if (list != null && list.Count > 0)
                    {
                        var ids = list.Select(p => p.Id).Join(",");
                        if (ids.Length == 0)
                        {
                            ids = "0";
                        }
                        //取出后置位isdeal 正在处理状态　isdeal=2
                        sql = "update T_Author_Bjh set IsDeal=2,RefreshTimes=RefreshTimes+1 where Id in({0})".Formats(ids);
                        Sql.ExecuteSql(sql);

                    }
                    else
                    {
                        //全部执行完后统一回位 isdeal=1
                        sql = "update T_Author_Bjh set IsDeal=1";
                        Sql.ExecuteSql(sql);

                        //全部执行完后统一回位 isdeal=1
                        sql = "update T_News_Bjh set IsDeal=1";
                        Sql.ExecuteSql(sql);
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return null;
        }


        #endregion

        #region === keywords ===
        /// <summary>
        /// 获取100条未处理的作者记录
        /// </summary>
        /// <returns></returns>
        public static List<DtoKeyword> GetNoDealKeyword()
        {
            try
            {
                var sql = "select Id,Keyword from t_keywords where IsDeal=0 order By Id asc limit 0,10";
                var list = Sql.Select<DtoKeyword>(sql);

                if (list != null && list.Count > 0)
                {
                    var ids = list.Select(p => p.Id).Join(",");
                    if (ids.Length == 0)
                    {
                        ids = "0";
                    }
                    //取出后置位isdeal 正在处理状态　isdeal=2
                    sql = "update t_keywords set IsDeal=1 where Id in({0})".Formats(ids);
                    Sql.ExecuteSql(sql);
                }
                else
                {
                    //当isdeal=0 =1的没有时，全部置位
                    sql = "update t_keywords set IsDeal=0";
                    Sql.ExecuteSql(sql);
                }

                return list;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return null;
        }
        #endregion

        #region === user deal ===
        /// <summary>
        /// 添加一条用户记录
        /// </summary>
        /// <param name="model">用户实体</param>
        /// <returns></returns>
        public static int Insert(DtoUser model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Descriptions))
                    model.Descriptions = "";
                if (model.MediaId == null)
                    model.MediaId = "";
                if (model.Name == null)
                    model.Name = "";
                if (model.OpenUrl == null)
                    model.OpenUrl = "";
                if (model.AvatarUrl == null)
                    model.AvatarUrl = "";


                var item = new T_User()
                {
                    UserId = model.UserId,
                    AvatarUrl = model.AvatarUrl,
                    Descriptions = model.Descriptions,
                    FansCount = model.FansCount,
                    FollowCount = model.FollowCount,
                    MediaId = model.MediaId,
                    Name = model.Name,
                    OpenUrl = model.OpenUrl,
                    CreateTime = DateTime.Now,
                    IsDeal = model.IsDeal,
                    LastDealTime = DateTime.Now,
                    IsShow = 0,

                };

                var id = Sql.InsertId<T_User>(item);

                return id;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
                return -1;
            }
        }

        /// <summary>
        /// 判断某个用户是否已存在
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public static bool IsExistsUser(string userId)
        {
            try
            {
                var sql = "select Id from T_User where UserId=?";

                var id = Sql.ExecuteScalar(0, sql, userId);

                return id > 0;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return false;
        }

        /// <summary>
        /// 更新用户的处理状态
        /// </summary>
        /// <param name="id">作者表id</param>
        /// <param name="isDeal">处理标识 0=no,1=yes</param>
        /// <returns></returns>
        public static bool UpdateUserIsDeal(int id, int isDeal)
        {
            try
            {
                if (isDeal > 1)
                    isDeal = 1;
                if (isDeal < 0)
                    isDeal = 0;

                var sql = string.Format("update T_User set IsDeal={0} where Id={1}", isDeal, id);

                var result = Sql.ExecuteSql(sql);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return false;
            }
        }


        /// <summary>
        /// 更新用户的处理状态
        /// </summary>
        /// <param name="id">作者表id</param>
        /// <param name="isShow">处理标识 0=no,1=yes</param>
        /// <returns></returns>
        public static bool UpdateUserIsShow(int id, int isShow)
        {
            try
            {
                if (isShow > 1)
                    isShow = 1;
                if (isShow < 0)
                    isShow = 0;

                var sql = string.Format("update T_User set IsShow={0} where Id={1}", isShow, id);

                var result = Sql.ExecuteSql(sql);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 更新用户的处理状态
        /// </summary>
        /// <param name="userinfo">用户信息实体</param>
        /// <returns></returns>
        public static bool UpdateUserInfo(DtoUser userinfo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userinfo.Descriptions))
                    userinfo.Descriptions = "";
                if (string.IsNullOrWhiteSpace(userinfo.Name))
                    userinfo.Name = "";
                var sql = string.Format("update T_User set Descriptions='{0}',FansCount={1} where UserId={2}", userinfo.Descriptions, userinfo.FansCount, userinfo.UserId);

                var result = Sql.ExecuteSql(sql);
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// 获取N条未抓取相关推荐的用户记录
        /// </summary>
        /// <returns></returns>
        public static List<DtoUser> GetNoDealUserList()
        {
            try
            {
                lock (lockObj1Author)
                {
                    var sql = "select * from T_User where (IsDeal<=1) order By Id DESC limit 0,100";
                    var list = Sql.Select<DtoUser>(sql);

                    if (list != null && list.Count > 0)
                    {
                        var ids = list.Select(p => p.Id).Join(",");
                        if (ids.Length == 0)
                        {
                            ids = "0";
                        }
                        //取出后置位isdeal 正在处理状态　isdeal=2
                        sql = "update T_User set IsDeal=2 where Id in({0})".Formats(ids);
                        Sql.ExecuteSql(sql);
                    }
                    else
                    {
                        //当isdeal=0 =1的没有时，全部置位
                        sql = "update T_User set IsDeal=1";
                        Sql.ExecuteSql(sql);
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// 获取N条未抓取用户资料的用户记录
        /// </summary>
        /// <returns></returns>
        public static List<DtoUser> GetNoShowUserList()
        {
            try
            {
                lock (lockObj1Author)
                {
                    var sql = "select * from T_User where (IsShow<=1) order By Id DESC limit 0,100";
                    var list = Sql.Select<DtoUser>(sql);

                    if (list != null && list.Count > 0)
                    {
                        var ids = list.Select(p => p.Id).Join(",");
                        if (ids.Length == 0)
                        {
                            ids = "0";
                        }
                        //取出后置位IsShow 正在处理状态　IsShow=2
                        sql = "update T_User set IsShow=2 where Id in({0})".Formats(ids);
                        Sql.ExecuteSql(sql);
                    }
                    else
                    {
                        //当isdeal=0 =1的没有时，全部置位
                        sql = "update T_User set IsShow=1";
                        Sql.ExecuteSql(sql);
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return null;
        }
        #endregion

        #region ==== baijiahao deal  ====

        /// <summary>
        /// 根据账号密码获取客户端用户id
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static int GetClientUserId(string userName, string password)
        {
            try
            {
                userName = userName.SqlFilter();
                password = password.SqlFilter();
                var sql = "select Id from t_clientuser where UserName='{0}' and Password='{1}'".Formats(userName, password);
                return Sql.ExecuteScalar(0, sql);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return 0;
        }

        /// <summary>
        /// 随机取N条关键字拼成字符串,以逗号分隔
        /// </summary>
        /// <returns></returns>
        public static string GetRndKeywords()
        {
            try
            {
                var sql = "select * from t_keywords ORDER BY RAND()  LIMIT 3";
                return Sql.Select<DtoBaiduKeyword>(sql).Select(p => p.Keyword).Join(",");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return "";
        }
        /// <summary>
        /// 随机取N条关键字拼成字符串,以逗号分隔
        /// </summary>
        /// <returns></returns>
        public static string GetRndKeywordsByArea(Comm.Global.DTO.News.DtoIpArea area)
        {
            try
            {
                //1、北京的只取北京；
                //2、上海的只取上海；
                //3、广州的只取广州；
                //4、深圳的只取深圳；
                //5、其他的，设置三类的并集，分别是本省本市的 + 本省且城市为00的 + 省份和城市都为00的
                var sql = "select Keyword,Keyword2,Keyword3 from t_keywords_new ORDER BY RAND() limit 1";
                Log.Info(area.Province + area.City);
                if (area.City == "北京" || area.City == "上海" || area.City == "广州" || area.City == "深圳")
                {
                    sql = "select Keyword,Keyword2,Keyword3 from t_keywords_new WHERE Province='{0}' and City='{1}' ORDER BY RAND() limit 1;".Formats(area.Province, area.City);
                }
                else
                {
                    sql = "select  Keyword,Keyword2,Keyword3 from (select Keyword,Keyword2,Keyword3 from t_keywords_new WHERE Province='{0}' and City='{1}' union select Keyword,Keyword2,Keyword3 from t_keywords_new WHERE Province='{0}' and City='00' union select Keyword,Keyword2,Keyword3 from t_keywords_new WHERE Province='00' and City='00')a ORDER BY RAND() LIMIT 1;";
                }
                
                //var sql = "select * from t_keywords ORDER BY RAND()  LIMIT 3";
                var list= Sql.Select<DtoBaiduKeywordThree>(sql);
                if (list != null && list.Count > 0)
                {
                    return list[0].Keyword + "," + list[0].Keyword2 + "," + list[0].Keyword3;
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return "";
        }
        /// <summary>
        /// 随机取一个顶贴内容
        /// </summary>
        /// <returns></returns>
        public static string GetRndUpPostContent()
        {
            try
            {
                var sql = "select * from t_uppostcontent ORDER BY RAND()  LIMIT 1";
                var list = Sql.Select<DtoUpPostContent>(sql);
                if (list != null && list.Count > 0)
                {
                    return list[0].Contents;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }

            return "";
        }
        /// <summary>
        /// 随机取一个顶贴内容
        /// </summary>
        /// <returns></returns>
        public static DtoBaiduUser GetRndBaiduUser()
        {
            try
            {
                var sql = "select * from t_baiduuser ORDER BY RAND()  LIMIT 1";
                var list = Sql.Select<DtoBaiduUser>(sql);
                if (list != null && list.Count > 0)
                {
                    return list[0];
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
            return null;
        }
        /// <summary>
        /// 随机获取一个可用的百家号链接
        /// </summary>
        /// <returns></returns>
        public static DtoNewsSimple GetRndBjhUrl() {
            try {
                #region === 取出当前操作的作者id isshow=1 表示当前正在操作===
                var author = GetAuthorId(0);
                if (author==null || string.IsNullOrWhiteSpace(author.AuthorId))
                {
                    return null;
                }

                var model = GetRndBjhUrlByAuthorId(author.AuthorId);
                if (model == null)
                {
                    var iCount = 0;
                    while (model == null)
                    {
                        author = GetAuthorId(author.Id);
                        if (author == null || string.IsNullOrWhiteSpace(author.AuthorId))
                        {
                            return null;
                        }
                        model = GetRndBjhUrlByAuthorId(author.AuthorId);
                        if (model != null)
                        {
                            break;
                        }
                        iCount++;
                        if (iCount > 30)
                            break;
                    }
                    if (model != null)
                    {
                        return model;
                    }
                    return null;
                }
                else
                {
                    return model;
                }
                #endregion 
            }
            catch { }
            return null;
        }

        public static DtoAuthorClient GetAuthorId(int id)
        {
            try
            {
                #region === 取出当前操作的作者id isshow=1 表示当前正在操作===
                var model = new DtoAuthorClient();
                var sql = "";
                if (id==0)
                {
                    //sql = "select AuthorId,Id from t_author_bjh_client where IsShow=1 order by id desc limit 0,1;";
                    sql = "select AuthorId,Id from t_author_bjh_client where Id<(select Id from t_author_bjh_client where IsShow=1 order by Id desc LIMIT 0,1) order by id desc limit 0,1;";
                }
                else
                {
                    sql = "update t_author_bjh_client set IsShow=0;";
                    Sql.ExecuteSql(sql);
                    sql = "select * from t_author_bjh_client where Id<{0} order by id desc limit 0,1;".Formats(id);
                }
                
                var list = Sql.Select<DtoAuthor>(sql);
                if (list != null && list.Count > 0)
                {
                    model.AuthorId = list[0].AuthorId;
                    model.Id = list[0].Id;
                    sql = "update t_author_bjh_client set IsShow=0;update t_author_bjh_client set IsShow=1 where Id='" + model.Id + "'";
                    Sql.ExecuteSql(sql);
                }
                else
                {
                    sql = "select * from t_author_bjh_client order by id desc limit 0,1;";
                    list = Sql.Select<DtoAuthor>(sql);
                    if (list != null && list.Count > 0)
                    {
                        model.AuthorId = list[0].AuthorId;
                        model.Id = list[0].Id;
                        sql = "update t_author_bjh_client set IsShow=0;update t_author_bjh_client set IsShow=1 where Id='" + model.Id + "'";
                        Sql.ExecuteSql(sql);
                    }
                }
                return model;
                #endregion
            }
            catch { }
            return null;
        }
        /// <summary>
        /// 随机获取一个可用的百家号链接
        /// </summary>
        /// <returns></returns>
        public static DtoNewsSimple GetRndBjhUrlByAuthorId(string authorId)
        {
            try
            {
                var sql = "select Id,FeedId,AuthorId from t_news_bjh_client where AuthorId='{0}' and OpTimes<{1} and FeedId<>'' and DATEDIFF(PubTime,'{2}')=0 and CurReadTimes>=1000 and Floor(CurReadTimes/1000)*5>OpTimes ORDER BY RAND()  LIMIT 1".Formats(authorId, Global.NewsTaskNums, DateTime.Now);
                var list2 = Sql.Select<DtoNewsSimple>(sql);
                if (list2 != null && list2.Count > 0)
                {
                    sql = "update t_news_bjh_client set OpTimes=OpTimes+1 where Id={0}".Formats(list2[0].Id);
                    Sql.ExecuteSql(sql);
                    return list2[0];
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
            return null;
        }

        /// <summary>
        /// 添加一条任务
        /// </summary>
        /// <param name="model">新闻实体</param>
        /// <returns></returns>
        public static int Insert_Task(DtoTask model)
        {
            try
            {
                if (model.Keywords == null)
                    model.Keywords = "";
                if (model.Memo == null)
                    model.Memo = "";
                if (model.OpString == null)
                    model.OpString = "";
                if (model.UpPostContnet == null)
                    model.UpPostContnet = "";
                if (model.Url == null)
                    model.Url = "";
                if (model.Ip == null)
                    model.Ip = "";
                if (model.FeedId == null)
                    model.FeedId = "";
                var item = new T_Task()
                {
                    BaiduUserId = model.BaiduUserId,
                    ClientUserId = model.ClientUserId,
                    Ip = model.Ip,
                    Keywords = model.Keywords,
                    Memo = model.Memo,
                    OpString = model.OpString,
                    UpPostContnet = model.UpPostContnet,
                    TaskId = model.TaskId,
                    Status = 0,
                    Url = model.Url,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    FeedId = model.FeedId,
                };


                var id = Sql.InsertId<T_Task>(item,"TaskId");

                return id;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                //Log.Error(ex.Message + ex.StackTrace);
                return -1;
            }
        }

        /// <summary>
        /// 添加一条任务
        /// </summary>
        /// <param name="model">新闻实体</param>
        /// <returns></returns>
        public static int Insert_Task2(DtoTask model)
        {
            try
            {
                if (model.Keywords == null)
                    model.Keywords = "";
                if (model.Memo == null)
                    model.Memo = "";
                if (model.OpString == null)
                    model.OpString = "";
                if (model.UpPostContnet == null)
                    model.UpPostContnet = "";
                if (model.Url == null)
                    model.Url = "";
                if (model.Ip == null)
                    model.Ip = "";
                if (model.FeedId == null)
                    model.FeedId = "";
                var item = new T_Task_New()
                {
                    BaiduUserId = model.BaiduUserId,
                    ClientUserId = model.ClientUserId,
                    Ip = model.Ip,
                    Keywords = model.Keywords,
                    Memo = model.Memo,
                    OpString = model.OpString,
                    UpPostContnet = model.UpPostContnet,
                    TaskId = model.TaskId,
                    Status = 0,
                    Url = model.Url,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    FeedId = model.FeedId,
                };


                var id = Sql.InsertId<T_Task_New>(item, "TaskId");

                return id;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                //Log.Error(ex.Message + ex.StackTrace);
                return -1;
            }
        }

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <param name="status">状态 1=成功 0=失败</param>
        /// <returns></returns>
        public static bool UpdateTaskStatus(string taskId, int status)
        {
            try
            {
                taskId = taskId.SqlFilter();
                var sql = "update t_task set Status={0},UpdateTime='{1}' where TaskId='{2}'".Formats(status,DateTime.Now, taskId);
                Sql.ExecuteSql(sql);
                if (status == 0)
                {
                    //失败了则把操作次数减1
                    var feedId = "";
                    sql = "select FeedId from t_task where TaskId='{0}' limit 0,1;".Formats(taskId);
                    var list = Sql.Select<DtoTask>(sql);
                    if(list!=null && list.Count>0)
                    {
                        feedId = list[0].FeedId;
                    }
                    if (!string.IsNullOrWhiteSpace(feedId))
                    {
                        sql = "update t_news_bjh_client set OpTimes=OpTimes-1 where FeedId='{0}'".Formats(feedId);
                        Sql.ExecuteSql(sql);
                    }
                    
                }
                return true;

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
            return false;
        }

        /// <summary>
        /// 更新任务状态2
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <param name="status">状态 1=成功 0=失败</param>
        /// <returns></returns>
        public static bool UpdateTaskStatus2(string taskId, int status)
        {
            try
            {
                taskId = taskId.SqlFilter();
                var sql = "update t_task_new set Status={0},UpdateTime='{1}' where TaskId='{2}'".Formats(status, DateTime.Now, taskId);
                Sql.ExecuteSql(sql);
                if (status == 0)
                {
                    //失败了则把操作次数减1
                    var feedId = "";
                    sql = "select FeedId from t_task_new where TaskId='{0}' limit 0,1;".Formats(taskId);
                    var list = Sql.Select<DtoTask>(sql);
                    if (list != null && list.Count > 0)
                    {
                        feedId = list[0].FeedId;
                    }
                    if (!string.IsNullOrWhiteSpace(feedId))
                    {
                        sql = "update t_news_bjh_client set OpTimes=OpTimes-1 where FeedId='{0}'".Formats(feedId);
                        Sql.ExecuteSql(sql);
                    }

                }
                return true;

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
            return false;
        }
        #endregion
    }
}
