using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Linq;
using System.Threading;
using Aspose.Cells;

namespace Comm.Tools.Utility
{
    public static class FileHelper
    {
        static readonly Log Log = new Log("System");

        /// <summary>
        /// 2003（Microsoft.Jet.Oledb.4.0）
        /// string strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'", excelFilePath);

        /// 2007（Microsoft.ACE.OLEDB.12.0）
        /// string strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'", excelFilePath);
        /// </summary>
        /// <param name="path"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(this string path, string columns)
        {
            try
            {
                var dt = new DataTable();
                var strCon = "";
                if (path.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                {
                    strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;data source=" + path;
                }
                else
                {
                    strCon = "Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=Excel 8.0;data source=" + path;
                }

                var _connExcel = new OleDbConnection(strCon);
                _connExcel.Open();
                var dtDATA = _connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                var sheetName = dtDATA.Rows[0][2].ToString().Trim();
                _connExcel.Close();

                var myConn = new OleDbConnection(strCon);
                var strCom = " SELECT {0} FROM [{1}]".Formats(columns, sheetName);
                myConn.Open();
                var myCommand = new OleDbDataAdapter(strCom, myConn);
                myCommand.Fill(dt);
                myConn.Close();

                return dt;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return null;
            }
        }

        public static DataTable ExcelToDataTable(this string path)
        {
            try
            {
                var book = new Workbook(path);
                var sheet = book.Worksheets[0];
                var cells = sheet.Cells;
                //获取excel中的数据保存到一个datatable中
                var dt_Import = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);
                // dt_Import.
                return dt_Import;
            }
            catch (Exception e)
            {
                Log.Error(e.Message + e.StackTrace);
                return null;
            }
        }

        public static DataSet ExcelToDataSet(this string path)
        {
            var book = new Workbook(path);
            var sheets = book.Worksheets;
            var ds = new DataSet();
            foreach (Worksheet sheet in sheets)
            {
                var cells = sheet.Cells;
                if (cells.MaxDataColumn <= 0)
                {
                    continue;
                }
                cells.ClearFormats(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1);
                var dc = cells.MaxDataColumn + 1;
                var dels = new List<int>();
                for (var i = 0; i < dc; i++)
                {
                    if (cells[0, i].StringValue.IsNullOrEmpty())
                    {
                        dels.Add(i);
                    }
                }
                var sortDels = dels.OrderByDescending(a => a).ToArray();
                if (sortDels.Length > 0)
                {
                    foreach (var i in sortDels)
                    {
                        cells.DeleteColumn(i);
                    }
                }
                //获取excel中的数据保存到一个datatable中
                var dtImport = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, true);
                dtImport.TableName = sheet.Name;
                ds.Tables.Add(dtImport);
            }
            GC.Collect();
            return ds;
        }

        public static string Md5(this FileStream fileStream, bool toUper = true)
        {
            try
            {
                var buffer = System.Security.Cryptography.MD5.Create().ComputeHash(fileStream);
                var builder = new StringBuilder();
                for (var i = 0; i < buffer.Length; i++)
                {
                    builder.Append(buffer[i].ToString("x2"));
                }
                return toUper ? builder.ToString().ToUpper() : builder.ToString();
            }
            finally
            {
                fileStream.Close();
            }
        }

        public static string Read(this string path)
        {
            return path.Read(Encoding.UTF8);
        }

