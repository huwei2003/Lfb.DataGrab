// ReSharper disable InconsistentNaming
namespace Comm.Global.Enum.Sys
{
    /// <summary>
    /// 服务命名
    /// 注意一旦确立，不能更存储对应值
    /// </summary>
    public enum ServiceName
    {
        #region 特殊服务 0
        
        /// <summary>
        /// 代表单元/集成测试工程
        /// </summary>
        Test = 0,

        #endregion

        #region Cloud 1--99
        /// <summary>
        /// 阿里云日志服务
        /// </summary>
        Cloud_SlsService=1,


        /// <summary>
        /// 阿里云消息服务
        /// </summary>
        Cloud_MnsService=2,

        /// <summary>
        /// 阿里云缓存服务
        /// </summary>
        Cloud_OcsService=3,

      
        /// <summary>
        /// 阿里云Rds mysql数据库
        /// </summary>
        Cloud_RdsService=4,

        /// <summary>
        /// 阿里云kv存储服务 
        /// </summary>
        Cloud_KvService=5,

        /// <summary>
        /// 阿里云Ops搜索服务 
        /// </summary>
        Cloud_OpsService = 6,

        /// <summary>
        /// 阿里云Oss存储服务 
        /// </summary>
        Cloud_OssService = 7,

        /// <summary>
        /// 阿里云Mts音视频转码服务 
        /// </summary>
        Cloud_MtsService = 8,

        /// <summary>
        /// 阿里云Ots表存储服务 
        /// </summary>
        Cloud_OtsService = 9,

        /// <summary>
        /// 环信服务easemob
        /// </summary>
        Cloud_ImEasemobService=21,
        /// <summary>
        /// aliyunwang服务
        /// </summary>
        Cloud_ImAliService = 22,
        /// <summary>
        /// neteasy yunxin服务
        /// </summary>
        Cloud_ImNeteasyService = 23,

        /// <summary>
        /// submail sms服务
        /// </summary>
        Cloud_Sms_SubMailService = 31,
        /// <summary>
        /// 天信易博 sms服务
        /// </summary>
        Cloud_Sms_TxybService=32,
        /// <summary>
        /// 美联 sms服务
        /// </summary>
        Cloud_Sms_MlService=33,
        /// <summary>
        /// ali sms服务
        /// </summary>
        Cloud_Sms_AliService = 34,

        /// <summary>
        /// SubMail Email服务
        /// </summary>
        Cloud_SubMailEmailService=41,

        /// <summary>
        /// 阿里 Email推送服务
        /// </summary>
        Cloud_AliEmailService = 42,

        /// <summary>
        /// ping++支付服务
        /// </summary>
        Cloud_PayService = 50,


        /// <summary>
        /// 百度地图服务
        /// </summary>
        Cloud_Map_BaiduService = 61,

        /// <summary>
        /// 身份证验证服务
        /// </summary>
        Cloud_CidService = 62,
        #endregion

        #region Manager 101-199
        Manager_RegistryService=101,
        Manager_MonitorService = 102,
        Manager_TesterService = 103,
        #endregion

        #region ApiGateway 201-299
        ApiGateway_MainService = 201,
        ApiGateway_DealerService = 202,
        ApiGateway_RegulatorService = 203,
        ApiGateway_MarketerService = 204,
        ApiGateway_AdvertiserService = 209,
       
        ApiGateway_OpenPlatformService = 251,
      
        #endregion

        #region MicroServices
        #region Actor 301-399

        /// <summary>
        /// 基本用户服务
        /// </summary>
        UserService=301,

        /// <summary>
        /// 基本商家服务
        /// </summary>
        SellerService = 351,

        /// <summary>
        /// 楼蜜客服服务
        /// </summary>
        StaffService = 381,

        /// <summary>
        /// 楼蜜管理人服务
        /// </summary>
        AdminService = 399,

        #endregion

        #region Office 401-499

        /// <summary>
        /// 同楼用户服务
        /// </summary>
        OfficeUserService = 401,

       
        /// <summary>
        /// 名片识别服务
        /// </summary>
        CardRecognizeService = 411,

        /// <summary>
        /// 新闻采集服务
        /// </summary>
        NewsGatheringService = 412,
        #endregion 

     
        #region Love 501-599

        /// <summary>
        /// 蜜友用户服务
        /// </summary>
        LoveUserService = 501,

       
        /// <summary>
        /// 蜜友有礼服务
        /// </summary>
        LoveGiftService = 502,

        /// <summary>
        /// 蜜友有约服务
        /// </summary>
        LovePartyService = 503,

        /// <summary>
        /// 蜜友有书服务
        /// </summary>
        LoveBookService = 504,
        #endregion 

        #region Shop 601-699
        
        /// <summary>
        /// 抢单聊天服务
        /// </summary>
        ChatService = 601,

        #endregion 

        #region Deal 801-899

        /// <summary>
        /// 资金服务
        /// </summary>
        BankRollService = 801,

        /// <summary>
        /// 实物订单服务
        /// </summary>
        OrderService = 802,

        /// <summary>
        /// 产品服务
        /// </summary>
        ProductService = 803,

        #endregion

        #region Advertisement 1001-1999

        /// <summary>
        /// 广告服务
        /// </summary>
        AdvertService = 1001,

        /// <summary>
        /// 广告服务专用db服务配置
        /// </summary>
        Cloud_RdsService_AdvertService= 1010,
        /// <summary>
        /// 广告服务专用db服务配置 写库
        /// </summary>
        Cloud_RdsService_AdvertService_Insert =1011,
        /// <summary>
        /// 广告服务专用db服务配置 只读库
        /// </summary>
        Cloud_RdsService_AdvertService_Read =1012,
        /// <summary>
        /// 广告服务专用db服务配置 下载库
        /// </summary>
        Cloud_RdsService_AdvertService_Download = 1013,
        #endregion

        #region Wiqun  2001-2999

        /// <summary>
        /// 推广商(员)服务
        /// </summary>
        PromotionService = 2001,
        #endregion


        

        #endregion
    }
}
