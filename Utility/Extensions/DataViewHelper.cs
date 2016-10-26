using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Comm.Tools.Utility
{
    public static class DataViewHelper
    {
        public static Dictionary<string, object>[] ToDictionarys(this IEnumerable<DataRowView> drs)
        {
            if (drs == null)
            {
                return null;
            }
            var rs = drs.ToArray();
            var columns = rs.First().DataView.Table.Columns.Cast<DataColumn>().ToArray();
            return rs.Select(r => columns.ToDictionary(c => c.ColumnName, c =>
            {
                var v = r[c.ColumnName];
                if (v is DBNull)
                {
                    v = c.DataType.DefaultValue();
                }
                return v;
            })).ToArray();
        }

        public static Dictionary<string, object>[] ToDictionarys(this DataView dv)
        {
            if (dv == null)
            {
                return null;
            }
            return dv.ToTable().ToDictionarys();
        }
    }
}
