namespace Comm.Global.Enum.Business
{
    /// <summary>
    /// ��Ʒ����
    /// </summary>
    public enum ProductBigCategory
    {
        /// <summary>
        /// ��
        /// </summary>
        None = 0,

        /// <summary>
        /// �罻��С��Ʒ
        /// ��ˮ�����ʻ�����ʳ
        /// </summary>
        С��Ʒ�� = 1 << ProductCategoryHelper.ProductBigCategoryMove,
       

        /// <summary>
        /// ���Ӫ����
        /// ���������������鱦
        /// </summary>
        ����� = 2 << ProductCategoryHelper.ProductBigCategoryMove,
      
        /// <summary>
        /// ������
        /// ��ױƷ������Ʒ
        /// </summary>
        ������ = 3 << ProductCategoryHelper.ProductBigCategoryMove,
       

        /// <summary>
        /// ��Ӫ��
        /// ��ˮ�����
        /// </summary>
        ��Ӫ�� = 4 << ProductCategoryHelper.ProductBigCategoryMove,
        
    }
}