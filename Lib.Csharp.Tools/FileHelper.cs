using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Lib.Csharp.Tools.Security;

namespace Lib.Csharp.Tools
{
    public class FileHelper
    {
        #region 静态底层操作类
        public static string Reader(string path)
        {
            return Reader(HttpContext.Current.Server.MapPath(path), "gb2312");
        }

        public static void Delete(string path)
        {
            EDelete(HttpContext.Current.Server.MapPath(path));
        }

        public static void Writer(string path, string text)
        {
            Writer(HttpContext.Current.Server.MapPath(path), text, "gb2312");
        }

        public static string Replace(string source, Hashtable ht)
        {
            foreach (DictionaryEntry de in ht)
            {
                source = source.Replace(de.Key.ToString(), de.Value.ToString());
            }
            return source;
        }
        /// <summary>
        /// 生成静态方法
        /// </summary>
        /// <param name="tempPath">模板文件路径</param>
        /// <param name="htmlPath">要生成的静态页面路径</param>
        /// <param name="ht">替换的内容组</param>
        /// <param name="readCoding">读文件的编码</param>
        /// <param name="writecoding">写文件的编码</param>
        public static void ToStaic(string tempPath, string htmlPath, Hashtable ht, string readCoding, string writecoding)
        {
            string tempContent = Reader(tempPath, readCoding);
            string htmlContent = Replace(tempContent, ht);
            Writer(htmlPath, htmlContent, writecoding);

        }
        private static StreamReader _sr;
        private static StreamWriter _sw;

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="coding">文件编码</param>
        /// <returns></returns>
        public static string Reader(string path, string coding)
        {
            string str = "";
            if (File.Exists(path))
            {
                try
                {
                    _sr = new StreamReader(path, Encoding.GetEncoding(coding));
                    str = _sr.ReadToEnd();
                    _sr.Close();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }

            return str;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="text">写入内容</param>
        /// <param name="coding">文件编码</param>
        public static void Writer(string path, string text, string coding)
        {
            if (File.Exists(path))
            {
                EDelete(path);
            }
            try
            {
                if (coding.ToUpper() == "UTF-8")
                {
                    _sw = new StreamWriter(path, false, Encoding.UTF8);
                }
                else
                {
                    _sw = new StreamWriter(path, false, Encoding.GetEncoding(coding));
                }
                _sw.WriteLine(text);
                _sw.Flush();
                _sw.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        private static void EDelete(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }
        }

        /// <summary>
        /// 根据指定路径，获取其下所有文件列表，
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <returns>文件名 list</returns>
        public static List<string> GetFileInfo(string filePath)
        {
            FileSystemInfo fileinfo = new DirectoryInfo(filePath);
            var filelist = ListFileSort(fileinfo);
            return filelist;
        }

        /// <summary>
        /// 获取其下所有文件列表
        /// </summary>
        /// <param name="fileinfo"></param>
        /// <returns></returns>
        public static List<string> ListFileSort(FileSystemInfo fileinfo)
        {
            var filelist = new List<string>();
            var indent = 0;
            if (!fileinfo.Exists) return null;
            var dirinfo = fileinfo as DirectoryInfo;
            if (dirinfo == null) return null; //不是目录
            indent++;//缩进加一
            var files = dirinfo.GetFileSystemInfos();
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i] as FileInfo;
                if (file != null) // 是文件
                {
                    filelist.Add(file.Name);

                }
                else   //是目录
                {
                    //this.richTextBox1.Text += files[i].FullName + "/r/n/r/n";
                    //sb.Append(files[i].FullName + "/r/n/r/n");
                    ListFileSort(files[i]);  //对子目录进行递归调用
                }
            }
            indent--;//缩进减一
            return filelist;
        }

        #endregion

        #region === 基础操作 ===

        public static string Read( string path)
        {
            return Read(path,Encoding.UTF8);
        }

        public static string Read( string path, Encoding e)
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
        public static string ReadLastLine( string path, Encoding e)
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
                //Log.Error(ex.Message + ex.StackTrace);
            }
            return string.Empty;
        }

        public static string ReadLastLine( string path)
        {
            return ReadLastLine(path, Encoding.UTF8);
        }

