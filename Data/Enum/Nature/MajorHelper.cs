using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comm.Global.Enum.Nature
{

    /// <summary>
    /// ö�ٰ�����
    /// </summary>
    static public class MajorHelper
    {
        // bit15-bit11 ѧ��,����ö��, 5bit
        // bit10-bit6 רҵ��,ѧ����ö��
        // bit5-bit0 רҵ,ѧ��רҵ����ö��

        /// <summary>
        /// ѧ��������
        /// </summary>
        private const int ScienceMask = 0xF800;

        /// <summary>
        /// רҵ��������
        /// </summary>
        private const int MajorcategoryMask = 0x07C0;

        /// <summary>
        /// רҵ������
        /// </summary>
        public const int MajorMask = 0x003F;

        /// <summary>
        /// ѧ��רҵ��������
        /// </summary>
        private const int FullmajorcategoryMask = ScienceMask | MajorcategoryMask;

        /// <summary>
        /// ѧ��רҵ��רҵ������
        /// </summary>
        private const int FullmajorMask = ScienceMask | MajorcategoryMask | MajorMask;

    
        /// <summary>
        /// �õ�רҵ���������ƶ�Ӧ������
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string,int> GetMajorIndexDictionary()
        {
            var dict=new Dictionary<string, int>();
            foreach (var m in GetAllMajors())
            {
                dict.Add(m.ToString(),(int)m);
            }
            return dict;
        }

        /// <summary>
        /// �õ�רҵ�������б�����
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, string[]>> GetMajorDictionary(out Dictionary<string, int> keySort)
        {
            keySort = new Dictionary<string, int>();
            var nameDictionary = new Dictionary<string, Dictionary<string, string[]>>();
            var ps = GetAllSciences();
            int i = 0;
            foreach (var p in ps)
            {
                keySort.Add(p.ToString(), i++);
                var subDic = new Dictionary<string, string[]>();
                var cs = p.MajorCategories(); //citys
                int j = 0;
                foreach (var c in cs)
                {
                    keySort.Add(c.ToString(), j++);
                    var ds = c.Majors();
                    var dsNames = ds.Select(d => d.ToString()).ToArray();
                    subDic.Add(c.ToString(), dsNames);
                    for (int k = 0; k < dsNames.Length; k++)
                        keySort.Add(dsNames[k], k);
                }
                nameDictionary.Add(p.ToString(), subDic);
            }

            return nameDictionary;
        }



        /// <summary>
        /// ����רҵö�٣�������0
        /// ��ϰ�����ѧ�ƣ�רҵ�࣬רҵ
        /// </summary>
        public static IEnumerable<Major> GetAllMajors()
        {
            return ((Major[])System.Enum.GetValues(typeof(Major)))
                .Where(d => d != Major.��);
        }
        /// <summary>
        /// ��д��̬����ToString
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        public static string FullName(this Major major)
        {
            var sb = new StringBuilder();
            if (major.IsScience()) //ֱϽרҵ��,ѧ��
                return major.ToString();
            if (major.IsMajorCategory())
                return string.Format("{0}-{1}", major.Science(), major);
            if (major.IsMajor())
                return string.Format("{0}-{1}-{2}", major.Science(), major.MajorCategory(), major);
            return sb.ToString();
        }

        /// <summary>
        /// �ж��Ƿ�һ�����ݿ������ֶ�����һ����������
        /// ������������
        /// </summary>
        /// <param name="major">���ݿ������ֶ�</param>
        /// <param name="upMajor">�����ϼ�����</param>
        /// <returns></returns>
        public static bool BelongTo(this Major major, Major upMajor)
        {
            //δ���岻�����κ���������
            if (upMajor == Major.��)
                return false;

            if (upMajor.IsMajorCategory()) //רҵ��
            {
                return (int)major >= (int)upMajor
                       && (int)major <= ((int)upMajor | MajorMask); //רҵ��������רҵ
            }
            if (upMajor.IsScience()) //ѧ��
            {
                return (int)major >= (int)upMajor
                       && (int)major <= ((int)upMajor | MajorcategoryMask | MajorMask); //ѧ��������רҵ��רҵ
            }
            return major == upMajor;
        }

      
        /// <summary>
        /// �ж��Ƿ���ѧ��,���������"ֱϽרҵ��"���ѧ��
        /// �����������ݿ��ֶ�
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        public static bool IsScience(this Major major)
        {
            return ((int)major & ScienceMask) != 0 && ((int)major & ~ScienceMask) == 0;
        }

        /// <summary>
        /// �õ�������Ӧ��ѧ��,���ж��Ƿ�Ϸ�
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        public static Major Science(this Major major)
        {
            return (Major)((int)major & ScienceMask);
        }
       

        /// <summary>
        /// �ж��Ƿ���רҵ��(����ͬʱΪֱϽרҵ�࣬����Ҫרҵ��)
        /// �����������ݿ��ֶ�
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        public static bool IsMajorCategory(this Major major)
        {
            return ((int)major & ScienceMask) != 0 && ((int)major & MajorcategoryMask) != 0 && ((int)major & ~FullmajorcategoryMask) == 0;
        }

        /// <summary>
        /// �õ�������Ӧ��רҵ��,���ж��Ƿ�Ϸ�
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        public static Major MajorCategory(this Major major)
        {
            return (Major)((int)major & FullmajorcategoryMask);
        }
        

        /// <summary>
        /// �ж��Ƿ�����רҵ
        /// �����������ݿ��ֶ�
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        public static bool IsMajor(this Major major)
        {
            return ((int)major & ScienceMask) != 0 && ((int)major & MajorcategoryMask) != 0
                   && ((int)major & MajorMask) != 0 && ((int)major & ~FullmajorMask) == 0;

        }
       

        /// <summary>
        /// �õ�������Ӧ��רҵ,�ж��Ƿ�Ϸ�
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        public static Major TryMajor(this Major major)
        {
            if (!major.IsMajor())
                return Major.��;
            return (Major)((int)major & FullmajorMask);
        }

       
       

        /// <summary>
        /// �õ�����ѧ��
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Major> GetAllSciences()
        {
            return GetAllMajors().Where(d => d.IsScience());

        }

        
        /// <summary>
        /// �õ�ѧ�������г�רҵ���б�,����Ҫ��רҵ��
        /// </summary>
        /// <param name="science"></param>
        /// <returns></returns>
        public static IEnumerable<Major> MajorCategories(this Major science)
        {
            return GetAllMajors().Where(r => r.IsMajorCategory() && r.BelongTo(science));
        }

        /// <summary>
        /// �õ�רҵ����������רҵ�б�,����Ҫ��רҵ
        /// </summary>
        /// <param name="majorCategory"></param>
        /// <returns></returns>
        public static IEnumerable<Major> Majors(this Major majorCategory)
        {
            return GetAllMajors().Where(r => r.IsMajor() && r.BelongTo(majorCategory));
        }

    }
}