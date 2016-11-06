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
            var url = "http://www.toutiao.com/api/article/feed/?category=news_tech&amp;utm_source=toutiao&amp;widen=0&amp;max_behot_time=0&amp;max_behot_time_tmp=0&amp;as=A1A5E89191FFB6B&amp;cp=5811FF9B161B1E1";
            bll.AuthorUrlGathering(url, 100);
        }

        [Test()]
        public void AuthorNewsGatheringTest()
        {
            bll.AuthorNewsGathering();
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

            HttpHelper.GetContentByMobileAgent("www.163.com", Encoding.UTF8);
        }

        [Test()]
        public void ModifyUrlMax_behot_timeTest()
        {
            var url =
                "http://www.toutiao.com/api/article/feed/?category=__all__&utm_source=toutiao&widen=0&max_behot_time=1477194899&max_behot_time_tmp=1477194899&as=A135F8A09C7897E&cp=580C08C9477EAE1";
            var result = bll.ModifyUrlMax_behot_time(url, "1481111222");
            Assert.IsTrue(result.Contains("1481111222"));
        }
    }
}
