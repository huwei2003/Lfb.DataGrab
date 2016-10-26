using System.Collections.Generic;
using System.Linq;

namespace Comm.Global.Enum.Business
{


    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class ProductCategoryHelper
    {

        #region 字典

        /// <summary>
        /// 所有产品分类数据库
        /// </summary>
        public static Dictionary<string, string[]> GetProductCategoryDictionary()
        {
            var regionDictionary = new Dictionary<string, string[]>();
            var bcs = GetAllBigCategories(); //大类
            foreach (var bc in bcs)
            {
                var cs = bc.ProductCategories(); //大类下小类
                var cNames = cs.Select(d => d.ToString()).ToArray();
                regionDictionary.Add(bc.ToString(), cNames);
            }

            return regionDictionary;

        }

        #endregion

        /// <summary>
        /// 产品大类左移位数
        /// </summary>
        public const int ProductBigCategoryMove = 6; 

        /// <summary>
        /// 产品大类屏蔽码
        /// </summary>
        public const int ProductBigCategoryMask = 0x0FC0;

        /// <summary>
        /// 产品小类屏蔽码
        /// </summary>
        public const int ProductSmallCategoryMask = 0x003F;

        /// <summary>
        /// 大类小类屏蔽码
        /// </summary>
        private const int FullProductCategoryMask = ProductBigCategoryMask | ProductSmallCategoryMask;

        /// <summary>
        /// 得到类别对应的大类,不判断是否合法
        /// </summary>
        /// <param name="productCategory"></param>
        /// <returns></returns>
        public static ProductBigCategory ProductBigCategory(this ProductCategory productCategory)
        {
            return (ProductBigCategory)((int)productCategory & ProductBigCategoryMask);
        }

        /// <summary>
        /// 得到所有小类
        /// </summary>
        public static IEnumerable<ProductCategory> GetAllCategories()
        {
            return ((ProductCategory[])System.Enum.GetValues(typeof(ProductCategory)))
                .Where(d => d != ProductCategory.None);
        }

        /// <summary>
        /// 得到大类下所有小类
        /// </summary>
        /// <param name="productBigCategory"></param>
        /// <returns></returns>
        public static IEnumerable<ProductCategory> ProductCategories(this ProductBigCategory productBigCategory)
        {
            return GetAllCategories().Where(r => r.ProductBigCategory() == productBigCategory);
        }

        /// <summary>
        /// 得到所有大类
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ProductBigCategory> GetAllBigCategories()
        {
            //重新排序
            return new []
            {
                Business.ProductBigCategory.小礼品类, 
                Business.ProductBigCategory.广告类,
                Business.ProductBigCategory.电商类,
                Business.ProductBigCategory.自营类,
            };
            //return GetAllRegions().Where(d => d.IsProvince());

        }


    }
}
