using System.Collections.Generic;
using System.Data;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using Comm.Tools.Utility;


namespace Comm.Global.Db
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

    /// <summary>
    /// 分页处理
    /// </summary>
    public class PagedDataSetResult
    {
        public int PageIndex;
        public int PageCount;
        public int RowCount;
        public DataSet Rows;
        [XmlIgnore]
        [ScriptIgnore]
        public int PageSize;

        public PagedDataSetResult(int pageSize, int pageIndex)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
        }
    }

    /// <summary>
    /// 统计型分页处理
    /// </summary>
    public class PagedStatResult
    {
        public int PageIndex;
        public int PageCount;
        public int RowCount;
        public DataTable Rows;
        public DataRow Stat;
        [XmlIgnore]
        [ScriptIgnore]
        public int PageSize;

        public PagedStatResult(int pageSize, int pageIndex)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
        }
    }
}
