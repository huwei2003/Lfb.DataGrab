namespace Comm.Global.Enum.Business
{
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    public enum ProductCategory
    {
        /// <summary>
        /// ��
        /// </summary>
        None = 0,

        /// <summary>
        /// �罻��С��Ʒ
        /// ��ˮ�����ʻ�����ʳ
        /// </summary>
        ˮ�� = ProductBigCategory.С��Ʒ�� | 1,
        �ʻ� = ProductBigCategory.С��Ʒ�� | 2,
        ��ʳ = ProductBigCategory.С��Ʒ�� | 3,


        /// <summary>
        /// ���Ӫ����
        /// ���������������鱦
        /// </summary>
        ���� = ProductBigCategory.����� | 1,
        ���� = ProductBigCategory.����� | 2,
        �鱦 = ProductBigCategory.����� | 3,

        /// <summary>
        /// ������
        /// ��ױƷ������Ʒ
        /// </summary>
        ����Ʒ = ProductBigCategory.������ | 1,
        ���� = ProductBigCategory.������ | 2,
        ����Ʒ = ProductBigCategory.������ | 3,
       

        /// <summary>
        /// ��Ӫ��
        /// ��ˮ�����
        /// </summary>
        Ͱװˮ = ProductBigCategory.��Ӫ�� | 1,
    }
}