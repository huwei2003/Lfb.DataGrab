using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Comm.Tools.Utility
{
    /// <summary>
    /// 分页处理，客户端用
    /// </summary>
    public class PagedResult<T>
    {
        public int PageIndex; 
        public int PageCount;
        public int RowCount;
        public List<T> Rows;
        /// <summary>
        /// 统计
        /// </summary>
        public Dictionary<string, object> Stat;
        [XmlIgnore]
        [ScriptIgnore]
        public int PageSize;

        public PagedResult()
        {
        }

        public PagedResult(int pageSize, int pageIndex)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
        }
    }
    
    /// <summary>
    /// 分页处理
    /// </summary>
    public class PagedDataTableResult
    {
        public int PageIndex;
        public int PageCount;
        public int RowCount;
        public DataTable Rows;
        public DataRow Stat;
        [XmlIgnore]
        [ScriptIgnore]
        public int PageSize;

        public PagedDataTableResult(int pageSize, int pageIndex)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public void ListResult<T>(PagedResult<T> pagedResult)
        {
            pagedResult.PageIndex = PageIndex;
            pagedResult.PageCount = PageCount;
            pagedResult.RowCount = RowCount;
            pagedResult.Rows = Rows.ToList<T>();
        }

        public PagedResult<T> Cast<T>()
        {
            var pagedResult = new PagedResult<T>
            {
                PageIndex = PageIndex,
                PageCount = PageCount,
                RowCount = RowCount,
                Rows = Rows.ToList<T>()
            };
            if (Stat != null)
            {
                pagedResult.Stat = Stat.ToDictionary();
            }
            return pagedResult;
        }
    }
}
