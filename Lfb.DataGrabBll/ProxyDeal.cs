using System.Collections.Generic;
using System.Text;
using System.Threading;
using Lib.Csharp.Tools;

namespace Lfb.DataGrabBll
{
    /// <summary>
    /// 代理处理类
    /// </summary>
    public class ProxyDeal
    {
        public static List<string> ProxyList;


        public static void GetProxyList()
        {
            //var ProxyList = (List<string>)Lib.Csharp.Tools.AppCache.Get("ProxyIpListForHttp");
            try
            {
                //ProxyList = Comm.Tools.Utility.Cache.GetCache<List<string>>("ProxyIpListForHttp");

                if (ProxyList == null)
                {
                    ProxyList = new List<string>();
                }

                #region === 取代理ip list ===
                Global.GetProxyIpUrl = Global.GetProxyIpUrl.Replace("amp;", "");
                string strContent = HttpHelper.GetContent(Global.GetProxyIpUrl, Encoding.UTF8);
                if (!string.IsNullOrWhiteSpace(strContent))
                {
                    try
                    {
                        var strArr = strContent.Replace("\r\n", ";").Split(';');
                        if (strArr.Length > 0)
                        {
                            foreach (var item in strArr)
                            {
                                if (!string.IsNullOrWhiteSpace(item))
                                {
                                    if (!ProxyList.Contains(item))
                                    {
                                        //访问百度，可以访问的才加入list
                                        strContent = HttpHelper.GetContentByMobileAgentForTestProxy("http://www.toutiao.com/",
                                            Encoding.UTF8, item);
                                        if (!string.IsNullOrWhiteSpace(strContent))
                                        {
                                            if (ProxyList.Count >= Global.ProxyPoolSize)
                                            {
                                                ProxyList.RemoveAt(0);
                                                Thread.Sleep(5 * 60 * 1000);
                                            }
                                            ProxyList.Add(item);
                                            Log.Info("代理:" + item + "可用 目前可用个数="+ProxyList.Count);
                                        }
                                        else
                                        {
                                            Log.Info("代理:" + item + "不可用");
                                        }
                                        Thread.Sleep(1000);
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                #endregion

                //数量太少则重复取
                if (ProxyList.Count < Global.ProxyPoolSize)
                {
                    Thread.Sleep(2*60*1000);
                    GetProxyList();
                }
                //Comm.Tools.Utility.Cache.SetCache("ProxyIpListForHttp", ProxyList, 3600);
                //Log.Info("可用代理个数:" + ProxyList.Count);
                //Lib.Csharp.Tools.AppCache.AddCache("ProxyIpListForHttp", ProxyList,1);
            }
            catch
            {
            }
        }
    }
}
