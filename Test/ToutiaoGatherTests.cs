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
        [Test()]
        public void NewsUrlGatheringTest()
        {
            ToutiaoGather bll = new ToutiaoGather();
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
    }
}
