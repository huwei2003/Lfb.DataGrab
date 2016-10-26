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
    public class XmlDealTests
    {
        [Test()]
        public void GetSitesInfoTest()
        {
            var list = XmlDeal.GetSitesInfo();
            Assert.IsTrue(list.Count > 0);
        }
    }
}
