using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lfb.DataGrab;
using Lfb.DataGrabBll;
using NUnit.Framework;
namespace Lfb.DataGrab.Tests
{
    [TestFixture()]
    public class ToutiaoGatherTests
    {
        private ToutiaoGather bll;
        /// <summary>
        /// 全局setup,不能使用async OneTimeSetUpAttribute  or TestFixtureSetUp
        /// </summary>
        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {

        }

        /// <summary>
        /// 每次测试setup,不能使用async
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            bll = new ToutiaoGather();

        }
        [Test()]
        public void NewsUrlGatheringTest()
        {
            //http://www.toutiao.com/api/article/recent/?source=2&amp;category=news_car&amp;as=A18538618270765&amp;cp=581280A796A56E1&amp;_=1477576549309


            //var url = "http://www.toutiao.com/api/article/feed/?category=__all__&utm_source=toutiao&widen=0&max_behot_time=0&max_behot_time_tmp=0&as=A1D5B8000CC7350&cp=580C772325900E1";
            var url = "http://www.toutiao.com/api/article/feed/?category=__all__&utm_source=toutiao&widen=0&max_behot_time=0&max_behot_time_tmp=0&as=A1B5E83210C6E63&cp=5820D6CE66539E1";
            bll.GatheringAuthorUrlFromChannel(url, 100,0);
        }

        [Test()]
        public void AuthorNewsGatheringTest()
        {
            //var t = Comm.Tools.Utility.DateTimeFormat.ToJsTime(DateTime.Now);
            bll.GatheringNewsFromAuthor();
        }

        [Test()]
        public void DealAuthorDataTest()
        {
            //var t = Comm.Tools.Utility.DateTimeFormat.ToJsTime(DateTime.Now);
            //var url = "http://www.toutiao.com/m6781620630/";
            var url = bll.GetAuthorDataUrl("6781620630");
            var authorId = "6781620630";

            bll.DealAuthorData(url, authorId, "",0);
        }
        
        [Test()]
        public void GatherAuthorFromNewsTest()
        {
            
            bll.GatherAuthorFromNews();
        }
        

        [Test()]
        public void GatherNewsFromZtRecentTest()
        {
            var url0 = "http://toutiao.com/group/6353545386958782977/";
            var str = Global.GetToutiaoGroupId(url0);

            var url = "http://www.toutiao.com/api/article/recent/?source=2&count=20&category=%E7%BB%84%E5%9B%BE&max_behot_time=0&utm_source=toutiao&device_platform=web&offset=0&as=A1B508A27D30C8F&cp=582D607C78CFCE1&_=1479347343375";
            bll.GatherNewsFromZtRecent(url,0);
        }

        [Test()]
        public void GatherRelationNewsFromAuthorTest()
        {
            bll.GatherRelationNewsFromAuthor();
        }
        [Test()]
        public void ModifyUrlTest()
        {
            //bll.ModifyUrl("");
        }
        [Test()]
        public void GetCpandAsTest()
        {
            string strCp;
            string strAs;
            bll.GetCpandAs(out strAs,out strCp);
        }
        [Test()]
        public void GetProxyListTest()
        {
            var list = ProxyDeal.ProxyList;
            ProxyDeal.GetProxyList();
            list = ProxyDeal.ProxyList;

        }

        [Test()]
        public void ModifyUrlMax_behot_timeTest()
        {
            var url =
                "http://www.toutiao.com/api/article/feed/?category=__all__&utm_source=toutiao&widen=0&max_behot_time=1477194899&max_behot_time_tmp=1477194899&as=A135F8A09C7897E&cp=580C08C9477EAE1";
            var result = bll.ModifyUrlMax_behot_time(url, "1481111222");
            Assert.IsTrue(result.Contains("1481111222"));
        }

        [Test()]
        public void GetContentTest()
        {
            var str = HttpHelper.GetContent("http://www.toutiao.com/item/6229498529518191106/", Encoding.UTF8);
            //var str = HttpHelper.GetContentByMobileAgent("http://omgmta.qq.com/mstat/report/?index=1479869384", Encoding.UTF8);
            Console.WriteLine(str);
        }
    }
}
