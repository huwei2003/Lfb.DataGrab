using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Formatting = System.Xml.Formatting;
using Log = Lib.Csharp.Tools.Log;

namespace Lfb.DataGrabBll
{
    /// <summary>
    /// 用于site.xml文件操作
    /// </summary>
    public class XmlDeal
    {

        private static readonly object LockObj = typeof(XmlDeal);


        /// <summary>
        /// 服务配置文件
        /// </summary>

        public static string Path = AppDomain.CurrentDomain.BaseDirectory + "/Site.xml";




        /// <summary>
        /// 获取所有站点
        /// </summary>
        /// <returns>json string</returns>
        private static string GetAllSites()
        {
            var strJson = "{}";
            try
            {
                var xmlDoc = new XmlDocument();

                lock (LockObj)
                {
                    xmlDoc.Load(Path);
                    var root = xmlDoc.SelectSingleNode("Sites");
                    //XmlNodeList nodeList = root.SelectNodes("ServiceName");
                    if (root != null)
                    {
                        strJson = JsonConvert.SerializeXmlNode(root, Newtonsoft.Json.Formatting.None, true);
                    }

                    return strJson;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
                return "";
            }
        }
        public static List<SiteData> GetSitesInfo()
        {
            try
            {
                var str = XmlDeal.GetAllSites();

                var objs = JObject.Parse(str);
                var strArray = objs["Site"].ToString();
                var objArray = JArray.Parse(strArray);

                var list = new List<SiteData>();
                foreach (var obj in objArray)
                {
                    var token = (JToken)obj;
                    var siteName = token["SiteName"].ToString();
                    var url = token["Url"].ToString();
                    var newsType = Convert.ToInt32(token["NewsType"].ToString());
                    var waitSeconds = Convert.ToInt32(token["WaitSeconds"].ToString());
                    var nums = Convert.ToInt32(token["Nums"].ToString());
                    var newsTypeName = token["NewsTypeName"].ToString();

                    list.Add(new SiteData
                    {
                        SiteName = siteName,
                        Url = url,
                        NewsType = newsType,
                        WaitSeconds = waitSeconds,
                        Nums = nums,
                        NewsTypeName = newsTypeName,
                    });

                }
                return list;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 修改某个站点的url
        /// </summary>
        /// <param name="newsUrl"></param>
        /// <returns></returns>
        public static bool UpdateSite(string newsUrl)
        {
            var isHaveService = false;
            var siteName = "";

            try
            {
                #region 根据newsurl来判断sitename

                if (newsUrl.IndexOf("ifeng.com") > -1)
                {
                    siteName = "ifeng";
                }


                #endregion

                var xmlDoc = new XmlDocument();
                lock (LockObj)
                {

                    xmlDoc.Load(Path);
                    var root = xmlDoc.SelectSingleNode("Sites");
                    if (root == null)
                        return false;

                    #region 在该服务节点下添加服务item

                    var servicenodelist1 = root.SelectNodes("Site");
                    if (servicenodelist1 != null && servicenodelist1.Count > 0)
                    {
                        foreach (var xn in servicenodelist1)
                        {

                            var xe = (XmlElement)xn;
                            if (xe.FirstChild.InnerText.ToLower() == siteName)
                            {
                                xe.LastChild.InnerText = newsUrl;
                            }
                        }
                    }

                    #endregion

                    xmlDoc.Save(Path);
                    //主动释放
                    xmlDoc.RemoveAll();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
