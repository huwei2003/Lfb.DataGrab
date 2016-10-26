using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.in2bits.MyXls;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// 利用myxls 第三方组件生成，读取xls的类(推荐用)
    /// </summary>
    public class MyXls
    {
        /// <summary>
        /// 根据datatable 生成excel文件
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="filePath">保存的文件路径,主路径保存在web.config,这里只要传文件名</param>
        /// <param name="sheetName">excel的sheet表名</param>
        /// <returns></returns>
        public static string DataTableToExcel(System.Data.DataTable dt, string filePath, string sheetName)
        {
            if (sheetName.Length < 1)
            {
                sheetName = Path.GetFileNameWithoutExtension(filePath);
            }
            var msg = "";
            var xls = new XlsDocument();

            try
            {
                //最大行限制
                var maxRowCount = 60000;
                var rowCount = dt.Rows.Count;
                var colCount = dt.Columns.Count;

                if (rowCount > 0 && rowCount <= maxRowCount)
                {
                    #region 不超过60000行时
                    var sheet = xls.Workbook.Worksheets.Add(sheetName);
                    for (var j = 0; j < colCount; j++)
                    {
                        sheet.Cells.Add(1, j + 1, dt.Columns[j].ColumnName.ToString());
                    }
                    for (var j = 0; j < rowCount; j++)
                    {
                        for (var k = 0; k < colCount; k++)
                        {
                            if (dt.Rows[j][k] != null && dt.Rows[j][k].ToString().Length > 0)
                            {
                                sheet.Cells.Add(j + 2, k + 1, dt.Rows[j][k]);
                            }
                            else
                            {
                                //是空的话则视数据类型不同插入0或空格
                                if (dt.Columns[k].DataType == typeof(float) || dt.Columns[k].DataType == typeof(int) || dt.Columns[k].DataType == typeof(double) || dt.Columns[k].DataType == typeof(decimal))
                                {

                                    sheet.Cells.Add(j + 2, k + 1, 0);
                                }
                                else
                                {
                                    sheet.Cells.Add(j + 2, k + 1, "");
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 超过sheet表单的就再创jian sheet表单
                    var sheetCount = 1; //sheet表单个数

                    if (rowCount % maxRowCount == 0)
                    {
                        sheetCount = rowCount / maxRowCount;
                    }
                    else
                    {
                        sheetCount = rowCount / maxRowCount + 1;
                    }
                    var flag = 1;
                    for (var m = 0; m < sheetCount; m++)
                    {
                        //添加一个sheet表单
                        var sheet = xls.Workbook.Worksheets.Add("第" + (m + 1) + "页数据");
                        //如果不是最后一个表单的话并且最后一个sheet表单数据不等于60000 
                        if (flag == sheetCount && (rowCount % maxRowCount != 0))
                        {
                            #region 最后一个sheet的数据
                            var newrowCount = rowCount - ((flag - 1) * maxRowCount);

                            var rowIndex = 0;
                            for (var j = 0; j < colCount; j++)
                            {
                                sheet.Cells.Add(1, j + 1, dt.Columns[j].ColumnName.ToString());
                            }
                            int startIndex = (flag - 1) * maxRowCount;
                            for (var n = startIndex; n < startIndex + newrowCount; n++)
                            {
                                for (var t = 0; t < colCount; t++)
                                {
                                    //sheet.Cells.Add(RowIndex + 2, t + 1, dt.Rows[n][t]);
                                    if (dt.Rows[n][t] != null && dt.Rows[n][t].ToString().Length > 0)
                                    {
                                        sheet.Cells.Add(rowIndex + 2, t + 1, dt.Rows[n][t]);
                                    }
                                    else
                                    {
                                        //是空的话则视数据类型不同插入0或空格
                                        if (dt.Columns[t].DataType == typeof(float) || dt.Columns[t].DataType == typeof(int) || dt.Columns[t].DataType == typeof(double) || dt.Columns[t].DataType == typeof(decimal))
                                        {
                                            sheet.Cells.Add(rowIndex + 2, t + 1, 0);
                                        }
                                        else
                                        {
                                            sheet.Cells.Add(rowIndex + 2, t + 1, "");
                                        }
                                    }


                                }
                                rowIndex++;
                            }
                            #endregion
                        }
                        else
                        {
                            #region
                            for (var j = 0; j < colCount; j++)
                            {
                                sheet.Cells.Add(1, j + 1, dt.Columns[j].ColumnName.ToString());
                            }
                            var startIndex = (flag - 1) * maxRowCount;
                            var rowIndex = 0;
                            for (var n = startIndex; n < startIndex + maxRowCount; n++)
                            {
                                for (var t = 0; t < colCount; t++)
                                {
                                    //sheet.Cells.Add(rowIndex + 2, t + 1, dt.Rows[n][t]);
                                    if (dt.Rows[n][t] != null && dt.Rows[n][t].ToString().Length > 0)
                                    {
                                        sheet.Cells.Add(rowIndex + 2, t + 1, dt.Rows[n][t]);
                                    }
                                    else
                                    {
                                        //是空的话则视数据类型不同插入0或空格
                                        if (dt.Columns[t].DataType == typeof(float) || dt.Columns[t].DataType == typeof(int) || dt.Columns[t].DataType == typeof(double) || dt.Columns[t].DataType == typeof(decimal))
                                        {
                                            sheet.Cells.Add(rowIndex + 2, t + 1, 0);
                                        }
                                        else
                                        {
                                            sheet.Cells.Add(rowIndex + 2, t + 1, "");
                                        }
                                    }
                                }
                                rowIndex++;
                            }
                            #endregion
                        }
                        flag++;
                    }
                    #endregion
                }
                #region 客户端保存

                //第一种保存方式：适用于Winform / WebForm 
                //string strFilePath = Path.GetDirectoryName(filePath);
                //xls.FileName = sheetName;
                //xls.Save(strFilePath);
                //xls = null;
                //---end---

                //文件路径合并成全路径
                //string mainPath = System.Configuration.ConfigurationManager.AppSettings["ExcelPath"];
                //filePath = mainPath + filePath;

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                string strFilePath = Path.GetDirectoryName(filePath);
                xls.FileName = Path.GetFileName(filePath);
                xls.Save(strFilePath);
                xls = null;

                //第二种保存方式适用于WebForm，可以选择保存路径

                using (var ms = new MemoryStream())
                {
                    //xls.Save(ms); 
                    //ms.Flush();  
                    //ms.Position = 0;
                    //   byte[] data = ms.ToArray();  
                    //System.Web.HttpContext.Current.Response.BinaryWrite(data); 
                    //xls = null;  

                    var fi = new FileInfo(filePath);
                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.Charset = "UTF-8";
                    System.Web.HttpContext.Current.Response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel"; 

                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + fi.Name));
                    System.Web.HttpContext.Current.Response.WriteFile(fi.FullName);

                }
                #endregion
            }
            catch
            {
                xls = null;
                GC.Collect();
                return "出现异常";
            }
            finally
            {
                xls = null;
                GC.Collect();
            }
            return msg;
        }

        /// <summary>
        /// 专用，同时插入三个dt,各为一个sheet,且不考虑行数超限
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <param name="dt3"></param>
        /// <param name="filePath"></param>
        /// <param name="sheetName1"></param>
        /// <param name="sheetName2"></param>
        /// <param name="sheetName3"></param>
        /// <returns></returns>
        public static string DataTableToExcel(System.Data.DataTable dt1, System.Data.DataTable dt2, System.Data.DataTable dt3, string filePath, string sheetName1, string sheetName2, string sheetName3)
        {
            //if (sheetName1.Length < 1)
            //{
            //    sheetName1 = Path.GetFileNameWithoutExtension(filePath);
            //}
            string msg = "";
            var xls = new XlsDocument();

            try
            {
                //最大行限制
                var maxRowCount = 60000;
                var rowCount = dt1.Rows.Count;
                var colCount = dt1.Columns.Count;

                if (rowCount > 0 && rowCount <= maxRowCount)
                {
                    var sheet = xls.Workbook.Worksheets.Add(sheetName1);
                    for (var j = 0; j < colCount; j++)
                    {
                        sheet.Cells.Add(1, j + 1, dt1.Columns[j].ColumnName.ToString());
                    }
                    for (var j = 0; j < rowCount; j++)
                    {
                        for (var k = 0; k < colCount; k++)
                        {
                            //sheet.Cells.Add(j + 2, k + 1, dt1.Rows[j][k]);
                            if (dt1.Rows[j][k] != null && dt1.Rows[j][k].ToString().Length > 0)
                            {
                                sheet.Cells.Add(j + 2, k + 1, dt1.Rows[j][k]);
                            }
                            else
                            {
                                //是空的话则视数据类型不同插入0或空格
                                if (dt1.Columns[k].DataType == typeof(float) || dt1.Columns[k].DataType == typeof(int) || dt1.Columns[k].DataType == typeof(double) || dt1.Columns[k].DataType == typeof(decimal))
                                {
                                    sheet.Cells.Add(j + 2, k + 1, 0);
                                }
                                else
                                {
                                    sheet.Cells.Add(j + 2, k + 1, "");
                                }
                            }
                        }
                    }
                }

                var rowCount2 = dt2.Rows.Count;
                var colCount2 = dt2.Columns.Count;

                if (rowCount > 0 && rowCount <= maxRowCount)
                {
                    var sheet = xls.Workbook.Worksheets.Add(sheetName2);
                    for (var j = 0; j < colCount; j++)
                    {
                        sheet.Cells.Add(1, j + 1, dt2.Columns[j].ColumnName.ToString());
                    }
                    for (var j = 0; j < rowCount; j++)
                    {
                        for (var k = 0; k < colCount; k++)
                        {
                            //sheet.Cells.Add(j + 2, k + 1, dt2.Rows[j][k]);
                            if (dt2.Rows[j][k] != null && dt2.Rows[j][k].ToString().Length > 0)
                            {
                                sheet.Cells.Add(j + 2, k + 1, dt2.Rows[j][k]);
                            }
                            else
                            {
                                //是空的话则视数据类型不同插入0或空格
                                if (dt2.Columns[k].DataType == typeof(float) || dt2.Columns[k].DataType == typeof(int) || dt2.Columns[k].DataType == typeof(double) || dt2.Columns[k].DataType == typeof(decimal))
                                {
                                    sheet.Cells.Add(j + 2, k + 1, 0);
                                }
                                else
                                {
                                    sheet.Cells.Add(j + 2, k + 1, "");
                                }
                            }

                        }
                    }
                }

                var rowCount3 = dt3.Rows.Count;
                var colCount3 = dt3.Columns.Count;

                if (rowCount > 0 && rowCount <= maxRowCount)
                {
                    var sheet = xls.Workbook.Worksheets.Add(sheetName3);
                    for (var j = 0; j < colCount; j++)
                    {
                        sheet.Cells.Add(1, j + 1, dt3.Columns[j].ColumnName.ToString());
                    }
                    for (var j = 0; j < rowCount; j++)
                    {
                        for (var k = 0; k < colCount; k++)
                        {
                            //sheet.Cells.Add(j + 2, k + 1, dt3.Rows[j][k]);
                            if (dt3.Rows[j][k] != null && dt3.Rows[j][k].ToString().Length > 0)
                            {
                                sheet.Cells.Add(j + 2, k + 1, dt3.Rows[j][k]);
                            }
                            else
                            {
                                //是空的话则视数据类型不同插入0或空格
                                if (dt3.Columns[k].DataType == typeof(float) || dt3.Columns[k].DataType == typeof(int) || dt3.Columns[k].DataType == typeof(double) || dt3.Columns[k].DataType == typeof(decimal))
                                {
                                    sheet.Cells.Add(j + 2, k + 1, 0);
                                }
                                else
                                {
                                    sheet.Cells.Add(j + 2, k + 1, "");
                                }
                            }
                        }
                    }
                }

                #region 客户端保存

                //第一种保存方式：适用于Winform / WebForm 
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                var strFilePath = Path.GetDirectoryName(filePath);
                xls.FileName = Path.GetFileName(filePath);
                xls.Save(strFilePath);
                xls = null;
                //第二种保存方式适用于WebForm，可以选择保存路径

                //using (MemoryStream ms = new MemoryStream()) 
                //{  
                // xls.Save(ms); 
                // ms.Flush();  
                // ms.Position = 0 

                // xls = null;  
                // HttpResponse response = System.Web.HttpContext.Current.Response; 
                // response.Clear();  
                // response.Charset = "UTF-8";  
                // response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel"; 

                // System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", 
                //string.Format("attachment; filename=" + xlsname);  
                // //System.Web.HttpContext.Current.Response.WriteFile(fi.FullName); 

                // byte[] data = ms.ToArray();  
                // System.Web.HttpContext.Current.Response.BinaryWrite(data); 
                //}  
                #endregion
            }
            catch
            {
                xls = null;
                GC.Collect();
                return "出现异常";
            }
            finally
            {
                xls = null;
                GC.Collect();
            }
            return msg;
        }
    }
}
