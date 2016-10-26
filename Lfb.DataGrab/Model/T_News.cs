using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comm.Global.Db;

namespace Lfb.DataGrab.Model
{
    public  class T_News : Table<T_News>
    {
        internal string _author;
        internal string _contents;
        internal DateTime? _createTime;
        internal string _fromSiteName;
        internal string _fromUrl;
        internal int? _id;
        internal int? _imgFlag;
        internal int? _isIndexPic;
        internal int? _isRecommand;
        internal int? _isShow;
        internal string _logoOriginalUrl;
        internal string _logoUrl;
        internal int? _newsTypeId;
        internal string _pubTime;
        internal string _title;
        internal int? _totalClick;
        internal int? _totalCollection;
        internal int? _totalComment;

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
			{"ImgFlag", @"图片再处理标识 0=未处理,1处理过"},
			{"IsIndexPic", @"是否首页轮播图 0 no 1 yes"},
			{"IsRecommand", @"是否推荐 0 no 1 yes"},
			{"IsShow", @"是否显示 0=no,1=yes"},
			{"LogoOriginalUrl", @"logo图原地址"},
			{"LogoUrl", @"logo图地址"},
			{"NewsTypeId", @"新闻分类id 100=新闻,200=素食,300=活动"},
			{"PubTime", @"发布时间"},
			{"Title", @"标题"},
			{"TotalClick", @"点击数"},
			{"TotalCollection", @"收藏数"},
			{"TotalComment", @"评论数"}
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
                return _createTime ?? default(DateTime);
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
                return _id ?? default(int);
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// 图片再处理标识 0=未处理,1处理过
        /// </summary>
        public int ImgFlag
        {
            get
            {
                return _imgFlag ?? default(int);
            }
            set
            {
                SetColumn("ImgFlag", _imgFlag, value);
                _imgFlag = value;
            }
        }

        /// <summary>
        /// 是否首页轮播图 0 no 1 yes
        /// </summary>
        public int IsIndexPic
        {
            get
            {
                return _isIndexPic ?? default(int);
            }
            set
            {
                SetColumn("IsIndexPic", _isIndexPic, value);
                _isIndexPic = value;
            }
        }

        /// <summary>
        /// 是否推荐 0 no 1 yes
        /// </summary>
        public int IsRecommand
        {
            get
            {
                return _isRecommand ?? default(int);
            }
            set
            {
                SetColumn("IsRecommand", _isRecommand, value);
                _isRecommand = value;
            }
        }

        /// <summary>
        /// 是否显示 0=no,1=yes
        /// </summary>
        public int IsShow
        {
            get
            {
                return _isShow ?? default(int);
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
                return _newsTypeId ?? default(int);
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
        public string PubTime
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
        /// 点击数
        /// </summary>
        public int TotalClick
        {
            get
            {
                return _totalClick ?? default(int);
            }
            set
            {
                SetColumn("TotalClick", _totalClick, value);
                _totalClick = value;
            }
        }

        /// <summary>
        /// 收藏数
        /// </summary>
        public int TotalCollection
        {
            get
            {
                return _totalCollection ?? default(int);
            }
            set
            {
                SetColumn("TotalCollection", _totalCollection, value);
                _totalCollection = value;
            }
        }

        /// <summary>
        /// 评论数
        /// </summary>
        public int TotalComment
        {
            get
            {
                return _totalComment ?? default(int);
            }
            set
            {
                SetColumn("TotalComment", _totalComment, value);
                _totalComment = value;
            }
        }

    }
}
