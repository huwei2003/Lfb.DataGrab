using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;
using Aspose.Cells;

namespace Comm.Tools.Utility
{
    public static class DataTableHelper
    {
        static readonly Log Log = new Log("System");

        public static void ExportToExcel(this DataTable dataTable, string filePath)
        {
            new[] { dataTable }.ExportToExcel(filePath);
        }

        public static Workbook ExportToExcel(this IEnumerable<DataTable> dataTables)
        {
            var book = new Workbook();
            book.Worksheets.RemoveAt(0);
            var style = book.Styles[book.Styles.Add()];
            style.Font.Name = "宋体";
            style.Font.Size = 11;
            foreach (var tab in dataTables)
            {
                var table = tab.Copy();
                var ws = book.Worksheets.Add(table.TableName);
                ws.Cells.ImportDataTable(table, true, 0, 0, true, false);
                ws.Cells.ApplyStyle(style, new StyleFlag { All = true });
                var cls = table.Columns.Cast<DataColumn>();
                var ci = 0;
                foreach (var column in cls)
                {
                    if (column.ColumnName.Contains("时间"))
                    {
                        for (var r = 1; r <= ws.Cells.MaxRow; r++)
                        {
                            ws.Cells[r, ci].Value = ws.Cells[r, ci].StringValue.OleDateTimeToString();
                        }
                    }
                    if (column.ColumnName.Contains("日期"))
                    {
                        for (var r = 1; r <= ws.Cells.MaxRow; r++)
                        {
                            ws.Cells[r, ci].Value = ws.Cells[r, ci].StringValue.OleDateTimeToString("yyyy-MM-dd");
                        }
                    }
                    ci++;
                }

                try
                {
                    ws.AutoFitColumns();
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + e.StackTrace);
                }
            }
            return book;
        }

        public static void ExportToExcel(this IEnumerable<DataTable> dataTables, string fileName)
        {
            dataTables.ExportToExcel().Save(fileName);
        }

        public static void ExportToExcel(this IEnumerable<DataTable> dataTables, HttpResponse response, string fileName, ContentDisposition contentDisposition, SaveOptions saveOptions)
        {
            dataTables.ExportToExcel().Save(response, fileName, contentDisposition, saveOptions);
        }

        public static string ToXml(this DataTable dt)
        {
            MemoryStream w = null;
            XmlTextWriter writer = null;
            var str = "";
            try
            {
                w = new MemoryStream();
                writer = new XmlTextWriter(w, Encoding.UTF8);
                dt.WriteXml(writer);
                var length = (int)w.Length;
                var buffer = new byte[length];
                w.Seek(0L, SeekOrigin.Begin);
                w.Read(buffer, 0, length);
                str = new UTF8Encoding().GetString(buffer).Trim();
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
                if (w != null)
                {
                    w.Dispose();
                }
            }
            return str;
        }

        public static DataTable ToDataTable(this string xml)
        {
            if (xml.IsNullOrEmpty())
            {
                return null;
            }
            StringReader input = null;
            XmlTextReader reader = null;
            var set = new DataSet();
            try
            {
                input = new StringReader(xml);
                reader = new XmlTextReader(input);
                set.ReadXml(reader);
            }
            catch (Exception e)
            {
                set = null;
                Log.Error(e.Message + e.StackTrace);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (input != null)
                {
                    input.Close();
                }
            }
            if ((set != null) && (set.Tables.Count >= 1))
            {
                return set.Tables[0];
            }
            return null;
        }

        public static Dictionary<string, object>[] ToDictionarys(this DataTable dt)
        {
            if (dt == null)
            {
                return null;
            }
            var columns = dt.Columns.Cast<DataColumn>().ToArray();
            return dt.Rows.Cast<DataRow>().Select(r => columns.ToDictionary(c => c.ColumnName, c =>
            {
                var v = r[c];
                if (v is DBNull)
                {
                    v = c.DataType.DefaultValue();
                }
                return v;
            })).ToArray();
        }

