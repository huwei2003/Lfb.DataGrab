// ReSharper disable InconsistentNaming
namespace Comm.Global.Enum.Business
{
    /// <summary>
    /// 支付类型
    /// 
    /// </summary>
    public enum PayChannel
    {
        /// <summary>
        /// 余额
        /// </summary>
        balance=0,

        /// <summary>
        /// 银行转账
        /// </summary>
        bank = 2,
     
        //以下为pingxx支持的渠道
        alipay = 3,
        wx = 4,
        upmp = 5,
        upacp = 6,
        bfb = 7,

        alipay_wap = 10,
        upacp_wap = 11,
        wx_pub = 12,
        bfb_wap = 13,
        yeepay_wap = 14,
        jdpay_wap = 15,
        alipay_pc_direct = 16,
        upacp_pc = 17,

        alipay_qr = 30,
        wx_pub_qr = 31,
        apple_pay = 50
    }
}
