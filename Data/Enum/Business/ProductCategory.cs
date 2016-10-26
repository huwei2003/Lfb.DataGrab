namespace Comm.Global.Enum.Business
{
    /// <summary>
    /// 产品分类
    /// </summary>
    public enum ProductCategory
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,

        /// <summary>
        /// 社交用小商品
        /// 如水果、鲜花、美食
        /// </summary>
        水果 = ProductBigCategory.小礼品类 | 1,
        鲜花 = ProductBigCategory.小礼品类 | 2,
        美食 = ProductBigCategory.小礼品类 | 3,


        /// <summary>
        /// 广告营销类
        /// 如汽车、房产、珠宝
        /// </summary>
        汽车 = ProductBigCategory.广告类 | 1,
        房产 = ProductBigCategory.广告类 | 2,
        珠宝 = ProductBigCategory.广告类 | 3,

        /// <summary>
        /// 电商类
        /// 化妆品、工艺品
        /// </summary>
        护肤品 = ProductBigCategory.电商类 | 1,
        美酒 = ProductBigCategory.电商类 | 2,
        工艺品 = ProductBigCategory.电商类 | 3,
       

        /// <summary>
        /// 自营类
        /// 如水、年货
        /// </summary>
        桶装水 = ProductBigCategory.自营类 | 1,
    }
}