        /// <summary>
        /// 注:T defaultValue 用于匿名泛型
        /// </summary>
        public static List<T> ToList<T>(this DataTable dt, T defaultValue = default(T))
        {
            var result = new List<T>();

            if (dt != null && dt.Rows.Count > 0)
            {
                var type = typeof(T);
                if (type != typeof(Nullable))
                {
                    var isAnonymousType = false;
                    var cns = dt.Columns;
                    FieldInfo[] fis;
                    //匿名类型
                    if (type.Name.Contains("AnonymousType"))
                    {
                        isAnonymousType = true;

                    }

                    fis = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

                    if (isAnonymousType)
                    {
                        #region anonymoustype deal
                        foreach (DataRow dr in dt.Rows)
                        {
                            var constructor = type.GetConstructors()[0];
                            var cps = constructor.GetParameters();
                            var ps = new List<object>();
                            foreach (var pi in cps)
                            {
                                var v = dr[pi.Name];
                                var pt = pi.ParameterType;
                                if (pt.IsArray && pt.Name != "Byte[]")
                                {
                                    var atype = pt.Assembly.GetType(pt.FullName.Replace("[]", ""));
                                    ps.Add(v.ToString().JsonToArray(atype));
                                }
                                else
                                {
                                    if (v is DBNull)
                                    {
                                        v = pt.DefaultValue();
                                    }
                                    ps.Add(v);
                                }
                            }
                            var obj = constructor.Invoke(ps.ToArray());
                            result.Add((T)obj);
                        }
                        #endregion
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            var constructor = type.GetConstructor(new Type[0]);
                            var obj = constructor.Invoke(new object[0]);
                            foreach (var fi in fis)
                            {
                                var fn = fi.Name.ToLower();
                                if (fn.StartsWith("_"))
                                {
                                    fn = fn.Substring(1);
                                }
                                if (cns.Contains(fn))
                                {
                                    var v = dr[fn];
                                    if (v.GetType() != typeof(DBNull))
                                    {
                                        if (fn == "updateflag")
                                        {
                                            fi.SetValue(obj, ((byte[])v).TimestampToString());
                                        }
                                        else
                                        {
                                            var ft = fi.FieldType;
                                            if (v is DateTime && ft == typeof(string))
                                            {
                                                fi.SetValue(obj, v.ConvertTo<DateTime>().ToString("yyyy-MM-dd HH:mm:ss"));
                                            }
                                            else
                                            {
                                                if (ft.IsArray && ft.Name != "Byte[]")
                                                {
                                                    var atype = type.Module.GetType(ft.Name.Replace("[]", ""));
                                                    fi.SetValue(obj, v.ToString().JsonToArray(atype));
                                                }
                                                else
                                                {
                                                    var vt = v.GetType();
                                                    var fgt = ft.GenericTypeArguments.FirstOrDefault();
                                                    fi.SetValue(obj, (ft == vt || fgt == vt) ? v : Convert.ChangeType(v, fgt ?? ft));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            result.Add((T)obj);
                        }
                    }
                }
            }

            return result;
        }

        public static List<T> ToList_bak20160806<T>(this DataTable dt, T defaultValue = default(T))
        {
            var result = new List<T>();

            if (dt != null && dt.Rows.Count > 0)
            {
                var type = typeof(T);
                if (type != typeof(Nullable))
                {
                    var isAnonymousType = false;
                    var cns = dt.Columns;
                    FieldInfo[] fis;
                    var isTable = type.BaseType.FullName.Contains("Utility.Data.Table");
                    if (isTable)
                    {
                        fis = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                    else
                    {
                        fis = type.GetFields();
                        if (fis.Length == 0)
                        {
                            //匿名类型
                            if (type.Name.Contains("AnonymousType"))
                            {
                                isAnonymousType = true;
                                fis = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                            }
                        }
                    }
                    if (isAnonymousType)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            var constructor = type.GetConstructors()[0];
                            var cps = constructor.GetParameters();
                            var ps = new List<object>();
                            foreach (var pi in cps)
                            {
                                var v = dr[pi.Name];
                                var pt = pi.ParameterType;
                                if (pt.IsArray && pt.Name != "Byte[]")
                                {
                                    var atype = pt.Assembly.GetType(pt.FullName.Replace("[]", ""));
                                    ps.Add(v.ToString().JsonToArray(atype));
                                }
                                else
                                {
                                    if (v is DBNull)
                                    {
                                        v = pt.DefaultValue();
                                    }
                                    ps.Add(v);
                                }
                            }
                            var obj = constructor.Invoke(ps.ToArray());
                            result.Add((T)obj);
                        }
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            var constructor = type.GetConstructor(new Type[0]);
                            var obj = constructor.Invoke(new object[0]);
                            foreach (var fi in fis)
                            {
                                var fn = fi.Name.ToLower();
                                if (fn.StartsWith("_"))
                                {
                                    fn = fn.Substring(1);
                                }
                                if (cns.Contains(fn))
                                {
                                    var v = dr[fn];
                                    if (v.GetType() != typeof(DBNull))
                                    {
                                        if (isTable && fn == "updateflag")
                                        {
                                            fi.SetValue(obj, ((byte[])v).TimestampToString());
                                        }
                                        else
                                        {
                                            var ft = fi.FieldType;
                                            if (v is DateTime && ft == typeof(string))
                                            {
                                                fi.SetValue(obj, v.ConvertTo<DateTime>().ToString("yyyy-MM-dd HH:mm:ss"));
                                            }
                                            else
                                            {
                                                if (ft.IsArray && ft.Name != "Byte[]")
                                                {
                                                    var atype = type.Module.GetType(ft.Name.Replace("[]", ""));
                                                    fi.SetValue(obj, v.ToString().JsonToArray(atype));
                                                }
                                                else
                                                {
                                                    var vt = v.GetType();
                                                    var fgt = ft.GenericTypeArguments.FirstOrDefault();
                                                    fi.SetValue(obj, (ft == vt || fgt == vt) ? v : Convert.ChangeType(v, fgt ?? ft));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            result.Add((T)obj);
                        }
                    }
                }
            }

            return result;
        }
        /// <summary>
        /// 对象集合转DataTable
        /// </summary>
        public static DataTable ToDataTable<T>(this IEnumerable<T> ts, DataTable dtSource)
        {
            if (ts == null)
            {
                return null;
            }
            try
            {
                var dt = dtSource.Clone();
                var type = typeof(T);
                var baseType = type.BaseType;
                var ctype = 1;
                var propertys = type.GetProperties();
                var fields = type.GetFields();
                if (baseType != null)
                {
                    var isTable = baseType.FullName.Contains("Utility.Data.Table");
                    if (isTable)
                    {
                        ctype = 0;
                    }
                }
                foreach (var t in ts)
                {
                    var dr = dt.NewRow();
                    switch (ctype)
                    {
                        case 0:
                            {
                                foreach (var property in propertys)
                                {
                                    dr[property.Name] = property.GetValue(t, null);
                                }
                            }
                            break;
                        case 1:
                            {
                                foreach (var field in fields)
                                {
                                    dr[field.Name] = field.GetValue(t);
                                }
                            }
                            break;
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
            }
            return null;
        }

        /// <summary>
        /// 将DataTable转化为Cson字符串
        /// </summary>
        public static string ToCsonString(this DataTable dt)
        {
            var sb = new StringBuilder();
            sb.Append(dt.Columns.Cast<DataColumn>().Select(a => a.ColumnName).Join("δ"));
            foreach (DataRow row in dt.Rows)
            {
                sb.Append("η").Append(row.ItemArray.Join("δ"));
            }
            return sb.ToString();
        }
    }
}