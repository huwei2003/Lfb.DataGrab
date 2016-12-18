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
    public class BaijiahaoGatherTests
    {
        private BaijiahaoGather bll;
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
            bll = new BaijiahaoGather();

        }
        [Test()]
        public void NewsUrlGatheringTest()
        {

            //bll.GatheringAuthorUrlFromSearch("辩证 百家号 贡献文章 总阅读数 作者文章 按时间", 100, 1);
            bll.GatheringAuthorUrlSearch();
        }

        [Test()]
        public void GatheringNewsFromAuthorTest()
        {
            //bll.GatheringAuthorUrlFromSearch("百家号 军事 阅读数", 100, 0);
            bll.GatheringNewsFromAuthor();
        }
        
    }
}
