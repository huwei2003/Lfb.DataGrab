using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// Json操作类 过时的，不推荐用
    /// </summary>
    public class JsonHelperOld
    {
        /// <summary>
        /// List转成json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="jsonName">json object's name</param>
        /// <returns></returns>
        public static string ListToJson<T>(IList<T> list, string jsonName)
        {
            var json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
                jsonName = list[0].GetType().Name;
            json.Append("{\"" + jsonName + "\":[");
            if (list.Count > 0)
            {
                for (var i = 0; i < list.Count; i++)
                {
                    T obj = Activator.CreateInstance<T>();
                    var pi = obj.GetType().GetProperties();

                    json.Append("{");
                    for (var j = 0; j < pi.Length; j++)
                    {
                        var type = pi[j].GetValue(list[i], null).GetType();
                        json.Append("\"" + pi[j].Name + "\":" + StringFormat(pi[j].GetValue(list[i], null).ToString(), type));

                        if (j < pi.Length - 1)
                        {
                            json.Append(",");
                        }
                    }
                    json.Append("}");
                    if (i < list.Count - 1)
                    {
                        json.Append(",");
                    }
                }
            }
            json.Append("]}");
            return json.ToString();
        }

        /// <summary>  
        /// List转成json   
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="list"></param>  
        /// <returns></returns>  
        public static string ListToJson<T>(IList<T> list)
        {
            object obj = list[0];
            return ListToJson<T>(list, obj.GetType().Name);
        }

        /// <summary>   
        /// 对象转换为Json字符串   
        /// </summary>   
        /// <param name="jsonObject">对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(object jsonObject)
        {
            string jsonString = "{";
            var propertyInfo = jsonObject.GetType().GetProperties();
            for (var i = 0; i < propertyInfo.Length; i++)
            {
                object objectValue = propertyInfo[i].GetGetMethod().Invoke(jsonObject, null);
                string value;
                if (objectValue is DateTime || objectValue is Guid || objectValue is TimeSpan)
                {
                    value = "'" + objectValue.ToString() + "'";
                }
                else if (objectValue is string)
                {
                    value = "'" + ToJson(objectValue.ToString()) + "'";
                }
                else if (objectValue is IEnumerable<object>)
                {
                    value = ToJson((IEnumerable<object>)objectValue);
                }
                else
                {
                    value = ToJson(objectValue.ToString());
                }
                jsonString += "\"" + ToJson(propertyInfo[i].Name) + "\":" + value + ",";
            }
            jsonString.Remove(jsonString.Length - 1, jsonString.Length);
            return jsonString + "}";
        }



        /// <summary>   
        /// Datatable转换为Json   
        /// </summary>   
        /// <param name="dt">Datatable对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(DataTable dt)
        {
            var jsonString = new StringBuilder();
            jsonString.Append("[");
            var drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    var strKey = dt.Columns[j].ColumnName;
                    var strValue = drc[i][j].ToString();
                    var type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }


        /// <summary>  
        /// DataTable转成Json   
        /// </summary>  
        /// <param name="jsonName"></param>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        public static string ToJson(DataTable dt, string jsonName)
        {
            var json = new StringBuilder();
            if (string.IsNullOrEmpty(jsonName))
                jsonName = dt.TableName;
            json.Append("{\"" + jsonName + "\":[");
            if (dt.Rows.Count > 0)
            {
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    json.Append("{");
                    for (var j = 0; j < dt.Columns.Count; j++)
                    {
                        var type = dt.Rows[i][j].GetType();
                        json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));
                        if (j < dt.Columns.Count - 1)
                        {
                            json.Append(",");
                        }
                    }
                    json.Append("}");
                    if (i < dt.Rows.Count - 1)
                    {
                        json.Append(",");
                    }
                }
            }
            json.Append("]}");
            return json.ToString();
        }

        /// <summary>   
        /// DataReader转换为Json   
        /// </summary>   
        /// <param name="dataReader">DataReader对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(IDataReader dataReader)
        {
            var jsonString = new StringBuilder();
            jsonString.Append("[");
            while (dataReader.Read())
            {
                jsonString.Append("{");
                for (var i = 0; i < dataReader.FieldCount; i++)
                {
                    var type = dataReader.GetFieldType(i);
                    var strKey = dataReader.GetName(i);
                    var strValue = dataReader[i].ToString();
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (i < dataReader.FieldCount - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            dataReader.Close();
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }

        /// <summary>   
        /// DataSet转换为Json   
        /// </summary>   
        /// <param name="dataSet">DataSet对象</param>   
        /// <returns>Json字符串</returns>   
        public static string ToJson(DataSet dataSet)
        {
            string jsonString = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                jsonString += "\"" + table.TableName + "\":" + ToJson(table) + ",";
            }
            jsonString = jsonString.TrimEnd(',');
            return jsonString + "}";
        }

        /// <summary>  
        /// 过滤特殊字符  
        /// </summary>  
        /// <param name="s"></param>  
        /// <returns></returns>  
        public static string String2Json(String s)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }

        /// <summary>  
        /// 格式化字符型、日期型、布尔型  
        /// </summary>  
        /// <param name="str"></param>  
        /// <param name="type"></param>  
        /// <returns></returns>  
        private static string StringFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            return str;
        }
    }
}
