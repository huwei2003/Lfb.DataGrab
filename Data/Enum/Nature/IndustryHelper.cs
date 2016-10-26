using System.Collections.Generic;
using System.Linq;

namespace Comm.Global.Enum.Nature
{
#pragma warning disable 1591

    /// <summary>
    /// ��ҵö�ٰ�����
    /// </summary>
    public static class IndustryHelper
    {
        /// bit11-bit6 ��ҵ����,����ö��
        /// bit5-bit0  ������ҵ,������ö��


        public const int IndustryCategoryMove = 6; // ��ҵ��������λ��

       

        /// <summary>
        /// ��ҵ������
        /// </summary>
        private const int IndustryCategoryMask = 0x00000FC0;

        /// <summary>
        /// ְλ������
        /// </summary>
        private const int IndustryMask = 0x0000003F;

       

        /// <summary>
        /// ȫ��ҵ������
        /// </summary>
        private const int FullIndustryMask = IndustryCategoryMask | IndustryMask;

       
        public static Dictionary<string, string[]> GetIndustryDictionary(
         out Dictionary<string, int> keySort)
        {
            keySort = new Dictionary<string, int>();

            var subDic = new Dictionary<string, string[]>();
            var Is = GetAllIndustryCategories(); //��ҵ��

            int j = 0;
            foreach (var i in Is) //ö�ٵڶ���
            {
                keySort.Add(i.ToString(), j++);
                var ps = i.Industries();

                var dsNames = ps;//.Select(d => d.ToString()).ToArray();
                subDic.Add(i.ToString(), dsNames);
                var ss = ps;//.Select(d => d.ToString()).ToArray();
                for (int k = 0; k < ss.Length; k++) //ö�ٵ�����
                    keySort.Add(ss[k], k);
            }
            return subDic;
        }

        /// <summary>
        /// jsר�õ�û���»��߷ָ����ֵ�
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string[]> GetIndustryDictionaryJs()
        {
           
            var subDic = new Dictionary<string, string[]>();
            var Is = GetAllIndustryCategories(); //��ҵ��

            int j = 0;
            foreach (var i in Is) //ö�ٵ�1��
            {
                var ps = i.Industrys();

                var dsNames = ps.Select(d => d.ShortName()).ToArray();
                subDic.Add(i.ToString(), dsNames);
            }
            return subDic;
        }

        /// <summary>
        /// ������ҵö�٣�������0
        /// ��ϰ�������ҵ���࣬��ҵ
        /// </summary>
        private static IEnumerable<Industry> GetAllBrandIndustrys()
        {
            return
                ((Industry[]) System.Enum.GetValues(typeof (Industry)))
                    .Where(d => d != Industry.��);
        }

        /// <summary>
        /// ְλ�Ķ�����
        /// ������_����==������
        /// </summary>
        /// <param name="industry"></param>
        /// <returns></returns>
        public static string ShortName(this Industry industry)
        {
            // StringBuilder sb = new StringBuilder();
            var ss = industry.ToString().Split('_');
            return ss[ss.Length - 1];//�������һ��
        }
  
      

        /// <summary>
        /// �ж��Ƿ��Ǿ�����ҵ���
        /// </summary>
        /// <param name="industry"></param>
        /// <returns></returns>
        public static bool IsIndustryCategory(this Industry industry)
        {
            return ((int)industry & IndustryCategoryMask) != 0 && ((int)industry & ~IndustryCategoryMask) == 0;
      
       }

        /// <summary>
        /// �ж��Ƿ��Ǿ�����ҵ
        /// </summary>
        /// <param name="industry"></param>
        /// <returns></returns>
        public static bool IsIndustry(this Industry industry)
        {
            return ((int)industry & IndustryMask) != 0 && ((int)industry & ~FullIndustryMask) == 0;
        }

       

        /// <summary>
        /// �õ�������ҵ����
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Industry> GetAllIndustryCategories()
        {
            return GetAllBrandIndustrys().Where(d => d.IsIndustryCategory());

        }
        public static bool BelongTo(this Industry industry, Industry upIndustry)
        {
            //δ���岻�����κ���������
            if (upIndustry == Industry.��)
                return false;

            if (upIndustry.IsIndustryCategory()) //��ҵ����
            {
                return (int)industry >= (int)upIndustry
                       && (int)industry <= ((int)upIndustry | IndustryMask);
            }
            return industry == upIndustry;
        }


        public static IEnumerable<Industry> Industrys(this Industry industryCategory)
        {
            return GetAllBrandIndustrys().Where(r => r.IsIndustry() && r.BelongTo(industryCategory));
        }
       
        /// <summary>
        /// �õ�������������ҵ�б�
        /// </summary>
        /// <param name="industryCategory"></param>
        /// <returns></returns>
        public static string[] Industries(this Industry industryCategory)
        {
            var categoryName = industryCategory.ToString();
            var names = System.Enum.GetNames(typeof(Industry));
            return names.Where(industry =>
            {
                var ss = industry.Split('_');
                return (ss.Length == 2 && ss[0] == categoryName);
            }).ToArray();
        }
      }
}