        /// <summary>
        /// 在指定文件最后一行追加保存
        /// </summary>
        public static void AppendLastWrite( string path, string content, Encoding e = null, FileMode fileMode = FileMode.Create)
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
                //Log.Error(ex.Message);
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
        public static bool Write(string path, string content, Encoding e = null, FileMode fileMode = FileMode.Create)
        {
            var result = false;
            if (e == null)
            {
                e = Encoding.UTF8;
            }

            Mutex mutexFile = null;
            try
            {
                mutexFile = new Mutex(false, StringSecurityHelper.Md5(path));
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
                //Log.Error(ex.Message);
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
        public static bool ZipCompress( IEnumerable<string> paths, string baseDirPath, string targetZipPath)
        {
            try
            {
                var files = GetFilePaths(paths);

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
        public static byte[] ZipCompress( IEnumerable<string> paths, string baseDirPath)
        {
            try
            {
                var files = GetFilePaths(paths);

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

        public static List<string> GetFilePaths( IEnumerable<string> paths)
        {
            var files = new List<string>();

            foreach (var path in paths)
            {
                if (Directory.Exists(path))
                {
                    files.AddRange(Directory.GetFiles(path));
                    files.AddRange(GetFilePaths(Directory.GetDirectories(path)));
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
        public static List<string> GetFilePaths( string dir)
        {
            var files = new List<string>();

            if (Directory.Exists(dir))
            {
                files.AddRange(Directory.GetFiles(dir));
                files.AddRange(GetFilePaths(Directory.GetDirectories(dir)));
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
        public static bool ZipDeCompress( string zipPath, string targetDirPath)
        {
            try
            {
                using (Stream source = File.OpenRead(zipPath))
                {
                    source.Position = 0;
                    using (var input = StreamHelper.Copy(new GZipStream(source, CompressionMode.Decompress, true)))
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

                                    //Log.Info("解压缩成功 " + path);
                                }
                                catch (Exception e)
                                {
                                    //Log.Error("解压缩失败 " + path + " " + e.Message + e.StackTrace);
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
        public static bool ZipDeCompress( string zipPath, IEnumerable<string> files, string targetDirPath)
        {
            try
            {
                if (files.Count() > 0)
                {
                    using (Stream source = File.OpenRead(zipPath))
                    {
                        source.Position = 0;
                        using (var input = StreamHelper.Copy(new GZipStream(source, CompressionMode.Decompress, true)))
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

                                            //Log.Info("解压缩成功 " + path);
                                        }
                                        catch (Exception e)
                                        {
                                            //Log.Error("解压缩失败 " + path + " " + e.Message + e.StackTrace);
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
        public static void DeleteFile(string filePath)
        {
            //如果文件存在
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// 将文件转换成byte[] 数组
        /// </summary>
        /// <param name="fileUrl">文件路径文件名称</param>
        /// <returns>byte[]</returns>
        public static byte[] GetFileData(string fileUrl)
        {
            FileStream fs = new FileStream(fileUrl, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);

                return buffur;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                //关闭资源
                fs.Close();
            }
        }


        /// <summary>
        /// 将文件转换成byte[] 数组
        /// </summary>
        /// <param name="fileUrl">文件路径文件名称</param>
        /// <returns>byte[]</returns>

        public static byte[] AuthGetFileData(string fileUrl)
        {
            using (FileStream fs = new FileStream(fileUrl, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                byte[] buffur = new byte[fs.Length];
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(buffur);
                    bw.Close();
                }
                return buffur;
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="data">文件字节数组</param>
        /// <param name="filePath">文件路径</param>
        /// <returns>true or false</returns>
        public static bool SaveFile(byte[] data, string filePath)
        {
            try
            {
                MemoryStream m = new MemoryStream(data);
                var path = Path.GetDirectoryName(filePath);
                if (string.IsNullOrEmpty(path))
                    return false;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
                m.WriteTo(fs);
                m.Close();
                fs.Close();
                // ReSharper disable once RedundantAssignment
                m = null;
                // ReSharper disable once RedundantAssignment
                fs = null;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
