using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Comm.Global.Enum.Nature
{
#pragma warning disable 1591

    /// <summary>
    /// ��Ƹְλö�ٰ�����
    /// </summary>
    public static class IndustryPostHelper
    {
        /// bit16-bit11 ��ҵ����,����ö��
        /// bit10-bit6 ������ҵ,������ö��
        /// bit5-bit0  ְλ

        public const int INDUSTRYCATEGORYLMOVE = 11; // ��ҵ��������λ��

        public const int INDUSTRYLMOVE = 6; // ��ҵ����λ��

        /// <summary>
        /// ��ҵ����������
        /// </summary>
        public const int INDUSTRYCATEGORYMASK = 0x0001F800;

        /// <summary>
        /// ��ҵ������
        /// </summary>
        public const int INDUSTRYMASK = 0x000007C0;

        /// <summary>
        /// ְλ������
        /// </summary>
        public const int POSTMASK = 0x0000003F;

        /// <summary>
        /// ȫ��ҵ������
        /// </summary>
        public const int FULLINDUSTRYMASK = INDUSTRYCATEGORYMASK | INDUSTRYMASK;

        /// <summary>
        /// ȫ��ҵ������
        /// </summary>
        public const int FULLPOSTMASK = INDUSTRYCATEGORYMASK | INDUSTRYMASK | POSTMASK;

        /// <summary>
        /// �õ���ѧ���������ƶ�Ӧ������
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, int> GetIndustryPostIndexDictionary()
        {
            var dict = new Dictionary<string, int>();
            foreach (var m in GetAllIndustryPosts())
            {
                dict.Add(m.ToString(), (int) m);
            }
            return dict;
        }

        public static Dictionary<string, Dictionary<string, string[]>> GetIndustryPostDictionary(
            out Dictionary<string, int> keySort)
        {
            keySort = new Dictionary<string, int>();
            var nameDictionary = new Dictionary<string, Dictionary<string, string[]>>();
            var ctgs = GetAllIndustryCategories(); //��ҵ��
            int ii = 0;
            foreach (var c in ctgs) //ö�ٵ�һ��
            {
                keySort.Add(c.ToString(), ii++);
                var subDic = new Dictionary<string, string[]>();
                var Is = c.Industries().ToList(); //��ҵ������ҵ
               
                int j = 0;
                foreach (var i in Is)//ö�ٵڶ���
                {
                    keySort.Add(i.ToString(), j++);
                    var ps = i.Posts();

                    var dsNames = ps.Select(d => d.PostName()).ToArray();
                    subDic.Add(i.ToString(), dsNames);
                    var ss = ps.Select(d => d.ToString()).ToArray();
                    for (int k = 0; k < ss.Length; k++)//ö�ٵ�����
                        keySort.Add(ss[k], k);
                }

                /* if (c != IndustryPost.���������)//���3��ְλ
                {
                    Is.Clear();
                    Is.Add(IndustryPost.���������_�ƶ�����);
                    Is.Add(IndustryPost.���������_ǰ�˿���);
                    Is.Add(IndustryPost.���������_��˿���);

                }*/
                nameDictionary.Add(c.ToString(), subDic);
            }
            return nameDictionary;
        }

        /// <summary>
        /// jsר�õ�û���»��߷ָ����ֵ�
        /// </summary>
        /// <param name="keySort"></param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<string, string[]>> GetIndustryPostDictionaryJs()
        {
             var nameDictionary = new Dictionary<string, Dictionary<string, string[]>>();
            var ctgs = GetAllIndustryCategories(); //��ҵ��
            int ii = 0;
            foreach (var c in ctgs) //ö�ٵ�һ��
            {
               var subDic = new Dictionary<string, string[]>();
                var Is = c.Industries().ToList(); //��ҵ������ҵ

                int j = 0;
                foreach (var i in Is)//ö�ٵڶ���
                {
                    var ps = i.Posts();

                    var dsNames = ps.Select(d => d.ShortName()).ToArray();
                    subDic.Add(i.ShortName(), dsNames);
                }
               
                nameDictionary.Add(c.ToString(), subDic);
            }
            return nameDictionary;
        }

        /// <summary>
        /// ������ҵö�٣�������0
        /// ��ϰ�������ҵ���࣬��ҵ
        /// </summary>
        private static IEnumerable<IndustryPost> GetAllIndustryPosts()
        {
            return
                ((IndustryPost[]) System.Enum.GetValues(typeof (IndustryPost)))
                    .Where(d => d != IndustryPost.��);
        }

        /// <summary>
        /// ְλ�Ķ�����
        /// ������_����==������
        /// </summary>
        /// <param name="industryPost"></param>
        /// <returns></returns>
        public static string ShortName(this IndustryPost industryPost)
        {
            // StringBuilder sb = new StringBuilder();
            var ss = industryPost.ToString().Split('_');
            return ss[ss.Length - 1];//�������һ��
        }

        /// <summary>
        /// ְλ��'�ɾ�'����
        /// ���������_�г�Ӫ��_Ӫ���ܼ�->Ӫ���ܼ�
        /// </summary>
        /// <param name="industryPost"></param>
        /// <returns></returns>
        public static string PostName(this IndustryPost industryPost)
        {
            var name = industryPost.ToString();
            int index = name.LastIndexOf('_');
            if (index < 0)
                return name;
            return name.Substring(index + 1);
        }

  

   

        /// <summary>
        /// �ж��Ƿ�һ�����ݿ���ҵ��������һ����ҵ
        /// ������������
        /// </summary>
        /// <param name="industryPost">���ݿ���ҵ�ֶ�</param>
        /// <param name="upIndustryPost">�����ϼ���ҵ</param>
        /// <returns></returns>
        public static bool BelongTo(this IndustryPost industryPost, IndustryPost upIndustryPost)
        {
            //δ���岻�����κ���������
            if (upIndustryPost == IndustryPost.��)
                return false;

            if (upIndustryPost.IsIndustryCategory()) //��ҵ����
            {
                return (int) industryPost >= (int) upIndustryPost
                       && (int) industryPost <= ((int) upIndustryPost | INDUSTRYMASK);
            }
            if (upIndustryPost.IsIndustry()) //��ҵ
            {
                return (int) industryPost >= (int) upIndustryPost
                       && (int) industryPost <= ((int) upIndustryPost | POSTMASK);
            }
            return industryPost == upIndustryPost;
        }

    
     
        /// <summary>
        /// �ж��Ƿ��Ƿ���
        /// </summary>
        /// <param name="industryPost"></param>
        /// <returns></returns>
        public static bool IsIndustryCategory(this IndustryPost industryPost)
        {
            return ((int) industryPost & INDUSTRYCATEGORYMASK) != 0 && ((int) industryPost & ~INDUSTRYCATEGORYMASK) == 0;
        }

        /// <summary>
        /// �õ���ҵ��Ӧ�����,���ж��Ƿ�Ϸ�
        /// </summary>
        /// <param name="industryPost"></param>
        /// <returns></returns>
        public static IndustryPost IndustryCategory(this IndustryPost industryPost)
        {
            return (IndustryPost) ((int) industryPost & INDUSTRYCATEGORYMASK);
        }


        /// <summary>
        /// �ж��Ƿ��Ǿ�����ҵ
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        public static bool IsIndustry(this IndustryPost industryPost)
        {
            return ((int) industryPost & INDUSTRYMASK) != 0 && ((int) industryPost & ~FULLINDUSTRYMASK) == 0;
        }

        /// <summary>
        /// �õ�������ҵ,���ж��Ƿ�Ϸ�
        /// </summary>
        /// <param name="industryPost"></param>
        /// <returns></returns>
        public static IndustryPost Industry(this IndustryPost industryPost)
        {
            return (IndustryPost) ((int) industryPost & FULLINDUSTRYMASK);
        }


        /// <summary>
        /// �ж��Ƿ��Ǿ�����ҵ
        /// </summary>
        /// <param name="industryPost"></param>
        /// <returns></returns>
        public static bool IsPost(this IndustryPost industryPost)
        {
            return ((int) industryPost & POSTMASK) != 0 && ((int) industryPost & ~FULLPOSTMASK) == 0;
        }

        /// <summary>
        /// �õ�������ҵ,���ж��Ƿ�Ϸ�
        /// </summary>
        /// <param name="industryPost"></param>
        /// <returns></returns>
        public static IndustryPost Post(this IndustryPost industryPost)
        {
            return (IndustryPost) ((int) industryPost & FULLPOSTMASK);
        }

        /// <summary>
        /// �õ�������ҵ����
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IndustryPost> GetAllIndustryCategories()
        {
            return GetAllIndustryPosts().Where(d => d.IsIndustryCategory());

        }



        /// <summary>
        /// �õ�������������ҵ�б�
        /// </summary>
        /// <param name="industryCategory"></param>
        /// <returns></returns>
        public static IEnumerable<IndustryPost> Industries(this IndustryPost industryCategory)
        {
            return GetAllIndustryPosts().Where(r => r.IsIndustry() && r.BelongTo(industryCategory));
        }

        /// <summary>
        /// �õ���ҵ������ְλ�б�
        /// </summary>
        /// <param name="industry"></param>
        /// <returns></returns>
        public static IEnumerable<IndustryPost> Posts(this IndustryPost industry)
        {
            return GetAllIndustryPosts().Where(r => r.IsPost() && r.BelongTo(industry));
        }

        /// <summary>
        /// �õ�����ְλ
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IndustryPost> GetAllPosts()
        {
            return GetAllIndustryPosts().Where(d => d.IsPost());

        }
        /// <summary>
        /// ��һ��ְλת��Ϊ�������ַ���ƴ��
        /// </summary>
        /// <param name="industries"></param>
        /// <returns></returns>
        public static string ToSlashString(this IEnumerable<IndustryPost> industries)
        {
            var sb = new StringBuilder();
            foreach (var post in industries)
            {
                sb.Append(post.ShortName());
                sb.Append('/');
            }
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }
}