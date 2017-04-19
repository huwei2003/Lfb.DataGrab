using System;
using System.Collections.Generic;
using Comm.Global.Db;

namespace Lfb.DataGrabBll.Model
{
    public class T_Task : Table<T_Task>
    {
        internal string _TaskId;
        internal string _Ip;
        internal int _ClientUserId;
        internal string _Url;
        internal string _Keywords;
        internal string _OpString;
        internal string _UpPostContnet;
        internal string _Memo;
        internal int _BaiduUserId;
        internal int _Status;
        internal DateTime _CreateTime;
        internal DateTime _UpdateTime;
        internal string _FeedId;

        static T_Task()
        {
            
        }

        /// <summary>
        /// 任务id
        /// </summary>
        public string TaskId
        {
            get
            {
                return _TaskId;
            }
            set
            {
                SetColumn("TaskId", _TaskId, value);
                _TaskId = value;
            }
        }

        /// <summary>
        /// ip
        /// </summary>
        public string Ip
        {
            get
            {
                return _Ip;
            }
            set
            {
                SetColumn("Ip", _Ip, value);
                _Ip = value;
            }
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                SetColumn("CreateTime", _CreateTime, value);
                _CreateTime = value;
            }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get
            {
                return _UpdateTime;
            }
            set
            {
                SetColumn("UpdateTime", _UpdateTime, value);
                _UpdateTime = value;
            }
        }

        /// <summary>
        /// 客户端userid
        /// </summary>
        public int ClientUserId
        {
            get
            {
                return _ClientUserId;
            }
            set
            {
                SetColumn("ClientUserId", _ClientUserId, value);
                _ClientUserId = value;
            }
        }

        /// <summary>
        /// 百家号链接url
        /// </summary>
        public string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                SetColumn("Url", _Url, value);
                _Url = value;
            }
        }
        /// <summary>
        /// 百家号账号id
        /// </summary>
        public int BaiduUserId
        {
            get
            {
                return _BaiduUserId;
            }
            set
            {
                SetColumn("BaiduUserId", _BaiduUserId, value);
                _BaiduUserId = value;
            }
        }

        /// <summary>
        /// 状态 0 no 1 success
        /// </summary>
        public int Status
        {
            get
            {
                return _Status;
            }
            set
            {
                SetColumn("Status", _Status, value);
                _Status = value;
            }
        }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keywords
        {
            get
            {
                return _Keywords;
            }
            set
            {
                SetColumn("Keywords", _Keywords, value);
                _Keywords = value;
            }
        }

        /// <summary>
        /// 操作串
        /// </summary>
        public string OpString
        {
            get
            {
                return _OpString;
            }
            set
            {
                SetColumn("OpString", _OpString, value);
                _OpString = value;
            }
        }
        /// <summary>
        /// 顶贴内容
        /// </summary>
        public string UpPostContnet
        {
            get
            {
                return _UpPostContnet;
            }
            set
            {
                SetColumn("UpPostContnet", _UpPostContnet, value);
                _UpPostContnet = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get
            {
                return _Memo;
            }
            set
            {
                SetColumn("Memo", _Memo, value);
                _Memo = value;
            }
        }

        /// <summary>
        /// feedid
        /// </summary>
        public string FeedId
        {
            get
            {
                return _FeedId;
            }
            set
            {
                SetColumn("FeedId", _FeedId, value);
                _FeedId = value;
            }
        }

    }

    public class T_Task_New : Table<T_Task_New>
    {
        internal string _TaskId;
        internal string _Ip;
        internal int _ClientUserId;
        internal string _Url;
        internal string _Keywords;
        internal string _OpString;
        internal string _UpPostContnet;
        internal string _Memo;
        internal int _BaiduUserId;
        internal int _Status;
        internal DateTime _CreateTime;
        internal DateTime _UpdateTime;
        internal string _FeedId;

        static T_Task_New()
        {

        }

        /// <summary>
        /// 任务id
        /// </summary>
        public string TaskId
        {
            get
            {
                return _TaskId;
            }
            set
            {
                SetColumn("TaskId", _TaskId, value);
                _TaskId = value;
            }
        }

        /// <summary>
        /// ip
        /// </summary>
        public string Ip
        {
            get
            {
                return _Ip;
            }
            set
            {
                SetColumn("Ip", _Ip, value);
                _Ip = value;
            }
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                SetColumn("CreateTime", _CreateTime, value);
                _CreateTime = value;
            }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get
            {
                return _UpdateTime;
            }
            set
            {
                SetColumn("UpdateTime", _UpdateTime, value);
                _UpdateTime = value;
            }
        }

        /// <summary>
        /// 客户端userid
        /// </summary>
        public int ClientUserId
        {
            get
            {
                return _ClientUserId;
            }
            set
            {
                SetColumn("ClientUserId", _ClientUserId, value);
                _ClientUserId = value;
            }
        }

        /// <summary>
        /// 百家号链接url
        /// </summary>
        public string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                SetColumn("Url", _Url, value);
                _Url = value;
            }
        }
        /// <summary>
        /// 百家号账号id
        /// </summary>
        public int BaiduUserId
        {
            get
            {
                return _BaiduUserId;
            }
            set
            {
                SetColumn("BaiduUserId", _BaiduUserId, value);
                _BaiduUserId = value;
            }
        }

        /// <summary>
        /// 状态 0 no 1 success
        /// </summary>
        public int Status
        {
            get
            {
                return _Status;
            }
            set
            {
                SetColumn("Status", _Status, value);
                _Status = value;
            }
        }

        /// <summary>
        /// 关键字
        /// </summary>
        public string Keywords
        {
            get
            {
                return _Keywords;
            }
            set
            {
                SetColumn("Keywords", _Keywords, value);
                _Keywords = value;
            }
        }

        /// <summary>
        /// 操作串
        /// </summary>
        public string OpString
        {
            get
            {
                return _OpString;
            }
            set
            {
                SetColumn("OpString", _OpString, value);
                _OpString = value;
            }
        }
        /// <summary>
        /// 顶贴内容
        /// </summary>
        public string UpPostContnet
        {
            get
            {
                return _UpPostContnet;
            }
            set
            {
                SetColumn("UpPostContnet", _UpPostContnet, value);
                _UpPostContnet = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get
            {
                return _Memo;
            }
            set
            {
                SetColumn("Memo", _Memo, value);
                _Memo = value;
            }
        }

        /// <summary>
        /// feedid
        /// </summary>
        public string FeedId
        {
            get
            {
                return _FeedId;
            }
            set
            {
                SetColumn("FeedId", _FeedId, value);
                _FeedId = value;
            }
        }

    }
}
