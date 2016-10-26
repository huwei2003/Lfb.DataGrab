using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lfb.DataGrab;
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
            
            var url = "http://www.toutiao.com/api/article/feed/?category=__all__&utm_source=toutiao&widen=0&max_behot_time=0&max_behot_time_tmp=0&as=A1D5B8000CC7350&cp=580C772325900E1";
            bll.NewsUrlGathering(url, 100);
        }

        [Test()]
        public void NewsGatheringTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void NewsPicGatheringTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void NewsPicGatheringOneTest()
        {
            Assert.Fail();
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
