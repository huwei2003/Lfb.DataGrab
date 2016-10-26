namespace Comm.Global.Enum.Business
{
    /// <summary>
    /// 产品大类
    /// </summary>
    public enum ProductBigCategory
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,

        /// <summary>
        /// 社交用小商品
        /// 如水果、鲜花、美食
        /// </summary>
        小礼品类 = 1 << ProductCategoryHelper.ProductBigCategoryMove,
       

        /// <summary>
        /// 广告营销类
        /// 如汽车、房产、珠宝
        /// </summary>
        广告类 = 2 << ProductCategoryHelper.ProductBigCategoryMove,
      
        /// <summary>
        /// 电商类
        /// 化妆品、工艺品
        /// </summary>
        电商类 = 3 << ProductCategoryHelper.ProductBigCategoryMove,
       

        /// <summary>
        /// 自营类
        /// 如水、年货
        /// </summary>
        自营类 = 4 << ProductCategoryHelper.ProductBigCategoryMove,
        
    }
}