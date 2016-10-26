// ReSharper disable InconsistentNaming

using System;

namespace Comm.Global.Enum.Business
{
    /// <summary>
    /// 支付类型帮助类
    /// </summary>
    public static class PayChannelHelper
    {
        public static string GetName(this PayChannel payChannel)
        {
            switch (payChannel)
            {
               case PayChannel.balance:
                    return "余额";
               case PayChannel.bank:
                    return "银行转账";
                case PayChannel.alipay:
                case PayChannel.alipay_wap:
                case PayChannel.alipay_pc_direct:
                case PayChannel.alipay_qr:
                    return "支付宝";
                case PayChannel.wx:
                case PayChannel.wx_pub:
                case PayChannel.wx_pub_qr:
                    return "微信";
                case PayChannel.upmp:
                case PayChannel.upacp:
                case PayChannel.upacp_wap:
                case PayChannel.upacp_pc:
                    return "银联";
                case PayChannel.bfb:
                case PayChannel.bfb_wap:
                    return "百度钱包";
                case PayChannel.yeepay_wap:
                    return "易宝";
                case PayChannel.jdpay_wap:
                    return "京东钱包";
                case PayChannel.apple_pay:
                    return "苹果";
                default:
                    return "未知";
            }
        }
      
    }
}
