using System.Collections.Generic;
using System.Linq;

namespace Comm.Global.Enum.Business
{


    /// <summary>
    /// ö�ٰ�����
    /// </summary>
    public static class ProductCategoryHelper
    {

        #region �ֵ�

        /// <summary>
        /// ���в�Ʒ�������ݿ�
        /// </summary>
        public static Dictionary<string, string[]> GetProductCategoryDictionary()
        {
            var regionDictionary = new Dictionary<string, string[]>();
            var bcs = GetAllBigCategories(); //����
            foreach (var bc in bcs)
            {
                var cs = bc.ProductCategories(); //������С��
                var cNames = cs.Select(d => d.ToString()).ToArray();
                regionDictionary.Add(bc.ToString(), cNames);
            }

            return regionDictionary;

        }

        #endregion

        /// <summary>
        /// ��Ʒ��������λ��
        /// </summary>
        public const int ProductBigCategoryMove = 6; 

        /// <summary>
        /// ��Ʒ����������
        /// </summary>
        public const int ProductBigCategoryMask = 0x0FC0;

        /// <summary>
        /// ��ƷС��������
        /// </summary>
        public const int ProductSmallCategoryMask = 0x003F;

        /// <summary>
        /// ����С��������
        /// </summary>
        private const int FullProductCategoryMask = ProductBigCategoryMask | ProductSmallCategoryMask;

        /// <summary>
        /// �õ�����Ӧ�Ĵ���,���ж��Ƿ�Ϸ�
        /// </summary>
        /// <param name="productCategory"></param>
        /// <returns></returns>
        public static ProductBigCategory ProductBigCategory(this ProductCategory productCategory)
        {
            return (ProductBigCategory)((int)productCategory & ProductBigCategoryMask);
        }

        /// <summary>
        /// �õ�����С��
        /// </summary>
        public static IEnumerable<ProductCategory> GetAllCategories()
        {
            return ((ProductCategory[])System.Enum.GetValues(typeof(ProductCategory)))
                .Where(d => d != ProductCategory.None);
        }

        /// <summary>
        /// �õ�����������С��
        /// </summary>
        /// <param name="productBigCategory"></param>
        /// <returns></returns>
        public static IEnumerable<ProductCategory> ProductCategories(this ProductBigCategory productBigCategory)
        {
            return GetAllCategories().Where(r => r.ProductBigCategory() == productBigCategory);
        }

        /// <summary>
        /// �õ����д���
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ProductBigCategory> GetAllBigCategories()
        {
            //��������
            return new []
            {
                Business.ProductBigCategory.С��Ʒ��, 
                Business.ProductBigCategory.�����,
                Business.ProductBigCategory.������,
                Business.ProductBigCategory.��Ӫ��,
            };
            //return GetAllRegions().Where(d => d.IsProvince());

        }


    }
}
