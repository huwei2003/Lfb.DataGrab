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
    public class GlobalTests
    {
        [Test()]
        public void IsToutiaoAuthorUrlTest()
        {
            var url1 = "http://toutiao.com/m3470331046/";
            var url2 = "https://toutiao.com/m3470331046/";
            var url3 = "http://toutiao.com/i3470331046/";
            var url4 = "http://toutiao.com/3470331046/";

            var isTouA1 = Global.IsToutiaoAuthorUrl(url1);

            var isTouA2 = Global.IsToutiaoAuthorUrl(url2);

            var isTouA3 = Global.IsToutiaoAuthorUrl(url3);

            var isTouA4 = Global.IsToutiaoAuthorUrl(url4);

        }
    }
}
