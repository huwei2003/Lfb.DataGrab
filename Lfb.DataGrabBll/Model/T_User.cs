using System;
using System.Collections.Generic;
using Comm.Global.Db;

namespace Lfb.DataGrabBll.Model
{
    public  class T_User : Table<T_User>
    {
        internal string _name;
        internal long _userId;
        internal DateTime _createTime;
        internal int _id;
        internal int _isDeal;
        internal DateTime _lastDealTime;
        internal string _openUrl;
        internal string _avatarUrl;
        internal int _IsShow;
        internal string _mediaId;
        internal string _descriptions;
        internal int _fansCount;
        internal int _followCount;

        static T_User()
        {
            Summary = new Dictionary<string, string>
		{
			{"Name", @"作者"},
			{"UserId", @"作者Id"},
			{"CreateTime", @"添加时间"},
			{"Id", @"作者表id"},
			{"IsDeal", @"处理标识 0=未处理,1处理过"},
			{"LastDealTime", @"最后处理时间"},
            {"OpenUrl", @"作者的url"},
            {"AvatarUrl", @"作者的AvatarUrl"},
            {"IsShow","_IsShow"},
            {"MediaId","_mediaId"},
            {"Descriptions","_descriptions"},
            {"FansCount","_fansCount"},
            {"FollowCount","_followCount"},	
		};
        }

        /// <summary>
        /// 作者
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetColumn("Name", _name, value);
                _name = value;
            }
        }

        /// <summary>
        /// 作者id
        /// </summary>
        public long UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                SetColumn("UserId", _userId, value);
                _userId = value;
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
        /// 是否处理
        /// </summary>
        public int IsShow
        {
            get
            {
                return _IsShow;
            }
            set
            {
                SetColumn("IsShow", _IsShow, value);
                _IsShow = value;
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
        /// 作者首页地址
        /// </summary>
        public string OpenUrl
        {
            get
            {
                return _openUrl;
            }
            set
            {
                SetColumn("OpenUrl", _openUrl, value);
                _openUrl = value;
            }
        }

        /// <summary>
        /// 作者首页地址
        /// </summary>
        public string AvatarUrl
        {
            get
            {
                return _avatarUrl;
            }
            set
            {
                SetColumn("AvatarUrl", _avatarUrl, value);
                _avatarUrl = value;
            }
        }

        /// <summary>
        /// MediaId
        /// </summary>
        public string MediaId
        {
            get
            {
                return _mediaId;
            }
            set
            {
                SetColumn("MediaId", _mediaId, value);
                _mediaId = value;
            }
        }

        /// <summary>
        /// Description
        /// </summary>
        public string Descriptions
        {
            get
            {
                return _descriptions;
            }
            set
            {
                SetColumn("Descriptions", _descriptions, value);
                _descriptions = value;
            }
        }

        /// <summary>
        /// FansCount
        /// </summary>
        public int FansCount
        {
            get
            {
                return _fansCount;
            }
            set
            {
                SetColumn("FansCount", _fansCount, value);
                _fansCount = value;
            }
        }
        /// <summary>
        /// FollowCount
        /// </summary>
        public int FollowCount
        {
            get
            {
                return _followCount;
            }
            set
            {
                SetColumn("FollowCount", _followCount, value);
                _followCount = value;
            }
        }

    }
}
