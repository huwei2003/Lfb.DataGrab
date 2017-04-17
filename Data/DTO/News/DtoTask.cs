using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comm.Global.DTO.News
{
    /// <summary>
    /// 客户端任务dto
    /// </summary>
    public class DtoTask
    {
        public string TaskId;
        public string Ip;
        public int ClientUserId;
        public string Url;
        public string Keywords;
        public string OpString;
        public string UpPostContnet;
        public string Memo;
        public int BaiduUserId;
        public int Status;
        public DateTime CreateTime;
        public DateTime UpdateTime;
        public string FeedId;
    }
    /// <summary>
    /// 客户端任务dto
    /// </summary>
    public class DtoTaskForClient
    {
        public string TaskId;
        public string Url;
        public string Keywords;
        public string OpString;
        public string UpPostContnet;
        public string BaiduUserName;
        public string BaiduPassword;
    }

    public class DtoClientUser 
    {
        public int Id;
        public string UserName;
        public string Password;
        public DateTime CreateTime;
        public int Status;
    }

    public class DtoBaiduUser
    {
        public int Id;
        public string UserName;
        public string Password;
        public DateTime CreateTime;
        public int Status;
    }

    public class DtoBaiduKeyword
    { 
        public int Id;
        public string Keyword; 
    }
    public class DtoBaiduKeywordThree
    {
        public int Id;
        public string Keyword;
        public string Keyword2;
        public string Keyword3;
    }
    public class DtoOpString
    { 
        public int Id;
        public string Contents; 
    }
    public class DtoUpPostContent
    { 
        public int Id;
        public string Contents; 
    }
    
}
