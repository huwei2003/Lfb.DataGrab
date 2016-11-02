using System;
using System.Collections.Generic;
using Comm.Global.Db;

namespace Lfb.DataGrabBll.Model
{
    public  class T_Author : Table<T_Author>
    {
        internal string _author;
        internal string _authorId;
        internal DateTime _createTime;
        internal int _id;
        internal int _isDeal;
        internal DateTime _lastDealTime;
        internal string _url;
        internal int _IntervalMinutes;


        static T_Author()
        {
            Summary = new Dictionary<string, string>
		{
			{"Author", @"作者"},
			{"AuthorId", @"内容"},
			{"CreateTime", @"添加时间"},
			{"Id", @"作者表id"},
			{"IsDeal", @"处理标识 0=未处理,1处理过"},
			{"LastDealTime", @"最后处理时间"},
            {"Url", @"作者的url"},
            {"IntervalMinutes",@"刷新时间间隔"},
			
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
        /// 作者id
        /// </summary>
        public string AuthorId
        {
            get
            {
                return _authorId;
            }
            set
            {
                SetColumn("AuthorId", _authorId, value);
                _authorId = value;
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
        /// 是否处理
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
        /// 来源url
        /// </summary>
        public DateTime LastDealTime
        {
            get
            {
                return _lastDealTime;
            }
            set
            {
                SetColumn("LastDealTime", _lastDealTime, value);
                _lastDealTime = value;
            }
        }

        /// <summary>
        /// 作者表id
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
        /// 作者首页地址
        /// </summary>
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                SetColumn("Url", _url, value);
                _url = value;
            }
        }

        /// <summary>
        /// 刷新时间间隔
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
    }
}
