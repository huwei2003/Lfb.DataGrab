using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Comm.Global.DTO.News;

namespace WebApi.Controllers
{
    [RoutePrefix("web")]
    public class WebController : ApiController
    {
        
        /// <summary>
        /// 账号登录
        /// </summary>
        /// <param name="name">账号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        [Route("login")]
        [HttpGet]
        public DtoTaskForClient Login(string name, string pwd)
        {
            var modelClient = new DtoTaskForClient();
            var model = new DtoTask();
            try
            {
                var clientIp = Comm.Tools.Utility.Web.Http.GetIp();
                var ipArea = Lfb.DataGrabBll.Global.GetIpArea(clientIp);
                //验证用户登录信息
                var clientUserId = Lfb.DataGrabBll.DalNews.GetClientUserId(name, pwd);
                if (clientUserId > 0)
                {
                    var taskId = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    var keywords = Lfb.DataGrabBll.DalNews.GetRndKeywordsByArea(ipArea);
                    //var keywords = Lfb.DataGrabBll.DalNews.GetRndKeywords();
                    var upPostContent = Lfb.DataGrabBll.DalNews.GetRndUpPostContent();
                    var opString = "";
                    var baiduUser = Lfb.DataGrabBll.DalNews.GetRndBaiduUser();
                    if (baiduUser == null)
                    { 
                        throw new Exception("未获取到可用的百度账号");
                    }
                    var bjh = Lfb.DataGrabBll.DalNews.GetRndBjhUrl();
                    if (bjh == null)
                    {
                        throw new Exception("未获取到可用的百家号链接");
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(bjh.FeedId)) {
                            throw new Exception("未获取到可用的百家号链接");
                        }
                    }
                    modelClient.TaskId = taskId;
                    modelClient.BaiduPassword = baiduUser.Password;
                    modelClient.BaiduUserName = baiduUser.UserName;
                    modelClient.Keywords = keywords;
                    modelClient.OpString = opString;
                    modelClient.UpPostContnet = upPostContent;

                    var url = string.Format("http://baijiahao.baidu.com/u?app_id={0}", bjh.AuthorId);
                    
                    //var url=string.Format("https://baijiahao.baidu.com/po/feed/share?context=%7B%22nid%22%3A%22{0}%22%2C%22sourceFrom%22%3A%22bjh%22%7D",bjh.FeedId);
                    //if(bjh.FeedId.Contains("sv"))
                    //{
                    //    url=string.Format("https://baijiahao.baidu.com/po/feed/video?context=%7B%22nid%22%3A%22{0}%22%2C%22sourceFrom%22%3A%22bjh%22%7D&type=video&fr=bjhauthor",bjh.FeedId);
                    //}

                    modelClient.Url = url;
                    model.FeedId = bjh.FeedId;
                    model.TaskId = taskId;
                    model.BaiduUserId = baiduUser.Id;
                    model.ClientUserId = clientUserId;
                    model.Keywords = keywords;
                    model.OpString = opString;
                    model.UpPostContnet = upPostContent;
                    model.Url = url;
                    model.Ip = clientIp;
                    model.Memo = "";
                    Lfb.DataGrabBll.DalNews.Insert_Task(model);
                }
                else
                {
                    throw new Exception("账号密码错误");
                }
            }
            catch(Exception ex)
            { }
            return modelClient;
        }

        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <param name="result">状态 0未成功 1成功</param>
        /// <returns></returns>
        [Route("update")]
        [HttpGet]
        public bool Update(string taskId,int result)
        {
            try {
                if (result >= 1)
                {
                    result = 1;
                }
                else
                    result = 0;
               return Lfb.DataGrabBll.DalNews.UpdateTaskStatus(taskId, result);
                
            }
            catch (Exception ex) { 
            
            }
            return false;
        }

        
    }
}