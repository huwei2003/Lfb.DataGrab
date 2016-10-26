using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace Comm.Tools.Utility
{
    public static class ObjectHelper
    {
        private static readonly Log Log = new Log("System");

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="sourse">源数据</param>
        /// <param name="defaultValue">默认数据</param>
        /// <returns>返回值</returns>
        public static T ConvertTo<T>(this object sourse, T defaultValue = default(T))
        {
            if (sourse == null || sourse.Equals(""))
            {
                return defaultValue;
            }

            T t;
            try
            {
                t = (T)((sourse is IConvertible) ? Convert.ChangeType(sourse, typeof(T)) : sourse);
            }
            catch
            {
                t = defaultValue;
            }

            return t;
        }
        /// <summary>
        /// 类型转换
        /// </summary>
        public static TK ConvertTo<T, TK>(this T obj, Func<T, TK> func)
        {
            return func(obj);
        }

        public static string ToString(this object o)
        {
            return o == null ? string.Empty : o.ToString();
        }

        public static string ToString(this object o, string d)
        {
            if (o != null)
            {
                d = o.ToString();
            }

            return d;
        }

        /// <summary>
        /// 序列化后保存
        /// </summary>
        public static void BinarySave(this object o, string path)
        {
            var fi = new FileInfo(path);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            using (var fs = new FileStream(path, FileMode.Create))
            {
                try
                {
                    new BinaryFormatter().Serialize(fs, o);
                }
                catch (Exception e)
                {
                    new Log().Error(e.Message + e.StackTrace);
                }
            }
        }

        public static MemoryStream Serialize(this object o)
        {
            try
            {
                if (o == null)
                {
                    return null;
                }
                using (var stream = new MemoryStream())
                {
                    new BinaryFormatter().Serialize(stream, o);
                    stream.Position = 0;
                    return stream;
                }
            }
            catch
            {
                return null;
            }
        }

        public static T Deserialize<T>(this Stream stream, T defaultValue = default(T))
        {
            T t;
            try
            {
                t = new BinaryFormatter().Deserialize(stream).ConvertTo(defaultValue);
            }
            catch (Exception e)
            {
                t = defaultValue;
                new Log().Error(e.Message + e.StackTrace);
            }
            return t;
        }

        /// <summary>
        /// 从文件读取序列化对象
        /// </summary>
        public static T BinaryRead<T>(this string path, T defaultValue = default(T))
        {
            var t = defaultValue;
            try
            {
                using (var fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    var fi = new FileInfo(path);
                    if (!fi.Directory.Exists)
                    {
                        fi.Directory.Create();
                    }
                    t = fs.Length == 0 ? defaultValue : new BinaryFormatter().Deserialize(fs).ConvertTo(defaultValue);
                }
            }
            catch (Exception e)
            {
                new Log().Error(e.Message + e.StackTrace);
            }
            return t;
        }

        /// <summary>
        /// 深度拷贝
        /// </summary>
        public static T Clone<T>(this T o)
        {
            using (var stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, o);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }

        public static byte[] ToArrays(this object o, byte[] defaultVaule = null)
        {
            try
            {
                if (o == null)
                {
                    return defaultVaule;
                }
                using (var stream = new MemoryStream())
                {
                    new BinaryFormatter().Serialize(stream, o);
                    stream.Position = 0;
                    return stream.ToArray();
                }
            }
            catch
            {
                return defaultVaule;
            }
        }

        public static T ToClass<T>(this byte[] bytes, T defaultValue = default(T))
        {
            var t = defaultValue;
            try
            {
                using (var stream = new MemoryStream(bytes))
                {
                    t = new BinaryFormatter().Deserialize(stream).ConvertTo(defaultValue);
                }
            }
            catch (Exception e)
            {
                new Log().Error(e.Message + e.StackTrace);
            }
            return t;
        }

        /// <summary>
        /// 类型转换,json方式
        /// </summary>
        public static T ToObj<T>(this object obj, T defaultValue = default(T))
        {
            return obj.ToJsonString().JsonToObject(defaultValue);
        }
        /// <summary>
        /// 类型转换,lambda方式
        /// </summary>
        public static TK ToObj<T, TK>(this T obj, Func<T, TK> func)
        {
            return func(obj);
        }

        internal static object JsonToArray(this string s, Type type)
        {
            var result = new ArrayList();
            try
            {
                if (s.IsNullOrEmpty() || !s.Contains("\""))
                {
                    return null;
                }

                var constructor = type.GetConstructor(new Type[0]);
                if (constructor == null)
                {
                    return null;
                }

                var arr = s.JsonToObject<List<Dictionary<string, object>>>();
                if (arr.Count > 0)
                {
                    var columns = arr.First().ToDictionary(a => a.Key.ToLower(), a => a.Key);
                    var isTable = type.BaseType.FullName.Contains("Utility.Data.Table");
                    FieldInfo[] fields;
                    if (isTable)
                    {
                        fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                        columns = columns.ToDictionary(a => "_" + a.Key, a => a.Value);
                    }
                    else
                    {
                        fields = type.GetFields();
                    }
                    var fis = fields.ToDictionary(a => a.Name.ToLower(), a => a);
                    foreach (var o in arr)
                    {
                        var obj = constructor.Invoke();
                        foreach (var column in columns)
                        {
                            FieldInfo f;
                            if (fis.TryGetValue(column.Key, out f))
                            {
                                var v = o[column.Value] ?? "";
                                var vt = v.GetType();
                                var ft = f.FieldType;
                                var fgt = ft.GenericTypeArguments.FirstOrDefault();
                                f.SetValue(obj, (ft == vt || fgt == vt) ? v : Convert.ChangeType(v, fgt ?? ft));
                            }
                        }
                        result.Add(obj);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return null;
            }
            return result.ToArray(type);
        }

        /// <summary>
        /// 类型默认值
        /// </summary>
        public static object DefaultValue(this Type targetType)
        {
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
        }

        /// <summary>
        /// 对象转Dictionary
        /// </summary>
        public static SortedDictionary<string, object> ToSortedDictionary(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return null;
            }
            var type = obj.GetType();
            try
            {
                var result = new SortedDictionary<string, object>();
                //匿名类型
                if (type.Name.Contains("AnonymousType"))
                {
                    var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    foreach (var field in fields)
                    {
                        result[field.Name.Substring("<", ">")] = field.GetValue(obj);
                    }
                }
                else
                {
                    var propertys = type.GetProperties();
                    foreach (var property in propertys)
                    {
                        result[property.Name] = property.GetValue(obj);
                    }
                    var fields = type.GetFields();
                    foreach (var field in fields)
                    {
                        result[field.Name] = field.GetValue(obj);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
            }
            return null;
        }

        /// <summary>
        /// 对象转Dictionary
        /// </summary>
        public static Dictionary<string, object> ToDictionary(this object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return null;
            }
            var type = obj.GetType();
            try
            {
                var result = new Dictionary<string, object>();
                //匿名类型
                if (type.Name.Contains("AnonymousType"))
                {
                    var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    foreach (var field in fields)
                    {
                        result[field.Name.Substring("<", ">")] = field.GetValue(obj);
                    }
                }
                else
                {
                    var propertys = type.GetProperties();
                    foreach (var property in propertys)
                    {
                        result[property.Name] = property.GetValue(obj);
                    }
                    var fields = type.GetFields();
                    foreach (var field in fields)
                    {
                        result[field.Name] = field.GetValue(obj);
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
            }
            return null;
        }

        /// <summary>
        /// 对象转化为可Json化等价结构对象
        /// </summary>
        internal static object ToJsonable(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            var type = obj.GetType();
            var typeName = type.Name;
            switch (typeName)
            {
                default:
                    {
                        if (!type.IsValueType)
                        {
                            if (type.IsArray || type.Namespace.In("System.Linq", "System.Collections.Generic"))
                            {
                                var eobjs = obj as IEnumerable<object>;
                                if (eobjs == null)
                                {
                                    return null;
                                }
                                return eobjs.Select(a => a.ToJsonable());
                            }
                            return obj.ToSortedDictionary().ToJsonable();
                        }
                    }
                    break;
                case "DataTable":
                    {
                        return ((DataTable)obj).ToDictionarys();
                    }
                case "DataRow":
                    {
                        return ((DataRow)obj).ToDictionary();
                    }
                case "DataSet":
                    {
                        return ((DataSet)obj).ToDictionarys();
                    }
                case "String":
                    break;
                case "Dictionary`2":
                case "SortedDictionary`2":
                    {
                        if (type.GenericTypeArguments[0].Name != "String")
                        {
                            Log.Error("错误:Dictionary 键必须为 string 才能转 Json");
                            return null;
                        }
                        var typeValue = type.GenericTypeArguments[1];
                        switch (typeValue.Name)
                        {
                            case "Object":
                                return ((IDictionary<string, object>)obj).ToJsonable();
                            case "DataTable":
                                return ((IDictionary<string, DataTable>)obj).ToJsonable();
                            case "DataSet":
                                return ((IDictionary<string, DataSet>)obj).ToJsonable();
                            case "ArrayList":
                                return ((IDictionary<string, ArrayList>)obj).ToJsonable();
                        }
                        break;
                    }
                case "ArrayList":
                    return ((ArrayList)obj).ToArray().ToJsonable();
            }
            return obj;
        }

        internal static SortedDictionary<string, object> ToJsonable<T, TK>(this IDictionary<T, TK> dict)
        {
            if (dict == null)
            {
                return null;
            }
            var result = new SortedDictionary<string, object>();
            foreach (var kv in dict)
            {
                result[kv.Key.ToString()] = kv.Value.ToJsonable();
            }
            return result;
        }

        /// <summary>
        /// 格式化金额(60000转成60,000)
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string FormatMoney(this object money)
        {
            return Regex.Replace(money.ToString("0"), "(\\d)(?=(\\d{3})+$)", "$1,");
        }
    }
}