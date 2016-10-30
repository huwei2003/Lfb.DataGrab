using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comm.Global.DTO.News;
using Comm.Global.Enum.Business;
using Lfb.DataGrab;
using Lfb.DataGrab.Model;
using NUnit.Framework;

namespace Lfb.DataGrab.Tests
{
    [TestFixture()]
    public class DalNewsTests
    {
        private DtoNews news;
        private DalNews db;
        
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
            
            news = new DtoNews()
            {
                Author = "test",
                Contents = DateTime.Now + "test",
                CreateTime = DateTime.Now,
                FromSiteName = "testsite",
                FromUrl = "http://localhost",
                //ImgFlag = 0,
                Title = "test1" + DateTime.Now,
                LogoUrl = "http://n.sinaimg.cn/fo/transform/20160705/pBto-fxtspsa6682768.jpg",
                NewsTypeId = NewsTypeEnum.新闻,
                PubTime = DateTime.Now,
                IsShow =1,
                LogoOriginalUrl="",
                AuthorId="",


                TotalComments = 0,
                Tags = "",
                NewsHotClass = 7,
                LastReadTimes = 0,
                LastDealTime = DateTime.Now,
                IsHot = 0,
                IsDeal = 0,
                IntervalMinutes = 60,
                CurReadTimes = 0,
            };

        }

        [Test()]
        public void InsertTest()
        {
            var id = DalNews.Insert(news);
            Assert.IsTrue(id > 0);

            var result = DalNews.GetNews(id);
            Assert.IsTrue(result.Id == id);

            var isHave = DalNews.IsExistsNews(result.Title);
            Assert.IsTrue(isHave);

            DalNews.UpdateImgFlag(id, 1);
            result = DalNews.GetNews(id);
            //Assert.IsTrue(result.ImgFlag == 1);

            var result2 = DalNews.DelNews(id);
            Assert.IsTrue(result2);
        }

    }
}
