namespace Comm.Global.DTO.Factory
{
    /// <summary>
    /// 所有工厂类的基类
    /// </summary>
    /// <typeparam name="TInterface">创建对象的泛型接口</typeparam>
    public abstract class BaseFactory<TInterface>
        where TInterface : class
    {
        private static TInterface _mockImplement;//测试用的mock对象

        protected static TInterface GetMockInstanse
        {
            get { return _mockImplement; }
        } 

        /// <summary>
        /// 设置一个mock对象,用于替换被测代码中注入的接口对象
        /// </summary>
        /// <param name="mock">mock产品对象，null表示关闭mock</param>
        public static void SetMockInstance(TInterface mock)
        {
            _mockImplement = mock;
        }
    }
}
