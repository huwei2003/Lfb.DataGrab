using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Linq;
using System.Text;
using System.Threading;

namespace Comm.Tools.Utility
{
    /// <summary>
    /// 通用扩展，主要是采用泛型
    /// </summary>
    public static class Generic
    {
        /// <summary>
        /// 可枚举类型转化为字符串，类似于js中数组的join函数
        /// </summary>
        public static string Join<T>(this IEnumerable<T> ss, string tag = "")
        {
            var sb = new StringBuilder();
            if (tag.IsNullOrEmpty())
            {
                foreach (var s in ss)
                {
                    sb.Append(s);
                }
            }
            else
            {
                foreach (var s in ss)
                {
                    sb.Append(tag).Append(s);
                }
                if (sb.Length >= tag.Length)
                {
                    sb.Remove(0, tag.Length);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 连续最大出现次数
        /// </summary>
        public static int MaxSeries<T>(this IEnumerable<T> ss, T tag)
        {
            var ms = ("," + ss.Join(",")).Matches("(," + tag + "){1,30}");
            return ms.Any() ? ms.Max(a => a.Split(',').Count() - 1) : 0;
        }

        /// <summary>
        /// Each扩展，例：a.Each(b => b.Enabled = false);
        /// </summary>
        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        /// <summary>
        /// Each扩展，例：a.Each((b,i) => b.Enabled = false);
        /// </summary>
        public static void Each<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            var i = 0;
            foreach (var item in source)
                action(item, i++);
        }

        /// <summary>
        /// 包含于
        /// </summary>
        public static bool In<T>(this T source, params T[] target)
        {
            return target.Contains(source);
        }

        /// <summary>
        /// 包含于
        /// </summary>
        public static bool In<T>(this T source, IEnumerable<T> target)
        {
            return target.Contains(source);
        }

        /// <summary>
        /// Cson:Csharp Object Notation 支持pulic类型字段或属性
        /// </summary>
        public static string ToCsonString<T>(this IEnumerable<T> objs, string columns = null)
        {
            var sb = new StringBuilder();
            var type = typeof(T);
            var fields = type.GetFields();
            if (fields.Length > 0)
            {
                if (columns.NotNullOrEmpty())
                {
                    columns = ",{0},".Formats(columns).ToLower();
                    fields = fields.Where(a => columns.Contains(",{0},".Formats(a.Name.ToLower()))).ToArray();
                }
                sb.Append(fields.Select(a => a.Name).Join("δ"));
                foreach (var obj in objs)
                {
                    sb.Append("η");
                    foreach (var field in fields)
                    {
                        var v = field.GetValue(obj);
                        sb.Append(v is DateTime ? ((DateTime)v).ToString("yyyy-MM-dd HH:mm:ss") : v).Append("δ");
                    }
                    sb.Remove(sb.Length - 1, 1);
                }
            }
            else
            {
                var properties = type.GetProperties();
                if (columns.NotNullOrEmpty())
                {
                    columns = ",{0},".Formats(columns).ToLower();
                    properties = properties.Where(a => columns.Contains(",{0},".Formats(a.Name.ToLower()))).ToArray();
                }
                sb.Append(properties.Select(a => a.Name).Join("δ"));
                foreach (var obj in objs)
                {
                    sb.Append("η");
                    foreach (var property in properties)
                    {
                        var v = property.GetValue(obj, null);
                        sb.Append(v is DateTime ? ((DateTime)v).ToString("yyyy-MM-dd HH:mm:ss") : v).Append("δ");
                    }
                    sb.Remove(sb.Length - 1, 1);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 将对象转化为JSON字符串
        /// </summary>
        public static string ToJsonString(this object obj, string datetimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            try
            {
                if (obj == null)
                {
                    return "";
                }
                var json = new JavaScriptSerializer { MaxJsonLength = int.MaxValue }.Serialize(obj);
                var pattern = @"\\/Date\((-){0,1}(\d+)\)\\/";
                if (json.IsMatch(pattern))
                {
                    json = Regex.Replace(json, pattern, match =>
                    {
                        try
                        {
                            if (match.Groups[1].Value == "")
                            {
                                var dt = new DateTime(1970, 1, 1);
                                dt = dt.AddMilliseconds(long.Parse(match.Groups[2].Value));
                                dt = dt.ToLocalTime();
                                return dt.ToString(datetimeFormat);
                            }
                            return "";
                        }
                        catch
                        {
                            return "";
                        }
                    });
                }
                return json;
            }
            catch
            {
                return obj.ToJsonable().ToJsonString();
            }
        }
        /// <summary>
        /// 将对象转化为JSON字符串(带排序)
        /// </summary>
        public static string ToJsonOrderString(this object obj, string datetimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            if (obj == null)
            {
                return "";
            }
            return obj.ToJsonable().ToJsonString();
        }
        /// <summary>
        /// 将对象转化为JSON字符串
        /// </summary>
        public static string ToJsonString<T>(this object obj, string columns = "*")
        {
            try
            {
                var type = typeof(T);
                var ot = obj.GetType();
                FieldInfo[] fis;
                var isTable = type.BaseType.FullName.Contains("Utility.Data.Table");
                if (isTable)
                {
                    fis = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                }
                else
                {
                    fis = type.GetFields();
                }
                string[] cols;
                if (columns == "*")
                {
                    cols = fis.Select(a => a.Name.ToLower().Replace("_", "")).ToArray();
                }
                else
                {
                    cols = columns.ToLower().Split(new[] { ",", " " });
                }
                if (ot.Namespace == "System.Collections.Generic" || ot.Namespace == "System.Linq" || (ot.FullName != null && ot.FullName.Contains("[]")))
                {
                    var cs = cols.Intersect(fis.Select(a => a.Name.ToLower().Replace("_", ""))).ToArray();
                    var result = (obj as IEnumerable<T>).Select(o => cs.Select(a => fis.First(b => b.Name.ToLower().Replace("_", "") == a).GetValue(o))).ToArray();
                    return new JavaScriptSerializer { MaxJsonLength = int.MaxValue }.Serialize(result);
                }
                else
                {
                    var cs = cols.Intersect(fis.Select(a => a.Name.ToLower().Replace("_", ""))).ToArray();
                    var result = cs.Select(a => fis.First(b => b.Name.ToLower().Replace("_", "") == a).GetValue(obj)).ToArray();
                    return new JavaScriptSerializer { MaxJsonLength = int.MaxValue }.Serialize(result);
                }
            }
            catch (Exception e)
            {
                new Log("System").Error(e.Message + e.StackTrace);
            }
            return string.Empty;
        }
        /// <summary>
        /// 将对象中属性字段转化为JOSN
        /// </summary>
        public static string ProToJsonString<T>(this IEnumerable<T> obj, string[] names = null)
        {
            var json = new StringBuilder("[");
            var type = typeof(T);
            var ps = type.GetProperties();
            if (names != null)
                ps = names.Select(a => ps.FirstOrDefault(b => b.Name == a)).Where(a => a != null).ToArray();
            foreach (T item in obj)
            {
                json.Append("[");
                foreach (var p in ps)
                {
                    json.AppendFormat("\"{0}\",", FormatString(p.GetValue(item, null)));
                }
                json.Remove(json.Length - 1, 1).Append("],");
            }
            if (json.Length > 1)
                json.Replace(',', ']', json.Length - 1, 1);
            return json.ToString();
        }
        private static string FormatString(object value)
        {
            var tp = value.GetType().Name;
            switch (tp)
            {
                case "Decimal":
                    return Convert.ToDecimal(value).ToString("0.000");
                case "Double":
                    return Convert.ToDecimal(value).ToString("0.00");
                default:
                    return value.ToString();
            }
        }

        /// <summary>
        /// 相同元素个数
        /// </summary>
        public static int SameCount<T>(this IEnumerable<T> one, IEnumerable<T> two)
        {
            var t2 = two.ToList();
            var c = t2.Count;
            foreach (var a in one.Where(t2.Contains))
            {
                t2.RemoveAt(t2.IndexOf(a));
            }
            return c - t2.Count;
        }
        /// <summary>
        /// 相同元素
        /// </summary>
        public static List<T> Sames<T>(this IEnumerable<T> one, IEnumerable<T> two)
        {
            var t2 = two.ToList();
            var r = new List<T>();
            foreach (var idx in one.Where(t2.Contains).Select(a => t2.IndexOf(a)))
            {
                r.Add(t2[idx]);
                t2.RemoveAt(idx);
            }
            return r;
        }

        /// <summary>
        /// 不相同元素(不判断重复情况)
        /// </summary>
        public static IEnumerable<T> Differents<T>(this IEnumerable<T> one, IEnumerable<T> two)
        {
            return one.Union(two).Except(one.Sames(two));
        }

        /// <summary>
        /// 是否不重复
        /// </summary>
        public static bool NotRepeat<T>(this T[] obj)
        {
            return obj.Length == obj.Distinct().Count();
        }
        /// <summary>
        /// 是否不重复并且长度>=minLength
        /// </summary>
        public static bool NotRepeatThanLength<T>(this T[] obj, int minLength)
        {
            return obj.Length >= minLength && obj.Length == obj.Distinct().Count();
        }
        /// <summary>
        /// 是否不重复
        /// </summary>
        public static bool NotRepeat<T>(this List<T> obj)
        {
            return obj.Count == obj.Distinct().Count();
        }
        /// <summary>
        /// 是否不重复并且长度>=minLength
        /// </summary>
        public static bool NotRepeatThanLength<T>(this List<T> obj, int minLength)
        {
            return obj.Count >= minLength && obj.Count == obj.Distinct().Count();
        }

        /// <summary>
        /// 取最后面count的元素
        /// </summary>
        public static IEnumerable<T> LastTake<T>(this IEnumerable<T> source, int count)
        {
            var length = source.Count();
            return count > length ? source : source.Skip(length - count);
        }

        /// <summary>
        /// 重复元素排前面
        /// </summary>
        public static IEnumerable<T> OrderBySameBegin<T>(this List<T> obj)
        {
            return obj.Where(a => obj.Count(b => b.Equals(a)) > 1).Concat(obj.Where(a => obj.Count(b => b.Equals(a)) == 1).OrderBy(a => a));
        }

        /// <summary>
        /// 重复元素排前面
        /// </summary>
        public static IEnumerable<T> OrderBySameBegin<T>(this T[] obj)
        {
            return obj.Where(a => obj.Count(b => b.Equals(a)) > 1).Concat(obj.Where(a => obj.Count(b => b.Equals(a)) == 1).OrderBy(a => a));
        }

        /// <summary>
        /// 跨度
        /// </summary>
        public static int Span<T>(this IEnumerable<T> source, Func<T, int> selector)
        {
            var s = source.ToArray();
            return s.Max<T, int>(selector) - s.Min<T, int>(selector);
        }

        /// <summary>
        /// 连乘(阶乘)
        /// </summary>
        public static int Factorial(this IEnumerable<int> source)
        {
            return source.Aggregate((a, b) => a * b);
        }
        /// <summary>
        /// 连乘(阶乘)
        /// </summary>
        public static decimal Factorial(this IEnumerable<decimal> source)
        {
            return source.Aggregate((a, b) => a * b);
        }
        /// <summary>
        /// 连乘(阶乘)
        /// </summary>
        public static long Factorial(this IEnumerable<long> source)
        {
            return source.Aggregate((a, b) => a * b);
        }

        /// <summary>
        /// 数组拆单(可用于投注号码复式拆单)
        /// [[1,2],[1,2,3]] = [[1,1],[1,2],[1,3],[2,1],[2,2],[2,3]]
        /// </summary>
        public static T[][] ToSingles<T>(this IEnumerable<IEnumerable<T>> source)
        {
            var resuls = source.Select(a => a.Select(b => b.ToString())).ToSingles();
            return resuls.Select(a => a.Select(b => b.ConvertTo<T>()).ToArray()).ToArray();
        }

        /// <summary>
        /// 数组拆单(可用于投注号码复式拆单)
        /// </summary>
        public static string[][] ToSingles(this IEnumerable<IEnumerable<string>> source)
        {
            var singles = source.Aggregate((m, n) => m.SelectMany(t1 => n.Select(t2 => new[] { t1, t2 }.Join("§"))).ToArray());
            return singles.Select(a => a.Split("§")).ToArray();
        }

        /// <summary>
        /// 数组拆单(可用于投注号码复式拆单)
        /// </summary>
        public static string[][] ToSingles(this IEnumerable<IEnumerable<char>> source)
        {
            return source.Select(a => a.Select(b => b.ToString())).ToSingles();
        }

        /// <summary>
        /// 介于start-end之间
        /// </summary>
        public static bool Between<T>(this IComparable<T> ts, T start, T end)
        {
            return ts.CompareTo(start) >= 0 && ts.CompareTo(end) <= 0;
        }
        /// <summary>
        /// 判断是否在指定范围内(不等于界限)
        /// </summary>
        public static bool BetweenIn<T>(this IComparable<T> ts, T start, T end)
        {
            return ts.CompareTo(start) > 0 && ts.CompareTo(end) < 0;
        }

        public static string OrderJoined<T>(this IEnumerable<T> s, string tag = "")
        {
            return s.OrderBy(a => a).Join(tag);
        }

        public static string OrderDescJoined<T>(this IEnumerable<T> s, string tag = "")
        {
            return s.OrderByDescending(a => a).Join(tag);
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            var set = new HashSet<T>();
            foreach (var t in source)
            {
                set.Add(t);
            }
            return set;
        }

        public static SortedSet<T> ToSortedSet<T>(this IEnumerable<T> source)
        {
            var set = new SortedSet<T>();
            foreach (var t in source)
            {
                set.Add(t);
            }
            return set;
        }

        /// <summary>
        /// 查找匹配的值返回索引 没找到返回默认值
        /// </summary>
        /// <param name="l"></param>
        /// <param name="match">搜索条件</param>
        /// <param name="d">默认值</param>
        /// <returns></returns>
        public static int FindIndexOrDefault<T>(this List<T> l, Predicate<T> match, int d = -1)
        {
            var i = l.FindIndex(match);
            return i > -1 ? i : d;
        }
        public static System.Collections.ArrayList GetLasts(this System.Collections.ArrayList data, int count)
        {
            var l = data.Count;
            count = l > count ? count : l;
            return data.GetRange(l - count, count);
        }

        /// <summary>
        /// 数据集分页 - 按页数量
        /// </summary>
        public static List<T>[] PageByCount<T>(this IEnumerable<T> ts, int pageCount)
        {
            var result = new List<T>[pageCount];
            for (var i = 0; i < pageCount; i++)
            {
                result[i] = new List<T>();
            }
            var idx = 0;
            foreach (var t in ts)
            {
                result[idx % pageCount].Add(t);
                idx++;
            }
            return result.Where(a => a.Count > 0).ToArray();
        }

        /// <summary>
        /// 数据集分页 - 按页大小
        /// </summary>
        public static List<T[]> PageBySize<T>(this IEnumerable<T> ts, int pageSize)
        {
            var result = new List<T[]>();
            var pageCount = (ts.Count() + pageSize - 1) / pageSize;
            for (var i = 0; i < pageCount; i++)
            {
                result.Add(ts.Skip(i * pageSize).Take(pageSize).ToArray());
            }
            return result;
        }

        /// <summary>
        /// 对象间属性更新，例：a.Update(()=>{a.Name = b.Uname;});
        /// </summary>
        public static bool Update<T>(this T a, Action action)
        {
            action();
            return true;
        }

        /// <summary>
        /// 转泛型数组
        /// </summary>
        public static TK[] ToArray<T, TK>(this IEnumerable<T> ts, Func<T, TK> func)
        {
            return ts.Select(func).ToArray();
        }

        /// <summary>
        /// 是否含有重复项
        /// </summary>
        public static bool ExitsRepeat<T>(this IEnumerable<T> ts)
        {
            return ts.GroupBy(a => a).Any(g => g.Count() > 1);
        }
        /// <summary>
        /// 重复元素
        /// </summary>
        public static IEnumerable<T> Repeats<T>(this IEnumerable<T> ts)
        {
            return ts.GroupBy(i => i).Where(g => g.Count() > 1).SelectMany(g => g);
        }

        /// <summary>
        /// 开启异步多线程运算
        /// </summary>
        public static void StartAsyncThreads<T>(this IEnumerable<T> ts, Action<T> action)
        {
            ts.Select(a => new Thread(() =>
            {
                action(a);
            })).StartAsync();
        }

        /// <summary>
        /// 开启同步多线程运算
        /// </summary>
        public static void StartSyncThreads<T>(this IEnumerable<T> ts, Action<T> action)
        {
            ts.Select(a => new Thread(() =>
            {
                action(a);
            })).StartSync();
        }

        /// <summary>
        /// 执行Action
        /// </summary>
        public static void Execute(this IEnumerable<Action> ts)
        {
            foreach (var action in ts)
            {
                action();
            }
        }

        /// <summary>
        /// 对象集合 转 DataTable
        /// </summary>
        public static DataTable ToDataTable<T>(this IEnumerable<T> ts, string tableName = null)
        {
            if (ts == null)
            {
                return null;
            }
            var dt = new DataTable();
            if (tableName.HasValue())
            {
                dt.TableName = tableName;
            }
            var type = typeof(T);
            var columns = new List<DataColumn>();
            var baseType = type.BaseType;
            var isTable = false;
            var propertys = type.GetProperties();
            var fields = type.GetFields();
            if (baseType != null)
            {
                isTable = baseType.FullName.Contains("Utility.Table");
                if (isTable)
                {
                    columns.AddRange(propertys.Select(property => new DataColumn(property.Name, property.PropertyType)));
                }
            }
            if (columns.Count == 0)
            {
                columns.AddRange(fields.Select(field => new DataColumn(field.Name, field.FieldType)));
            }
            dt.Columns.AddRange(columns.ToArray());
            object[] arr;
            if (isTable)
            {
                var l = propertys.Length;
                foreach (var t in ts)
                {
                    arr = new object[l];
                    for (var i = 0; i < l; i++)
                    {
                        arr[i] = propertys[i].GetValue(t, null);
                    }
                    dt.Rows.Add(arr);
                }
            }
            else
            {
                var l = fields.Length;
                foreach (var t in ts)
                {
                    arr = new object[l];
                    for (var i = 0; i < l; i++)
                    {
                        arr[i] = fields[i].GetValue(t);
                    }
                    dt.Rows.Add(arr);
                }
            }
            return dt;
        }
    }
}