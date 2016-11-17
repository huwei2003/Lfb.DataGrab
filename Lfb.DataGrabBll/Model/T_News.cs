using System;
using System.Collections.Generic;
using Comm.Global.Db;

namespace Lfb.DataGrabBll.Model
{
    public class T_News : Table<T_News>
    {

        internal string _contents;
        internal DateTime _createTime;
        internal string _fromSiteName;
        internal string _fromUrl;
        internal int _id;
        internal string _logoOriginalUrl;
        internal string _logoUrl;
        internal DateTime _pubTime;
        internal string _title;

        internal string _author;
        internal int _isShow;
        internal int _TotalComments;
        internal int _newsTypeId;
        internal int _isDeal;
        internal int _IntervalMinutes;
        internal DateTime _LastDealTime;
        internal int _isHot;
        internal int _NewsHotClass;
        internal int _CurReadTimes;
        internal int _LastReadTimes;
        internal string _Tags;
        internal string _AuthorId;
        internal int _RefreshTimes;
        internal string _GroupId;

        static T_News()
        {
            Summary = new Dictionary<string, string>
		{
			{"Author", @"作者"},
			{"Contents", @"内容"},
			{"CreateTime", @"添加时间"},
			{"FromSiteName", @"来源站点"},
			{"FromUrl", @"来源url"},
			{"Id", @"新闻资讯表id"},
			{"IsDeal", @"刷新处理标识 0=未处理,1处理过"},
			{"IsHot", @"是否爆文 0 no 1 yes"},
			{"IsShow", @"是否显示 0=no,1=yes"},
			{"LogoOriginalUrl", @"logo图原地址"},
			{"LogoUrl", @"logo图地址"},
			{"NewsTypeId", @"新闻分类id 1"},
			{"PubTime", @"发布时间"},
			{"Title", @"标题"},
			{"TotalComment", @"评论数"},
            {"AuthorId","作者id"},
            {"Tags","tags"},
            {"CurReadTimes","CurReadTimes"},
            {"NewsHotClass","NewsHotClass"},
            {"LastDealTime","LastDealTime"},
            {"IntervalMinutes","IntervalMinutes"},
            {"TotalComments","TotalComments"},
            {"RefreshTimes","RefreshTimes"},
            {"GroupId","GroupId"}
		};
        }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author
        {
            get
            {
                return _author;
            }
            set
            {
                SetColumn("Author", _author, value);
                _author = value;
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Contents
        {
            get
            {
                return _contents;
            }
            set
            {
                SetColumn("Contents", _contents, value);
                _contents = value;
            }
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime
        {
            get
            {
                return _createTime;
            }
            set
            {
                SetColumn("CreateTime", _createTime, value);
                _createTime = value;
            }
        }

        /// <summary>
        /// 来源站点
        /// </summary>
        public string FromSiteName
        {
            get
            {
                return _fromSiteName;
            }
            set
            {
                SetColumn("FromSiteName", _fromSiteName, value);
                _fromSiteName = value;
            }
        }

        /// <summary>
        /// 来源url
        /// </summary>
        public string FromUrl
        {
            get
            {
                return _fromUrl;
            }
            set
            {
                SetColumn("FromUrl", _fromUrl, value);
                _fromUrl = value;
            }
        }

        /// <summary>
        /// 新闻资讯表id
        /// </summary>
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// 刷新处理标识 0=未处理,1处理过
        /// </summary>
        public int IsDeal
        {
            get
            {
                return _isDeal;
            }
            set
            {
                SetColumn("IsDeal", _isDeal, value);
                _isDeal = value;
            }
        }

        /// <summary>
        /// 是否首页轮播图 0 no 1 yes
        /// </summary>
        public int IsHot
        {
            get
            {
                return _isHot;
            }
            set
            {
                SetColumn("IsHot", _isHot, value);
                _isHot = value;
            }
        }


        /// <summary>
        /// 是否显示 0=no,1=yes
        /// </summary>
        public int IsShow
        {
            get
            {
                return _isShow;
            }
            set
            {
                SetColumn("IsShow", _isShow, value);
                _isShow = value;
            }
        }

        /// <summary>
        /// logo图原地址
        /// </summary>
        public string LogoOriginalUrl
        {
            get
            {
                return _logoOriginalUrl;
            }
            set
            {
                SetColumn("LogoOriginalUrl", _logoOriginalUrl, value);
                _logoOriginalUrl = value;
            }
        }

        /// <summary>
        /// logo图地址
        /// </summary>
        public string LogoUrl
        {
            get
            {
                return _logoUrl;
            }
            set
            {
                SetColumn("LogoUrl", _logoUrl, value);
                _logoUrl = value;
            }
        }

        /// <summary>
        /// 新闻分类id 100=新闻,200=素食,300=活动
        /// </summary>
        public int NewsTypeId
        {
            get
            {
                return _newsTypeId;
            }
            set
            {
                SetColumn("NewsTypeId", _newsTypeId, value);
                _newsTypeId = value;
            }
        }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PubTime
        {
            get
            {
                return _pubTime;
            }
            set
            {
                SetColumn("PubTime", _pubTime, value);
                _pubTime = value;
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                SetColumn("Title", _title, value);
                _title = value;
            }
        }

        /// <summary>
        /// 作者id
        /// </summary>
        public string AuthorId
        {
            get
            {
                return _AuthorId;
            }
            set
            {
                SetColumn("AuthorId", _AuthorId, value);
                _AuthorId = value;
            }
        }

        /// <summary>
        /// tags
        /// </summary>
        public string Tags
        {
            get
            {
                return _Tags;
            }
            set
            {
                SetColumn("Tags", _Tags, value);
                _Tags = value;
            }
        }

        /// <summary>
        /// 最近一次阅读数
        /// </summary>
        public int LastReadTimes
        {
            get
            {
                return _LastReadTimes;
            }
            set
            {
                SetColumn("LastReadTimes", _LastReadTimes, value);
                _LastReadTimes = value;
            }
        }

        /// <summary>
        /// 当前阅读数
        /// </summary>
        public int CurReadTimes
        {
            get
            {
                return _CurReadTimes;
            }
            set
            {
                SetColumn("CurReadTimes", _CurReadTimes, value);
                _CurReadTimes = value;
            }
        }

        /// <summary>
        /// 新闻热度级别
        /// </summary>
        public int NewsHotClass
        {
            get
            {
                return _NewsHotClass;
            }
            set
            {
                SetColumn("NewsHotClass", _NewsHotClass, value);
                _NewsHotClass = value;
            }
        }

        /// <summary>
        /// 最后刷新时间
        /// </summary>
        public DateTime LastDealTime
        {
            get
            {
                return _LastDealTime;
            }
            set
            {
                SetColumn("LastDealTime", _LastDealTime, value);
                _LastDealTime = value;
            }
        }

        /// <summary>
        /// 刷新间隔时长
        /// </summary>
        public int IntervalMinutes
        {
            get
            {
                return _IntervalMinutes;
            }
            set
            {
                SetColumn("IntervalMinutes", _IntervalMinutes, value);
                _IntervalMinutes = value;
            }
        }

        /// <summary>
        /// 评论数
        /// </summary>
        public int TotalComments
        {
            get
            {
                return _TotalComments;
            }
            set
            {
                SetColumn("TotalComments", _TotalComments, value);
                _TotalComments = value;
            }
        }

        /// <summary>
        /// 刷新次数
        /// </summary>
        public int RefreshTimes
        {
            get
            {
                return _RefreshTimes;
            }
            set
            {
                SetColumn("RefreshTimes", _RefreshTimes, value);
                _RefreshTimes = value;
            }
        }
        /// <summary>
        /// groupid
        /// </summary>
        public string GroupId
        {
            get
            {
                return _GroupId;
            }
            set
            {
                SetColumn("GroupId", _GroupId, value);
                _GroupId = value;
            }
        }
    }
}
