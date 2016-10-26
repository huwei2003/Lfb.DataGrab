using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml;

namespace Comm.Tools.Utility
{
    public static class DataSetHelper
    {
        static readonly Log Log = new Log("System");

        public static Dictionary<string, object>[][] ToDictionarys(this DataSet ds)
        {
            return ds == null ? null : ds.Tables.Cast<DataTable>().Select(a => a.ToDictionarys()).ToArray();
        }

        public static DataSet ToDataSet(this string xml)
        {
            if (xml.IsNullOrEmpty())
            {
                return null;
            }
            StringReader input = null;
            XmlTextReader reader = null;
            var ds = new DataSet();
            try
            {
                input = new StringReader(xml);
                reader = new XmlTextReader(input);
                ds.ReadXml(reader);
            }
            catch (Exception e)
            {
                ds = null;
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
            if ((ds != null) && (ds.Tables.Count >= 1))
            {
                return ds;
            }
            return null;
        }
    }
}