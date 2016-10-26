using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// ftp操作类,ftp上传，下载，创建目录，检查文件存在，删除文件
    /// </summary>
    public class FtpHelper
    {
        /// <summary>
        /// 从ftp上下载文件
        /// </summary>
        /// <param name="filePath">下载到本地的文件路径</param>
        /// <param name="fileSrc">ftp源文件夹</param>
        /// <param name="fileName">下载后的文件名</param>
        /// <param name="ftpServerIp"></param>
        /// <param name="ftpUserName"></param>
        /// <param name="ftpPwd"></param>
        public static void Download(string filePath, string fileSrc, string fileName, string ftpServerIp, string ftpUserName, string ftpPwd)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            using (var outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create))
            {
                var reqFtp = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIp + "/" + fileSrc));

                reqFtp.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFtp.UseBinary = true;
                reqFtp.Credentials = new NetworkCredential(ftpUserName, ftpPwd);
                using (var response = (FtpWebResponse)reqFtp.GetResponse())
                {
                    using (var ftpStream = response.GetResponseStream())
                    {
                        //long len = response.ContentLength;
                        var bufferSize = 2048;

                        var buffer = new byte[bufferSize];
                        if (ftpStream != null)
                        {
                            var readCount = ftpStream.Read(buffer, 0, bufferSize);
                            while (readCount > 0)
                            {
                                outputStream.Write(buffer, 0, readCount);
                                readCount = ftpStream.Read(buffer, 0, bufferSize);
                            }
                            ftpStream.Close();
                        }
                    }
                    response.Close();
                }
                outputStream.Close();
            }

        }

        /// <summary>
        /// 从服务器上传文件到FTP上
        /// </summary>
        /// <param name="sourceFolderPrex">目录前缀，固定不变的部分</param>
        /// <param name="sFileDstPath">源文件夹名称</param>
        /// <param name="folderName">ftp服务器的目标文件夹,完整目录去掉目录前缀</param>
        /// <param name="ftpServerIp">ftp ip</param>
        /// <param name="ftpUserName">用户名</param>
        /// <param name="ftpPwd">密码</param>
        public static void UploadFolder(string sourceFolderPrex, string sFileDstPath, string folderName, string ftpServerIp, string ftpUserName, string ftpPwd)
        {
            var dirinfo = new DirectoryInfo(sFileDstPath);
            var files = dirinfo.GetFileSystemInfos();
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i] as FileInfo;
                if (file != null) // 是文件
                {
                    //Console.WriteLine("file.FullName = " + file.FullName + " folderName=" + folderName);
                    UploadSmall(file.FullName, folderName, ftpServerIp, ftpUserName, ftpPwd);
                }
                else   //是目录
                {
                    //this.richTextBox1.Text += files[i].FullName + "/r/n/r/n";
                    //sb.Append(files[i].FullName + "/r/n/r/n");
                    var folder = files[i].FullName.Replace("\\\\", "\\").Replace(sourceFolderPrex, "").Replace("\\\\", "\\");
                    CreateDirectory(folder, ftpServerIp, ftpUserName, ftpPwd);
                    UploadFolder(sourceFolderPrex, files[i].FullName, files[i].FullName.Replace(sourceFolderPrex, ""), ftpServerIp, ftpUserName, ftpPwd);  //对子目录进行递归调用

                    //Console.WriteLine();
                }
            }
        }
        /// <summary>
        /// 从服务器上传文件到FTP上
        /// </summary>
        /// <param name="sFileDstPath">源文件名称</param>
        /// <param name="folderName">ftp服务器的目标文件夹</param>
        /// <param name="ftpServerIp">ftp ip</param>
        /// <param name="ftpUserName">用户名</param>
        /// <param name="ftpPwd">密码</param>
        public static void UploadSmall(string sFileDstPath, string folderName, string ftpServerIp, string ftpUserName, string ftpPwd)
        {
            try
            {
                var fileInf = new FileInfo(sFileDstPath);

                //Console.WriteLine("file:=ftp://" + ftpServerIp + "/" + folderName + "/" + fileInf.Name);

                var reqFtp =
                    (FtpWebRequest)
                        WebRequest.Create(new Uri("ftp://" + ftpServerIp + "/" + folderName + "/" + fileInf.Name));

                reqFtp.Credentials = new NetworkCredential(ftpUserName, ftpPwd);
                reqFtp.KeepAlive = true;
                reqFtp.Method = WebRequestMethods.Ftp.UploadFile;
                reqFtp.UseBinary = true;
                reqFtp.ContentLength = fileInf.Length;
                var buffLength = 81920;
                var buff = new byte[buffLength];

                using (var fs = fileInf.OpenRead())
                {
                    using (var strm = reqFtp.GetRequestStream())
                    {
                        var contentLen = fs.Read(buff, 0, buffLength);
                        while (contentLen != 0)
                        {
                            strm.Write(buff, 0, contentLen);
                            contentLen = fs.Read(buff, 0, buffLength);
                        }
                        strm.Close();
                    }
                    fs.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }


        /// <summary>
        /// 删除FTP上的文件
        /// </summary>
        /// <param name="arrName">要删除的文件数组</param>
        /// <param name="folderName">要删除文件所在的ftp文件夹</param>
        /// <param name="ftpServerIp"></param>
        /// <param name="ftpUserName"></param>
        /// <param name="ftpPwd"></param>
        public static void DeleteFtpFile(string[] arrName, string folderName, string ftpServerIp, string ftpUserName, string ftpPwd)
        {
            foreach (var imageName in arrName)
            {
                var fileList = GetFileList(folderName, ftpServerIp, ftpUserName, ftpPwd);
                for (var i = 0; i < fileList.Length; i++)
                {
                    var name = fileList[i];
                    if (name != imageName) continue;
                    var reqFtp = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIp + "/" + folderName + "/" + imageName));

                    reqFtp.Credentials = new NetworkCredential(ftpUserName, ftpPwd);
                    reqFtp.KeepAlive = false;
                    reqFtp.Method = WebRequestMethods.Ftp.DeleteFile;
                    reqFtp.UseBinary = true;

                    using (var response = (FtpWebResponse)reqFtp.GetResponse())
                    {
                        //long size = response.ContentLength;
                        using (var datastream = response.GetResponseStream())
                        {
                            if (datastream != null)
                            {
                                using (var sr = new StreamReader(datastream))
                                {
                                    sr.ReadToEnd();
                                    sr.Close();
                                }
                                datastream.Close();
                            }
                        }
                        response.Close();
                    }
                }

            }

        }
        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="folderName">ftp文件夹</param>
        /// <param name="ftpServerIp"></param>
        /// <param name="ftpUserName"></param>
        /// <param name="ftpPwd"></param>
        /// <returns></returns>
        public static string[] GetFileList(string folderName, string ftpServerIp, string ftpUserName, string ftpPwd)
        {
            var result = new StringBuilder();
            try
            {
                var reqFtp = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIp + "/" + folderName + "/"));

                reqFtp.UseBinary = true;
                reqFtp.Credentials = new NetworkCredential(ftpUserName, ftpPwd);
                reqFtp.Method = WebRequestMethods.Ftp.ListDirectory;

                var response = reqFtp.GetResponse();
                var rs = response.GetResponseStream();
                if (rs == null)
                {
                    return null;
                }
                var reader = new StreamReader(rs);

                var line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                // to remove the trailing '\n'        
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 从客户端上传文件到FTP上
        /// </summary>
        /// <param name="sFilePath">客户端文件上传类</param>
        /// <param name="filename">ftp文件名</param>
        /// <param name="folderName">ftp文件夹</param>
        /// <param name="ftpServerIp"></param>
        /// <param name="ftpUserName"></param>
        /// <param name="ftpPwd"></param>
        public static void UploadFtp(HttpPostedFile sFilePath, string filename, string folderName, string ftpServerIp, string ftpUserName, string ftpPwd)
        {
            //获取的服务器路径
            //FileInfo fileInf = new FileInfo(sFilePath);

            var reqFtp = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIp + "/" + folderName + "/" + filename));
            reqFtp.Credentials = new NetworkCredential(ftpUserName, ftpPwd);
            reqFtp.KeepAlive = false;
            reqFtp.Method = WebRequestMethods.Ftp.UploadFile;
            reqFtp.UseBinary = true;
            reqFtp.ContentLength = sFilePath.ContentLength;
            //设置缓存
            var buffLength = 2048;
            var buff = new byte[buffLength];
            using (var fs = sFilePath.InputStream)
            {
                using (var strm = reqFtp.GetRequestStream())
                {
                    int contentLen = fs.Read(buff, 0, buffLength);
                    while (contentLen != 0)
                    {
                        strm.Write(buff, 0, contentLen);
                        contentLen = fs.Read(buff, 0, buffLength);
                    }
                    strm.Close();
                }
                fs.Close();
            }

        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="folderName">ftp目录</param>
        /// <param name="ftpServerIp"></param>
        /// <param name="ftpUserName"></param>
        /// <param name="ftpPwd"></param>
        public static void CreateDirectory(string folderName, string ftpServerIp, string ftpUserName, string ftpPwd)
        {
            //创建日期目录
            try
            {
                var reqFtp = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIp + "/" + folderName));

                reqFtp.UseBinary = true;
                reqFtp.Credentials = new NetworkCredential(ftpUserName, ftpPwd);
                reqFtp.KeepAlive = false;
                reqFtp.Method = WebRequestMethods.Ftp.MakeDirectory;

                var response = (FtpWebResponse)reqFtp.GetResponse();
            }
            catch(Exception ex)
            {
                throw new ApplicationException("创建目录Fail"+ex.Message);
            }
        }

        private static readonly Regex RegexName = new Regex(@"[^\s]*$", RegexOptions.Compiled);
        /// <summary>
        /// 检查日期目录和文件是否存在
        /// </summary>
        /// <param name="folderName">文件(夹)名</param>
        /// <param name="ftpServerIp"></param>
        /// <param name="ftpUserName"></param>
        /// <param name="ftpPwd"></param>
        /// <returns></returns>
        public static bool CheckFileOrPath(string folderName, string ftpServerIp, string ftpUserName, string ftpPwd)
        {
            //检查一下日期目录是否存在
            //var reqFtp = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIp + "/"));
            var reqFtp = (FtpWebRequest)WebRequest.Create(new Uri("ftp://" + ftpServerIp + "/"));
            reqFtp.UseBinary = true;
            reqFtp.Credentials = new NetworkCredential(ftpUserName, ftpPwd);
            reqFtp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            var stream = reqFtp.GetResponse().GetResponseStream();
            if (stream == null)
                return false;
            using (var sr = new StreamReader(stream))
            {
                var line = sr.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    var gc = RegexName.Match(line).Groups;
                    if (gc.Count != 1)
                    {
                        throw new ApplicationException("FTP 返回的字串格式不正确");
                    }

                    var path = gc[0].Value;
                    if (path == folderName)
                    {
                        return true;
                    }

                    line = sr.ReadLine();
                }
            }
            return false;
        }

    }
}
