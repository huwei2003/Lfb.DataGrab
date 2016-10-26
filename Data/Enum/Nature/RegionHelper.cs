using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comm.Global.Enum.Nature
{
#pragma warning disable 1591

    /// <summary>
    /// ö�ٰ�����
    /// </summary>
    static public class RegionHelper 
    {
        /// <summary>
        /// ���������ö��
        /// bit31-bit24 //����/����
        /// bit23-bit19 //bit15-bit11 ʡ,����ö��, 5bit,1��ʾֱϽ��,31��ʾ����
        /// bit18-bit14 //bit10-bit6 ��,ʡ��ö��
        /// bit13-bit8 //bit5-bit0 ����,ʡ����ö��
        /// bit7-bit0 ѧУ,����ͳһö��,�ϼ���û������
        /// </summary>

        public const int Provincelmove = 19;// ʡ����λ��

        public const int Citylmove = 14;// ������λ��

        public const int Districtlmove = 8;// ��������λ��

        
        /// <summary>
        /// ʡ������
        /// </summary>
        public const int Provincemask = 0xF80000;

        /// <summary>
        /// ��������
        /// </summary>
        public const int Citymask = 0x07C000;

        /// <summary>
        /// ��������
        /// </summary>
        public const int Districtmask = 0x003F00;

       
        /// <summary>
        /// ʡ��������
        /// </summary>
        public const int Fullcitymask = Provincemask | Citymask;

        /// <summary>
        /// ʡ����������
        /// </summary>
        public const int Fulldistrictmask = Provincemask | Citymask | Districtmask;

         
        #region �ֵ�
        
        /// <summary>
        /// ���е������ݿ�
        /// </summary>
        public static Dictionary<string, Dictionary<string, string[]>> GetRegionDictionary()
        {
            var regionDictionary = new Dictionary<string, Dictionary<string, string[]>>();
            var ps = GetAllProvinces(); //ʡ
            int i = 0;
            foreach (var p in ps)
            {
                var subDic = new Dictionary<string, string[]>();
                var cs = p.Cities(); //ʡ����
                int j = 0;
                foreach (var c in cs)
                {
                    var ds = c.Districts(); //��������
                    var dsNames = ds.Select(d => d.ToString()).ToArray();
                    subDic.Add(c.ToString(), dsNames);
                }
                regionDictionary.Add(p.ToString(), subDic);
            }

            return regionDictionary;

        }

        #endregion

        /// <summary>
        /// ���е���ѧУö�٣�������0
        /// ��ϰ�����ʡ����
        /// </summary>
        public static IEnumerable<Region> GetAllRegions ()
        {
            return ((Region[])System.Enum.GetValues(typeof(Region)))
                .Where(d => d != Region.��);
        }
        /// <summary>
        /// ��д��̬����ToString
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static string FullName(this Region region)
        {
            var sb = new StringBuilder();
            if (region.IsCCity() ) //ֱϽ��
                return region.ToString();
            if (region.IsCity()) //��ͨ��
                return string.Format("{0}-{1}", region.Province(), region);
            if (region.IsDistrict())//����
                return string.Format("{0}-{1}-{2}", region.Province(), region.City(), region);
            return sb.ToString();
        }

        #region ���ж�

        /// <summary>
        /// �ж��Ƿ���ʡ,���������"ֱϽ��"���ʡ�ʹ�½��,����region�ǺϷ�ֵ
        /// �����������ݿ��ֶ�
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static bool IsProvince(this Region region)
        {
            return ((int)region & Provincemask) != 0 && ((int)region & ~Provincemask) == 0;
        }

        /// <summary>
        /// �õ�������Ӧ��ʡ,���ж��Ƿ�Ϸ�
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static Region Province(this Region region)
        {
            return (Region)((int)region & Provincemask);
        }

       

        /// <summary>
        /// �ж��Ƿ�����(����ͬʱΪֱϽ�У�����Ҫ��),ֻ����Region���ںϷ�ֵ
        /// �����������ݿ��ֶ�
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static bool IsCity(this Region region)
        {
            return ((int)region & Citymask) != 0 && ((int)region & ~Fullcitymask) == 0;
        }

        /// <summary>
        /// �ж��Ƿ���ֱϽ��
        /// �����������ݿ��ֶ�
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static bool IsCCity(this Region region)
        {
            return region.IsCity() && region.Province() == Region.ֱϽ��;
        }

        /// <summary>
        /// �õ�������Ӧ����,���ж��Ƿ�Ϸ�
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static Region City(this Region region)
        {
            return (Region)((int)region & Fullcitymask);
        }

       

        /// <summary>
        /// �ж��Ƿ�������,ֻ����Region���ںϷ�ֵ
        /// �����������ݿ��ֶ�
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static bool IsDistrict(this Region region)
        {
            return ((int)region & Districtmask) != 0 && ((int)region & ~Fulldistrictmask) == 0;
          
        }
        /// <summary>
        /// �õ�������Ӧ����,���ж��Ƿ�Ϸ�
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static Region District(this Region region)
        {
            return (Region)((int)region & Fulldistrictmask);
        }
        
        /// <summary>
        /// �õ�����ʡ
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Region> GetAllProvinces()
        {
            //��������
            return new[]
            {
                Region.ֱϽ��, 
                Region.�㶫,
                Region.�㽭,
                Region.����,
                Region.����,
                Region.ɽ��,
                Region.�Ĵ�,
                Region.����,
                Region.����,
                Region.����,
                Region.����,
                Region.����,
                Region.����,
                Region.����,
                Region.�ӱ�,
                Region.����,
                Region.ɽ��,
                Region.����,
                Region.����,
                Region.����,
                Region.������,
                Region.����,
                Region.���ɹ�,
                Region.����,
                Region.�½�,
                Region.����,
                Region.�ຣ,
                Region.����,
                Region.��½��
            };
            //return GetAllRegions().Where(d => d.IsProvince());
        
        }

       
        

        /// <summary>
        /// �õ�����ֱϽ��,������Ҫ��
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Region> GetAllCCities()
        {
            return GetAllRegions().Where(d => d.IsCCity());                
        }

        

        /// <summary>
        /// �õ����г����б�
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Region> GetAllCities()
        {
            return GetAllRegions().Where(r => r.IsCity());
        }

        public static IEnumerable<Region> GetAllDistricts()
        {
            return GetAllRegions().Where(r => r.IsDistrict());
        }

       

        /// <summary>
        /// �õ�ʡ�����г����б�,����Ҫ����
        /// </summary>
        /// <param name="province"></param>
        /// <returns></returns>
        public static IEnumerable<Region> Cities(this Region province)
        {
            return GetAllRegions().Where(r => r.IsCity() && r.BelongTo(province));
        }

        /// <summary>
        /// �õ����������������б�
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static IEnumerable<Region> Districts(this Region region)
        {
            return GetAllRegions().Where(r => r.IsDistrict() && r.BelongTo(region));
        }

      

        #endregion

        #region ���ݿ����ƥ��

        /// <summary>
        /// �ж��Ƿ�һ�����ݿ������ֶ�����һ����������
        /// ������������
        /// </summary>
        /// <param name="region">���ݿ������ֶ�</param>
        /// <param name="upRegion">�����ϼ�����</param>
        /// <returns></returns>
        public static bool BelongTo(this Region region, Region upRegion)
        {
            //δ���岻�����κ���������
            if (upRegion == Region.��)
                return false;
            //��������ȫ��
            //if (upRegion == Region.ȫ��)
            //    return region != Region.��;

            if (upRegion.IsProvince()) //ʡ
            {
                return (int)region >= (int)upRegion
                       && (int)region <= ((int)upRegion | Citymask | Districtmask ); //ʡ����������
            }
            if (upRegion.IsCity()) //��
            {
                return (int)region >= (int)upRegion
                       && (int)region <= ((int)upRegion | Districtmask ); //����������
            }
            return region == upRegion;
        }

      

        /// <summary>
        /// �ж��Ƿ�һ�����ݿ������ֶκ�����һ�����������н���
        /// ��Ȼ����¼�
        /// </summary>
        /// <param name="region"></param>
        /// <param name="interRegion"></param>
        /// <returns></returns>
        public static bool Intersect(this Region region, Region interRegion)
        {
            if (interRegion == Region.��)
                return false;

            var district = interRegion.District();
            var city = interRegion.City();
            var procince = interRegion.Province();

            if (interRegion.IsProvince()) //ʡ
            {
                return (int)region >= (int)interRegion
                       && (int)region <= ((int)interRegion | Citymask | Districtmask ); //ʡ����������
            }
            if (interRegion.IsCity()) //��
            {
                return region == procince
                       || (int)region >= (int)interRegion
                       && (int)region <= ((int)interRegion | Districtmask ); //����������
            }
            return region == interRegion
                   || region == district
                   || region == city
                   || region == procince;
        }
        #endregion 

       
    }
}