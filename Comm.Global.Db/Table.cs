using System;
using System.Collections.Generic;
using System.Linq;

namespace Comm.Global.Db
{
    [Serializable]
    public class TableBase
    {

        /// <summary>
        /// 字段更改副本
        /// </summary>
        public readonly Dictionary<string, object> EditColumns = new Dictionary<string, object>();
        protected readonly Dictionary<string, object[]> UpdateInfo = new Dictionary<string, object[]>();

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        private string _updateFlag;
        /// <summary>
        /// 更新标识 一般为数据表中的timestamp字段
        /// </summary>
        public string UpdateFlag
        {
            get
            {
                return _updateFlag;
            }
            set
            {
                _updateFlag = value;
            }
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="propertyName">区分大小写的，以DAL.cs字段大小写为准</param>
        public object GetPropertyValue(string propertyName)
        {
            var filed = GetType().GetProperty(propertyName);
            return filed == null ? null : filed.GetValue(this, null);
        }
    }

    [Serializable]
    public class Table<T> : TableBase
    {
        protected static Dictionary<string, string> Summary;

        protected Table()
        {
            TableName = typeof(T).Name;
        }

        /// <summary>
        /// 获取属性描述
        /// </summary>
        /// <param name="fieldName">区分大小写的，以DAL.cs字段大小写为准</param>
        public string GetPropertySummary(string fieldName)
        {
            string s;
            return Summary.TryGetValue(fieldName, out s) ? s : "";
        }

        /// <summary>
        /// 设置表字段更改副本
        /// </summary>
        public void SetColumn(string name, object oldValue, object newValue)
        {
            if (newValue != null && newValue.Equals(oldValue))
            {
                return;
            }
            EditColumns[name] = newValue;
            UpdateInfo[name] = new[] { oldValue, newValue };
        }

        /// <summary>
        /// 获取更改字段的数量
        /// </summary>
        public int GetColumnEditCount()
        {
            return EditColumns.Count;
        }

        /// <summary>
        /// 清空表字段更改副本
        /// </summary>
        public void Clear()
        {
            EditColumns.Clear();
        }

        /// <summary>
        /// 插入数据内容
        /// </summary>
        public Dictionary<string, object> GetInsertContext()
        {
            return EditColumns.Count == 0 ? EditColumns : new[] { new KeyValuePair<string, object>("Id", TableName) }.Concat(EditColumns).ToDictionary(a => a.Key, a => a.Value);
        }

        /*
        /// <summary>
        /// 插入数据描述
        /// </summary>
        public Dictionary<string, object> GetInsertSummary()
        {
            var err = EditColumns.ToDictionary(a => a.Key, a => GetPropertySummary(a.Key)).GroupBy(a => a.Value)
                .Where(a => a.Count() > 1)
                .Select(a => "表字段{0}描述相同，均为'{1}'".Formats(a.Select(b => b.Key).Join(","), a.Key)).Join("；");
            if (err.HasValue())
            {
                throw new Exception(err);
            }
            return EditColumns.ToDictionary(a => GetPropertySummary(a.Key), a => a.Value);
        }

        /// <summary>
        /// 更新内容
        /// </summary>
        /// <param name="summaryFields">附加描述字段(尽量唯一)逗号间隔</param>
        public Dictionary<string, object> GetUpdateContext(string summaryFields)
        {
            var result = new Dictionary<string, object>();
            result["TableName"] = TableName;
            result["Id"] = GetPropertyValue("Id");
            summaryFields.Split(",").Where(f => f != "Id").Except(UpdateInfo.Keys).Each(f =>
            {
                result[f] = GetPropertyValue(f);
            });
            result["Update"] = UpdateInfo;
            return result;
        }
        /// <summary>
        /// 更新描述
        /// </summary>
        /// <param name="summaryFields">附加描述字段(尽量唯一)逗号间隔</param>
        public Dictionary<string, object> GetUpdateSummary(string summaryFields)
        {
            var summary = summaryFields.Split(",").Except(UpdateInfo.Keys).Distinct().ToDictionary(a => a, GetPropertySummary);
            var err = summary.GroupBy(a => a.Value)
                .Where(a => a.Count() > 1)
                .Select(a => "表字段{0}描述相同，均为'{1}'".Formats(a.Select(b => b.Key).Join(","), a.Key)).Join("；");
            if (err.HasValue())
            {
                throw new Exception(err);
            }
            var result = new Dictionary<string, object>();
            summary.Each(s =>
            {
                result[s.Value] = GetPropertyValue(s.Key);
            });
            result["数据变动"] = UpdateInfo.ToDictionary(a => GetPropertySummary(a.Key), a => a.Value);
            return result;
        }

        /// <summary>
        /// 删除描述
        /// </summary>
        /// <param name="summaryFields">附加描述字段(尽量唯一)逗号间隔</param>
        public Dictionary<string, object> GetDeleteSummary(string summaryFields)
        {
            var summary = summaryFields.Split(",").ToDictionary(a => a, GetPropertySummary);
            var err = summary.GroupBy(a => a.Value)
                .Where(a => a.Count() > 1)
                .Select(a => "表字段{0}描述相同，均为'{1}'".Formats(a.Select(b => b.Key).Join(","), a.Key)).Join("；");
            if (err.HasValue())
            {
                throw new Exception(err);
            }
            var result = new Dictionary<string, object>();
            summary.Each(s =>
            {
                result[s.Value] = GetPropertyValue(s.Key);
            });
            return result;
        }

        /// <summary>
        /// 属性重新赋值(为了使对象的引用不发生变化)
        /// </summary>
        /// <param name="obj">新的值对象</param>
        public void PropertiesUpdate(Table<T> obj)
        {
            if (obj != null && obj.UpdateFlag != UpdateFlag)
                typeof(T).GetProperties().Each(a => a.SetValue(this, a.GetValue(obj)));
        }
        */
    }
}