        public static string Read(this string path, Encoding e)
        {
            StreamReader sr = null;
            try
            {
                if (File.Exists(path))
                {
                    sr = new StreamReader(path, e);
                    return sr.ReadToEnd();
                }
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 读取文件最后一行数据
        /// </summary>
        public static string ReadLastLine(this string path, Encoding e)
        {
            try
            {
                if (File.Exists(path))
                {
                    string oldValue = string.Empty, newValue;
                    using (var read = new StreamReader(path, true))
                    {
                        do
                        {
                            newValue = read.ReadLine();
                            oldValue = newValue != null ? newValue : oldValue;
                        } while (newValue != null);
                    }
                    return oldValue;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
            }
            return string.Empty;
        }

        public static string ReadLastLine(this string path)
        {
            return ReadLastLine(path, Encoding.UTF8);
        }

        /// <summary>
        /// 在指定文件最后一行追加保存
        /// </summary>
        public static void AppendLastWrite(this string path, string content, Encoding e = null, FileMode fileMode = FileMode.Create)
        {
            if (e == null)
            {
                e = Encoding.UTF8;
            }

            StreamWriter fs = null;
            try
            {
                fs = new StreamWriter(new FileStream(path, FileMode.Append, FileAccess.Write), e);
                fs.WriteLine(content);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 指定操作系统应创建新文件。如果文件已存在，它将被改写。
        /// </summary>
        public static bool Write(this string path, string content, Encoding e = null, FileMode fileMode = FileMode.Create)
        {
            var result = false;
            if (e == null)
            {
                e = Encoding.UTF8;
            }

            Mutex mutexFile = null;
            try
            {
                mutexFile = new Mutex(false, path.Md5());
                mutexFile.WaitOne();
            }
            catch
            {
            }

            try
            {
                var dir = new FileInfo(path).Directory.FullName;
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    using (var writer = new StreamWriter(fs, e))
                    {
                        writer.Write(content);
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            if (mutexFile != null)
            {
                try
                {
                    mutexFile.ReleaseMutex();
                    mutexFile.Close();
                }
                catch
                {
                }
            }
            return result;
        }

        /// <summary>
        /// 压缩文件和文件夹
        /// </summary>
        public static bool ZipCompress(this IEnumerable<string> paths, string baseDirPath, string targetZipPath)
        {
            try
            {
                var files = paths.GetFilePaths();

                using (Stream destination = new FileStream(targetZipPath, FileMode.Create, FileAccess.Write))
                {
                    using (var output = new GZipStream(destination, CompressionMode.Compress))
                    {
                        using (var bw = new BinaryWriter(output))
                        {
                            bw.Write(files.Count());
                            foreach (var f in files)
                            {
                                var destBuffer = File.ReadAllBytes(f);
                                bw.Write(f.Replace(baseDirPath, ""));
                                bw.Write(destBuffer.Length);
                                bw.Write(destBuffer, 0, destBuffer.Length);
                            }
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 压缩文件和文件夹为流
        /// </summary>
        public static byte[] ZipCompress(this IEnumerable<string> paths, string baseDirPath)
        {
            try
            {
                var files = paths.GetFilePaths();

                using (var stream = new MemoryStream())
                {
                    var output = new GZipStream(stream, CompressionMode.Compress);
                    var bw = new BinaryWriter(output);
                    bw.Write(files.Count());
                    foreach (var f in files)
                    {
                        var destBuffer = File.ReadAllBytes(f);
                        bw.Write(f.Replace(baseDirPath, ""));
                        bw.Write(destBuffer.Length);
                        bw.Write(destBuffer, 0, destBuffer.Length);
                    }
                    bw.Close();
                    output.Close();

                    return stream.ToArray();
                }
            }
            catch
            {
                return null;
            }
        }

        public static List<string> GetFilePaths(this IEnumerable<string> paths)
        {
            var files = new List<string>();

            foreach (var path in paths)
            {
                if (Directory.Exists(path))
                {
                    files.AddRange(Directory.GetFiles(path));
                    files.AddRange(Directory.GetDirectories(path).GetFilePaths());
                }
                else
                {
                    files.Add(path);
                }
            }

            return files;
        }

        /// <summary>
        /// 获取目录下所有文件（包括子文件夹下）
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static List<string> GetFilePaths(this string dir)
        {
            var files = new List<string>();

            if (Directory.Exists(dir))
            {
                files.AddRange(Directory.GetFiles(dir));
                files.AddRange(Directory.GetDirectories(dir).GetFilePaths());
            }
            else
            {
                files.Add(dir);
            }

            return files;
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        public static bool ZipDeCompress(this string zipPath, string targetDirPath)
        {
            try
            {
                using (Stream source = File.OpenRead(zipPath))
                {
                    source.Position = 0;
                    using (var input = new GZipStream(source, CompressionMode.Decompress, true).Copy())
                    {
                        input.Position = 0;
                        using (var br = new BinaryReader(input))
                        {
                            var len = br.ReadInt32();
                            for (var i = 0; i < len; i++)
                            {
                                //文件相对路径
                                var fileSubPath = br.ReadString();
                                //文件长度
                                var l = br.ReadInt32();
                                //文件保存路径
                                var path = targetDirPath + fileSubPath;
                                var fileInfo = new FileInfo(path);
                                if (!fileInfo.Directory.Exists)
                                {
                                    fileInfo.Directory.Create();
                                }
                                var bytes = br.ReadBytes(l);
                                try
                                {
                                    using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                                    {
                                        fs.Write(bytes, 0, bytes.Length);
                                        fs.Close();
                                    }

                                    Log.Info("解压缩成功 " + path);
                                }
                                catch (Exception e)
                                {
                                    Log.Error("解压缩失败 " + path + " " + e.Message + e.StackTrace);
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 解压缩部分文件
        /// </summary>
        public static bool ZipDeCompress(this string zipPath, IEnumerable<string> files, string targetDirPath)
        {
            try
            {
                if (files.Count() > 0)
                {
                    using (Stream source = File.OpenRead(zipPath))
                    {
                        source.Position = 0;
                        using (var input = new GZipStream(source, CompressionMode.Decompress, true).Copy())
                        {
                            input.Position = 0;
                            using (var br = new BinaryReader(input))
                            {
                                var len = br.ReadInt32();
                                for (var i = 0; i < len; i++)
                                {
                                    //文件相对路径
                                    var fileSubPath = br.ReadString();
                                    //文件长度
                                    var l = br.ReadInt32();
                                    if (files.Contains(fileSubPath))
                                    {
                                        //文件保存路径
                                        var path = targetDirPath + fileSubPath;
                                        var fileInfo = new FileInfo(path);
                                        if (!fileInfo.Directory.Exists)
                                        {
                                            fileInfo.Directory.Create();
                                        }
                                        var bytes = br.ReadBytes(l);
                                        try
                                        {
                                            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                                            {
                                                fs.Write(bytes, 0, bytes.Length);
                                                fs.Close();
                                            }

                                            Log.Info("解压缩成功 " + path);
                                        }
                                        catch (Exception e)
                                        {
                                            Log.Error("解压缩失败 " + path + " " + e.Message + e.StackTrace);
                                        }
                                    }
                                    else
                                    {
                                        br.ReadBytes(l);
                                    }
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static void DeleteFile(this string filePath)
        {
            //如果文件存在
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}