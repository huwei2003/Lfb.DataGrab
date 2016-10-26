
namespace Comm.Global.Enum.Nature
{
#pragma warning disable 1591


    /// <summary>
    /// 行政区域的枚举
    /// bit31-bit24 //国家/地区
    /// bit23-bit19 //bit15-bit11 省,自行枚举, 5bit,1表示直辖市,31表示其它
    /// bit18-bit14 //bit10-bit6 市,省下枚举
    /// bit13-bit8 //bit5-bit0 区县,省市下枚举
    /// 所有枚举默认0值为'无'
    /// </summary>
    public enum Region 
    {
        /// <summary>
        /// 没有初始化城市
        /// </summary>
        无 = 0,

#pragma warning disable 1591

        #region 所有直辖市虚拟省

        直辖市 = 1 << RegionHelper.Provincelmove,//虚拟省  
        #region 直辖市
        上海市 = 直辖市 | 1 << RegionHelper.Citylmove, //截至2012 2.1 
        #region 区县
        黄浦区 = 上海市 | 1 << RegionHelper.Districtlmove,
        徐汇区 = 上海市 | 2 << RegionHelper.Districtlmove,
        长宁区 = 上海市 | 3 << RegionHelper.Districtlmove,
        静安区 = 上海市 | 4 << RegionHelper.Districtlmove,
        普陀区 = 上海市 | 5 << RegionHelper.Districtlmove,
        闸北区 = 上海市 | 6 << RegionHelper.Districtlmove,
        虹口区 = 上海市 | 7 << RegionHelper.Districtlmove,
        杨浦区 = 上海市 | 8 << RegionHelper.Districtlmove,
        闵行区 = 上海市 | 9 << RegionHelper.Districtlmove,
        宝山区 = 上海市 | 10 << RegionHelper.Districtlmove,
        嘉定区 = 上海市 | 11 << RegionHelper.Districtlmove,
        浦东新区 = 上海市 | 12 << RegionHelper.Districtlmove,
        金山区 = 上海市 | 13 << RegionHelper.Districtlmove,
        松江区 = 上海市 | 14 << RegionHelper.Districtlmove,
        青浦区 = 上海市 | 15 << RegionHelper.Districtlmove,
        奉贤区 = 上海市 | 16 << RegionHelper.Districtlmove,
        崇明县 = 上海市 | 17 << RegionHelper.Districtlmove,
        #endregion 区县

        北京市 = 直辖市 | 2 << RegionHelper.Citylmove, //直辖市
        #region 区县
        东城区 = 北京市 | 1 << RegionHelper.Districtlmove,
        西城区 = 北京市 | 2 << RegionHelper.Districtlmove,
        朝阳区 = 北京市 | 3 << RegionHelper.Districtlmove,
        丰台区 = 北京市 | 4 << RegionHelper.Districtlmove,
        石景山区 = 北京市 | 5 << RegionHelper.Districtlmove,
        海淀区 = 北京市 | 6 << RegionHelper.Districtlmove,
        门头沟区 = 北京市 | 7 << RegionHelper.Districtlmove,
        房山区 = 北京市 | 8 << RegionHelper.Districtlmove,
        通州区 = 北京市 | 9 << RegionHelper.Districtlmove,
        顺义区 = 北京市 | 10 << RegionHelper.Districtlmove,
        昌平区 = 北京市 | 11 << RegionHelper.Districtlmove,
        大兴区 = 北京市 | 12 << RegionHelper.Districtlmove,
        怀柔区 = 北京市 | 13 << RegionHelper.Districtlmove,
        平谷区 = 北京市 | 14 << RegionHelper.Districtlmove,
        密云县 = 北京市 | 15 << RegionHelper.Districtlmove,
        延庆县 = 北京市 | 16 << RegionHelper.Districtlmove,
        #endregion 区县

        天津市 = 直辖市 | 3 << RegionHelper.Citylmove, //直辖市
        #region 区县
        河西区 = 天津市 | 1 << RegionHelper.Districtlmove,
        河东区 = 天津市 | 2 << RegionHelper.Districtlmove,
        和平区 = 天津市 | 3 << RegionHelper.Districtlmove,
        南开区 = 天津市 | 4 << RegionHelper.Districtlmove,
        河北区 = 天津市 | 5 << RegionHelper.Districtlmove,
        红桥区 = 天津市 | 6 << RegionHelper.Districtlmove,
        东丽区 = 天津市 | 7 << RegionHelper.Districtlmove,
        西青区 = 天津市 | 8 << RegionHelper.Districtlmove,
        津南区 = 天津市 | 9 << RegionHelper.Districtlmove,
        北辰区 = 天津市 | 10 << RegionHelper.Districtlmove,
        武清区 = 天津市 | 11 << RegionHelper.Districtlmove,
        宝坻区 = 天津市 | 12 << RegionHelper.Districtlmove,
        滨海新区 = 天津市 | 13 << RegionHelper.Districtlmove,
        宁河县 = 天津市 | 14 << RegionHelper.Districtlmove,
        静海县 = 天津市 | 15 << RegionHelper.Districtlmove,
        蓟县 = 天津市 | 16 << RegionHelper.Districtlmove,
        #endregion 区县

        重庆市 = 直辖市 | 4 << RegionHelper.Citylmove, //直辖市
        #region 区县 截止2011
        渝中区 = 重庆市 | 1 << RegionHelper.Districtlmove,
        大渡口区 = 重庆市 | 2 << RegionHelper.Districtlmove,
        江北区 = 重庆市 | 3 << RegionHelper.Districtlmove,
        南岸区 = 重庆市 | 4 << RegionHelper.Districtlmove,
        北碚区 = 重庆市 | 5 << RegionHelper.Districtlmove,
        渝北区 = 重庆市 | 6 << RegionHelper.Districtlmove,
        巴南区 = 重庆市 | 7 << RegionHelper.Districtlmove,
        长寿区 = 重庆市 | 8 << RegionHelper.Districtlmove,
        大足区 = 重庆市 | 9 << RegionHelper.Districtlmove,
        沙坪坝区 = 重庆市 | 10 << RegionHelper.Districtlmove,
        万州区 = 重庆市 | 11 << RegionHelper.Districtlmove,
        涪陵区 = 重庆市 | 12 << RegionHelper.Districtlmove,
        黔江区 = 重庆市 | 13 << RegionHelper.Districtlmove,
        永川区 = 重庆市 | 14 << RegionHelper.Districtlmove,
        合川区 = 重庆市 | 15 << RegionHelper.Districtlmove,
        江津区 = 重庆市 | 16 << RegionHelper.Districtlmove,
        九龙坡区 = 重庆市 | 17 << RegionHelper.Districtlmove,
        南川区 = 重庆市 | 18 << RegionHelper.Districtlmove,
        綦江区 = 重庆市 | 19 << RegionHelper.Districtlmove,
        潼南县 = 重庆市 | 20 << RegionHelper.Districtlmove,
        荣昌县 = 重庆市 | 21 << RegionHelper.Districtlmove,
        璧山县 = 重庆市 | 22 << RegionHelper.Districtlmove,
        铜梁县 = 重庆市 | 23 << RegionHelper.Districtlmove,
        梁平县 = 重庆市 | 24 << RegionHelper.Districtlmove,
        开县 = 重庆市 | 25 << RegionHelper.Districtlmove,
        忠县 = 重庆市 | 26 << RegionHelper.Districtlmove,
        城口县 = 重庆市 | 27 << RegionHelper.Districtlmove,
        垫江县 = 重庆市 | 28 << RegionHelper.Districtlmove,
        武隆县 = 重庆市 | 29 << RegionHelper.Districtlmove,
        丰都县 = 重庆市 | 30 << RegionHelper.Districtlmove,
        奉节县 = 重庆市 | 31 << RegionHelper.Districtlmove,
        云阳县 = 重庆市 | 32 << RegionHelper.Districtlmove,
        巫溪县 = 重庆市 | 33 << RegionHelper.Districtlmove,
        巫山县 = 重庆市 | 34 << RegionHelper.Districtlmove,
        石柱县 = 重庆市 | 35 << RegionHelper.Districtlmove,
        秀山县 = 重庆市 | 36 << RegionHelper.Districtlmove,
        酉阳县 = 重庆市 | 37 << RegionHelper.Districtlmove,
        彭水县 = 重庆市 | 38 << RegionHelper.Districtlmove,
        北部新区 = 重庆市 | 39<<RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 直辖市
        #endregion


        #region 普通省
        广东 = 2 << RegionHelper.Provincelmove,//省                
        #region 市
        深圳市 = 广东 | 1 << RegionHelper.Citylmove,//2011最后调整区县
        #region 区县
        福田区 = 深圳市 | 1 << RegionHelper.Districtlmove,
        南山区 = 深圳市 | 2 << RegionHelper.Districtlmove,
        罗湖区 = 深圳市 | 3 << RegionHelper.Districtlmove,
        盐田区 = 深圳市 | 4 << RegionHelper.Districtlmove,
        宝安区 = 深圳市 | 5 << RegionHelper.Districtlmove,
        龙岗区 = 深圳市 | 6 << RegionHelper.Districtlmove,
        光明新区 = 深圳市 | 7 << RegionHelper.Districtlmove,
        坪山新区 = 深圳市 | 8 << RegionHelper.Districtlmove,
        龙华新区 = 深圳市 | 9 << RegionHelper.Districtlmove,
        大鹏新区 = 深圳市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        广州市 = 广东 | 2 << RegionHelper.Citylmove,//2005最后调整区县
        #region 区县
        越秀区 = 广州市 | 1 << RegionHelper.Districtlmove,
        海珠区 = 广州市 | 2 << RegionHelper.Districtlmove,
        荔湾区 = 广州市 | 3 << RegionHelper.Districtlmove,
        天河区 = 广州市 | 4 << RegionHelper.Districtlmove,
        白云区 = 广州市 | 5 << RegionHelper.Districtlmove,
        黄埔区 = 广州市 | 6 << RegionHelper.Districtlmove,
        花都区 = 广州市 | 7 << RegionHelper.Districtlmove,
        番禺区 = 广州市 | 8 << RegionHelper.Districtlmove,
        南沙区 = 广州市 | 9 << RegionHelper.Districtlmove,
        萝岗区 = 广州市 | 10 << RegionHelper.Districtlmove,
        增城市 = 广州市 | 11 << RegionHelper.Districtlmove,
        从化市 = 广州市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县


        佛山市 = 广东 | 3 << RegionHelper.Citylmove,
        #region 区县
        禅城区 = 佛山市 | 1 << RegionHelper.Districtlmove,
        南海区 = 佛山市 | 2 << RegionHelper.Districtlmove,
        三水区 = 佛山市 | 3 << RegionHelper.Districtlmove,
        高明区 = 佛山市 | 4 << RegionHelper.Districtlmove,
        顺德区 = 佛山市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县


        东莞市 = 广东 | 4 << RegionHelper.Citylmove,
        #region 区县
        莞城区 = 东莞市 | 1 << RegionHelper.Districtlmove,
        东莞东城区 = 东莞市 | 2 << RegionHelper.Districtlmove,
        南城区 = 东莞市 | 3 << RegionHelper.Districtlmove,
        万江区 = 东莞市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县


        中山市 = 广东 | 5 << RegionHelper.Citylmove,
        #region 区县
        石岐区 = 中山市 | 1 << RegionHelper.Districtlmove,
        东区 = 中山市 | 2 << RegionHelper.Districtlmove,
        西区 = 中山市 | 3 << RegionHelper.Districtlmove,
        南区 = 中山市 | 4 << RegionHelper.Districtlmove,
        五桂山区 = 中山市 | 5 << RegionHelper.Districtlmove,
        火炬开发区 = 中山市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县


        惠州市 = 广东 | 6 << RegionHelper.Citylmove,
        #region 区县
        惠城区 = 惠州市 | 1 << RegionHelper.Districtlmove,
        惠阳区 = 惠州市 | 2 << RegionHelper.Districtlmove,
        博罗县 = 惠州市 | 3 << RegionHelper.Districtlmove,
        惠东县 = 惠州市 | 4 << RegionHelper.Districtlmove,
        龙门县 = 惠州市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县


        茂名市 = 广东 | 7 << RegionHelper.Citylmove,
        #region 区县
        茂南区 = 茂名市 | 1 << RegionHelper.Districtlmove,
        茂港区 = 茂名市 | 2 << RegionHelper.Districtlmove,
        高州市 = 茂名市 | 3 << RegionHelper.Districtlmove,
        化州市 = 茂名市 | 4 << RegionHelper.Districtlmove,
        信宜市 = 茂名市 | 5 << RegionHelper.Districtlmove,
        电白县 = 茂名市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县


        江门市 = 广东 | 8 << RegionHelper.Citylmove,
        #region 区县
        蓬江区 = 江门市 | 1 << RegionHelper.Districtlmove,
        江海区 = 江门市 | 2 << RegionHelper.Districtlmove,
        新会区 = 江门市 | 3 << RegionHelper.Districtlmove,
        台山市 = 江门市 | 4 << RegionHelper.Districtlmove,
        鹤山市 = 江门市 | 5 << RegionHelper.Districtlmove,
        开平市 = 江门市 | 6 << RegionHelper.Districtlmove,
        恩平市 = 江门市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县


        湛江市 = 广东 | 9 << RegionHelper.Citylmove,
        #region 区县
        霞山区 = 湛江市 | 1 << RegionHelper.Districtlmove,
        赤坎区 = 湛江市 | 2 << RegionHelper.Districtlmove,
        坡头区 = 湛江市 | 3 << RegionHelper.Districtlmove,
        麻章区 = 湛江市 | 4 << RegionHelper.Districtlmove,
        遂溪县 = 湛江市 | 5 << RegionHelper.Districtlmove,
        徐闻县 = 湛江市 | 6 << RegionHelper.Districtlmove,
        吴川市 = 湛江市 | 7 << RegionHelper.Districtlmove,
        廉江市 = 湛江市 | 8 << RegionHelper.Districtlmove,
        雷州市 = 湛江市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县


        珠海市 = 广东 | 10 << RegionHelper.Citylmove,
        #region 区县
        香洲区 = 珠海市 | 1 << RegionHelper.Districtlmove,
        金湾区 = 珠海市 | 2 << RegionHelper.Districtlmove,
        斗门区 = 珠海市 | 3 << RegionHelper.Districtlmove,
        #endregion 区县


        汕头市 = 广东 | 11 << RegionHelper.Citylmove,
        #region 区县
        金平区 = 汕头市 | 1 << RegionHelper.Districtlmove,
        龙湖区 = 汕头市 | 2 << RegionHelper.Districtlmove,
        濠江区 = 汕头市 | 3 << RegionHelper.Districtlmove,
        南澳县 = 汕头市 | 4 << RegionHelper.Districtlmove,
        澄海区 = 汕头市 | 5 << RegionHelper.Districtlmove,
        潮阳区 = 汕头市 | 6 << RegionHelper.Districtlmove,
        潮南区 = 汕头市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县


        揭阳市 = 广东 | 12 << RegionHelper.Citylmove,
        #region 区县
        榕城区 = 揭阳市 | 1 << RegionHelper.Districtlmove,
        揭东县 = 揭阳市 | 2 << RegionHelper.Districtlmove,
        揭西县 = 揭阳市 | 3 << RegionHelper.Districtlmove,
        惠来县 = 揭阳市 | 4 << RegionHelper.Districtlmove,
        普宁市 = 揭阳市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县



        肇庆市 = 广东 | 13 << RegionHelper.Citylmove,
        #region 区县
        端州区 = 肇庆市 | 1 << RegionHelper.Districtlmove,
        鼎湖区 = 肇庆市 | 2 << RegionHelper.Districtlmove,
        高要市 = 肇庆市 | 3 << RegionHelper.Districtlmove,
        四会市 = 肇庆市 | 4 << RegionHelper.Districtlmove,
        广宁县 = 肇庆市 | 5 << RegionHelper.Districtlmove,
        德庆县 = 肇庆市 | 6 << RegionHelper.Districtlmove,
        封开县 = 肇庆市 | 7 << RegionHelper.Districtlmove,
        怀集县 = 肇庆市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县


        清远市 = 广东 | 14 << RegionHelper.Citylmove,
        #region 区县
        清城区 = 清远市 | 1 << RegionHelper.Districtlmove,
        佛冈县 = 清远市 | 2 << RegionHelper.Districtlmove,
        阳山县 = 清远市 | 3 << RegionHelper.Districtlmove,
        连山县 = 清远市 | 4 << RegionHelper.Districtlmove,
        连南县 = 清远市 | 5 << RegionHelper.Districtlmove,
        清新县 = 清远市 | 6 << RegionHelper.Districtlmove,
        英德市 = 清远市 | 7 << RegionHelper.Districtlmove,
        连州市 = 清远市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县


        阳江市 = 广东 | 15 << RegionHelper.Citylmove,
        #region 区县
        江城区 = 阳江市 | 1 << RegionHelper.Districtlmove,
        阳西县 = 阳江市 | 2 << RegionHelper.Districtlmove,
        阳东县 = 阳江市 | 3 << RegionHelper.Districtlmove,
        阳春市 = 阳江市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县


        韶关市 = 广东 | 16 << RegionHelper.Citylmove,
        #region 区县
        武江区 = 韶关市 | 1 << RegionHelper.Districtlmove,
        浈江区 = 韶关市 | 2 << RegionHelper.Districtlmove,
        曲江区 = 韶关市 | 3 << RegionHelper.Districtlmove,
        始兴县 = 韶关市 | 4 << RegionHelper.Districtlmove,
        仁化县 = 韶关市 | 5 << RegionHelper.Districtlmove,
        翁源县 = 韶关市 | 6 << RegionHelper.Districtlmove,
        乳源县 = 韶关市 | 7 << RegionHelper.Districtlmove,
        新丰县 = 韶关市 | 8 << RegionHelper.Districtlmove,
        乐昌市 = 韶关市 | 9 << RegionHelper.Districtlmove,
        南雄市 = 韶关市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县


        潮州市 = 广东 | 17 << RegionHelper.Citylmove,
        #region 区县
        湘桥区 = 潮州市 | 1 << RegionHelper.Districtlmove,
        潮安区 = 潮州市 | 2 << RegionHelper.Districtlmove,
        饶平县 = 潮州市 | 3 << RegionHelper.Districtlmove,
        #endregion 区县


        梅州市 = 广东 | 18 << RegionHelper.Citylmove,
        #region 区县
        梅江区 = 梅州市 | 1 << RegionHelper.Districtlmove,
        梅县区 = 梅州市 | 2 << RegionHelper.Districtlmove,
        大埔县 = 梅州市 | 3 << RegionHelper.Districtlmove,
        丰顺县 = 梅州市 | 4 << RegionHelper.Districtlmove,
        五华县 = 梅州市 | 5 << RegionHelper.Districtlmove,
        平远县 = 梅州市 | 6 << RegionHelper.Districtlmove,
        蕉岭县 = 梅州市 | 7 << RegionHelper.Districtlmove,
        兴宁市 = 梅州市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县


        河源市 = 广东 | 19 << RegionHelper.Citylmove,
        #region 区县
        源城区 = 河源市 | 1 << RegionHelper.Districtlmove,
        紫金县 = 河源市 | 2 << RegionHelper.Districtlmove,
        龙川县 = 河源市 | 3 << RegionHelper.Districtlmove,
        连平县 = 河源市 | 4 << RegionHelper.Districtlmove,
        和平县 = 河源市 | 5 << RegionHelper.Districtlmove,
        东源县 = 河源市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县


        汕尾市 = 广东 | 20 << RegionHelper.Citylmove,
        #region 区县
        城区 = 汕尾市 | 1 << RegionHelper.Districtlmove,
        海丰县 = 汕尾市 | 2 << RegionHelper.Districtlmove,
        陆河县 = 汕尾市 | 3 << RegionHelper.Districtlmove,
        陆丰市 = 汕尾市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县


        云浮市 = 广东 | 21 << RegionHelper.Citylmove,
        #region 区县
        云城区 = 云浮市 | 1 << RegionHelper.Districtlmove,
        新兴县 = 云浮市 | 2 << RegionHelper.Districtlmove,
        郁南县 = 云浮市 | 3 << RegionHelper.Districtlmove,
        云安县 = 云浮市 | 4 << RegionHelper.Districtlmove,
        罗定市 = 云浮市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        浙江 = 3 << RegionHelper.Provincelmove,//省                
        #region 市
        杭州市 = 浙江 | 1 << RegionHelper.Citylmove,//2011最后调整区县
        #region 区县
        上城区 = 杭州市 | 1 << RegionHelper.Districtlmove,
        下城区 = 杭州市 | 2 << RegionHelper.Districtlmove,
        江干区 = 杭州市 | 3 << RegionHelper.Districtlmove,
        拱墅区 = 杭州市 | 4 << RegionHelper.Districtlmove,
        西湖区 = 杭州市 | 5 << RegionHelper.Districtlmove,
        滨江区 = 杭州市 | 6 << RegionHelper.Districtlmove,
        萧山区 = 杭州市 | 7 << RegionHelper.Districtlmove,
        余杭区 = 杭州市 | 8 << RegionHelper.Districtlmove,
        桐庐县 = 杭州市 | 9 << RegionHelper.Districtlmove,
        淳安县 = 杭州市 | 10 << RegionHelper.Districtlmove,
        建德市 = 杭州市 | 11 << RegionHelper.Districtlmove,
        富阳市 = 杭州市 | 12 << RegionHelper.Districtlmove,
        临安市 = 杭州市 | 13 << RegionHelper.Districtlmove,
        #endregion 区县
        宁波市 = 浙江 | 2 << RegionHelper.Citylmove,
        #region 区县
        海曙区 = 宁波市 | 1 << RegionHelper.Districtlmove,
        江东区 = 宁波市 | 2 << RegionHelper.Districtlmove,
        宁波江北区 = 宁波市 | 3 << RegionHelper.Districtlmove,
        北仑区 = 宁波市 | 4 << RegionHelper.Districtlmove,
        镇海区 = 宁波市 | 5 << RegionHelper.Districtlmove,
        鄞州区 = 宁波市 | 6 << RegionHelper.Districtlmove,
        象山县 = 宁波市 | 7 << RegionHelper.Districtlmove,
        宁海县 = 宁波市 | 8 << RegionHelper.Districtlmove,
        余姚市 = 宁波市 | 9 << RegionHelper.Districtlmove,
        慈溪市 = 宁波市 | 10 << RegionHelper.Districtlmove,
        奉化市 = 宁波市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        温州市 = 浙江 | 3 << RegionHelper.Citylmove,
        #region 区县
        鹿城区 = 温州市 | 1 << RegionHelper.Districtlmove,
        龙湾区 = 温州市 | 2 << RegionHelper.Districtlmove,
        瓯海区 = 温州市 | 3 << RegionHelper.Districtlmove,
        洞头县 = 温州市 | 4 << RegionHelper.Districtlmove,
        永嘉县 = 温州市 | 5 << RegionHelper.Districtlmove,
        平阳县 = 温州市 | 6 << RegionHelper.Districtlmove,
        苍南县 = 温州市 | 7 << RegionHelper.Districtlmove,
        文成县 = 温州市 | 8 << RegionHelper.Districtlmove,
        泰顺县 = 温州市 | 9 << RegionHelper.Districtlmove,
        瑞安市 = 温州市 | 10 << RegionHelper.Districtlmove,
        乐清市 = 温州市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县
        嘉兴市 = 浙江 | 4 << RegionHelper.Citylmove,
        #region 区县
        南湖区 = 嘉兴市 | 1 << RegionHelper.Districtlmove,
        秀洲区 = 嘉兴市 | 2 << RegionHelper.Districtlmove,
        嘉善县 = 嘉兴市 | 3 << RegionHelper.Districtlmove,
        海盐县 = 嘉兴市 | 4 << RegionHelper.Districtlmove,
        海宁市 = 嘉兴市 | 5 << RegionHelper.Districtlmove,
        平湖市 = 嘉兴市 | 6 << RegionHelper.Districtlmove,
        桐乡市 = 嘉兴市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        湖州市 = 浙江 | 5 << RegionHelper.Citylmove,
        #region 区县
        吴兴区 = 湖州市 | 1 << RegionHelper.Districtlmove,
        南浔区 = 湖州市 | 2 << RegionHelper.Districtlmove,
        德清县 = 湖州市 | 3 << RegionHelper.Districtlmove,
        长兴县 = 湖州市 | 4 << RegionHelper.Districtlmove,
        安吉县 = 湖州市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        绍兴市 = 浙江 | 6 << RegionHelper.Citylmove,
        #region 区县
        越城区 = 绍兴市 | 1 << RegionHelper.Districtlmove,
        柯桥区 = 绍兴市 | 2 << RegionHelper.Districtlmove,
        新昌县 = 绍兴市 | 3 << RegionHelper.Districtlmove,
        诸暨市 = 绍兴市 | 4 << RegionHelper.Districtlmove,
        上虞区 = 绍兴市 | 5 << RegionHelper.Districtlmove,
        嵊州市 = 绍兴市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        金华市 = 浙江 | 7 << RegionHelper.Citylmove,
        #region 区县
        婺城区 = 金华市 | 1 << RegionHelper.Districtlmove,
        金东区 = 金华市 | 2 << RegionHelper.Districtlmove,
        武义县 = 金华市 | 3 << RegionHelper.Districtlmove,
        浦江县 = 金华市 | 4 << RegionHelper.Districtlmove,
        磐安县 = 金华市 | 5 << RegionHelper.Districtlmove,
        兰溪市 = 金华市 | 6 << RegionHelper.Districtlmove,
        义乌市 = 金华市 | 7 << RegionHelper.Districtlmove,
        东阳市 = 金华市 | 8 << RegionHelper.Districtlmove,
        永康市 = 金华市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县


        衢州市 = 浙江 | 8 << RegionHelper.Citylmove,
        #region 区县
        柯城区 = 衢州市 | 1 << RegionHelper.Districtlmove,
        衢江区 = 衢州市 | 2 << RegionHelper.Districtlmove,
        常山县 = 衢州市 | 3 << RegionHelper.Districtlmove,
        开化县 = 衢州市 | 4 << RegionHelper.Districtlmove,
        龙游县 = 衢州市 | 5 << RegionHelper.Districtlmove,
        江山市 = 衢州市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县
        舟山市 = 浙江 | 9 << RegionHelper.Citylmove,
        #region 区县
        定海区 = 舟山市 | 1 << RegionHelper.Districtlmove,
        舟山普陀区 = 舟山市 | 2 << RegionHelper.Districtlmove,
        岱山县 = 舟山市 | 3 << RegionHelper.Districtlmove,
        嵊泗县 = 舟山市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县
        台州市 = 浙江 | 10 << RegionHelper.Citylmove,
        #region 区县
        椒江区 = 台州市 | 1 << RegionHelper.Districtlmove,
        黄岩区 = 台州市 | 2 << RegionHelper.Districtlmove,
        路桥区 = 台州市 | 3 << RegionHelper.Districtlmove,
        玉环县 = 台州市 | 4 << RegionHelper.Districtlmove,
        三门县 = 台州市 | 5 << RegionHelper.Districtlmove,
        天台县 = 台州市 | 6 << RegionHelper.Districtlmove,
        仙居县 = 台州市 | 7 << RegionHelper.Districtlmove,
        温岭市 = 台州市 | 8 << RegionHelper.Districtlmove,
        临海市 = 台州市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县
        丽水市 = 浙江 | 11 << RegionHelper.Citylmove,
        #region 区县
        莲都区 = 丽水市 | 1 << RegionHelper.Districtlmove,
        青田县 = 丽水市 | 2 << RegionHelper.Districtlmove,
        缙云县 = 丽水市 | 3 << RegionHelper.Districtlmove,
        遂昌县 = 丽水市 | 4 << RegionHelper.Districtlmove,
        松阳县 = 丽水市 | 5 << RegionHelper.Districtlmove,
        云和县 = 丽水市 | 6 << RegionHelper.Districtlmove,
        庆元县 = 丽水市 | 7 << RegionHelper.Districtlmove,
        景宁县 = 丽水市 | 8 << RegionHelper.Districtlmove,
        龙泉市 = 丽水市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        江苏 = 4 << RegionHelper.Provincelmove,//省                
        #region 市
        南京市 = 江苏 | 1 << RegionHelper.Citylmove,//2011最后调整区县
        #region 区县
        玄武区 = 南京市 | 1 << RegionHelper.Districtlmove,
        秦淮区 = 南京市 | 2 << RegionHelper.Districtlmove,
        建邺区 = 南京市 | 3 << RegionHelper.Districtlmove,
        鼓楼区 = 南京市 | 4 << RegionHelper.Districtlmove,
        浦口区 = 南京市 | 5 << RegionHelper.Districtlmove,
        栖霞区 = 南京市 | 6 << RegionHelper.Districtlmove,
        雨花台区 = 南京市 | 7 << RegionHelper.Districtlmove,
        江宁区 = 南京市 | 8 << RegionHelper.Districtlmove,
        六合区 = 南京市 | 9 << RegionHelper.Districtlmove,
        溧水区 = 南京市 | 10 << RegionHelper.Districtlmove,
        高淳区 = 南京市 | 11 << RegionHelper.Districtlmove,
        白下区 = 南京市 | 12 << RegionHelper.Districtlmove,
        下关区 = 南京市 | 13 << RegionHelper.Districtlmove,
        #endregion 区县


        无锡市 = 江苏 | 2 << RegionHelper.Citylmove,//2011最后调整区县
        #region 区县
        崇安区 = 无锡市 | 1 << RegionHelper.Districtlmove,
        南长区 = 无锡市 | 2 << RegionHelper.Districtlmove,
        北塘区 = 无锡市 | 3 << RegionHelper.Districtlmove,
        锡山区 = 无锡市 | 4 << RegionHelper.Districtlmove,
        惠山区 = 无锡市 | 5 << RegionHelper.Districtlmove,
        滨湖区 = 无锡市 | 6 << RegionHelper.Districtlmove,
        江阴市 = 无锡市 | 7 << RegionHelper.Districtlmove,
        宜兴市 = 无锡市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县
        徐州市 = 江苏 | 3 << RegionHelper.Citylmove,
        #region 区县
        徐州鼓楼区 = 徐州市 | 1 << RegionHelper.Districtlmove,
        云龙区 = 徐州市 | 2 << RegionHelper.Districtlmove,
        贾汪区 = 徐州市 | 3 << RegionHelper.Districtlmove,
        泉山区 = 徐州市 | 4 << RegionHelper.Districtlmove,
        铜山区 = 徐州市 | 5 << RegionHelper.Districtlmove,
        丰县 = 徐州市 | 6 << RegionHelper.Districtlmove,
        沛县 = 徐州市 | 7 << RegionHelper.Districtlmove,
        睢宁县 = 徐州市 | 8 << RegionHelper.Districtlmove,
        新沂市 = 徐州市 | 9 << RegionHelper.Districtlmove,
        邳州市 = 徐州市 | 10 << RegionHelper.Districtlmove,
        九里区 = 徐州市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        常州市 = 江苏 | 4 << RegionHelper.Citylmove,
        #region 区县
        天宁区 = 常州市 | 1 << RegionHelper.Districtlmove,
        钟楼区 = 常州市 | 2 << RegionHelper.Districtlmove,
        戚墅堰区 = 常州市 | 3 << RegionHelper.Districtlmove,
        新北区 = 常州市 | 4 << RegionHelper.Districtlmove,
        武进区 = 常州市 | 5 << RegionHelper.Districtlmove,
        溧阳市 = 常州市 | 6 << RegionHelper.Districtlmove,
        金坛市 = 常州市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        苏州市 = 江苏 | 5 << RegionHelper.Citylmove,
        #region 区县
        虎丘区 = 苏州市 | 1 << RegionHelper.Districtlmove,
        吴中区 = 苏州市 | 2 << RegionHelper.Districtlmove,
        相城区 = 苏州市 | 3 << RegionHelper.Districtlmove,
        姑苏区 = 苏州市 | 4 << RegionHelper.Districtlmove,
        吴江区 = 苏州市 | 5 << RegionHelper.Districtlmove,
        常熟市 = 苏州市 | 6 << RegionHelper.Districtlmove,
        张家港市 = 苏州市 | 7 << RegionHelper.Districtlmove,
        昆山市 = 苏州市 | 8 << RegionHelper.Districtlmove,
        太仓市 = 苏州市 | 9 << RegionHelper.Districtlmove,
        金阊区 = 苏州市 | 10 << RegionHelper.Districtlmove,
        沧浪区 = 苏州市 | 11 << RegionHelper.Districtlmove,
        平江区 = 苏州市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        南通市 = 江苏 | 6 << RegionHelper.Citylmove,
        #region 区县
        崇川区 = 南通市 | 1 << RegionHelper.Districtlmove,
        港闸区 = 南通市 | 2 << RegionHelper.Districtlmove,
        南通通州区 = 南通市 | 3 << RegionHelper.Districtlmove,
        海安县 = 南通市 | 4 << RegionHelper.Districtlmove,
        如东县 = 南通市 | 5 << RegionHelper.Districtlmove,
        启东市 = 南通市 | 6 << RegionHelper.Districtlmove,
        如皋市 = 南通市 | 7 << RegionHelper.Districtlmove,
        海门市 = 南通市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        连云港市 = 江苏 | 7 << RegionHelper.Citylmove,
        #region 区县
        连云区 = 连云港市 | 1 << RegionHelper.Districtlmove,
        新浦区 = 连云港市 | 2 << RegionHelper.Districtlmove,
        海州区 = 连云港市 | 3 << RegionHelper.Districtlmove,
        赣榆县 = 连云港市 | 4 << RegionHelper.Districtlmove,
        东海县 = 连云港市 | 5 << RegionHelper.Districtlmove,
        灌云县 = 连云港市 | 6 << RegionHelper.Districtlmove,
        灌南县 = 连云港市 | 7 << RegionHelper.Districtlmove,

        #endregion 区县

        淮安市 = 江苏 | 8 << RegionHelper.Citylmove,
        #region 区县
        清河区 = 淮安市 | 1 << RegionHelper.Districtlmove,
        淮安区 = 淮安市 | 2 << RegionHelper.Districtlmove,
        淮阴区 = 淮安市 | 3 << RegionHelper.Districtlmove,
        清浦区 = 淮安市 | 4 << RegionHelper.Districtlmove,
        涟水县 = 淮安市 | 5 << RegionHelper.Districtlmove,
        盱眙县 = 淮安市 | 6 << RegionHelper.Districtlmove,
        金湖县 = 淮安市 | 7 << RegionHelper.Districtlmove,
        洪泽县 = 淮安市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        盐城市 = 江苏 | 9 << RegionHelper.Citylmove,
        #region 区县
        亭湖区 = 盐城市 | 1 << RegionHelper.Districtlmove,
        盐都区 = 盐城市 | 2 << RegionHelper.Districtlmove,
        响水县 = 盐城市 | 3 << RegionHelper.Districtlmove,
        滨海县 = 盐城市 | 4 << RegionHelper.Districtlmove,
        阜宁县 = 盐城市 | 5 << RegionHelper.Districtlmove,
        射阳县 = 盐城市 | 6 << RegionHelper.Districtlmove,
        建湖县 = 盐城市 | 7 << RegionHelper.Districtlmove,
        东台市 = 盐城市 | 8 << RegionHelper.Districtlmove,
        大丰市 = 盐城市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        扬州市 = 江苏 | 10 << RegionHelper.Citylmove,
        #region 区县
        广陵区 = 扬州市 | 1 << RegionHelper.Districtlmove,
        邗江区 = 扬州市 | 2 << RegionHelper.Districtlmove,
        江都区 = 扬州市 | 3 << RegionHelper.Districtlmove,
        宝应县 = 扬州市 | 4 << RegionHelper.Districtlmove,
        仪征市 = 扬州市 | 5 << RegionHelper.Districtlmove,
        高邮市 = 扬州市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        镇江市 = 江苏 | 11 << RegionHelper.Citylmove,
        #region 区县
        京口区 = 镇江市 | 1 << RegionHelper.Districtlmove,
        润州区 = 镇江市 | 2 << RegionHelper.Districtlmove,
        丹徒区 = 镇江市 | 3 << RegionHelper.Districtlmove,
        丹阳市 = 镇江市 | 4 << RegionHelper.Districtlmove,
        扬中市 = 镇江市 | 5 << RegionHelper.Districtlmove,
        句容市 = 镇江市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        泰州市 = 江苏 | 12 << RegionHelper.Citylmove,
        #region 区县
        海陵区 = 泰州市 | 1 << RegionHelper.Districtlmove,
        高港区 = 泰州市 | 2 << RegionHelper.Districtlmove,
        兴化市 = 泰州市 | 3 << RegionHelper.Districtlmove,
        靖江市 = 泰州市 | 4 << RegionHelper.Districtlmove,
        泰兴市 = 泰州市 | 5 << RegionHelper.Districtlmove,
        姜堰区 = 泰州市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        宿迁市 = 江苏 | 13 << RegionHelper.Citylmove,
        #region 区县
        宿城区 = 宿迁市 | 1 << RegionHelper.Districtlmove,
        宿豫区 = 宿迁市 | 2 << RegionHelper.Districtlmove,
        沭阳县 = 宿迁市 | 3 << RegionHelper.Districtlmove,
        泗阳县 = 宿迁市 | 4 << RegionHelper.Districtlmove,
        泗洪县 = 宿迁市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        #endregion 市

        山东 = 5 << RegionHelper.Provincelmove,//省                
        #region 市
        济南市 = 山东 | 1 << RegionHelper.Citylmove,//2011最后调整区县
        #region 区县
        历下区 = 济南市 | 1 << RegionHelper.Districtlmove,
        市中区 = 济南市 | 2 << RegionHelper.Districtlmove,
        槐荫区 = 济南市 | 3 << RegionHelper.Districtlmove,
        天桥区 = 济南市 | 4 << RegionHelper.Districtlmove,
        历城区 = 济南市 | 5 << RegionHelper.Districtlmove,
        长清区 = 济南市 | 6 << RegionHelper.Districtlmove,
        平阴县 = 济南市 | 7 << RegionHelper.Districtlmove,
        济阳县 = 济南市 | 8 << RegionHelper.Districtlmove,
        商河县 = 济南市 | 9 << RegionHelper.Districtlmove,
        章丘市 = 济南市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县
        青岛市 = 山东 | 2 << RegionHelper.Citylmove,
        #region 区县
        市南区 = 青岛市 | 1 << RegionHelper.Districtlmove,
        市北区 = 青岛市 | 2 << RegionHelper.Districtlmove,
        黄岛区 = 青岛市 | 3 << RegionHelper.Districtlmove,
        崂山区 = 青岛市 | 4 << RegionHelper.Districtlmove,
        李沧区 = 青岛市 | 5 << RegionHelper.Districtlmove,
        城阳区 = 青岛市 | 6 << RegionHelper.Districtlmove,
        胶州市 = 青岛市 | 7 << RegionHelper.Districtlmove,
        即墨市 = 青岛市 | 8 << RegionHelper.Districtlmove,
        平度市 = 青岛市 | 9 << RegionHelper.Districtlmove,
        莱西市 = 青岛市 | 10 << RegionHelper.Districtlmove,
        四方区 = 青岛市 | 11 << RegionHelper.Districtlmove,
        胶南市 = 青岛市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县
        淄博市 = 山东 | 3 << RegionHelper.Citylmove,
        #region 区县
        淄川区 = 淄博市 | 1 << RegionHelper.Districtlmove,
        张店区 = 淄博市 | 2 << RegionHelper.Districtlmove,
        博山区 = 淄博市 | 3 << RegionHelper.Districtlmove,
        临淄区 = 淄博市 | 4 << RegionHelper.Districtlmove,
        周村区 = 淄博市 | 5 << RegionHelper.Districtlmove,
        桓台县 = 淄博市 | 6 << RegionHelper.Districtlmove,
        高青县 = 淄博市 | 7 << RegionHelper.Districtlmove,
        沂源县 = 淄博市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        枣庄市 = 山东 | 4 << RegionHelper.Citylmove,
        #region 区县
        枣庄市中区 = 枣庄市 | 1 << RegionHelper.Districtlmove,
        薛城区 = 枣庄市 | 2 << RegionHelper.Districtlmove,
        峄城区 = 枣庄市 | 3 << RegionHelper.Districtlmove,
        台儿庄区 = 枣庄市 | 4 << RegionHelper.Districtlmove,
        山亭区 = 枣庄市 | 5 << RegionHelper.Districtlmove,
        滕州市 = 枣庄市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县
        东营市 = 山东 | 5 << RegionHelper.Citylmove,
        #region 区县
        东营区 = 东营市 | 1 << RegionHelper.Districtlmove,
        河口区 = 东营市 | 2 << RegionHelper.Districtlmove,
        垦利县 = 东营市 | 3 << RegionHelper.Districtlmove,
        利津县 = 东营市 | 4 << RegionHelper.Districtlmove,
        广饶县 = 东营市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县
        烟台市 = 山东 | 6 << RegionHelper.Citylmove,
        #region 区县
        芝罘区 = 烟台市 | 1 << RegionHelper.Districtlmove,
        福山区 = 烟台市 | 2 << RegionHelper.Districtlmove,
        牟平区 = 烟台市 | 3 << RegionHelper.Districtlmove,
        莱山区 = 烟台市 | 4 << RegionHelper.Districtlmove,
        长岛县 = 烟台市 | 5 << RegionHelper.Districtlmove,
        龙口市 = 烟台市 | 6 << RegionHelper.Districtlmove,
        莱阳市 = 烟台市 | 7 << RegionHelper.Districtlmove,
        莱州市 = 烟台市 | 8 << RegionHelper.Districtlmove,
        蓬莱市 = 烟台市 | 9 << RegionHelper.Districtlmove,
        招远市 = 烟台市 | 10 << RegionHelper.Districtlmove,
        栖霞市 = 烟台市 | 11 << RegionHelper.Districtlmove,
        海阳市 = 烟台市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县
        潍坊市 = 山东 | 7 << RegionHelper.Citylmove,
        #region 区县
        潍城区 = 潍坊市 | 1 << RegionHelper.Districtlmove,
        寒亭区 = 潍坊市 | 2 << RegionHelper.Districtlmove,
        坊子区 = 潍坊市 | 3 << RegionHelper.Districtlmove,
        奎文区 = 潍坊市 | 4 << RegionHelper.Districtlmove,
        临朐县 = 潍坊市 | 5 << RegionHelper.Districtlmove,
        昌乐县 = 潍坊市 | 6 << RegionHelper.Districtlmove,
        青州市 = 潍坊市 | 7 << RegionHelper.Districtlmove,
        诸城市 = 潍坊市 | 8 << RegionHelper.Districtlmove,
        寿光市 = 潍坊市 | 9 << RegionHelper.Districtlmove,
        安丘市 = 潍坊市 | 10 << RegionHelper.Districtlmove,
        高密市 = 潍坊市 | 11 << RegionHelper.Districtlmove,
        昌邑市 = 潍坊市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        济宁市 = 山东 | 8 << RegionHelper.Citylmove,
        #region 区县
        任城区 = 济宁市 | 1 << RegionHelper.Districtlmove,
        兖州区 = 济宁市 | 2 << RegionHelper.Districtlmove,
        微山县 = 济宁市 | 3 << RegionHelper.Districtlmove,
        鱼台县 = 济宁市 | 4 << RegionHelper.Districtlmove,
        金乡县 = 济宁市 | 5 << RegionHelper.Districtlmove,
        嘉祥县 = 济宁市 | 6 << RegionHelper.Districtlmove,
        汶上县 = 济宁市 | 7 << RegionHelper.Districtlmove,
        泗水县 = 济宁市 | 8 << RegionHelper.Districtlmove,
        梁山县 = 济宁市 | 9 << RegionHelper.Districtlmove,
        曲阜市 = 济宁市 | 10 << RegionHelper.Districtlmove,
        邹城市 = 济宁市 | 11 << RegionHelper.Districtlmove,

        #endregion 区县
        泰安市 = 山东 | 9 << RegionHelper.Citylmove,
        #region 区县
        泰山区 = 泰安市 | 1 << RegionHelper.Districtlmove,
        岱岳区 = 泰安市 | 2 << RegionHelper.Districtlmove,
        宁阳县 = 泰安市 | 3 << RegionHelper.Districtlmove,
        东平县 = 泰安市 | 4 << RegionHelper.Districtlmove,
        新泰市 = 泰安市 | 5 << RegionHelper.Districtlmove,
        肥城市 = 泰安市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县
        威海市 = 山东 | 10 << RegionHelper.Citylmove,
        #region 区县
        环翠区 = 威海市 | 1 << RegionHelper.Districtlmove,
        文登市 = 威海市 | 2 << RegionHelper.Districtlmove,
        荣成市 = 威海市 | 3 << RegionHelper.Districtlmove,
        乳山市 = 威海市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县
        日照市 = 山东 | 11 << RegionHelper.Citylmove,
        #region 区县
        东港区 = 日照市 | 1 << RegionHelper.Districtlmove,
        岚山区 = 日照市 | 2 << RegionHelper.Districtlmove,
        五莲县 = 日照市 | 3 << RegionHelper.Districtlmove,
        莒县 = 日照市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县
        莱芜市 = 山东 | 12 << RegionHelper.Citylmove,
        #region 区县
        莱城区 = 莱芜市 | 1 << RegionHelper.Districtlmove,
        钢城区 = 莱芜市 | 2 << RegionHelper.Districtlmove,
        #endregion 区县

        临沂市 = 山东 | 13 << RegionHelper.Citylmove,
        #region 区县
        兰山区 = 临沂市 | 1 << RegionHelper.Districtlmove,
        罗庄区 = 临沂市 | 2 << RegionHelper.Districtlmove,
        临沂河东区 = 临沂市 | 3 << RegionHelper.Districtlmove,
        沂南县 = 临沂市 | 4 << RegionHelper.Districtlmove,
        郯城县 = 临沂市 | 5 << RegionHelper.Districtlmove,
        沂水县 = 临沂市 | 6 << RegionHelper.Districtlmove,
        苍山县 = 临沂市 | 7 << RegionHelper.Districtlmove,
        费县 = 临沂市 | 8 << RegionHelper.Districtlmove,
        平邑县 = 临沂市 | 9 << RegionHelper.Districtlmove,
        莒南县 = 临沂市 | 10 << RegionHelper.Districtlmove,
        蒙阴县 = 临沂市 | 11 << RegionHelper.Districtlmove,
        临沭县 = 临沂市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县
        德州市 = 山东 | 14 << RegionHelper.Citylmove,
        #region 区县
        德城区 = 德州市 | 1 << RegionHelper.Districtlmove,
        陵县 = 德州市 | 2 << RegionHelper.Districtlmove,
        宁津县 = 德州市 | 3 << RegionHelper.Districtlmove,
        庆云县 = 德州市 | 4 << RegionHelper.Districtlmove,
        临邑县 = 德州市 | 5 << RegionHelper.Districtlmove,
        齐河县 = 德州市 | 6 << RegionHelper.Districtlmove,
        平原县 = 德州市 | 7 << RegionHelper.Districtlmove,
        夏津县 = 德州市 | 8 << RegionHelper.Districtlmove,
        武城县 = 德州市 | 9 << RegionHelper.Districtlmove,
        乐陵市 = 德州市 | 10 << RegionHelper.Districtlmove,
        禹城市 = 德州市 | 11 << RegionHelper.Districtlmove,

        #endregion 区县
        聊城市 = 山东 | 15 << RegionHelper.Citylmove,
        #region 区县
        东昌府区 = 聊城市 | 1 << RegionHelper.Districtlmove,
        阳谷县 = 聊城市 | 2 << RegionHelper.Districtlmove,
        莘县 = 聊城市 | 3 << RegionHelper.Districtlmove,
        茌平县 = 聊城市 | 4 << RegionHelper.Districtlmove,
        东阿县 = 聊城市 | 5 << RegionHelper.Districtlmove,
        冠县 = 聊城市 | 6 << RegionHelper.Districtlmove,
        高唐县 = 聊城市 | 7 << RegionHelper.Districtlmove,
        临清市 = 聊城市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县
        滨州市 = 山东 | 16 << RegionHelper.Citylmove,
        #region 区县
        滨城区 = 滨州市 | 1 << RegionHelper.Districtlmove,
        惠民县 = 滨州市 | 2 << RegionHelper.Districtlmove,
        阳信县 = 滨州市 | 3 << RegionHelper.Districtlmove,
        无棣县 = 滨州市 | 4 << RegionHelper.Districtlmove,
        沾化县 = 滨州市 | 5 << RegionHelper.Districtlmove,
        博兴县 = 滨州市 | 6 << RegionHelper.Districtlmove,
        邹平县 = 滨州市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        菏泽市 = 山东 | 17 << RegionHelper.Citylmove,
        #region 区县
        牡丹区 = 菏泽市 | 1 << RegionHelper.Districtlmove,
        曹县 = 菏泽市 | 2 << RegionHelper.Districtlmove,
        单县 = 菏泽市 | 3 << RegionHelper.Districtlmove,
        成武县 = 菏泽市 | 4 << RegionHelper.Districtlmove,
        巨野县 = 菏泽市 | 5 << RegionHelper.Districtlmove,
        郓城县 = 菏泽市 | 6 << RegionHelper.Districtlmove,
        鄄城县 = 菏泽市 | 7 << RegionHelper.Districtlmove,
        定陶县 = 菏泽市 | 8 << RegionHelper.Districtlmove,
        东明县 = 菏泽市 | 9 << RegionHelper.Districtlmove,

        #endregion 区县
        #endregion 市

        河南 = 6 << RegionHelper.Provincelmove,//省                
        #region 市
        郑州市 = 河南 | 1 << RegionHelper.Citylmove,
        #region 区县
        中原区 = 郑州市 | 1 << RegionHelper.Districtlmove,
        二七区 = 郑州市 | 2 << RegionHelper.Districtlmove,
        管城回族区 = 郑州市 | 3 << RegionHelper.Districtlmove,
        金水区 = 郑州市 | 4 << RegionHelper.Districtlmove,
        上街区 = 郑州市 | 5 << RegionHelper.Districtlmove,
        惠济区 = 郑州市 | 6 << RegionHelper.Districtlmove,
        中牟县 = 郑州市 | 7 << RegionHelper.Districtlmove,
        巩义市 = 郑州市 | 8 << RegionHelper.Districtlmove,
        荥阳市 = 郑州市 | 9 << RegionHelper.Districtlmove,
        新密市 = 郑州市 | 10 << RegionHelper.Districtlmove,
        新郑市 = 郑州市 | 11 << RegionHelper.Districtlmove,
        登封市 = 郑州市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县
        开封市 = 河南 | 2 << RegionHelper.Citylmove,
        #region 区县
        龙亭区 = 开封市 | 1 << RegionHelper.Districtlmove,
        顺河回族区 = 开封市 | 2 << RegionHelper.Districtlmove,
        开封鼓楼区 = 开封市 | 3 << RegionHelper.Districtlmove,
        禹王台区 = 开封市 | 4 << RegionHelper.Districtlmove,
        金明区 = 开封市 | 5 << RegionHelper.Districtlmove,
        杞县 = 开封市 | 6 << RegionHelper.Districtlmove,
        通许县 = 开封市 | 7 << RegionHelper.Districtlmove,
        尉氏县 = 开封市 | 8 << RegionHelper.Districtlmove,
        开封县 = 开封市 | 9 << RegionHelper.Districtlmove,
        兰考县 = 开封市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县
        洛阳市 = 河南 | 3 << RegionHelper.Citylmove,
        #region 区县
        老城区 = 洛阳市 | 1 << RegionHelper.Districtlmove,
        西工区 = 洛阳市 | 2 << RegionHelper.Districtlmove,
        瀍河区 = 洛阳市 | 3 << RegionHelper.Districtlmove,
        涧西区 = 洛阳市 | 4 << RegionHelper.Districtlmove,
        吉利区 = 洛阳市 | 5 << RegionHelper.Districtlmove,
        洛龙区 = 洛阳市 | 6 << RegionHelper.Districtlmove,
        孟津县 = 洛阳市 | 7 << RegionHelper.Districtlmove,
        新安县 = 洛阳市 | 8 << RegionHelper.Districtlmove,
        栾川县 = 洛阳市 | 9 << RegionHelper.Districtlmove,
        嵩县 = 洛阳市 | 10 << RegionHelper.Districtlmove,
        汝阳县 = 洛阳市 | 11 << RegionHelper.Districtlmove,
        宜阳县 = 洛阳市 | 12 << RegionHelper.Districtlmove,
        洛宁县 = 洛阳市 | 13 << RegionHelper.Districtlmove,
        伊川县 = 洛阳市 | 14 << RegionHelper.Districtlmove,
        偃师市 = 洛阳市 | 15 << RegionHelper.Districtlmove,
        #endregion 区县

        平顶山市 = 河南 | 4 << RegionHelper.Citylmove,
        #region 区县
        平顶山新华区 = 平顶山市 | 1 << RegionHelper.Districtlmove,
        卫东区 = 平顶山市 | 2 << RegionHelper.Districtlmove,
        石龙区 = 平顶山市 | 3 << RegionHelper.Districtlmove,
        湛河区 = 平顶山市 | 4 << RegionHelper.Districtlmove,
        宝丰县 = 平顶山市 | 5 << RegionHelper.Districtlmove,
        叶县 = 平顶山市 | 6 << RegionHelper.Districtlmove,
        鲁山县 = 平顶山市 | 7 << RegionHelper.Districtlmove,
        郏县 = 平顶山市 | 8 << RegionHelper.Districtlmove,
        舞钢市 = 平顶山市 | 9 << RegionHelper.Districtlmove,
        汝州市 = 平顶山市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        安阳市 = 河南 | 5 << RegionHelper.Citylmove,
        #region 区县
        文峰区 = 安阳市 | 1 << RegionHelper.Districtlmove,
        北关区 = 安阳市 | 2 << RegionHelper.Districtlmove,
        殷都区 = 安阳市 | 3 << RegionHelper.Districtlmove,
        龙安区 = 安阳市 | 4 << RegionHelper.Districtlmove,
        安阳县 = 安阳市 | 5 << RegionHelper.Districtlmove,
        汤阴县 = 安阳市 | 6 << RegionHelper.Districtlmove,
        滑县 = 安阳市 | 7 << RegionHelper.Districtlmove,
        内黄县 = 安阳市 | 8 << RegionHelper.Districtlmove,
        林州市 = 安阳市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        鹤壁市 = 河南 | 6 << RegionHelper.Citylmove,
        #region 区县
        鹤山区 = 鹤壁市 | 1 << RegionHelper.Districtlmove,
        山城区 = 鹤壁市 | 2 << RegionHelper.Districtlmove,
        淇滨区 = 鹤壁市 | 3 << RegionHelper.Districtlmove,
        浚县 = 鹤壁市 | 4 << RegionHelper.Districtlmove,
        淇县 = 鹤壁市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        新乡市 = 河南 | 7 << RegionHelper.Citylmove,
        #region 区县
        红旗区 = 新乡市 | 1 << RegionHelper.Districtlmove,
        卫滨区 = 新乡市 | 2 << RegionHelper.Districtlmove,
        凤泉区 = 新乡市 | 3 << RegionHelper.Districtlmove,
        牧野区 = 新乡市 | 4 << RegionHelper.Districtlmove,
        新乡县 = 新乡市 | 5 << RegionHelper.Districtlmove,
        获嘉县 = 新乡市 | 6 << RegionHelper.Districtlmove,
        原阳县 = 新乡市 | 7 << RegionHelper.Districtlmove,
        延津县 = 新乡市 | 8 << RegionHelper.Districtlmove,
        封丘县 = 新乡市 | 9 << RegionHelper.Districtlmove,
        长垣县 = 新乡市 | 10 << RegionHelper.Districtlmove,
        卫辉市 = 新乡市 | 11 << RegionHelper.Districtlmove,
        辉县市 = 新乡市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        焦作市 = 河南 | 8 << RegionHelper.Citylmove,
        #region 区县
        解放区 = 焦作市 | 1 << RegionHelper.Districtlmove,
        中站区 = 焦作市 | 2 << RegionHelper.Districtlmove,
        马村区 = 焦作市 | 3 << RegionHelper.Districtlmove,
        山阳区 = 焦作市 | 4 << RegionHelper.Districtlmove,
        修武县 = 焦作市 | 5 << RegionHelper.Districtlmove,
        博爱县 = 焦作市 | 6 << RegionHelper.Districtlmove,
        武陟县 = 焦作市 | 7 << RegionHelper.Districtlmove,
        温县 = 焦作市 | 8 << RegionHelper.Districtlmove,
        沁阳市 = 焦作市 | 9 << RegionHelper.Districtlmove,
        孟州市 = 焦作市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        濮阳市 = 河南 | 9 << RegionHelper.Citylmove,
        #region 区县
        华龙区 = 濮阳市 | 1 << RegionHelper.Districtlmove,
        清丰县 = 濮阳市 | 2 << RegionHelper.Districtlmove,
        南乐县 = 濮阳市 | 3 << RegionHelper.Districtlmove,
        范县 = 濮阳市 | 4 << RegionHelper.Districtlmove,
        台前县 = 濮阳市 | 5 << RegionHelper.Districtlmove,
        濮阳县 = 濮阳市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        许昌市 = 河南 | 10 << RegionHelper.Citylmove,
        #region 区县
        魏都区 = 许昌市 | 1 << RegionHelper.Districtlmove,
        许昌县 = 许昌市 | 2 << RegionHelper.Districtlmove,
        鄢陵县 = 许昌市 | 3 << RegionHelper.Districtlmove,
        襄城县 = 许昌市 | 4 << RegionHelper.Districtlmove,
        禹州市 = 许昌市 | 5 << RegionHelper.Districtlmove,
        长葛市 = 许昌市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        漯河市 = 河南 | 11 << RegionHelper.Citylmove,
        #region 区县
        源汇区 = 漯河市 | 1 << RegionHelper.Districtlmove,
        郾城区 = 漯河市 | 2 << RegionHelper.Districtlmove,
        召陵区 = 漯河市 | 3 << RegionHelper.Districtlmove,
        舞阳县 = 漯河市 | 4 << RegionHelper.Districtlmove,
        临颍县 = 漯河市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        三门峡市 = 河南 | 12 << RegionHelper.Citylmove,
        #region 区县
        湖滨区 = 三门峡市 | 1 << RegionHelper.Districtlmove,
        渑池县 = 三门峡市 | 2 << RegionHelper.Districtlmove,
        陕县 = 三门峡市 | 3 << RegionHelper.Districtlmove,
        卢氏县 = 三门峡市 | 4 << RegionHelper.Districtlmove,
        义马市 = 三门峡市 | 5 << RegionHelper.Districtlmove,
        灵宝市 = 三门峡市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        南阳市 = 河南 | 13 << RegionHelper.Citylmove,
        #region 区县
        宛城区 = 南阳市 | 1 << RegionHelper.Districtlmove,
        卧龙区 = 南阳市 | 2 << RegionHelper.Districtlmove,
        南召县 = 南阳市 | 3 << RegionHelper.Districtlmove,
        方城县 = 南阳市 | 4 << RegionHelper.Districtlmove,
        西峡县 = 南阳市 | 5 << RegionHelper.Districtlmove,
        镇平县 = 南阳市 | 6 << RegionHelper.Districtlmove,
        内乡县 = 南阳市 | 7 << RegionHelper.Districtlmove,
        淅川县 = 南阳市 | 8 << RegionHelper.Districtlmove,
        社旗县 = 南阳市 | 9 << RegionHelper.Districtlmove,
        唐河县 = 南阳市 | 10 << RegionHelper.Districtlmove,
        新野县 = 南阳市 | 11 << RegionHelper.Districtlmove,
        桐柏县 = 南阳市 | 12 << RegionHelper.Districtlmove,
        邓州市 = 南阳市 | 13 << RegionHelper.Districtlmove,
        #endregion 区县

        商丘市 = 河南 | 14 << RegionHelper.Citylmove,
        #region 区县
        梁园区 = 商丘市 | 1 << RegionHelper.Districtlmove,
        睢阳区 = 商丘市 | 2 << RegionHelper.Districtlmove,
        民权县 = 商丘市 | 3 << RegionHelper.Districtlmove,
        睢县 = 商丘市 | 4 << RegionHelper.Districtlmove,
        宁陵县 = 商丘市 | 5 << RegionHelper.Districtlmove,
        柘城县 = 商丘市 | 6 << RegionHelper.Districtlmove,
        虞城县 = 商丘市 | 7 << RegionHelper.Districtlmove,
        夏邑县 = 商丘市 | 8 << RegionHelper.Districtlmove,
        永城市 = 商丘市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        信阳市 = 河南 | 15 << RegionHelper.Citylmove,
        #region 区县
        浉河区 = 信阳市 | 1 << RegionHelper.Districtlmove,
        平桥区 = 信阳市 | 2 << RegionHelper.Districtlmove,
        罗山县 = 信阳市 | 3 << RegionHelper.Districtlmove,
        光山县 = 信阳市 | 4 << RegionHelper.Districtlmove,
        新县 = 信阳市 | 5 << RegionHelper.Districtlmove,
        商城县 = 信阳市 | 6 << RegionHelper.Districtlmove,
        固始县 = 信阳市 | 7 << RegionHelper.Districtlmove,
        潢川县 = 信阳市 | 8 << RegionHelper.Districtlmove,
        淮滨县 = 信阳市 | 9 << RegionHelper.Districtlmove,
        息县 = 信阳市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县
        周口市 = 河南 | 16 << RegionHelper.Citylmove,
        #region 区县
        川汇区 = 周口市 | 1 << RegionHelper.Districtlmove,
        扶沟县 = 周口市 | 2 << RegionHelper.Districtlmove,
        西华县 = 周口市 | 3 << RegionHelper.Districtlmove,
        商水县 = 周口市 | 4 << RegionHelper.Districtlmove,
        沈丘县 = 周口市 | 5 << RegionHelper.Districtlmove,
        郸城县 = 周口市 | 6 << RegionHelper.Districtlmove,
        淮阳县 = 周口市 | 7 << RegionHelper.Districtlmove,
        太康县 = 周口市 | 8 << RegionHelper.Districtlmove,
        鹿邑县 = 周口市 | 9 << RegionHelper.Districtlmove,
        项城市 = 周口市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        驻马店市 = 河南 | 17 << RegionHelper.Citylmove,
        #region 区县
        驿城区 = 驻马店市 | 1 << RegionHelper.Districtlmove,
        西平县 = 驻马店市 | 2 << RegionHelper.Districtlmove,
        上蔡县 = 驻马店市 | 3 << RegionHelper.Districtlmove,
        平舆县 = 驻马店市 | 4 << RegionHelper.Districtlmove,
        正阳县 = 驻马店市 | 5 << RegionHelper.Districtlmove,
        确山县 = 驻马店市 | 6 << RegionHelper.Districtlmove,
        泌阳县 = 驻马店市 | 7 << RegionHelper.Districtlmove,
        汝南县 = 驻马店市 | 8 << RegionHelper.Districtlmove,
        遂平县 = 驻马店市 | 9 << RegionHelper.Districtlmove,
        新蔡县 = 驻马店市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县
        济源市 = 河南 | 18 << RegionHelper.Citylmove,
        #region 区县
        沁园街道 = 济源市 | 1 << RegionHelper.Districtlmove,
        济水街道 = 济源市 | 2 << RegionHelper.Districtlmove,
        北海街道 = 济源市 | 3 << RegionHelper.Districtlmove,
        天坛街道 = 济源市 | 4 << RegionHelper.Districtlmove,
        玉泉街道 = 济源市 | 5 << RegionHelper.Districtlmove,
        克井镇 = 济源市 | 6 << RegionHelper.Districtlmove,
        五龙口镇 = 济源市 | 7 << RegionHelper.Districtlmove,
        轵城镇 = 济源市 | 8 << RegionHelper.Districtlmove,
        承留镇 = 济源市 | 9 << RegionHelper.Districtlmove,
        邵原镇 = 济源市 | 10 << RegionHelper.Districtlmove,
        坡头镇 = 济源市 | 11 << RegionHelper.Districtlmove,
        梨林镇 = 济源市 | 12 << RegionHelper.Districtlmove,
        大峪镇 = 济源市 | 13 << RegionHelper.Districtlmove,
        思礼镇 = 济源市 | 14 << RegionHelper.Districtlmove,
        王屋镇 = 济源市 | 15 << RegionHelper.Districtlmove,
        下冶镇 = 济源市 | 16 << RegionHelper.Districtlmove,

        #endregion 区县
        #endregion 市

        河北 = 7 << RegionHelper.Provincelmove,//省                
        #region 市
        石家庄市 = 河北 | 1 << RegionHelper.Citylmove,
        #region 区县
        长安区 = 石家庄市 | 1 << RegionHelper.Districtlmove,
        桥东区 = 石家庄市 | 2 << RegionHelper.Districtlmove,
        桥西区 = 石家庄市 | 3 << RegionHelper.Districtlmove,
        新华区 = 石家庄市 | 4 << RegionHelper.Districtlmove,
        井陉矿区 = 石家庄市 | 5 << RegionHelper.Districtlmove,
        裕华区 = 石家庄市 | 6 << RegionHelper.Districtlmove,
        井陉县 = 石家庄市 | 7 << RegionHelper.Districtlmove,
        正定县 = 石家庄市 | 8 << RegionHelper.Districtlmove,
        栾城县 = 石家庄市 | 9 << RegionHelper.Districtlmove,
        行唐县 = 石家庄市 | 10 << RegionHelper.Districtlmove,
        灵寿县 = 石家庄市 | 11 << RegionHelper.Districtlmove,
        高邑县 = 石家庄市 | 12 << RegionHelper.Districtlmove,
        深泽县 = 石家庄市 | 13 << RegionHelper.Districtlmove,
        赞皇县 = 石家庄市 | 14 << RegionHelper.Districtlmove,
        无极县 = 石家庄市 | 15 << RegionHelper.Districtlmove,
        平山县 = 石家庄市 | 16 << RegionHelper.Districtlmove,
        元氏县 = 石家庄市 | 17 << RegionHelper.Districtlmove,
        赵县 = 石家庄市 | 18 << RegionHelper.Districtlmove,
        辛集市 = 石家庄市 | 19 << RegionHelper.Districtlmove,
        藁城市 = 石家庄市 | 20 << RegionHelper.Districtlmove,
        晋州市 = 石家庄市 | 21 << RegionHelper.Districtlmove,
        新乐市 = 石家庄市 | 22 << RegionHelper.Districtlmove,
        鹿泉市 = 石家庄市 | 23 << RegionHelper.Districtlmove,
        #endregion 区县
        唐山市 = 河北 | 2 << RegionHelper.Citylmove,
        #region 区县
        路南区 = 唐山市 | 1 << RegionHelper.Districtlmove,
        路北区 = 唐山市 | 2 << RegionHelper.Districtlmove,
        古冶区 = 唐山市 | 3 << RegionHelper.Districtlmove,
        开平区 = 唐山市 | 4 << RegionHelper.Districtlmove,
        丰南区 = 唐山市 | 5 << RegionHelper.Districtlmove,
        丰润区 = 唐山市 | 6 << RegionHelper.Districtlmove,
        滦县 = 唐山市 | 7 << RegionHelper.Districtlmove,
        滦南县 = 唐山市 | 8 << RegionHelper.Districtlmove,
        乐亭县 = 唐山市 | 9 << RegionHelper.Districtlmove,
        迁西县 = 唐山市 | 10 << RegionHelper.Districtlmove,
        玉田县 = 唐山市 | 11 << RegionHelper.Districtlmove,
        曹妃甸区 = 唐山市 | 12 << RegionHelper.Districtlmove,
        遵化市 = 唐山市 | 13 << RegionHelper.Districtlmove,
        迁安市 = 唐山市 | 14 << RegionHelper.Districtlmove,
        #endregion 区县
        秦皇岛市 = 河北 | 3 << RegionHelper.Citylmove,
        #region 区县
        海港区 = 秦皇岛市 | 1 << RegionHelper.Districtlmove,
        山海关区 = 秦皇岛市 | 2 << RegionHelper.Districtlmove,
        北戴河区 = 秦皇岛市 | 3 << RegionHelper.Districtlmove,
        青龙县 = 秦皇岛市 | 4 << RegionHelper.Districtlmove,
        昌黎县 = 秦皇岛市 | 5 << RegionHelper.Districtlmove,
        抚宁县 = 秦皇岛市 | 6 << RegionHelper.Districtlmove,
        卢龙县 = 秦皇岛市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        邯郸市 = 河北 | 4 << RegionHelper.Citylmove,
        #region 区县
        邯山区 = 邯郸市 | 1 << RegionHelper.Districtlmove,
        丛台区 = 邯郸市 | 2 << RegionHelper.Districtlmove,
        复兴区 = 邯郸市 | 3 << RegionHelper.Districtlmove,
        峰峰矿区 = 邯郸市 | 4 << RegionHelper.Districtlmove,
        邯郸县 = 邯郸市 | 5 << RegionHelper.Districtlmove,
        临漳县 = 邯郸市 | 6 << RegionHelper.Districtlmove,
        成安县 = 邯郸市 | 7 << RegionHelper.Districtlmove,
        大名县 = 邯郸市 | 8 << RegionHelper.Districtlmove,
        涉县 = 邯郸市 | 9 << RegionHelper.Districtlmove,
        磁县 = 邯郸市 | 10 << RegionHelper.Districtlmove,
        肥乡县 = 邯郸市 | 11 << RegionHelper.Districtlmove,
        永年县 = 邯郸市 | 12 << RegionHelper.Districtlmove,
        邱县 = 邯郸市 | 13 << RegionHelper.Districtlmove,
        鸡泽县 = 邯郸市 | 14 << RegionHelper.Districtlmove,
        广平县 = 邯郸市 | 15 << RegionHelper.Districtlmove,
        馆陶县 = 邯郸市 | 16 << RegionHelper.Districtlmove,
        魏县 = 邯郸市 | 17 << RegionHelper.Districtlmove,
        曲周县 = 邯郸市 | 18 << RegionHelper.Districtlmove,
        武安市 = 邯郸市 | 19 << RegionHelper.Districtlmove,
        #endregion 区县
        邢台市 = 河北 | 5 << RegionHelper.Citylmove,
        #region 区县
        邢台桥东区 = 邢台市 | 1 << RegionHelper.Districtlmove,
        邢台桥西区 = 邢台市 | 2 << RegionHelper.Districtlmove,
        邢台县 = 邢台市 | 3 << RegionHelper.Districtlmove,
        临城县 = 邢台市 | 4 << RegionHelper.Districtlmove,
        内丘县 = 邢台市 | 5 << RegionHelper.Districtlmove,
        柏乡县 = 邢台市 | 6 << RegionHelper.Districtlmove,
        隆尧县 = 邢台市 | 7 << RegionHelper.Districtlmove,
        任县 = 邢台市 | 8 << RegionHelper.Districtlmove,
        南和县 = 邢台市 | 9 << RegionHelper.Districtlmove,
        宁晋县 = 邢台市 | 10 << RegionHelper.Districtlmove,
        巨鹿县 = 邢台市 | 11 << RegionHelper.Districtlmove,
        新河县 = 邢台市 | 12 << RegionHelper.Districtlmove,
        广宗县 = 邢台市 | 13 << RegionHelper.Districtlmove,
        平乡县 = 邢台市 | 14 << RegionHelper.Districtlmove,
        威县 = 邢台市 | 15 << RegionHelper.Districtlmove,
        清河县 = 邢台市 | 16 << RegionHelper.Districtlmove,
        临西县 = 邢台市 | 17 << RegionHelper.Districtlmove,
        南宫市 = 邢台市 | 18 << RegionHelper.Districtlmove,
        沙河市 = 邢台市 | 19 << RegionHelper.Districtlmove,
        #endregion 区县

        保定市 = 河北 | 6 << RegionHelper.Citylmove,
        #region 区县
        保定新市区 = 保定市 | 1 << RegionHelper.Districtlmove,
        北市区 = 保定市 | 2 << RegionHelper.Districtlmove,
        南市区 = 保定市 | 3 << RegionHelper.Districtlmove,
        满城县 = 保定市 | 4 << RegionHelper.Districtlmove,
        清苑县 = 保定市 | 5 << RegionHelper.Districtlmove,
        涞水县 = 保定市 | 6 << RegionHelper.Districtlmove,
        阜平县 = 保定市 | 7 << RegionHelper.Districtlmove,
        徐水县 = 保定市 | 8 << RegionHelper.Districtlmove,
        定兴县 = 保定市 | 9 << RegionHelper.Districtlmove,
        唐县 = 保定市 | 10 << RegionHelper.Districtlmove,
        高阳县 = 保定市 | 11 << RegionHelper.Districtlmove,
        容城县 = 保定市 | 12 << RegionHelper.Districtlmove,
        涞源县 = 保定市 | 13 << RegionHelper.Districtlmove,
        望都县 = 保定市 | 14 << RegionHelper.Districtlmove,
        安新县 = 保定市 | 15 << RegionHelper.Districtlmove,
        易县 = 保定市 | 16 << RegionHelper.Districtlmove,
        曲阳县 = 保定市 | 17 << RegionHelper.Districtlmove,
        蠡县 = 保定市 | 18 << RegionHelper.Districtlmove,
        顺平县 = 保定市 | 19 << RegionHelper.Districtlmove,
        博野县 = 保定市 | 20 << RegionHelper.Districtlmove,
        雄县 = 保定市 | 21 << RegionHelper.Districtlmove,
        涿州市 = 保定市 | 22 << RegionHelper.Districtlmove,
        定州市 = 保定市 | 23 << RegionHelper.Districtlmove,
        安国市 = 保定市 | 24 << RegionHelper.Districtlmove,
        高碑店市 = 保定市 | 25 << RegionHelper.Districtlmove,
        #endregion 区县
        张家口市 = 河北 | 7 << RegionHelper.Citylmove,
        #region 区县
        张家口桥东区 = 张家口市 | 1 << RegionHelper.Districtlmove,
        张家口桥西区 = 张家口市 | 2 << RegionHelper.Districtlmove,
        宣化区 = 张家口市 | 3 << RegionHelper.Districtlmove,
        下花园区 = 张家口市 | 4 << RegionHelper.Districtlmove,
        宣化县 = 张家口市 | 5 << RegionHelper.Districtlmove,
        张北县 = 张家口市 | 6 << RegionHelper.Districtlmove,
        康保县 = 张家口市 | 7 << RegionHelper.Districtlmove,
        沽源县 = 张家口市 | 8 << RegionHelper.Districtlmove,
        尚义县 = 张家口市 | 9 << RegionHelper.Districtlmove,
        蔚县 = 张家口市 | 10 << RegionHelper.Districtlmove,
        阳原县 = 张家口市 | 11 << RegionHelper.Districtlmove,
        怀安县 = 张家口市 | 12 << RegionHelper.Districtlmove,
        万全县 = 张家口市 | 13 << RegionHelper.Districtlmove,
        怀来县 = 张家口市 | 14 << RegionHelper.Districtlmove,
        涿鹿县 = 张家口市 | 15 << RegionHelper.Districtlmove,
        赤城县 = 张家口市 | 16 << RegionHelper.Districtlmove,
        崇礼县 = 张家口市 | 17 << RegionHelper.Districtlmove,
        #endregion 区县

        承德市 = 河北 | 8 << RegionHelper.Citylmove,
        #region 区县
        双桥区 = 承德市 | 1 << RegionHelper.Districtlmove,
        双滦区 = 承德市 | 2 << RegionHelper.Districtlmove,
        鹰手营子矿区 = 承德市 | 3 << RegionHelper.Districtlmove,
        承德县 = 承德市 | 4 << RegionHelper.Districtlmove,
        兴隆县 = 承德市 | 5 << RegionHelper.Districtlmove,
        平泉县 = 承德市 | 6 << RegionHelper.Districtlmove,
        滦平县 = 承德市 | 7 << RegionHelper.Districtlmove,
        隆化县 = 承德市 | 8 << RegionHelper.Districtlmove,
        丰宁满族自治县 = 承德市 | 9 << RegionHelper.Districtlmove,
        宽城县 = 承德市 | 10 << RegionHelper.Districtlmove,
        围场县 = 承德市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        沧州市 = 河北 | 9 << RegionHelper.Citylmove,
        #region 区县
        沧州新华区 = 沧州市 | 1 << RegionHelper.Districtlmove,
        运河区 = 沧州市 | 2 << RegionHelper.Districtlmove,
        沧县 = 沧州市 | 3 << RegionHelper.Districtlmove,
        青县 = 沧州市 | 4 << RegionHelper.Districtlmove,
        东光县 = 沧州市 | 5 << RegionHelper.Districtlmove,
        海兴县 = 沧州市 | 6 << RegionHelper.Districtlmove,
        盐山县 = 沧州市 | 7 << RegionHelper.Districtlmove,
        肃宁县 = 沧州市 | 8 << RegionHelper.Districtlmove,
        南皮县 = 沧州市 | 9 << RegionHelper.Districtlmove,
        吴桥县 = 沧州市 | 10 << RegionHelper.Districtlmove,
        献县 = 沧州市 | 11 << RegionHelper.Districtlmove,
        孟村回族自治县 = 沧州市 | 12 << RegionHelper.Districtlmove,
        泊头市 = 沧州市 | 13 << RegionHelper.Districtlmove,
        任丘市 = 沧州市 | 14 << RegionHelper.Districtlmove,
        黄骅市 = 沧州市 | 15 << RegionHelper.Districtlmove,
        河间市 = 沧州市 | 16 << RegionHelper.Districtlmove,
        #endregion 区县

        廊坊市 = 河北 | 10 << RegionHelper.Citylmove,
        #region 区县
        安次区 = 廊坊市 | 1 << RegionHelper.Districtlmove,
        广阳区 = 廊坊市 | 2 << RegionHelper.Districtlmove,
        固安县 = 廊坊市 | 3 << RegionHelper.Districtlmove,
        永清县 = 廊坊市 | 4 << RegionHelper.Districtlmove,
        香河县 = 廊坊市 | 5 << RegionHelper.Districtlmove,
        大城县 = 廊坊市 | 6 << RegionHelper.Districtlmove,
        文安县 = 廊坊市 | 7 << RegionHelper.Districtlmove,
        大厂回族自治县 = 廊坊市 | 8 << RegionHelper.Districtlmove,
        霸州市 = 廊坊市 | 9 << RegionHelper.Districtlmove,
        三河市 = 廊坊市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县
        衡水市 = 河北 | 11 << RegionHelper.Citylmove,
        #region 区县
        桃城区 = 衡水市 | 1 << RegionHelper.Districtlmove,
        枣强县 = 衡水市 | 2 << RegionHelper.Districtlmove,
        武邑县 = 衡水市 | 3 << RegionHelper.Districtlmove,
        武强县 = 衡水市 | 4 << RegionHelper.Districtlmove,
        饶阳县 = 衡水市 | 5 << RegionHelper.Districtlmove,
        安平县 = 衡水市 | 6 << RegionHelper.Districtlmove,
        故城县 = 衡水市 | 7 << RegionHelper.Districtlmove,
        景县 = 衡水市 | 8 << RegionHelper.Districtlmove,
        阜城县 = 衡水市 | 9 << RegionHelper.Districtlmove,
        冀州市 = 衡水市 | 10 << RegionHelper.Districtlmove,
        深州市 = 衡水市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        辽宁 = 8 << RegionHelper.Provincelmove,//省                
        #region 市
        沈阳市 = 辽宁 | 1 << RegionHelper.Citylmove,
        #region 区县
        沈阳和平区 = 沈阳市 | 1 << RegionHelper.Districtlmove,
        沈河区 = 沈阳市 | 2 << RegionHelper.Districtlmove,
        大东区 = 沈阳市 | 3 << RegionHelper.Districtlmove,
        皇姑区 = 沈阳市 | 4 << RegionHelper.Districtlmove,
        铁西区 = 沈阳市 | 5 << RegionHelper.Districtlmove,
        苏家屯区 = 沈阳市 | 6 << RegionHelper.Districtlmove,
        东陵区 = 沈阳市 | 7 << RegionHelper.Districtlmove,
        新城子 = 沈阳市 | 8 << RegionHelper.Districtlmove,
        于洪区 = 沈阳市 | 9 << RegionHelper.Districtlmove,
        辽中县 = 沈阳市 | 10 << RegionHelper.Districtlmove,
        康平县 = 沈阳市 | 11 << RegionHelper.Districtlmove,
        法库县 = 沈阳市 | 12 << RegionHelper.Districtlmove,
        新民市 = 沈阳市 | 13 << RegionHelper.Districtlmove,
        #endregion 区县

        大连市 = 辽宁 | 2 << RegionHelper.Citylmove,
        #region 区县
        中山区 = 大连市 | 1 << RegionHelper.Districtlmove,
        西岗区 = 大连市 | 2 << RegionHelper.Districtlmove,
        沙河口区 = 大连市 | 3 << RegionHelper.Districtlmove,
        甘井子区 = 大连市 | 4 << RegionHelper.Districtlmove,
        旅顺口区 = 大连市 | 5 << RegionHelper.Districtlmove,
        金州区 = 大连市 | 6 << RegionHelper.Districtlmove,
        长海县 = 大连市 | 7 << RegionHelper.Districtlmove,
        瓦房店市 = 大连市 | 8 << RegionHelper.Districtlmove,
        普兰店市 = 大连市 | 9 << RegionHelper.Districtlmove,
        庄河市 = 大连市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        鞍山市 = 辽宁 | 3 << RegionHelper.Citylmove,
        #region 区县
        铁东区 = 鞍山市 | 1 << RegionHelper.Districtlmove,
        鞍山铁西区 = 鞍山市 | 2 << RegionHelper.Districtlmove,
        立山区 = 鞍山市 | 3 << RegionHelper.Districtlmove,
        千山区 = 鞍山市 | 4 << RegionHelper.Districtlmove,
        台安县 = 鞍山市 | 5 << RegionHelper.Districtlmove,
        岫岩满族自治县 = 鞍山市 | 6 << RegionHelper.Districtlmove,
        海城市 = 鞍山市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        抚顺市 = 辽宁 | 4 << RegionHelper.Citylmove,
        #region 区县
        新抚区 = 抚顺市 | 1 << RegionHelper.Districtlmove,
        东洲区 = 抚顺市 | 2 << RegionHelper.Districtlmove,
        望花区 = 抚顺市 | 3 << RegionHelper.Districtlmove,
        顺城区 = 抚顺市 | 4 << RegionHelper.Districtlmove,
        抚顺县 = 抚顺市 | 5 << RegionHelper.Districtlmove,
        新宾满族自治县 = 抚顺市 | 6 << RegionHelper.Districtlmove,
        清原满族自治县 = 抚顺市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        本溪市 = 辽宁 | 5 << RegionHelper.Citylmove,
        #region 区县
        平山区 = 本溪市 | 1 << RegionHelper.Districtlmove,
        溪湖区 = 本溪市 | 2 << RegionHelper.Districtlmove,
        明山区 = 本溪市 | 3 << RegionHelper.Districtlmove,
        南芬区 = 本溪市 | 4 << RegionHelper.Districtlmove,
        本溪满族自治县 = 本溪市 | 5 << RegionHelper.Districtlmove,
        桓仁满族自治县 = 本溪市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        丹东市 = 辽宁 | 6 << RegionHelper.Citylmove,
        #region 区县
        元宝区 = 丹东市 | 1 << RegionHelper.Districtlmove,
        振兴区 = 丹东市 | 2 << RegionHelper.Districtlmove,
        振安区 = 丹东市 | 3 << RegionHelper.Districtlmove,
        宽甸满族自治县 = 丹东市 | 4 << RegionHelper.Districtlmove,
        东港市 = 丹东市 | 5 << RegionHelper.Districtlmove,
        凤城市 = 丹东市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        锦州市 = 辽宁 | 7 << RegionHelper.Citylmove,
        #region 区县
        古塔区 = 锦州市 | 1 << RegionHelper.Districtlmove,
        凌河区 = 锦州市 | 2 << RegionHelper.Districtlmove,
        太和区 = 锦州市 | 3 << RegionHelper.Districtlmove,
        黑山县 = 锦州市 | 4 << RegionHelper.Districtlmove,
        义县 = 锦州市 | 5 << RegionHelper.Districtlmove,
        凌海市 = 锦州市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        营口市 = 辽宁 | 8 << RegionHelper.Citylmove,
        #region 区县
        站前区 = 营口市 | 1 << RegionHelper.Districtlmove,
        西市区 = 营口市 | 2 << RegionHelper.Districtlmove,
        鲅鱼圈区 = 营口市 | 3 << RegionHelper.Districtlmove,
        老边区 = 营口市 | 4 << RegionHelper.Districtlmove,
        盖州市 = 营口市 | 5 << RegionHelper.Districtlmove,
        大石桥市 = 营口市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        阜新市 = 辽宁 | 9 << RegionHelper.Citylmove,
        #region 区县
        阜新海州区 = 阜新市 | 1 << RegionHelper.Districtlmove,
        新邱区 = 阜新市 | 2 << RegionHelper.Districtlmove,
        太平区 = 阜新市 | 3 << RegionHelper.Districtlmove,
        清河门区 = 阜新市 | 4 << RegionHelper.Districtlmove,
        细河区 = 阜新市 | 5 << RegionHelper.Districtlmove,
        阜新蒙古族自治县 = 阜新市 | 6 << RegionHelper.Districtlmove,
        彰武县 = 阜新市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县


        辽阳市 = 辽宁 | 10 << RegionHelper.Citylmove,
        #region 区县
        白塔区 = 辽阳市 | 1 << RegionHelper.Districtlmove,
        文圣区 = 辽阳市 | 2 << RegionHelper.Districtlmove,
        宏伟区 = 辽阳市 | 3 << RegionHelper.Districtlmove,
        弓长岭区 = 辽阳市 | 4 << RegionHelper.Districtlmove,
        太子河区 = 辽阳市 | 5 << RegionHelper.Districtlmove,
        辽阳县 = 辽阳市 | 6 << RegionHelper.Districtlmove,
        灯塔市 = 辽阳市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        盘锦市 = 辽宁 | 11 << RegionHelper.Citylmove,
        #region 区县
        双台子区 = 盘锦市 | 1 << RegionHelper.Districtlmove,
        兴隆台区 = 盘锦市 | 2 << RegionHelper.Districtlmove,
        大洼县 = 盘锦市 | 3 << RegionHelper.Districtlmove,
        盘山县 = 盘锦市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        铁岭市 = 辽宁 | 12 << RegionHelper.Citylmove,
        #region 区县
        银州区 = 铁岭市 | 1 << RegionHelper.Districtlmove,
        铁岭清河区 = 铁岭市 | 2 << RegionHelper.Districtlmove,
        铁岭县 = 铁岭市 | 3 << RegionHelper.Districtlmove,
        西丰县 = 铁岭市 | 4 << RegionHelper.Districtlmove,
        昌图县 = 铁岭市 | 5 << RegionHelper.Districtlmove,
        调兵山市 = 铁岭市 | 6 << RegionHelper.Districtlmove,
        开原市 = 铁岭市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        朝阳市 = 辽宁 | 13 << RegionHelper.Citylmove,
        #region 区县
        双塔区 = 朝阳市 | 1 << RegionHelper.Districtlmove,
        龙城区 = 朝阳市 | 2 << RegionHelper.Districtlmove,
        朝阳县 = 朝阳市 | 3 << RegionHelper.Districtlmove,
        建平县 = 朝阳市 | 4 << RegionHelper.Districtlmove,
        喀左县 = 朝阳市 | 5 << RegionHelper.Districtlmove,
        北票市 = 朝阳市 | 6 << RegionHelper.Districtlmove,
        凌源市 = 朝阳市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        葫芦岛市 = 辽宁 | 14 << RegionHelper.Citylmove,
        #region 区县
        连山区 = 葫芦岛市 | 1 << RegionHelper.Districtlmove,
        龙港区 = 葫芦岛市 | 2 << RegionHelper.Districtlmove,
        南票区 = 葫芦岛市 | 3 << RegionHelper.Districtlmove,
        绥中县 = 葫芦岛市 | 4 << RegionHelper.Districtlmove,
        建昌县 = 葫芦岛市 | 5 << RegionHelper.Districtlmove,
        兴城市 = 葫芦岛市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        四川 = 9 << RegionHelper.Provincelmove,//省                
        #region 市
        成都市 = 四川 | 1 << RegionHelper.Citylmove,
        #region 区县
        锦江区 = 成都市 | 1 << RegionHelper.Districtlmove,
        青羊区 = 成都市 | 2 << RegionHelper.Districtlmove,
        金牛区 = 成都市 | 3 << RegionHelper.Districtlmove,
        武侯区 = 成都市 | 4 << RegionHelper.Districtlmove,
        成华区 = 成都市 | 5 << RegionHelper.Districtlmove,
        龙泉驿区 = 成都市 | 6 << RegionHelper.Districtlmove,
        青白江区 = 成都市 | 7 << RegionHelper.Districtlmove,
        新都区 = 成都市 | 8 << RegionHelper.Districtlmove,
        温江区 = 成都市 | 9 << RegionHelper.Districtlmove,
        金堂县 = 成都市 | 10 << RegionHelper.Districtlmove,
        双流县 = 成都市 | 11 << RegionHelper.Districtlmove,
        郫县 = 成都市 | 12 << RegionHelper.Districtlmove,
        大邑县 = 成都市 | 13 << RegionHelper.Districtlmove,
        蒲江县 = 成都市 | 14 << RegionHelper.Districtlmove,
        新津县 = 成都市 | 15 << RegionHelper.Districtlmove,
        都江堰市 = 成都市 | 16 << RegionHelper.Districtlmove,
        彭州市 = 成都市 | 17 << RegionHelper.Districtlmove,
        邛崃市 = 成都市 | 18 << RegionHelper.Districtlmove,
        崇州市 = 成都市 | 19 << RegionHelper.Districtlmove,
        #endregion 区县
        自贡市 = 四川 | 2 << RegionHelper.Citylmove,
        #region 区县
        自流井区 = 自贡市 | 1 << RegionHelper.Districtlmove,
        贡井区 = 自贡市 | 2 << RegionHelper.Districtlmove,
        大安区 = 自贡市 | 3 << RegionHelper.Districtlmove,
        沿滩区 = 自贡市 | 4 << RegionHelper.Districtlmove,
        荣县 = 自贡市 | 5 << RegionHelper.Districtlmove,
        富顺县 = 自贡市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县
        攀枝花市 = 四川 | 3 << RegionHelper.Citylmove,
        #region 区县
        攀枝花东区 = 攀枝花市 | 1 << RegionHelper.Districtlmove,
        攀枝花西区 = 攀枝花市 | 2 << RegionHelper.Districtlmove,
        仁和区 = 攀枝花市 | 3 << RegionHelper.Districtlmove,
        米易县 = 攀枝花市 | 4 << RegionHelper.Districtlmove,
        盐边县 = 攀枝花市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县
        泸州市 = 四川 | 4 << RegionHelper.Citylmove,
        #region 区县
        江阳区 = 泸州市 | 1 << RegionHelper.Districtlmove,
        纳溪区 = 泸州市 | 2 << RegionHelper.Districtlmove,
        龙马潭区 = 泸州市 | 3 << RegionHelper.Districtlmove,
        泸县 = 泸州市 | 4 << RegionHelper.Districtlmove,
        合江县 = 泸州市 | 5 << RegionHelper.Districtlmove,
        叙永县 = 泸州市 | 6 << RegionHelper.Districtlmove,
        古蔺县 = 泸州市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县
        德阳市 = 四川 | 5 << RegionHelper.Citylmove,
        #region 区县
        旌阳区 = 德阳市 | 1 << RegionHelper.Districtlmove,
        中江县 = 德阳市 | 2 << RegionHelper.Districtlmove,
        罗江县 = 德阳市 | 3 << RegionHelper.Districtlmove,
        广汉市 = 德阳市 | 4 << RegionHelper.Districtlmove,
        什邡市 = 德阳市 | 5 << RegionHelper.Districtlmove,
        绵竹市 = 德阳市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县
        绵阳市 = 四川 | 6 << RegionHelper.Citylmove,
        #region 区县
        涪城区 = 绵阳市 | 1 << RegionHelper.Districtlmove,
        游仙区 = 绵阳市 | 2 << RegionHelper.Districtlmove,
        三台县 = 绵阳市 | 3 << RegionHelper.Districtlmove,
        盐亭县 = 绵阳市 | 4 << RegionHelper.Districtlmove,
        安县 = 绵阳市 | 5 << RegionHelper.Districtlmove,
        梓潼县 = 绵阳市 | 6 << RegionHelper.Districtlmove,
        北川县 = 绵阳市 | 7 << RegionHelper.Districtlmove,
        平武县 = 绵阳市 | 8 << RegionHelper.Districtlmove,
        江油市 = 绵阳市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        广元市 = 四川 | 7 << RegionHelper.Citylmove,
        #region 区县
        利州区 = 广元市 | 1 << RegionHelper.Districtlmove,
        昭化区 = 广元市 | 2 << RegionHelper.Districtlmove,
        朝天区 = 广元市 | 3 << RegionHelper.Districtlmove,
        旺苍县 = 广元市 | 4 << RegionHelper.Districtlmove,
        青川县 = 广元市 | 5 << RegionHelper.Districtlmove,
        剑阁县 = 广元市 | 6 << RegionHelper.Districtlmove,
        苍溪县 = 广元市 | 7 << RegionHelper.Districtlmove,

        #endregion 区县
        遂宁市 = 四川 | 8 << RegionHelper.Citylmove,
        #region 区县
        船山区 = 遂宁市 | 1 << RegionHelper.Districtlmove,
        安居区 = 遂宁市 | 2 << RegionHelper.Districtlmove,
        蓬溪县 = 遂宁市 | 3 << RegionHelper.Districtlmove,
        射洪县 = 遂宁市 | 4 << RegionHelper.Districtlmove,
        大英县 = 遂宁市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        内江市 = 四川 | 9 << RegionHelper.Citylmove,
        #region 区县
        内江市中区 = 内江市 | 1 << RegionHelper.Districtlmove,
        东兴区 = 内江市 | 2 << RegionHelper.Districtlmove,
        威远县 = 内江市 | 3 << RegionHelper.Districtlmove,
        资中县 = 内江市 | 4 << RegionHelper.Districtlmove,
        隆昌县 = 内江市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县
        乐山市 = 四川 | 10 << RegionHelper.Citylmove,
        #region 区县
        乐山市中区 = 乐山市 | 1 << RegionHelper.Districtlmove,
        沙湾区 = 乐山市 | 2 << RegionHelper.Districtlmove,
        五通桥区 = 乐山市 | 3 << RegionHelper.Districtlmove,
        金口河区 = 乐山市 | 4 << RegionHelper.Districtlmove,
        犍为县 = 乐山市 | 5 << RegionHelper.Districtlmove,
        井研县 = 乐山市 | 6 << RegionHelper.Districtlmove,
        夹江县 = 乐山市 | 7 << RegionHelper.Districtlmove,
        沐川县 = 乐山市 | 8 << RegionHelper.Districtlmove,
        峨边彝族自治县 = 乐山市 | 9 << RegionHelper.Districtlmove,
        马边彝族自治县 = 乐山市 | 10 << RegionHelper.Districtlmove,
        峨眉山市 = 乐山市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        南充市 = 四川 | 11 << RegionHelper.Citylmove,
        #region 区县
        顺庆区 = 南充市 | 1 << RegionHelper.Districtlmove,
        高坪区 = 南充市 | 2 << RegionHelper.Districtlmove,
        嘉陵区 = 南充市 | 3 << RegionHelper.Districtlmove,
        南部县 = 南充市 | 4 << RegionHelper.Districtlmove,
        营山县 = 南充市 | 5 << RegionHelper.Districtlmove,
        蓬安县 = 南充市 | 6 << RegionHelper.Districtlmove,
        仪陇县 = 南充市 | 7 << RegionHelper.Districtlmove,
        西充县 = 南充市 | 8 << RegionHelper.Districtlmove,
        阆中市 = 南充市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        眉山市 = 四川 | 12 << RegionHelper.Citylmove,
        #region 区县
        东坡区 = 眉山市 | 1 << RegionHelper.Districtlmove,
        仁寿县 = 眉山市 | 2 << RegionHelper.Districtlmove,
        彭山县 = 眉山市 | 3 << RegionHelper.Districtlmove,
        洪雅县 = 眉山市 | 4 << RegionHelper.Districtlmove,
        丹棱县 = 眉山市 | 5 << RegionHelper.Districtlmove,
        青神县 = 眉山市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        宜宾市 = 四川 | 13 << RegionHelper.Citylmove,
        #region 区县
        翠屏区 = 宜宾市 | 1 << RegionHelper.Districtlmove,
        南溪区 = 宜宾市 | 2 << RegionHelper.Districtlmove,
        宜宾县 = 宜宾市 | 3 << RegionHelper.Districtlmove,
        江安县 = 宜宾市 | 4 << RegionHelper.Districtlmove,
        长宁县 = 宜宾市 | 5 << RegionHelper.Districtlmove,
        高县 = 宜宾市 | 6 << RegionHelper.Districtlmove,
        珙县 = 宜宾市 | 7 << RegionHelper.Districtlmove,
        筠连县 = 宜宾市 | 8 << RegionHelper.Districtlmove,
        兴文县 = 宜宾市 | 9 << RegionHelper.Districtlmove,
        屏山县 = 宜宾市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        广安市 = 四川 | 14 << RegionHelper.Citylmove,
        #region 区县
        前锋区 = 广安市 | 1 << RegionHelper.Districtlmove,
        广安区 = 广安市 | 2 << RegionHelper.Districtlmove,
        岳池县 = 广安市 | 3 << RegionHelper.Districtlmove,
        武胜县 = 广安市 | 4 << RegionHelper.Districtlmove,
        邻水县 = 广安市 | 5 << RegionHelper.Districtlmove,
        华蓥市 = 广安市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县
        达州市 = 四川 | 15 << RegionHelper.Citylmove,
        #region 区县
        通川区 = 达州市 | 1 << RegionHelper.Districtlmove,
        达川区 = 达州市 | 2 << RegionHelper.Districtlmove,
        宣汉县 = 达州市 | 3 << RegionHelper.Districtlmove,
        开江县 = 达州市 | 4 << RegionHelper.Districtlmove,
        大竹县 = 达州市 | 5 << RegionHelper.Districtlmove,
        渠县 = 达州市 | 6 << RegionHelper.Districtlmove,
        万源市 = 达州市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        雅安市 = 四川 | 16 << RegionHelper.Citylmove,
        #region 区县
        雨城区 = 雅安市 | 1 << RegionHelper.Districtlmove,
        名山区 = 雅安市 | 2 << RegionHelper.Districtlmove,
        荥经县 = 雅安市 | 3 << RegionHelper.Districtlmove,
        汉源县 = 雅安市 | 4 << RegionHelper.Districtlmove,
        石棉县 = 雅安市 | 5 << RegionHelper.Districtlmove,
        天全县 = 雅安市 | 6 << RegionHelper.Districtlmove,
        芦山县 = 雅安市 | 7 << RegionHelper.Districtlmove,
        宝兴县 = 雅安市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        巴中市 = 四川 | 17 << RegionHelper.Citylmove,
        #region 区县
        巴州区 = 巴中市 | 1 << RegionHelper.Districtlmove,
        恩阳区 = 巴中市 | 2 << RegionHelper.Districtlmove,
        通江县 = 巴中市 | 3 << RegionHelper.Districtlmove,
        南江县 = 巴中市 | 4 << RegionHelper.Districtlmove,
        平昌县 = 巴中市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        资阳市 = 四川 | 18 << RegionHelper.Citylmove,
        #region 区县
        雁江区 = 资阳市 | 1 << RegionHelper.Districtlmove,
        安岳县 = 资阳市 | 2 << RegionHelper.Districtlmove,
        乐至县 = 资阳市 | 3 << RegionHelper.Districtlmove,
        简阳市 = 资阳市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县
        阿坝市 = 四川 | 19 << RegionHelper.Citylmove,
        #region 区县
        汶川县 = 阿坝市 | 1 << RegionHelper.Districtlmove,
        理县 = 阿坝市 | 2 << RegionHelper.Districtlmove,
        茂县 = 阿坝市 | 3 << RegionHelper.Districtlmove,
        松潘县 = 阿坝市 | 4 << RegionHelper.Districtlmove,
        九寨沟 = 阿坝市 | 5 << RegionHelper.Districtlmove,
        金川县 = 阿坝市 | 6 << RegionHelper.Districtlmove,
        小金县 = 阿坝市 | 7 << RegionHelper.Districtlmove,
        黑水县 = 阿坝市 | 8 << RegionHelper.Districtlmove,
        马尔康县 = 阿坝市 | 9 << RegionHelper.Districtlmove,
        壤塘县 = 阿坝市 | 10 << RegionHelper.Districtlmove,
        阿坝县 = 阿坝市 | 11 << RegionHelper.Districtlmove,
        若尔盖县 = 阿坝市 | 12 << RegionHelper.Districtlmove,
        红原县 = 阿坝市 | 13 << RegionHelper.Districtlmove,
        #endregion 区县

        甘孜州 = 四川 | 20 << RegionHelper.Citylmove,
        #region 区县
        康定县 = 甘孜州 | 1 << RegionHelper.Districtlmove,
        泸定县 = 甘孜州 | 2 << RegionHelper.Districtlmove,
        丹巴县 = 甘孜州 | 3 << RegionHelper.Districtlmove,
        九龙县 = 甘孜州 | 4 << RegionHelper.Districtlmove,
        雅江县 = 甘孜州 | 5 << RegionHelper.Districtlmove,
        道孚县 = 甘孜州 | 6 << RegionHelper.Districtlmove,
        炉霍县 = 甘孜州 | 7 << RegionHelper.Districtlmove,
        甘孜县 = 甘孜州 | 8 << RegionHelper.Districtlmove,
        新龙县 = 甘孜州 | 9 << RegionHelper.Districtlmove,
        德格县 = 甘孜州 | 10 << RegionHelper.Districtlmove,
        白玉县 = 甘孜州 | 11 << RegionHelper.Districtlmove,
        石渠县 = 甘孜州 | 12 << RegionHelper.Districtlmove,
        色达县 = 甘孜州 | 13 << RegionHelper.Districtlmove,
        理塘县 = 甘孜州 | 14 << RegionHelper.Districtlmove,
        巴塘县 = 甘孜州 | 15 << RegionHelper.Districtlmove,
        乡城县 = 甘孜州 | 16 << RegionHelper.Districtlmove,
        稻城县 = 甘孜州 | 17 << RegionHelper.Districtlmove,
        得荣县 = 甘孜州 | 18 << RegionHelper.Districtlmove,
        #endregion 区县

        凉山州 = 四川 | 21 << RegionHelper.Citylmove,
        #region 区县
        西昌市 = 凉山州 | 1 << RegionHelper.Districtlmove,
        木里县 = 凉山州 | 2 << RegionHelper.Districtlmove,
        盐源县 = 凉山州 | 3 << RegionHelper.Districtlmove,
        德昌县 = 凉山州 | 4 << RegionHelper.Districtlmove,
        会理县 = 凉山州 | 5 << RegionHelper.Districtlmove,
        会东县 = 凉山州 | 6 << RegionHelper.Districtlmove,
        宁南县 = 凉山州 | 7 << RegionHelper.Districtlmove,
        普格县 = 凉山州 | 8 << RegionHelper.Districtlmove,
        布拖县 = 凉山州 | 9 << RegionHelper.Districtlmove,
        金阳县 = 凉山州 | 10 << RegionHelper.Districtlmove,
        昭觉县 = 凉山州 | 11 << RegionHelper.Districtlmove,
        喜德县 = 凉山州 | 12 << RegionHelper.Districtlmove,
        冕宁县 = 凉山州 | 13 << RegionHelper.Districtlmove,
        越西县 = 凉山州 | 14 << RegionHelper.Districtlmove,
        甘洛县 = 凉山州 | 15 << RegionHelper.Districtlmove,
        美姑县 = 凉山州 | 16 << RegionHelper.Districtlmove,
        雷波县 = 凉山州 | 17 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        湖北 = 10 << RegionHelper.Provincelmove,//省                
        #region 市
        武汉市 = 湖北 | 1 << RegionHelper.Citylmove,
        #region 区县
        江岸区 = 武汉市 | 1 << RegionHelper.Districtlmove,
        江汉区 = 武汉市 | 2 << RegionHelper.Districtlmove,
        硚口区 = 武汉市 | 3 << RegionHelper.Districtlmove,
        汉阳区 = 武汉市 | 4 << RegionHelper.Districtlmove,
        武昌区 = 武汉市 | 5 << RegionHelper.Districtlmove,
        青山区 = 武汉市 | 6 << RegionHelper.Districtlmove,
        洪山区 = 武汉市 | 7 << RegionHelper.Districtlmove,
        东西湖区 = 武汉市 | 8 << RegionHelper.Districtlmove,
        汉南区 = 武汉市 | 9 << RegionHelper.Districtlmove,
        蔡甸区 = 武汉市 | 10 << RegionHelper.Districtlmove,
        江夏区 = 武汉市 | 11 << RegionHelper.Districtlmove,
        黄陂区 = 武汉市 | 12 << RegionHelper.Districtlmove,
        新洲区 = 武汉市 | 13 << RegionHelper.Districtlmove,
        #endregion 区县
        黄石市 = 湖北 | 2 << RegionHelper.Citylmove,
        #region 区县
        黄石港区 = 黄石市 | 1 << RegionHelper.Districtlmove,
        西塞山区 = 黄石市 | 2 << RegionHelper.Districtlmove,
        下陆区 = 黄石市 | 3 << RegionHelper.Districtlmove,
        铁山区 = 黄石市 | 4 << RegionHelper.Districtlmove,
        阳新县 = 黄石市 | 5 << RegionHelper.Districtlmove,
        大冶市 = 黄石市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        十堰市 = 湖北 | 3 << RegionHelper.Citylmove,
        #region 区县
        茅箭区 = 十堰市 | 1 << RegionHelper.Districtlmove,
        张湾区 = 十堰市 | 2 << RegionHelper.Districtlmove,
        郧县 = 十堰市 | 3 << RegionHelper.Districtlmove,
        郧西县 = 十堰市 | 4 << RegionHelper.Districtlmove,
        竹山县 = 十堰市 | 5 << RegionHelper.Districtlmove,
        竹溪县 = 十堰市 | 6 << RegionHelper.Districtlmove,
        房县 = 十堰市 | 7 << RegionHelper.Districtlmove,
        丹江口市 = 十堰市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        宜昌市 = 湖北 | 4 << RegionHelper.Citylmove,
        #region 区县
        西陵区 = 宜昌市 | 1 << RegionHelper.Districtlmove,
        伍家岗区 = 宜昌市 | 2 << RegionHelper.Districtlmove,
        点军区 = 宜昌市 | 3 << RegionHelper.Districtlmove,
        猇亭区 = 宜昌市 | 4 << RegionHelper.Districtlmove,
        夷陵区 = 宜昌市 | 5 << RegionHelper.Districtlmove,
        远安县 = 宜昌市 | 6 << RegionHelper.Districtlmove,
        兴山县 = 宜昌市 | 7 << RegionHelper.Districtlmove,
        秭归县 = 宜昌市 | 8 << RegionHelper.Districtlmove,
        长阳县 = 宜昌市 | 9 << RegionHelper.Districtlmove,
        五峰县 = 宜昌市 | 10 << RegionHelper.Districtlmove,
        宜都市 = 宜昌市 | 11 << RegionHelper.Districtlmove,
        当阳市 = 宜昌市 | 12 << RegionHelper.Districtlmove,
        枝江市 = 宜昌市 | 13 << RegionHelper.Districtlmove,
        #endregion 区县

        襄阳市 = 湖北 | 5 << RegionHelper.Citylmove,
        #region 区县
        襄城区 = 襄阳市 | 1 << RegionHelper.Districtlmove,
        樊城区 = 襄阳市 | 2 << RegionHelper.Districtlmove,
        襄州区 = 襄阳市 | 3 << RegionHelper.Districtlmove,
        南漳县 = 襄阳市 | 4 << RegionHelper.Districtlmove,
        谷城县 = 襄阳市 | 5 << RegionHelper.Districtlmove,
        保康县 = 襄阳市 | 6 << RegionHelper.Districtlmove,
        老河口市 = 襄阳市 | 7 << RegionHelper.Districtlmove,
        枣阳市 = 襄阳市 | 8 << RegionHelper.Districtlmove,
        宜城市 = 襄阳市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        鄂州市 = 湖北 | 6 << RegionHelper.Citylmove,
        #region 区县
        梁子湖区 = 鄂州市 | 1 << RegionHelper.Districtlmove,
        华容区 = 鄂州市 | 2 << RegionHelper.Districtlmove,
        鄂城区 = 鄂州市 | 3 << RegionHelper.Districtlmove,
        #endregion 区县

        荆门市 = 湖北 | 7 << RegionHelper.Citylmove,
        #region 区县
        东宝区 = 荆门市 | 1 << RegionHelper.Districtlmove,
        掇刀区 = 荆门市 | 2 << RegionHelper.Districtlmove,
        京山县 = 荆门市 | 3 << RegionHelper.Districtlmove,
        沙洋县 = 荆门市 | 4 << RegionHelper.Districtlmove,
        钟祥市 = 荆门市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        孝感市 = 湖北 | 8 << RegionHelper.Citylmove,
        #region 区县
        孝南区 = 孝感市 | 1 << RegionHelper.Districtlmove,
        孝昌县 = 孝感市 | 2 << RegionHelper.Districtlmove,
        大悟县 = 孝感市 | 3 << RegionHelper.Districtlmove,
        云梦县 = 孝感市 | 4 << RegionHelper.Districtlmove,
        应城市 = 孝感市 | 5 << RegionHelper.Districtlmove,
        安陆市 = 孝感市 | 6 << RegionHelper.Districtlmove,
        汉川市 = 孝感市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        荆州市 = 湖北 | 9 << RegionHelper.Citylmove,
        #region 区县
        沙市区 = 荆州市 | 1 << RegionHelper.Districtlmove,
        荆州区 = 荆州市 | 2 << RegionHelper.Districtlmove,
        公安县 = 荆州市 | 3 << RegionHelper.Districtlmove,
        监利县 = 荆州市 | 4 << RegionHelper.Districtlmove,
        江陵县 = 荆州市 | 5 << RegionHelper.Districtlmove,
        石首市 = 荆州市 | 6 << RegionHelper.Districtlmove,
        洪湖市 = 荆州市 | 7 << RegionHelper.Districtlmove,
        松滋市 = 荆州市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        黄冈市 = 湖北 | 10 << RegionHelper.Citylmove,
        #region 区县
        黄州区 = 黄冈市 | 1 << RegionHelper.Districtlmove,
        团风县 = 黄冈市 | 2 << RegionHelper.Districtlmove,
        红安县 = 黄冈市 | 3 << RegionHelper.Districtlmove,
        罗田县 = 黄冈市 | 4 << RegionHelper.Districtlmove,
        英山县 = 黄冈市 | 5 << RegionHelper.Districtlmove,
        浠水县 = 黄冈市 | 6 << RegionHelper.Districtlmove,
        蕲春县 = 黄冈市 | 7 << RegionHelper.Districtlmove,
        黄梅县 = 黄冈市 | 8 << RegionHelper.Districtlmove,
        麻城市 = 黄冈市 | 9 << RegionHelper.Districtlmove,
        武穴市 = 黄冈市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县


        咸宁市 = 湖北 | 11 << RegionHelper.Citylmove,
        #region 区县
        咸安区 = 咸宁市 | 1 << RegionHelper.Districtlmove,
        嘉鱼县 = 咸宁市 | 2 << RegionHelper.Districtlmove,
        通城县 = 咸宁市 | 3 << RegionHelper.Districtlmove,
        崇阳县 = 咸宁市 | 4 << RegionHelper.Districtlmove,
        通山县 = 咸宁市 | 5 << RegionHelper.Districtlmove,
        赤壁市 = 咸宁市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县



        随州市 = 湖北 | 12 << RegionHelper.Citylmove,
        #region 区县
        曾都区 = 随州市 | 1 << RegionHelper.Districtlmove,
        随县 = 随州市 | 2 << RegionHelper.Districtlmove,
        广水市 = 随州市 | 3 << RegionHelper.Districtlmove,
        #endregion 区县


        恩施州 = 湖北 | 13 << RegionHelper.Citylmove,
        #region 区县
        恩施市 = 恩施州 | 1 << RegionHelper.Districtlmove,
        利川市 = 恩施州 | 2 << RegionHelper.Districtlmove,
        建始县 = 恩施州 | 3 << RegionHelper.Districtlmove,
        巴东县 = 恩施州 | 4 << RegionHelper.Districtlmove,
        宣恩县 = 恩施州 | 5 << RegionHelper.Districtlmove,
        咸丰县 = 恩施州 | 6 << RegionHelper.Districtlmove,
        来凤县 = 恩施州 | 7 << RegionHelper.Districtlmove,
        鹤峰县 = 恩施州 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        仙桃市 = 湖北 | 14 << RegionHelper.Citylmove,
        #region 区县
        沙嘴街道 = 仙桃市 | 1 << RegionHelper.Districtlmove,
        干河街道 = 仙桃市 | 2 << RegionHelper.Districtlmove,
        龙华山街道 = 仙桃市 | 3 << RegionHelper.Districtlmove,
        郑场镇 = 仙桃市 | 4 << RegionHelper.Districtlmove,
        毛嘴镇 = 仙桃市 | 5 << RegionHelper.Districtlmove,
        剅河镇 = 仙桃市 | 6 << RegionHelper.Districtlmove,
        三伏潭镇 = 仙桃市 | 7 << RegionHelper.Districtlmove,
        胡场镇 = 仙桃市 | 8 << RegionHelper.Districtlmove,
        长埫口镇 = 仙桃市 | 9 << RegionHelper.Districtlmove,
        西流河镇 = 仙桃市 | 10 << RegionHelper.Districtlmove,
        沙湖镇 = 仙桃市 | 11 << RegionHelper.Districtlmove,
        杨林尾镇 = 仙桃市 | 12 << RegionHelper.Districtlmove,
        彭场镇 = 仙桃市 | 13 << RegionHelper.Districtlmove,
        张沟镇 = 仙桃市 | 14 << RegionHelper.Districtlmove,
        郭河镇 = 仙桃市 | 15 << RegionHelper.Districtlmove,
        沔城回族镇 = 仙桃市 | 16 << RegionHelper.Districtlmove,
        通海口镇 = 仙桃市 | 17 << RegionHelper.Districtlmove,
        陈场镇 = 仙桃市 | 18 << RegionHelper.Districtlmove,
        高新园区 = 仙桃市 | 19 << RegionHelper.Districtlmove,
        沙湖原种场 = 仙桃市 | 20 << RegionHelper.Districtlmove,
        排湖风景区 = 仙桃市 | 21 << RegionHelper.Districtlmove,
        高新产业园 = 仙桃市 | 22 << RegionHelper.Districtlmove,
        仙桃工业园 = 仙桃市 | 23 << RegionHelper.Districtlmove,
        #endregion 区县


        潜江市 = 湖北 | 15 << RegionHelper.Citylmove,
        #region 区县
        园林街道 = 潜江市 | 1 << RegionHelper.Districtlmove,
        杨市街道 = 潜江市 | 2 << RegionHelper.Districtlmove,
        周矶街道 = 潜江市 | 3 << RegionHelper.Districtlmove,
        广华街道 = 潜江市 | 4 << RegionHelper.Districtlmove,
        竹根滩镇 = 潜江市 | 5 << RegionHelper.Districtlmove,
        渔洋镇 = 潜江市 | 6 << RegionHelper.Districtlmove,
        王场镇 = 潜江市 | 7 << RegionHelper.Districtlmove,
        高石碑镇 = 潜江市 | 8 << RegionHelper.Districtlmove,
        熊口镇 = 潜江市 | 9 << RegionHelper.Districtlmove,
        老新镇 = 潜江市 | 10 << RegionHelper.Districtlmove,
        浩口镇 = 潜江市 | 11 << RegionHelper.Districtlmove,
        积玉口镇 = 潜江市 | 12 << RegionHelper.Districtlmove,
        张金镇 = 潜江市 | 13 << RegionHelper.Districtlmove,
        龙湾镇 = 潜江市 | 14 << RegionHelper.Districtlmove,
        江汉石油管理局 = 潜江市 | 15 << RegionHelper.Districtlmove,
        潜江经济开发区 = 潜江市 | 16 << RegionHelper.Districtlmove,
        周矶管理区 = 潜江市 | 17 << RegionHelper.Districtlmove,
        后湖管理区 = 潜江市 | 18 << RegionHelper.Districtlmove,
        熊口管理区 = 潜江市 | 19 << RegionHelper.Districtlmove,
        总口管理区 = 潜江市 | 20 << RegionHelper.Districtlmove,
        西大垸管理区 = 潜江市 | 21 << RegionHelper.Districtlmove,
        运粮湖管理区 = 潜江市 | 22 << RegionHelper.Districtlmove,
        高场原种场 = 潜江市 | 23 << RegionHelper.Districtlmove,
        #endregion 区县

        天门市 = 湖北 | 16 << RegionHelper.Citylmove,
        #region 区县
        竟陵街道 = 天门市 | 1 << RegionHelper.Districtlmove,
        侨乡街道 = 天门市 | 2 << RegionHelper.Districtlmove,
        杨林街道 = 天门市 | 3 << RegionHelper.Districtlmove,
        多宝镇 = 天门市 | 4 << RegionHelper.Districtlmove,
        拖市镇 = 天门市 | 5 << RegionHelper.Districtlmove,
        张港镇 = 天门市 | 6 << RegionHelper.Districtlmove,
        蒋场镇 = 天门市 | 7 << RegionHelper.Districtlmove,
        汪场镇 = 天门市 | 8 << RegionHelper.Districtlmove,
        渔薪镇 = 天门市 | 9 << RegionHelper.Districtlmove,
        黄潭镇 = 天门市 | 10 << RegionHelper.Districtlmove,
        岳口镇 = 天门市 | 11 << RegionHelper.Districtlmove,
        横林镇 = 天门市 | 12 << RegionHelper.Districtlmove,
        彭市镇 = 天门市 | 13 << RegionHelper.Districtlmove,
        麻洋镇 = 天门市 | 14 << RegionHelper.Districtlmove,
        多祥镇 = 天门市 | 15 << RegionHelper.Districtlmove,
        干驿镇 = 天门市 | 16 << RegionHelper.Districtlmove,
        马湾镇 = 天门市 | 17 << RegionHelper.Districtlmove,
        卢市镇 = 天门市 | 18 << RegionHelper.Districtlmove,
        小板镇 = 天门市 | 19 << RegionHelper.Districtlmove,
        九真镇 = 天门市 | 20 << RegionHelper.Districtlmove,
        皂市镇 = 天门市 | 21 << RegionHelper.Districtlmove,
        胡市镇 = 天门市 | 22 << RegionHelper.Districtlmove,
        石河镇 = 天门市 | 23 << RegionHelper.Districtlmove,
        佛子山镇 = 天门市 | 24 << RegionHelper.Districtlmove,
        净潭乡 = 天门市 | 25 << RegionHelper.Districtlmove,
        蒋湖农场 = 天门市 | 26 << RegionHelper.Districtlmove,
        白茅湖农场 = 天门市 | 27 << RegionHelper.Districtlmove,
        #endregion 区县

        神农架 = 湖北 | 17 << RegionHelper.Citylmove,
        #region 区县
        松柏镇 = 神农架 | 1 << RegionHelper.Districtlmove,
        阳日镇 = 神农架 | 2 << RegionHelper.Districtlmove,
        木鱼镇 = 神农架 | 3 << RegionHelper.Districtlmove,
        红坪镇 = 神农架 | 4 << RegionHelper.Districtlmove,
        新华镇 = 神农架 | 5 << RegionHelper.Districtlmove,
        宋洛乡 = 神农架 | 6 << RegionHelper.Districtlmove,
        九湖乡 = 神农架 | 7 << RegionHelper.Districtlmove,
        下谷坪土家族乡 = 神农架 | 8 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        湖南 = 11 << RegionHelper.Provincelmove,//省                
        #region 市
        长沙市 = 湖南 | 1 << RegionHelper.Citylmove,
        #region 区县
        芙蓉区 = 长沙市 | 1 << RegionHelper.Districtlmove,
        天心区 = 长沙市 | 2 << RegionHelper.Districtlmove,
        岳麓区 = 长沙市 | 3 << RegionHelper.Districtlmove,
        开福区 = 长沙市 | 4 << RegionHelper.Districtlmove,
        雨花区 = 长沙市 | 5 << RegionHelper.Districtlmove,
        望城区 = 长沙市 | 6 << RegionHelper.Districtlmove,
        长沙县 = 长沙市 | 7 << RegionHelper.Districtlmove,
        宁乡县 = 长沙市 | 8 << RegionHelper.Districtlmove,
        浏阳市 = 长沙市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        株洲市 = 湖南 | 2 << RegionHelper.Citylmove,
        #region 区县
        荷塘区 = 株洲市 | 1 << RegionHelper.Districtlmove,
        芦淞区 = 株洲市 | 2 << RegionHelper.Districtlmove,
        石峰区 = 株洲市 | 3 << RegionHelper.Districtlmove,
        天元区 = 株洲市 | 4 << RegionHelper.Districtlmove,
        株洲县 = 株洲市 | 5 << RegionHelper.Districtlmove,
        攸县 = 株洲市 | 6 << RegionHelper.Districtlmove,
        茶陵县 = 株洲市 | 7 << RegionHelper.Districtlmove,
        炎陵县 = 株洲市 | 8 << RegionHelper.Districtlmove,
        醴陵市 = 株洲市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县


        湘潭市 = 湖南 | 3 << RegionHelper.Citylmove,
        #region 区县
        雨湖区 = 湘潭市 | 1 << RegionHelper.Districtlmove,
        岳塘区 = 湘潭市 | 2 << RegionHelper.Districtlmove,
        湘潭县 = 湘潭市 | 3 << RegionHelper.Districtlmove,
        湘乡市 = 湘潭市 | 4 << RegionHelper.Districtlmove,
        韶山市 = 湘潭市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县


        衡阳市 = 湖南 | 4 << RegionHelper.Citylmove,
        #region 区县
        珠晖区 = 衡阳市 | 1 << RegionHelper.Districtlmove,
        雁峰区 = 衡阳市 | 2 << RegionHelper.Districtlmove,
        石鼓区 = 衡阳市 | 3 << RegionHelper.Districtlmove,
        蒸湘区 = 衡阳市 | 4 << RegionHelper.Districtlmove,
        南岳区 = 衡阳市 | 5 << RegionHelper.Districtlmove,
        衡阳县 = 衡阳市 | 6 << RegionHelper.Districtlmove,
        衡南县 = 衡阳市 | 7 << RegionHelper.Districtlmove,
        衡山县 = 衡阳市 | 8 << RegionHelper.Districtlmove,
        衡东县 = 衡阳市 | 9 << RegionHelper.Districtlmove,
        祁东县 = 衡阳市 | 10 << RegionHelper.Districtlmove,
        耒阳市 = 衡阳市 | 11 << RegionHelper.Districtlmove,
        常宁市 = 衡阳市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        邵阳市 = 湖南 | 5 << RegionHelper.Citylmove,
        #region 区县
        双清区 = 邵阳市 | 1 << RegionHelper.Districtlmove,
        大祥区 = 邵阳市 | 2 << RegionHelper.Districtlmove,
        北塔区 = 邵阳市 | 3 << RegionHelper.Districtlmove,
        邵东县 = 邵阳市 | 4 << RegionHelper.Districtlmove,
        新邵县 = 邵阳市 | 5 << RegionHelper.Districtlmove,
        邵阳县 = 邵阳市 | 6 << RegionHelper.Districtlmove,
        隆回县 = 邵阳市 | 7 << RegionHelper.Districtlmove,
        洞口县 = 邵阳市 | 8 << RegionHelper.Districtlmove,
        绥宁县 = 邵阳市 | 9 << RegionHelper.Districtlmove,
        新宁县 = 邵阳市 | 10 << RegionHelper.Districtlmove,
        城步苗族自治县 = 邵阳市 | 11 << RegionHelper.Districtlmove,
        武冈市 = 邵阳市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县


        岳阳市 = 湖南 | 6 << RegionHelper.Citylmove,
        #region 区县
        岳阳楼区 = 岳阳市 | 1 << RegionHelper.Districtlmove,
        云溪区 = 岳阳市 | 2 << RegionHelper.Districtlmove,
        君山区 = 岳阳市 | 3 << RegionHelper.Districtlmove,
        岳阳县 = 岳阳市 | 4 << RegionHelper.Districtlmove,
        华容县 = 岳阳市 | 5 << RegionHelper.Districtlmove,
        湘阴县 = 岳阳市 | 6 << RegionHelper.Districtlmove,
        平江县 = 岳阳市 | 7 << RegionHelper.Districtlmove,
        汨罗市 = 岳阳市 | 8 << RegionHelper.Districtlmove,
        临湘市 = 岳阳市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        常德市 = 湖南 | 7 << RegionHelper.Citylmove,
        #region 区县
        武陵区 = 常德市 | 1 << RegionHelper.Districtlmove,
        鼎城区 = 常德市 | 2 << RegionHelper.Districtlmove,
        安乡县 = 常德市 | 3 << RegionHelper.Districtlmove,
        汉寿县 = 常德市 | 4 << RegionHelper.Districtlmove,
        澧县 = 常德市 | 5 << RegionHelper.Districtlmove,
        临澧县 = 常德市 | 6 << RegionHelper.Districtlmove,
        桃源县 = 常德市 | 7 << RegionHelper.Districtlmove,
        石门县 = 常德市 | 8 << RegionHelper.Districtlmove,
        津市市 = 常德市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        张家界市 = 湖南 | 8 << RegionHelper.Citylmove,
        #region 区县
        永定区 = 张家界市 | 1 << RegionHelper.Districtlmove,
        武陵源区 = 张家界市 | 2 << RegionHelper.Districtlmove,
        慈利县 = 张家界市 | 3 << RegionHelper.Districtlmove,
        桑植县 = 张家界市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        益阳市 = 湖南 | 9 << RegionHelper.Citylmove,
        #region 区县
        资阳区 = 益阳市 | 1 << RegionHelper.Districtlmove,
        赫山区 = 益阳市 | 2 << RegionHelper.Districtlmove,
        南县 = 益阳市 | 3 << RegionHelper.Districtlmove,
        桃江县 = 益阳市 | 4 << RegionHelper.Districtlmove,
        安化县 = 益阳市 | 5 << RegionHelper.Districtlmove,
        沅江市 = 益阳市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县
        郴州市 = 湖南 | 10 << RegionHelper.Citylmove,
        #region 区县
        北湖区 = 郴州市 | 1 << RegionHelper.Districtlmove,
        苏仙区 = 郴州市 | 2 << RegionHelper.Districtlmove,
        桂阳县 = 郴州市 | 3 << RegionHelper.Districtlmove,
        宜章县 = 郴州市 | 4 << RegionHelper.Districtlmove,
        永兴县 = 郴州市 | 5 << RegionHelper.Districtlmove,
        嘉禾县 = 郴州市 | 6 << RegionHelper.Districtlmove,
        临武县 = 郴州市 | 7 << RegionHelper.Districtlmove,
        汝城县 = 郴州市 | 8 << RegionHelper.Districtlmove,
        桂东县 = 郴州市 | 9 << RegionHelper.Districtlmove,
        安仁县 = 郴州市 | 10 << RegionHelper.Districtlmove,
        资兴市 = 郴州市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        永州市 = 湖南 | 11 << RegionHelper.Citylmove,
        #region 区县
        零陵区 = 永州市 | 1 << RegionHelper.Districtlmove,
        冷水滩区 = 永州市 | 2 << RegionHelper.Districtlmove,
        祁阳县 = 永州市 | 3 << RegionHelper.Districtlmove,
        东安县 = 永州市 | 4 << RegionHelper.Districtlmove,
        双牌县 = 永州市 | 5 << RegionHelper.Districtlmove,
        道县 = 永州市 | 6 << RegionHelper.Districtlmove,
        江永县 = 永州市 | 7 << RegionHelper.Districtlmove,
        宁远县 = 永州市 | 8 << RegionHelper.Districtlmove,
        蓝山县 = 永州市 | 9 << RegionHelper.Districtlmove,
        新田县 = 永州市 | 10 << RegionHelper.Districtlmove,
        江华县 = 永州市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        怀化市 = 湖南 | 12 << RegionHelper.Citylmove,
        #region 区县
        鹤城区 = 怀化市 | 1 << RegionHelper.Districtlmove,
        中方县 = 怀化市 | 2 << RegionHelper.Districtlmove,
        沅陵县 = 怀化市 | 3 << RegionHelper.Districtlmove,
        辰溪县 = 怀化市 | 4 << RegionHelper.Districtlmove,
        溆浦县 = 怀化市 | 5 << RegionHelper.Districtlmove,
        会同县 = 怀化市 | 6 << RegionHelper.Districtlmove,
        麻阳县 = 怀化市 | 7 << RegionHelper.Districtlmove,
        新晃县 = 怀化市 | 8 << RegionHelper.Districtlmove,
        芷江县 = 怀化市 | 9 << RegionHelper.Districtlmove,
        靖州县 = 怀化市 | 10 << RegionHelper.Districtlmove,
        通道县 = 怀化市 | 11 << RegionHelper.Districtlmove,
        洪江市 = 怀化市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        娄底市 = 湖南 | 13 << RegionHelper.Citylmove,
        #region 区县
        娄星区 = 娄底市 | 1 << RegionHelper.Districtlmove,
        双峰县 = 娄底市 | 2 << RegionHelper.Districtlmove,
        新化县 = 娄底市 | 3 << RegionHelper.Districtlmove,
        冷水江市 = 娄底市 | 4 << RegionHelper.Districtlmove,
        涟源市 = 娄底市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        湘西州 = 湖南 | 14 << RegionHelper.Citylmove,
        #region 区县
        吉首市 = 湘西州 | 1 << RegionHelper.Districtlmove,
        泸溪县 = 湘西州 | 2 << RegionHelper.Districtlmove,
        凤凰县 = 湘西州 | 3 << RegionHelper.Districtlmove,
        花垣县 = 湘西州 | 4 << RegionHelper.Districtlmove,
        保靖县 = 湘西州 | 5 << RegionHelper.Districtlmove,
        古丈县 = 湘西州 | 6 << RegionHelper.Districtlmove,
        永顺县 = 湘西州 | 7 << RegionHelper.Districtlmove,
        龙山县 = 湘西州 | 8 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        福建 = 12 << RegionHelper.Provincelmove,//省                
        #region 市
        福州市 = 福建 | 1 << RegionHelper.Citylmove,
        #region 区县
        福州鼓楼区 = 福州市 | 1 << RegionHelper.Districtlmove,
        台江区 = 福州市 | 2 << RegionHelper.Districtlmove,
        仓山区 = 福州市 | 3 << RegionHelper.Districtlmove,
        马尾区 = 福州市 | 4 << RegionHelper.Districtlmove,
        晋安区 = 福州市 | 5 << RegionHelper.Districtlmove,
        闽侯县 = 福州市 | 6 << RegionHelper.Districtlmove,
        连江县 = 福州市 | 7 << RegionHelper.Districtlmove,
        罗源县 = 福州市 | 8 << RegionHelper.Districtlmove,
        闽清县 = 福州市 | 9 << RegionHelper.Districtlmove,
        永泰县 = 福州市 | 10 << RegionHelper.Districtlmove,
        平潭县 = 福州市 | 11 << RegionHelper.Districtlmove,
        福清市 = 福州市 | 12 << RegionHelper.Districtlmove,
        长乐市 = 福州市 | 13 << RegionHelper.Districtlmove,
        #endregion 区县


        厦门市 = 福建 | 2 << RegionHelper.Citylmove,
        #region 区县
        思明区 = 厦门市 | 1 << RegionHelper.Districtlmove,
        海沧区 = 厦门市 | 2 << RegionHelper.Districtlmove,
        湖里区 = 厦门市 | 3 << RegionHelper.Districtlmove,
        集美区 = 厦门市 | 4 << RegionHelper.Districtlmove,
        同安区 = 厦门市 | 5 << RegionHelper.Districtlmove,
        翔安区 = 厦门市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        莆田市 = 福建 | 3 << RegionHelper.Citylmove,
        #region 区县
        城厢区 = 莆田市 | 1 << RegionHelper.Districtlmove,
        涵江区 = 莆田市 | 2 << RegionHelper.Districtlmove,
        荔城区 = 莆田市 | 3 << RegionHelper.Districtlmove,
        秀屿区 = 莆田市 | 4 << RegionHelper.Districtlmove,
        仙游县 = 莆田市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        三明市 = 福建 | 4 << RegionHelper.Citylmove,
        #region 区县
        梅列区 = 三明市 | 1 << RegionHelper.Districtlmove,
        三元区 = 三明市 | 2 << RegionHelper.Districtlmove,
        明溪县 = 三明市 | 3 << RegionHelper.Districtlmove,
        清流县 = 三明市 | 4 << RegionHelper.Districtlmove,
        宁化县 = 三明市 | 5 << RegionHelper.Districtlmove,
        大田县 = 三明市 | 6 << RegionHelper.Districtlmove,
        尤溪县 = 三明市 | 7 << RegionHelper.Districtlmove,
        沙县 = 三明市 | 8 << RegionHelper.Districtlmove,
        将乐县 = 三明市 | 9 << RegionHelper.Districtlmove,
        泰宁县 = 三明市 | 10 << RegionHelper.Districtlmove,
        建宁县 = 三明市 | 11 << RegionHelper.Districtlmove,
        永安市 = 三明市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        泉州市 = 福建 | 5 << RegionHelper.Citylmove,
        #region 区县
        鲤城区 = 泉州市 | 1 << RegionHelper.Districtlmove,
        丰泽区 = 泉州市 | 2 << RegionHelper.Districtlmove,
        洛江区 = 泉州市 | 3 << RegionHelper.Districtlmove,
        泉港区 = 泉州市 | 4 << RegionHelper.Districtlmove,
        惠安县 = 泉州市 | 5 << RegionHelper.Districtlmove,
        安溪县 = 泉州市 | 6 << RegionHelper.Districtlmove,
        永春县 = 泉州市 | 7 << RegionHelper.Districtlmove,
        德化县 = 泉州市 | 8 << RegionHelper.Districtlmove,
        金门县 = 泉州市 | 9 << RegionHelper.Districtlmove,
        石狮市 = 泉州市 | 10 << RegionHelper.Districtlmove,
        晋江市 = 泉州市 | 11 << RegionHelper.Districtlmove,
        南安市 = 泉州市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县
        漳州市 = 福建 | 6 << RegionHelper.Citylmove,
        #region 区县
        芗城区 = 漳州市 | 1 << RegionHelper.Districtlmove,
        龙文区 = 漳州市 | 2 << RegionHelper.Districtlmove,
        云霄县 = 漳州市 | 3 << RegionHelper.Districtlmove,
        漳浦县 = 漳州市 | 4 << RegionHelper.Districtlmove,
        诏安县 = 漳州市 | 5 << RegionHelper.Districtlmove,
        长泰县 = 漳州市 | 6 << RegionHelper.Districtlmove,
        东山县 = 漳州市 | 7 << RegionHelper.Districtlmove,
        南靖县 = 漳州市 | 8 << RegionHelper.Districtlmove,
        平和县 = 漳州市 | 9 << RegionHelper.Districtlmove,
        华安县 = 漳州市 | 10 << RegionHelper.Districtlmove,
        龙海市 = 漳州市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        南平市 = 福建 | 7 << RegionHelper.Citylmove,
        #region 区县
        延平区 = 南平市 | 1 << RegionHelper.Districtlmove,
        顺昌县 = 南平市 | 2 << RegionHelper.Districtlmove,
        浦城县 = 南平市 | 3 << RegionHelper.Districtlmove,
        光泽县 = 南平市 | 4 << RegionHelper.Districtlmove,
        松溪县 = 南平市 | 5 << RegionHelper.Districtlmove,
        政和县 = 南平市 | 6 << RegionHelper.Districtlmove,
        邵武市 = 南平市 | 7 << RegionHelper.Districtlmove,
        武夷山市 = 南平市 | 8 << RegionHelper.Districtlmove,
        建瓯市 = 南平市 | 9 << RegionHelper.Districtlmove,
        建阳市 = 南平市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县


        龙岩市 = 福建 | 8 << RegionHelper.Citylmove,
        #region 区县
        新罗区 = 龙岩市 | 1 << RegionHelper.Districtlmove,
        长汀县 = 龙岩市 | 2 << RegionHelper.Districtlmove,
        永定县 = 龙岩市 | 3 << RegionHelper.Districtlmove,
        上杭县 = 龙岩市 | 4 << RegionHelper.Districtlmove,
        武平县 = 龙岩市 | 5 << RegionHelper.Districtlmove,
        连城县 = 龙岩市 | 6 << RegionHelper.Districtlmove,
        漳平市 = 龙岩市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县
        宁德市 = 福建 | 9 << RegionHelper.Citylmove,
        #region 区县
        蕉城区 = 宁德市 | 1 << RegionHelper.Districtlmove,
        霞浦县 = 宁德市 | 2 << RegionHelper.Districtlmove,
        古田县 = 宁德市 | 3 << RegionHelper.Districtlmove,
        屏南县 = 宁德市 | 4 << RegionHelper.Districtlmove,
        寿宁县 = 宁德市 | 5 << RegionHelper.Districtlmove,
        周宁县 = 宁德市 | 6 << RegionHelper.Districtlmove,
        柘荣县 = 宁德市 | 7 << RegionHelper.Districtlmove,
        福安市 = 宁德市 | 8 << RegionHelper.Districtlmove,
        福鼎市 = 宁德市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        安徽 = 13 << RegionHelper.Provincelmove,//省                
        #region 市
        合肥市 = 安徽 | 1 << RegionHelper.Citylmove,
        #region 区县
        瑶海区 = 合肥市 | 1 << RegionHelper.Districtlmove,
        庐阳区 = 合肥市 | 2 << RegionHelper.Districtlmove,
        蜀山区 = 合肥市 | 3 << RegionHelper.Districtlmove,
        包河区 = 合肥市 | 4 << RegionHelper.Districtlmove,
        长丰县 = 合肥市 | 5 << RegionHelper.Districtlmove,
        肥东县 = 合肥市 | 6 << RegionHelper.Districtlmove,
        肥西县 = 合肥市 | 7 << RegionHelper.Districtlmove,
        巢湖市 = 合肥市 | 8 << RegionHelper.Districtlmove,
        庐江县 = 合肥市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县
        芜湖市 = 安徽 | 2 << RegionHelper.Citylmove,
        #region 区县
        镜湖区 = 芜湖市 | 1 << RegionHelper.Districtlmove,
        弋江区 = 芜湖市 | 2 << RegionHelper.Districtlmove,
        鸠江区 = 芜湖市 | 3 << RegionHelper.Districtlmove,
        三山区 = 芜湖市 | 4 << RegionHelper.Districtlmove,
        芜湖县 = 芜湖市 | 5 << RegionHelper.Districtlmove,
        繁昌县 = 芜湖市 | 6 << RegionHelper.Districtlmove,
        南陵县 = 芜湖市 | 7 << RegionHelper.Districtlmove,
        无为县 = 芜湖市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县
        蚌埠市 = 安徽 | 3 << RegionHelper.Citylmove,
        #region 区县
        龙子湖区 = 蚌埠市 | 1 << RegionHelper.Districtlmove,
        蚌山区 = 蚌埠市 | 2 << RegionHelper.Districtlmove,
        禹会区 = 蚌埠市 | 3 << RegionHelper.Districtlmove,
        淮上区 = 蚌埠市 | 4 << RegionHelper.Districtlmove,
        怀远县 = 蚌埠市 | 5 << RegionHelper.Districtlmove,
        五河县 = 蚌埠市 | 6 << RegionHelper.Districtlmove,
        固镇县 = 蚌埠市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县
        淮南市 = 安徽 | 4 << RegionHelper.Citylmove,
        #region 区县
        大通区 = 淮南市 | 1 << RegionHelper.Districtlmove,
        田家庵区 = 淮南市 | 2 << RegionHelper.Districtlmove,
        谢家集区 = 淮南市 | 3 << RegionHelper.Districtlmove,
        八公山区 = 淮南市 | 4 << RegionHelper.Districtlmove,
        潘集区 = 淮南市 | 5 << RegionHelper.Districtlmove,
        凤台县 = 淮南市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        马鞍山市 = 安徽 | 5 << RegionHelper.Citylmove,
        #region 区县
        博望区 = 马鞍山市 | 1 << RegionHelper.Districtlmove,
        花山区 = 马鞍山市 | 2 << RegionHelper.Districtlmove,
        雨山区 = 马鞍山市 | 3 << RegionHelper.Districtlmove,
        当涂县 = 马鞍山市 | 4 << RegionHelper.Districtlmove,
        含山县 = 马鞍山市 | 5 << RegionHelper.Districtlmove,
        和县 = 马鞍山市 | 6 << RegionHelper.Districtlmove,
        金家庄区 = 马鞍山市 | 7 << RegionHelper.Districtlmove,

        #endregion 区县
        淮北市 = 安徽 | 6 << RegionHelper.Citylmove,
        #region 区县
        杜集区 = 淮北市 | 1 << RegionHelper.Districtlmove,
        相山区 = 淮北市 | 2 << RegionHelper.Districtlmove,
        烈山区 = 淮北市 | 3 << RegionHelper.Districtlmove,
        濉溪县 = 淮北市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        铜陵市 = 安徽 | 7 << RegionHelper.Citylmove,
        #region 区县
        铜官山区 = 铜陵市 | 1 << RegionHelper.Districtlmove,
        狮子山区 = 铜陵市 | 2 << RegionHelper.Districtlmove,
        郊区 = 铜陵市 | 3 << RegionHelper.Districtlmove,
        铜陵县 = 铜陵市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        安庆市 = 安徽 | 8 << RegionHelper.Citylmove,
        #region 区县
        迎江区 = 安庆市 | 1 << RegionHelper.Districtlmove,
        大观区 = 安庆市 | 2 << RegionHelper.Districtlmove,
        宜秀区 = 安庆市 | 3 << RegionHelper.Districtlmove,
        怀宁县 = 安庆市 | 4 << RegionHelper.Districtlmove,
        枞阳县 = 安庆市 | 5 << RegionHelper.Districtlmove,
        潜山县 = 安庆市 | 6 << RegionHelper.Districtlmove,
        太湖县 = 安庆市 | 7 << RegionHelper.Districtlmove,
        宿松县 = 安庆市 | 8 << RegionHelper.Districtlmove,
        望江县 = 安庆市 | 9 << RegionHelper.Districtlmove,
        岳西县 = 安庆市 | 10 << RegionHelper.Districtlmove,
        桐城市 = 安庆市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县
        黄山市 = 安徽 | 9 << RegionHelper.Citylmove,
        #region 区县
        屯溪区 = 黄山市 | 1 << RegionHelper.Districtlmove,
        黄山区 = 黄山市 | 2 << RegionHelper.Districtlmove,
        徽州区 = 黄山市 | 3 << RegionHelper.Districtlmove,
        歙县 = 黄山市 | 4 << RegionHelper.Districtlmove,
        休宁县 = 黄山市 | 5 << RegionHelper.Districtlmove,
        黟县 = 黄山市 | 6 << RegionHelper.Districtlmove,
        祁门县 = 黄山市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        滁州市 = 安徽 | 10 << RegionHelper.Citylmove,
        #region 区县
        琅琊区 = 滁州市 | 1 << RegionHelper.Districtlmove,
        南谯区 = 滁州市 | 2 << RegionHelper.Districtlmove,
        来安县 = 滁州市 | 3 << RegionHelper.Districtlmove,
        全椒县 = 滁州市 | 4 << RegionHelper.Districtlmove,
        定远县 = 滁州市 | 5 << RegionHelper.Districtlmove,
        凤阳县 = 滁州市 | 6 << RegionHelper.Districtlmove,
        天长市 = 滁州市 | 7 << RegionHelper.Districtlmove,
        明光市 = 滁州市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        阜阳市 = 安徽 | 11 << RegionHelper.Citylmove,
        #region 区县
        颍州区 = 阜阳市 | 1 << RegionHelper.Districtlmove,
        颍东区 = 阜阳市 | 2 << RegionHelper.Districtlmove,
        颍泉区 = 阜阳市 | 3 << RegionHelper.Districtlmove,
        临泉县 = 阜阳市 | 4 << RegionHelper.Districtlmove,
        太和县 = 阜阳市 | 5 << RegionHelper.Districtlmove,
        阜南县 = 阜阳市 | 6 << RegionHelper.Districtlmove,
        颍上县 = 阜阳市 | 7 << RegionHelper.Districtlmove,
        界首市 = 阜阳市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        宿州市 = 安徽 | 12 << RegionHelper.Citylmove,
        #region 区县
        埇桥区 = 宿州市 | 1 << RegionHelper.Districtlmove,
        砀山县 = 宿州市 | 2 << RegionHelper.Districtlmove,
        萧县 = 宿州市 | 3 << RegionHelper.Districtlmove,
        灵璧县 = 宿州市 | 4 << RegionHelper.Districtlmove,
        泗县 = 宿州市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        六安市 = 安徽 | 13 << RegionHelper.Citylmove,
        #region 区县
        金安区 = 六安市 | 1 << RegionHelper.Districtlmove,
        裕安区 = 六安市 | 2 << RegionHelper.Districtlmove,
        寿县 = 六安市 | 3 << RegionHelper.Districtlmove,
        霍邱县 = 六安市 | 4 << RegionHelper.Districtlmove,
        舒城县 = 六安市 | 5 << RegionHelper.Districtlmove,
        金寨县 = 六安市 | 6 << RegionHelper.Districtlmove,
        霍山县 = 六安市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        亳州市 = 安徽 | 14 << RegionHelper.Citylmove,
        #region 区县
        谯城区 = 亳州市 | 1 << RegionHelper.Districtlmove,
        涡阳县 = 亳州市 | 2 << RegionHelper.Districtlmove,
        蒙城县 = 亳州市 | 3 << RegionHelper.Districtlmove,
        利辛县 = 亳州市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        池州市 = 安徽 | 15 << RegionHelper.Citylmove,
        #region 区县
        贵池区 = 池州市 | 1 << RegionHelper.Districtlmove,
        东至县 = 池州市 | 2 << RegionHelper.Districtlmove,
        石台县 = 池州市 | 3 << RegionHelper.Districtlmove,
        青阳县 = 池州市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        宣城市 = 安徽 | 16 << RegionHelper.Citylmove,
        #region 区县
        宣州区 = 宣城市 | 1 << RegionHelper.Districtlmove,
        郎溪县 = 宣城市 | 2 << RegionHelper.Districtlmove,
        广德县 = 宣城市 | 3 << RegionHelper.Districtlmove,
        泾县 = 宣城市 | 4 << RegionHelper.Districtlmove,
        绩溪县 = 宣城市 | 5 << RegionHelper.Districtlmove,
        旌德县 = 宣城市 | 6 << RegionHelper.Districtlmove,
        宁国市 = 宣城市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        黑龙江 = 14 << RegionHelper.Provincelmove,//省                
        #region 市
        哈尔滨市 = 黑龙江 | 1 << RegionHelper.Citylmove,
        #region 区县
        道里区 = 哈尔滨市 | 1 << RegionHelper.Districtlmove,
        南岗区 = 哈尔滨市 | 2 << RegionHelper.Districtlmove,
        道外区 = 哈尔滨市 | 3 << RegionHelper.Districtlmove,
        平房区 = 哈尔滨市 | 4 << RegionHelper.Districtlmove,
        松北区 = 哈尔滨市 | 5 << RegionHelper.Districtlmove,
        香坊区 = 哈尔滨市 | 6 << RegionHelper.Districtlmove,
        呼兰区 = 哈尔滨市 | 7 << RegionHelper.Districtlmove,
        阿城区 = 哈尔滨市 | 8 << RegionHelper.Districtlmove,
        依兰县 = 哈尔滨市 | 9 << RegionHelper.Districtlmove,
        方正县 = 哈尔滨市 | 10 << RegionHelper.Districtlmove,
        宾县 = 哈尔滨市 | 11 << RegionHelper.Districtlmove,
        巴彦县 = 哈尔滨市 | 12 << RegionHelper.Districtlmove,
        木兰县 = 哈尔滨市 | 13 << RegionHelper.Districtlmove,
        通河县 = 哈尔滨市 | 14 << RegionHelper.Districtlmove,
        延寿县 = 哈尔滨市 | 15 << RegionHelper.Districtlmove,
        双城市 = 哈尔滨市 | 16 << RegionHelper.Districtlmove,
        尚志市 = 哈尔滨市 | 17 << RegionHelper.Districtlmove,
        五常市 = 哈尔滨市 | 18 << RegionHelper.Districtlmove,
        #endregion 区县

        齐齐哈尔市 = 黑龙江 | 2 << RegionHelper.Citylmove,
        #region 区县
        龙沙区 = 齐齐哈尔市 | 1 << RegionHelper.Districtlmove,
        建华区 = 齐齐哈尔市 | 2 << RegionHelper.Districtlmove,
        铁锋区 = 齐齐哈尔市 | 3 << RegionHelper.Districtlmove,
        昂昂溪区 = 齐齐哈尔市 | 4 << RegionHelper.Districtlmove,
        富拉尔基区 = 齐齐哈尔市 | 5 << RegionHelper.Districtlmove,
        碾子山区 = 齐齐哈尔市 | 6 << RegionHelper.Districtlmove,
        梅里斯达斡尔族区 = 齐齐哈尔市 | 7 << RegionHelper.Districtlmove,
        龙江县 = 齐齐哈尔市 | 8 << RegionHelper.Districtlmove,
        依安县 = 齐齐哈尔市 | 9 << RegionHelper.Districtlmove,
        泰来县 = 齐齐哈尔市 | 10 << RegionHelper.Districtlmove,
        甘南县 = 齐齐哈尔市 | 11 << RegionHelper.Districtlmove,
        富裕县 = 齐齐哈尔市 | 12 << RegionHelper.Districtlmove,
        克山县 = 齐齐哈尔市 | 13 << RegionHelper.Districtlmove,
        克东县 = 齐齐哈尔市 | 14 << RegionHelper.Districtlmove,
        拜泉县 = 齐齐哈尔市 | 15 << RegionHelper.Districtlmove,
        讷河市 = 齐齐哈尔市 | 16 << RegionHelper.Districtlmove,
        #endregion 区县
        鸡西市 = 黑龙江 | 3 << RegionHelper.Citylmove,
        #region 区县
        鸡冠区 = 鸡西市 | 1 << RegionHelper.Districtlmove,
        恒山市 = 鸡西市 | 2 << RegionHelper.Districtlmove,
        滴道区 = 鸡西市 | 3 << RegionHelper.Districtlmove,
        梨树区 = 鸡西市 | 4 << RegionHelper.Districtlmove,
        城子河区 = 鸡西市 | 5 << RegionHelper.Districtlmove,
        麻山区 = 鸡西市 | 6 << RegionHelper.Districtlmove,
        鸡东县 = 鸡西市 | 7 << RegionHelper.Districtlmove,
        虎林市 = 鸡西市 | 8 << RegionHelper.Districtlmove,
        密山市 = 鸡西市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        鹤岗市 = 黑龙江 | 4 << RegionHelper.Citylmove,
        #region 区县
        鹤岗向阳区 = 鹤岗市 | 1 << RegionHelper.Districtlmove,
        工农区 = 鹤岗市 | 2 << RegionHelper.Districtlmove,
        鹤岗南山区 = 鹤岗市 | 3 << RegionHelper.Districtlmove,
        兴安区 = 鹤岗市 | 4 << RegionHelper.Districtlmove,
        东山区 = 鹤岗市 | 5 << RegionHelper.Districtlmove,
        兴山区 = 鹤岗市 | 6 << RegionHelper.Districtlmove,
        萝北县 = 鹤岗市 | 7 << RegionHelper.Districtlmove,
        绥滨县 = 鹤岗市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        双鸭山市 = 黑龙江 | 5 << RegionHelper.Citylmove,
        #region 区县
        尖山区 = 双鸭山市 | 1 << RegionHelper.Districtlmove,
        岭东区 = 双鸭山市 | 2 << RegionHelper.Districtlmove,
        四方台区 = 双鸭山市 | 3 << RegionHelper.Districtlmove,
        双鸭山宝山区 = 双鸭山市 | 4 << RegionHelper.Districtlmove,
        集贤县 = 双鸭山市 | 5 << RegionHelper.Districtlmove,
        友谊县 = 双鸭山市 | 6 << RegionHelper.Districtlmove,
        宝清县 = 双鸭山市 | 7 << RegionHelper.Districtlmove,
        饶河县 = 双鸭山市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        大庆市 = 黑龙江 | 6 << RegionHelper.Citylmove,
        #region 区县
        萨尔图区 = 大庆市 | 1 << RegionHelper.Districtlmove,
        龙凤区 = 大庆市 | 2 << RegionHelper.Districtlmove,
        让胡路区 = 大庆市 | 3 << RegionHelper.Districtlmove,
        红岗区 = 大庆市 | 4 << RegionHelper.Districtlmove,
        大同区 = 大庆市 | 5 << RegionHelper.Districtlmove,
        肇州县 = 大庆市 | 6 << RegionHelper.Districtlmove,
        肇源县 = 大庆市 | 7 << RegionHelper.Districtlmove,
        林甸县 = 大庆市 | 8 << RegionHelper.Districtlmove,
        杜尔伯特蒙县 = 大庆市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        伊春市 = 黑龙江 | 7 << RegionHelper.Citylmove,
        #region 区县
        伊春区 = 伊春市 | 1 << RegionHelper.Districtlmove,
        南岔区 = 伊春市 | 2 << RegionHelper.Districtlmove,
        友好区 = 伊春市 | 3 << RegionHelper.Districtlmove,
        西林区 = 伊春市 | 4 << RegionHelper.Districtlmove,
        翠峦区 = 伊春市 | 5 << RegionHelper.Districtlmove,
        新青区 = 伊春市 | 6 << RegionHelper.Districtlmove,
        美溪区 = 伊春市 | 7 << RegionHelper.Districtlmove,
        金山屯 = 伊春市 | 8 << RegionHelper.Districtlmove,
        五营区 = 伊春市 | 9 << RegionHelper.Districtlmove,
        乌马河 = 伊春市 | 10 << RegionHelper.Districtlmove,
        汤旺河 = 伊春市 | 11 << RegionHelper.Districtlmove,
        带岭区 = 伊春市 | 12 << RegionHelper.Districtlmove,
        乌伊岭 = 伊春市 | 13 << RegionHelper.Districtlmove,
        红星区 = 伊春市 | 14 << RegionHelper.Districtlmove,
        上甘岭 = 伊春市 | 15 << RegionHelper.Districtlmove,
        嘉荫县 = 伊春市 | 16 << RegionHelper.Districtlmove,
        铁力市 = 伊春市 | 17 << RegionHelper.Districtlmove,
        #endregion 区县

        佳木斯市 = 黑龙江 | 8 << RegionHelper.Citylmove,
        #region 区县
        向阳区 = 佳木斯市 | 1 << RegionHelper.Districtlmove,
        前进区 = 佳木斯市 | 2 << RegionHelper.Districtlmove,
        东风区 = 佳木斯市 | 3 << RegionHelper.Districtlmove,
        佳木斯郊区 = 佳木斯市 | 4 << RegionHelper.Districtlmove,
        桦南县 = 佳木斯市 | 5 << RegionHelper.Districtlmove,
        桦川县 = 佳木斯市 | 6 << RegionHelper.Districtlmove,
        汤原县 = 佳木斯市 | 7 << RegionHelper.Districtlmove,
        抚远县 = 佳木斯市 | 8 << RegionHelper.Districtlmove,
        同江市 = 佳木斯市 | 9 << RegionHelper.Districtlmove,
        富锦市 = 佳木斯市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        七台河市 = 黑龙江 | 9 << RegionHelper.Citylmove,
        #region 区县
        新兴区 = 七台河市 | 1 << RegionHelper.Districtlmove,
        桃山市 = 七台河市 | 2 << RegionHelper.Districtlmove,
        茄子河区 = 七台河市 | 3 << RegionHelper.Districtlmove,
        勃利县 = 七台河市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        牡丹江市 = 黑龙江 | 10 << RegionHelper.Citylmove,
        #region 区县
        东安区 = 牡丹江市 | 1 << RegionHelper.Districtlmove,
        阳明区 = 牡丹江市 | 2 << RegionHelper.Districtlmove,
        爱民区 = 牡丹江市 | 3 << RegionHelper.Districtlmove,
        西安区 = 牡丹江市 | 4 << RegionHelper.Districtlmove,
        东宁县 = 牡丹江市 | 5 << RegionHelper.Districtlmove,
        林口县 = 牡丹江市 | 6 << RegionHelper.Districtlmove,
        绥芬河市 = 牡丹江市 | 7 << RegionHelper.Districtlmove,
        海林市 = 牡丹江市 | 8 << RegionHelper.Districtlmove,
        宁安市 = 牡丹江市 | 9 << RegionHelper.Districtlmove,
        穆棱市 = 牡丹江市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        黑河市 = 黑龙江 | 11 << RegionHelper.Citylmove,
        #region 区县
        爱辉区 = 黑河市 | 1 << RegionHelper.Districtlmove,
        嫩江县 = 黑河市 | 2 << RegionHelper.Districtlmove,
        逊克县 = 黑河市 | 3 << RegionHelper.Districtlmove,
        孙吴县 = 黑河市 | 4 << RegionHelper.Districtlmove,
        北安市 = 黑河市 | 5 << RegionHelper.Districtlmove,
        五大连池市 = 黑河市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        绥化市 = 黑龙江 | 12 << RegionHelper.Citylmove,
        #region 区县
        北林区 = 绥化市 | 1 << RegionHelper.Districtlmove,
        望奎县 = 绥化市 | 2 << RegionHelper.Districtlmove,
        兰西县 = 绥化市 | 3 << RegionHelper.Districtlmove,
        青冈县 = 绥化市 | 4 << RegionHelper.Districtlmove,
        庆安县 = 绥化市 | 5 << RegionHelper.Districtlmove,
        明水县 = 绥化市 | 6 << RegionHelper.Districtlmove,
        绥棱县 = 绥化市 | 7 << RegionHelper.Districtlmove,
        安达市 = 绥化市 | 8 << RegionHelper.Districtlmove,
        肇东市 = 绥化市 | 9 << RegionHelper.Districtlmove,
        海伦市 = 绥化市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县


        大兴安岭 = 黑龙江 | 13 << RegionHelper.Citylmove,
        #region 区县
        加格达奇区 = 大兴安岭 | 1 << RegionHelper.Districtlmove,
        松岭区 = 大兴安岭 | 2 << RegionHelper.Districtlmove,
        新林区 = 大兴安岭 | 3 << RegionHelper.Districtlmove,
        呼中区 = 大兴安岭 | 4 << RegionHelper.Districtlmove,
        呼玛县 = 大兴安岭 | 5 << RegionHelper.Districtlmove,
        塔河县 = 大兴安岭 | 6 << RegionHelper.Districtlmove,
        漠河县 = 大兴安岭 | 7 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        内蒙古 = 15 << RegionHelper.Provincelmove,//省 自治区               
        #region 市
        呼和浩特市 = 内蒙古 | 1 << RegionHelper.Citylmove,
        #region 区县
        新城区 = 呼和浩特市 | 1 << RegionHelper.Districtlmove,
        回民区 = 呼和浩特市 | 2 << RegionHelper.Districtlmove,
        玉泉区 = 呼和浩特市 | 3 << RegionHelper.Districtlmove,
        赛罕区 = 呼和浩特市 | 4 << RegionHelper.Districtlmove,
        土默特左旗 = 呼和浩特市 | 5 << RegionHelper.Districtlmove,
        托克托县 = 呼和浩特市 | 6 << RegionHelper.Districtlmove,
        和林格尔县 = 呼和浩特市 | 7 << RegionHelper.Districtlmove,
        清水河县 = 呼和浩特市 | 8 << RegionHelper.Districtlmove,
        武川县 = 呼和浩特市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        包头市 = 内蒙古 | 2 << RegionHelper.Citylmove,
        #region 区县
        东河区 = 包头市 | 1 << RegionHelper.Districtlmove,
        昆都仑区 = 包头市 | 2 << RegionHelper.Districtlmove,
        包头青山区 = 包头市 | 3 << RegionHelper.Districtlmove,
        石拐区 = 包头市 | 4 << RegionHelper.Districtlmove,
        白云矿区 = 包头市 | 5 << RegionHelper.Districtlmove,
        九原区 = 包头市 | 6 << RegionHelper.Districtlmove,
        土默特右旗 = 包头市 | 7 << RegionHelper.Districtlmove,
        固阳县 = 包头市 | 8 << RegionHelper.Districtlmove,
        达尔罕旗 = 包头市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        乌海市 = 内蒙古 | 3 << RegionHelper.Citylmove,
        #region 区县
        海勃湾区 = 乌海市 | 1 << RegionHelper.Districtlmove,
        海南区 = 乌海市 | 2 << RegionHelper.Districtlmove,
        乌达区 = 乌海市 | 3 << RegionHelper.Districtlmove,
        #endregion 区县
        赤峰市 = 内蒙古 | 4 << RegionHelper.Citylmove,
        #region 区县
        红山区 = 赤峰市 | 1 << RegionHelper.Districtlmove,
        元宝山区 = 赤峰市 | 2 << RegionHelper.Districtlmove,
        松山区 = 赤峰市 | 3 << RegionHelper.Districtlmove,
        阿鲁科尔旗 = 赤峰市 | 4 << RegionHelper.Districtlmove,
        巴林左旗 = 赤峰市 | 5 << RegionHelper.Districtlmove,
        巴林右旗 = 赤峰市 | 6 << RegionHelper.Districtlmove,
        林西县 = 赤峰市 | 7 << RegionHelper.Districtlmove,
        克什克腾旗 = 赤峰市 | 8 << RegionHelper.Districtlmove,
        翁牛特旗 = 赤峰市 | 9 << RegionHelper.Districtlmove,
        喀喇沁旗 = 赤峰市 | 10 << RegionHelper.Districtlmove,
        宁城县 = 赤峰市 | 11 << RegionHelper.Districtlmove,
        敖汉旗 = 赤峰市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        通辽市 = 内蒙古 | 5 << RegionHelper.Citylmove,
        #region 区县
        科尔沁区 = 通辽市 | 1 << RegionHelper.Districtlmove,
        科尔沁左翼中旗 = 通辽市 | 2 << RegionHelper.Districtlmove,
        科尔沁左翼后旗 = 通辽市 | 3 << RegionHelper.Districtlmove,
        开鲁县 = 通辽市 | 4 << RegionHelper.Districtlmove,
        库伦旗 = 通辽市 | 5 << RegionHelper.Districtlmove,
        奈曼旗 = 通辽市 | 6 << RegionHelper.Districtlmove,
        扎鲁特旗 = 通辽市 | 7 << RegionHelper.Districtlmove,
        霍林郭勒市 = 通辽市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县
        鄂尔多斯市 = 内蒙古 | 6 << RegionHelper.Citylmove,
        #region 区县
        东胜区 = 鄂尔多斯市 | 1 << RegionHelper.Districtlmove,
        达拉特旗 = 鄂尔多斯市 | 2 << RegionHelper.Districtlmove,
        准格尔旗 = 鄂尔多斯市 | 3 << RegionHelper.Districtlmove,
        鄂托克前旗 = 鄂尔多斯市 | 4 << RegionHelper.Districtlmove,
        鄂托克旗 = 鄂尔多斯市 | 5 << RegionHelper.Districtlmove,
        杭锦旗 = 鄂尔多斯市 | 6 << RegionHelper.Districtlmove,
        乌审旗 = 鄂尔多斯市 | 7 << RegionHelper.Districtlmove,
        伊金霍洛旗 = 鄂尔多斯市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        呼伦贝尔市 = 内蒙古 | 7 << RegionHelper.Citylmove,
        #region 区县
        海拉尔区 = 呼伦贝尔市 | 1 << RegionHelper.Districtlmove,
        阿荣旗 = 呼伦贝尔市 | 2 << RegionHelper.Districtlmove,
        莫力达瓦旗 = 呼伦贝尔市 | 3 << RegionHelper.Districtlmove,
        鄂伦春自治旗 = 呼伦贝尔市 | 4 << RegionHelper.Districtlmove,
        鄂温克族自治旗 = 呼伦贝尔市 | 5 << RegionHelper.Districtlmove,
        陈巴尔虎旗 = 呼伦贝尔市 | 6 << RegionHelper.Districtlmove,
        新巴尔虎左旗 = 呼伦贝尔市 | 7 << RegionHelper.Districtlmove,
        新巴尔虎右旗 = 呼伦贝尔市 | 8 << RegionHelper.Districtlmove,
        满洲里 = 呼伦贝尔市 | 9 << RegionHelper.Districtlmove,
        牙克石市 = 呼伦贝尔市 | 10 << RegionHelper.Districtlmove,
        扎兰屯市 = 呼伦贝尔市 | 11 << RegionHelper.Districtlmove,
        额尔古纳市 = 呼伦贝尔市 | 12 << RegionHelper.Districtlmove,
        根河市 = 呼伦贝尔市 | 13 << RegionHelper.Districtlmove,
        #endregion 区县
        巴彦淖尔市 = 内蒙古 | 8 << RegionHelper.Citylmove,
        #region 区县
        临河区 = 巴彦淖尔市 | 1 << RegionHelper.Districtlmove,
        五原县 = 巴彦淖尔市 | 2 << RegionHelper.Districtlmove,
        磴口县 = 巴彦淖尔市 | 3 << RegionHelper.Districtlmove,
        乌拉特前旗 = 巴彦淖尔市 | 4 << RegionHelper.Districtlmove,
        乌拉特中旗 = 巴彦淖尔市 | 5 << RegionHelper.Districtlmove,
        乌拉特后旗 = 巴彦淖尔市 | 6 << RegionHelper.Districtlmove,
        杭锦后旗 = 巴彦淖尔市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        乌兰察布市 = 内蒙古 | 9 << RegionHelper.Citylmove,
        #region 区县
        集宁区 = 乌兰察布市 | 1 << RegionHelper.Districtlmove,
        卓资县 = 乌兰察布市 | 2 << RegionHelper.Districtlmove,
        化德县 = 乌兰察布市 | 3 << RegionHelper.Districtlmove,
        商都县 = 乌兰察布市 | 4 << RegionHelper.Districtlmove,
        兴和县 = 乌兰察布市 | 5 << RegionHelper.Districtlmove,
        凉城县 = 乌兰察布市 | 6 << RegionHelper.Districtlmove,
        察哈尔右翼前旗 = 乌兰察布市 | 7 << RegionHelper.Districtlmove,
        察哈尔右翼中旗 = 乌兰察布市 | 8 << RegionHelper.Districtlmove,
        察哈尔右翼后旗 = 乌兰察布市 | 9 << RegionHelper.Districtlmove,
        四子王旗 = 乌兰察布市 | 10 << RegionHelper.Districtlmove,
        丰镇市 = 乌兰察布市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        兴安盟 = 内蒙古 | 10 << RegionHelper.Citylmove,
        #region 区县
        乌兰浩特市 = 兴安盟 | 1 << RegionHelper.Districtlmove,
        阿尔山市 = 兴安盟 | 2 << RegionHelper.Districtlmove,
        科尔沁右翼前旗 = 兴安盟 | 3 << RegionHelper.Districtlmove,
        科尔沁右翼中旗 = 兴安盟 | 4 << RegionHelper.Districtlmove,
        扎赉特旗 = 兴安盟 | 5 << RegionHelper.Districtlmove,
        突泉县 = 兴安盟 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        锡林郭勒盟 = 内蒙古 | 11 << RegionHelper.Citylmove,
        #region 区县
        二连浩特市 = 锡林郭勒盟 | 1 << RegionHelper.Districtlmove,
        锡林浩特市 = 锡林郭勒盟 | 2 << RegionHelper.Districtlmove,
        阿巴嘎旗 = 锡林郭勒盟 | 3 << RegionHelper.Districtlmove,
        苏尼特左旗 = 锡林郭勒盟 | 4 << RegionHelper.Districtlmove,
        苏尼特右旗 = 锡林郭勒盟 | 5 << RegionHelper.Districtlmove,
        东乌珠穆沁旗 = 锡林郭勒盟 | 6 << RegionHelper.Districtlmove,
        西乌珠穆沁 = 锡林郭勒盟 | 7 << RegionHelper.Districtlmove,
        太仆寺旗 = 锡林郭勒盟 | 8 << RegionHelper.Districtlmove,
        镶黄旗 = 锡林郭勒盟 | 9 << RegionHelper.Districtlmove,
        正镶白旗 = 锡林郭勒盟 | 10 << RegionHelper.Districtlmove,
        正蓝旗 = 锡林郭勒盟 | 11 << RegionHelper.Districtlmove,
        多伦县 = 锡林郭勒盟 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        阿拉善盟 = 内蒙古 | 12 << RegionHelper.Citylmove,
        #region 区县
        阿拉善左旗 = 阿拉善盟 | 1 << RegionHelper.Districtlmove,
        阿拉善右旗 = 阿拉善盟 | 2 << RegionHelper.Districtlmove,
        额济纳旗 = 阿拉善盟 | 3 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        广西 = 16 << RegionHelper.Provincelmove,//省                
        #region 市
        南宁市 = 广西 | 1 << RegionHelper.Citylmove,
        #region 区县
        兴宁区 = 南宁市 | 1 << RegionHelper.Districtlmove,
        青秀区 = 南宁市 | 2 << RegionHelper.Districtlmove,
        江南区 = 南宁市 | 3 << RegionHelper.Districtlmove,
        西乡塘区 = 南宁市 | 4 << RegionHelper.Districtlmove,
        良庆区 = 南宁市 | 5 << RegionHelper.Districtlmove,
        邕宁区 = 南宁市 | 6 << RegionHelper.Districtlmove,
        武鸣县 = 南宁市 | 7 << RegionHelper.Districtlmove,
        隆安县 = 南宁市 | 8 << RegionHelper.Districtlmove,
        马山县 = 南宁市 | 9 << RegionHelper.Districtlmove,
        上林县 = 南宁市 | 10 << RegionHelper.Districtlmove,
        宾阳县 = 南宁市 | 11 << RegionHelper.Districtlmove,
        横县 = 南宁市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        柳州市 = 广西 | 2 << RegionHelper.Citylmove,
        #region 区县
        城中区 = 柳州市 | 1 << RegionHelper.Districtlmove,
        鱼峰区 = 柳州市 | 2 << RegionHelper.Districtlmove,
        柳南区 = 柳州市 | 3 << RegionHelper.Districtlmove,
        柳北区 = 柳州市 | 4 << RegionHelper.Districtlmove,
        柳江县 = 柳州市 | 5 << RegionHelper.Districtlmove,
        柳城县 = 柳州市 | 6 << RegionHelper.Districtlmove,
        鹿寨县 = 柳州市 | 7 << RegionHelper.Districtlmove,
        融安县 = 柳州市 | 8 << RegionHelper.Districtlmove,
        融水县 = 柳州市 | 9 << RegionHelper.Districtlmove,
        三江县 = 柳州市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县
        桂林市 = 广西 | 3 << RegionHelper.Citylmove,
        #region 区县
        秀峰区 = 桂林市 | 1 << RegionHelper.Districtlmove,
        叠彩区 = 桂林市 | 2 << RegionHelper.Districtlmove,
        象山区 = 桂林市 | 3 << RegionHelper.Districtlmove,
        七星区 = 桂林市 | 4 << RegionHelper.Districtlmove,
        雁山区 = 桂林市 | 5 << RegionHelper.Districtlmove,
        阳朔县 = 桂林市 | 6 << RegionHelper.Districtlmove,
        临桂区 = 桂林市 | 7 << RegionHelper.Districtlmove,
        灵川县 = 桂林市 | 8 << RegionHelper.Districtlmove,
        全州县 = 桂林市 | 9 << RegionHelper.Districtlmove,
        兴安县 = 桂林市 | 10 << RegionHelper.Districtlmove,
        永福县 = 桂林市 | 11 << RegionHelper.Districtlmove,
        灌阳县 = 桂林市 | 12 << RegionHelper.Districtlmove,
        龙胜县 = 桂林市 | 13 << RegionHelper.Districtlmove,
        资源县 = 桂林市 | 14 << RegionHelper.Districtlmove,
        平乐县 = 桂林市 | 15 << RegionHelper.Districtlmove,
        荔浦县 = 桂林市 | 16 << RegionHelper.Districtlmove,
        恭城县 = 桂林市 | 17 << RegionHelper.Districtlmove,
        #endregion 区县
        梧州市 = 广西 | 4 << RegionHelper.Citylmove,
        #region 区县
        万秀区 = 梧州市 | 1 << RegionHelper.Districtlmove,
        龙圩区 = 梧州市 | 2 << RegionHelper.Districtlmove,
        长洲区 = 梧州市 | 3 << RegionHelper.Districtlmove,
        苍梧县 = 梧州市 | 4 << RegionHelper.Districtlmove,
        藤县 = 梧州市 | 5 << RegionHelper.Districtlmove,
        蒙山县 = 梧州市 | 6 << RegionHelper.Districtlmove,
        岑溪市 = 梧州市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县
        北海市 = 广西 | 5 << RegionHelper.Citylmove,
        #region 区县
        海城区 = 北海市 | 1 << RegionHelper.Districtlmove,
        银海区 = 北海市 | 2 << RegionHelper.Districtlmove,
        铁山港区 = 北海市 | 3 << RegionHelper.Districtlmove,
        合浦县 = 北海市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        防城港市 = 广西 | 6 << RegionHelper.Citylmove,
        #region 区县
        港口区 = 防城港市 | 1 << RegionHelper.Districtlmove,
        防城区 = 防城港市 | 2 << RegionHelper.Districtlmove,
        上思县 = 防城港市 | 3 << RegionHelper.Districtlmove,
        东兴市 = 防城港市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县
        钦州市 = 广西 | 7 << RegionHelper.Citylmove,
        #region 区县
        钦南区 = 钦州市 | 1 << RegionHelper.Districtlmove,
        钦北区 = 钦州市 | 2 << RegionHelper.Districtlmove,
        灵山县 = 钦州市 | 3 << RegionHelper.Districtlmove,
        浦北区 = 钦州市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        贵港市 = 广西 | 8 << RegionHelper.Citylmove,
        #region 区县
        港北区 = 贵港市 | 1 << RegionHelper.Districtlmove,
        港南区 = 贵港市 | 2 << RegionHelper.Districtlmove,
        覃塘区 = 贵港市 | 3 << RegionHelper.Districtlmove,
        平南县 = 贵港市 | 4 << RegionHelper.Districtlmove,
        桂平市 = 贵港市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        玉林市 = 广西 | 9 << RegionHelper.Citylmove,
        #region 区县
        玉州区 = 玉林市 | 1 << RegionHelper.Districtlmove,
        容县 = 玉林市 | 2 << RegionHelper.Districtlmove,
        陆川县 = 玉林市 | 3 << RegionHelper.Districtlmove,
        博白县 = 玉林市 | 4 << RegionHelper.Districtlmove,
        兴业县 = 玉林市 | 5 << RegionHelper.Districtlmove,
        北流市 = 玉林市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        百色市 = 广西 | 10 << RegionHelper.Citylmove,
        #region 区县
        右江区 = 百色市 | 1 << RegionHelper.Districtlmove,
        田阳县 = 百色市 | 2 << RegionHelper.Districtlmove,
        田东县 = 百色市 | 3 << RegionHelper.Districtlmove,
        平果县 = 百色市 | 4 << RegionHelper.Districtlmove,
        德保县 = 百色市 | 5 << RegionHelper.Districtlmove,
        靖西县 = 百色市 | 6 << RegionHelper.Districtlmove,
        那坡县 = 百色市 | 7 << RegionHelper.Districtlmove,
        凌云县 = 百色市 | 8 << RegionHelper.Districtlmove,
        乐业县 = 百色市 | 9 << RegionHelper.Districtlmove,
        田林县 = 百色市 | 10 << RegionHelper.Districtlmove,
        西林县 = 百色市 | 11 << RegionHelper.Districtlmove,
        隆林县 = 百色市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        贺州市 = 广西 | 11 << RegionHelper.Citylmove,
        #region 区县
        八步区 = 贺州市 | 1 << RegionHelper.Districtlmove,
        昭平县 = 贺州市 | 2 << RegionHelper.Districtlmove,
        钟山县 = 贺州市 | 3 << RegionHelper.Districtlmove,
        富川县 = 贺州市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        河池市 = 广西 | 12 << RegionHelper.Citylmove,
        #region 区县
        金城江区 = 河池市 | 1 << RegionHelper.Districtlmove,
        南丹县 = 河池市 | 2 << RegionHelper.Districtlmove,
        天蛾县 = 河池市 | 3 << RegionHelper.Districtlmove,
        凤山县 = 河池市 | 4 << RegionHelper.Districtlmove,
        东兰县 = 河池市 | 5 << RegionHelper.Districtlmove,
        罗城县 = 河池市 | 6 << RegionHelper.Districtlmove,
        环江县 = 河池市 | 7 << RegionHelper.Districtlmove,
        巴马县 = 河池市 | 8 << RegionHelper.Districtlmove,
        都安县 = 河池市 | 9 << RegionHelper.Districtlmove,
        大化县 = 河池市 | 10 << RegionHelper.Districtlmove,
        宜州市 = 河池市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        来宾市 = 广西 | 13 << RegionHelper.Citylmove,
        #region 区县
        兴宾区 = 来宾市 | 1 << RegionHelper.Districtlmove,
        忻城县 = 来宾市 | 2 << RegionHelper.Districtlmove,
        象州县 = 来宾市 | 3 << RegionHelper.Districtlmove,
        武宣县 = 来宾市 | 4 << RegionHelper.Districtlmove,
        金秀县 = 来宾市 | 5 << RegionHelper.Districtlmove,
        合山市 = 来宾市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        崇左市 = 广西 | 14 << RegionHelper.Citylmove,
        #region 区县
        江州区 = 崇左市 | 1 << RegionHelper.Districtlmove,
        扶绥县 = 崇左市 | 2 << RegionHelper.Districtlmove,
        宁明县 = 崇左市 | 3 << RegionHelper.Districtlmove,
        龙州县 = 崇左市 | 4 << RegionHelper.Districtlmove,
        大新县 = 崇左市 | 5 << RegionHelper.Districtlmove,
        天等县 = 崇左市 | 6 << RegionHelper.Districtlmove,
        凭祥市 = 崇左市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        #endregion 市

        山西 = 17 << RegionHelper.Provincelmove,//省                
        #region 市
        太原市 = 山西 | 1 << RegionHelper.Citylmove,
        #region 区县
        小店区 = 太原市 | 1 << RegionHelper.Districtlmove,
        迎泽区 = 太原市 | 2 << RegionHelper.Districtlmove,
        杏花岭区 = 太原市 | 3 << RegionHelper.Districtlmove,
        尖草坪区 = 太原市 | 4 << RegionHelper.Districtlmove,
        万柏林区 = 太原市 | 5 << RegionHelper.Districtlmove,
        晋源区 = 太原市 | 6 << RegionHelper.Districtlmove,
        清徐县 = 太原市 | 7 << RegionHelper.Districtlmove,
        阳曲县 = 太原市 | 8 << RegionHelper.Districtlmove,
        娄烦县 = 太原市 | 9 << RegionHelper.Districtlmove,
        古交市 = 太原市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        大同市 = 山西 | 2 << RegionHelper.Citylmove,
        #region 区县
        大同城区 = 大同市 | 1 << RegionHelper.Districtlmove,
        大同矿区 = 大同市 | 2 << RegionHelper.Districtlmove,
        南郊区 = 大同市 | 3 << RegionHelper.Districtlmove,
        新荣区 = 大同市 | 4 << RegionHelper.Districtlmove,
        阳高县 = 大同市 | 5 << RegionHelper.Districtlmove,
        天镇县 = 大同市 | 6 << RegionHelper.Districtlmove,
        广灵县 = 大同市 | 7 << RegionHelper.Districtlmove,
        灵丘县 = 大同市 | 8 << RegionHelper.Districtlmove,
        浑源县 = 大同市 | 9 << RegionHelper.Districtlmove,
        左云县 = 大同市 | 10 << RegionHelper.Districtlmove,
        大同县 = 大同市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县


        阳泉市 = 山西 | 3 << RegionHelper.Citylmove,
        #region 区县
        阳泉城区 = 阳泉市 | 1 << RegionHelper.Districtlmove,
        矿区 = 阳泉市 | 2 << RegionHelper.Districtlmove,
        阳泉郊区 = 阳泉市 | 3 << RegionHelper.Districtlmove,
        平定县 = 阳泉市 | 4 << RegionHelper.Districtlmove,
        盂县 = 阳泉市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县


        长治市 = 山西 | 4 << RegionHelper.Citylmove,
        #region 区县
        长治城区 = 长治市 | 1 << RegionHelper.Districtlmove,
        长治郊区 = 长治市 | 2 << RegionHelper.Districtlmove,
        长治县 = 长治市 | 3 << RegionHelper.Districtlmove,
        襄垣县 = 长治市 | 4 << RegionHelper.Districtlmove,
        屯留县 = 长治市 | 5 << RegionHelper.Districtlmove,
        平顺县 = 长治市 | 6 << RegionHelper.Districtlmove,
        黎城县 = 长治市 | 7 << RegionHelper.Districtlmove,
        壶关县 = 长治市 | 8 << RegionHelper.Districtlmove,
        长子县 = 长治市 | 9 << RegionHelper.Districtlmove,
        武乡县 = 长治市 | 10 << RegionHelper.Districtlmove,
        沁县 = 长治市 | 11 << RegionHelper.Districtlmove,
        沁源县 = 长治市 | 12 << RegionHelper.Districtlmove,
        潞城市 = 长治市 | 13 << RegionHelper.Districtlmove,
        #endregion 区县

        晋城市 = 山西 | 5 << RegionHelper.Citylmove,
        #region 区县
        晋城城区 = 晋城市 | 1 << RegionHelper.Districtlmove,
        沁水县 = 晋城市 | 2 << RegionHelper.Districtlmove,
        阳城县 = 晋城市 | 3 << RegionHelper.Districtlmove,
        陵川县 = 晋城市 | 4 << RegionHelper.Districtlmove,
        泽州县 = 晋城市 | 5 << RegionHelper.Districtlmove,
        高平市 = 晋城市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        朔州市 = 山西 | 6 << RegionHelper.Citylmove,
        #region 区县
        朔城区 = 朔州市 | 1 << RegionHelper.Districtlmove,
        平鲁区 = 朔州市 | 2 << RegionHelper.Districtlmove,
        山阴县 = 朔州市 | 3 << RegionHelper.Districtlmove,
        应县 = 朔州市 | 4 << RegionHelper.Districtlmove,
        右玉县 = 朔州市 | 5 << RegionHelper.Districtlmove,
        怀仁县 = 朔州市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县
        晋中市 = 山西 | 7 << RegionHelper.Citylmove,
        #region 区县
        榆次区 = 晋中市 | 1 << RegionHelper.Districtlmove,
        榆社县 = 晋中市 | 2 << RegionHelper.Districtlmove,
        左权县 = 晋中市 | 3 << RegionHelper.Districtlmove,
        和顺县 = 晋中市 | 4 << RegionHelper.Districtlmove,
        昔阳县 = 晋中市 | 5 << RegionHelper.Districtlmove,
        寿阳县 = 晋中市 | 6 << RegionHelper.Districtlmove,
        太谷县 = 晋中市 | 7 << RegionHelper.Districtlmove,
        祁县 = 晋中市 | 8 << RegionHelper.Districtlmove,
        平遥县 = 晋中市 | 9 << RegionHelper.Districtlmove,
        灵石县 = 晋中市 | 10 << RegionHelper.Districtlmove,
        介休市 = 晋中市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        运城市 = 山西 | 8 << RegionHelper.Citylmove,
        #region 区县
        盐湖区 = 运城市 | 1 << RegionHelper.Districtlmove,
        临猗县 = 运城市 | 2 << RegionHelper.Districtlmove,
        万荣县 = 运城市 | 3 << RegionHelper.Districtlmove,
        闻喜县 = 运城市 | 4 << RegionHelper.Districtlmove,
        稷山县 = 运城市 | 5 << RegionHelper.Districtlmove,
        新绛县 = 运城市 | 6 << RegionHelper.Districtlmove,
        绛县 = 运城市 | 7 << RegionHelper.Districtlmove,
        垣曲县 = 运城市 | 8 << RegionHelper.Districtlmove,
        夏县 = 运城市 | 9 << RegionHelper.Districtlmove,
        平陆县 = 运城市 | 10 << RegionHelper.Districtlmove,
        芮城县 = 运城市 | 11 << RegionHelper.Districtlmove,
        永济市 = 运城市 | 12 << RegionHelper.Districtlmove,
        河津市 = 运城市 | 13 << RegionHelper.Districtlmove,
        #endregion 区县

        忻州市 = 山西 | 9 << RegionHelper.Citylmove,
        #region 区县
        忻府区 = 忻州市 | 1 << RegionHelper.Districtlmove,
        定襄县 = 忻州市 | 2 << RegionHelper.Districtlmove,
        五台县 = 忻州市 | 3 << RegionHelper.Districtlmove,
        代县 = 忻州市 | 4 << RegionHelper.Districtlmove,
        繁峙县 = 忻州市 | 5 << RegionHelper.Districtlmove,
        宁武县 = 忻州市 | 6 << RegionHelper.Districtlmove,
        静乐县 = 忻州市 | 7 << RegionHelper.Districtlmove,
        神池县 = 忻州市 | 8 << RegionHelper.Districtlmove,
        五寨县 = 忻州市 | 9 << RegionHelper.Districtlmove,
        岢岚县 = 忻州市 | 10 << RegionHelper.Districtlmove,
        河曲县 = 忻州市 | 11 << RegionHelper.Districtlmove,
        保德县 = 忻州市 | 12 << RegionHelper.Districtlmove,
        偏关县 = 忻州市 | 13 << RegionHelper.Districtlmove,
        原平市 = 忻州市 | 14 << RegionHelper.Districtlmove,
        #endregion 区县

        临汾市 = 山西 | 10 << RegionHelper.Citylmove,
        #region 区县
        尧都区 = 临汾市 | 1 << RegionHelper.Districtlmove,
        曲沃县 = 临汾市 | 2 << RegionHelper.Districtlmove,
        翼城县 = 临汾市 | 3 << RegionHelper.Districtlmove,
        襄汾县 = 临汾市 | 4 << RegionHelper.Districtlmove,
        洪洞县 = 临汾市 | 5 << RegionHelper.Districtlmove,
        古县 = 临汾市 | 6 << RegionHelper.Districtlmove,
        安泽县 = 临汾市 | 7 << RegionHelper.Districtlmove,
        浮山县 = 临汾市 | 8 << RegionHelper.Districtlmove,
        吉县 = 临汾市 | 9 << RegionHelper.Districtlmove,
        乡宁县 = 临汾市 | 10 << RegionHelper.Districtlmove,
        大宁县 = 临汾市 | 11 << RegionHelper.Districtlmove,
        隰县 = 临汾市 | 12 << RegionHelper.Districtlmove,
        永和县 = 临汾市 | 13 << RegionHelper.Districtlmove,
        蒲县 = 临汾市 | 14 << RegionHelper.Districtlmove,
        汾西县 = 临汾市 | 15 << RegionHelper.Districtlmove,
        侯马市 = 临汾市 | 16 << RegionHelper.Districtlmove,
        霍州市 = 临汾市 | 17 << RegionHelper.Districtlmove,
        #endregion 区县

        吕梁市 = 山西 | 11 << RegionHelper.Citylmove,
        #region 区县
        离石区 = 吕梁市 | 1 << RegionHelper.Districtlmove,
        文水县 = 吕梁市 | 2 << RegionHelper.Districtlmove,
        交城县 = 吕梁市 | 3 << RegionHelper.Districtlmove,
        兴县 = 吕梁市 | 4 << RegionHelper.Districtlmove,
        临县 = 吕梁市 | 5 << RegionHelper.Districtlmove,
        柳林县 = 吕梁市 | 6 << RegionHelper.Districtlmove,
        石楼县 = 吕梁市 | 7 << RegionHelper.Districtlmove,
        岚县 = 吕梁市 | 8 << RegionHelper.Districtlmove,
        方山县 = 吕梁市 | 9 << RegionHelper.Districtlmove,
        中阳县 = 吕梁市 | 10 << RegionHelper.Districtlmove,
        交口县 = 吕梁市 | 11 << RegionHelper.Districtlmove,
        孝义市 = 吕梁市 | 12 << RegionHelper.Districtlmove,
        汾阳市 = 吕梁市 | 13 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        陕西 = 18 << RegionHelper.Provincelmove,//省                
        #region 市
        西安市 = 陕西 | 1 << RegionHelper.Citylmove,
        #region 区县
        西安新城区 = 西安市 | 1 << RegionHelper.Districtlmove,
        碑林区 = 西安市 | 2 << RegionHelper.Districtlmove,
        莲湖区 = 西安市 | 3 << RegionHelper.Districtlmove,
        灞桥区 = 西安市 | 4 << RegionHelper.Districtlmove,
        未央区 = 西安市 | 5 << RegionHelper.Districtlmove,
        雁塔区 = 西安市 | 6 << RegionHelper.Districtlmove,
        阎良区 = 西安市 | 7 << RegionHelper.Districtlmove,
        临潼区 = 西安市 | 8 << RegionHelper.Districtlmove,
        西安长安区 = 西安市 | 9 << RegionHelper.Districtlmove,
        蓝田县 = 西安市 | 10 << RegionHelper.Districtlmove,
        周至县 = 西安市 | 11 << RegionHelper.Districtlmove,
        户县 = 西安市 | 12 << RegionHelper.Districtlmove,
        高陵县 = 西安市 | 13 << RegionHelper.Districtlmove,
        #endregion 区县

        铜川市 = 陕西 | 2 << RegionHelper.Citylmove,
        #region 区县
        王益区 = 铜川市 | 1 << RegionHelper.Districtlmove,
        印台区 = 铜川市 | 2 << RegionHelper.Districtlmove,
        耀州区 = 铜川市 | 3 << RegionHelper.Districtlmove,
        宜君县 = 铜川市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县
        宝鸡市 = 陕西 | 3 << RegionHelper.Citylmove,
        #region 区县
        渭滨区 = 宝鸡市 | 1 << RegionHelper.Districtlmove,
        金台区 = 宝鸡市 | 2 << RegionHelper.Districtlmove,
        陈仓区 = 宝鸡市 | 3 << RegionHelper.Districtlmove,
        凤翔县 = 宝鸡市 | 4 << RegionHelper.Districtlmove,
        岐山县 = 宝鸡市 | 5 << RegionHelper.Districtlmove,
        扶风县 = 宝鸡市 | 6 << RegionHelper.Districtlmove,
        眉县 = 宝鸡市 | 7 << RegionHelper.Districtlmove,
        陇县 = 宝鸡市 | 8 << RegionHelper.Districtlmove,
        千阳县 = 宝鸡市 | 9 << RegionHelper.Districtlmove,
        麟游县 = 宝鸡市 | 10 << RegionHelper.Districtlmove,
        凤县 = 宝鸡市 | 11 << RegionHelper.Districtlmove,
        太白县 = 宝鸡市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县
        咸阳市 = 陕西 | 4 << RegionHelper.Citylmove,
        #region 区县
        秦都区 = 咸阳市 | 1 << RegionHelper.Districtlmove,
        杨陵区 = 咸阳市 | 2 << RegionHelper.Districtlmove,
        渭城区 = 咸阳市 | 3 << RegionHelper.Districtlmove,
        三原县 = 咸阳市 | 4 << RegionHelper.Districtlmove,
        泾阳县 = 咸阳市 | 5 << RegionHelper.Districtlmove,
        乾县 = 咸阳市 | 6 << RegionHelper.Districtlmove,
        礼泉县 = 咸阳市 | 7 << RegionHelper.Districtlmove,
        永寿县 = 咸阳市 | 8 << RegionHelper.Districtlmove,
        彬县 = 咸阳市 | 9 << RegionHelper.Districtlmove,
        长武县 = 咸阳市 | 10 << RegionHelper.Districtlmove,
        旬邑县 = 咸阳市 | 11 << RegionHelper.Districtlmove,
        淳化县 = 咸阳市 | 12 << RegionHelper.Districtlmove,
        武功县 = 咸阳市 | 13 << RegionHelper.Districtlmove,
        兴平市 = 咸阳市 | 14 << RegionHelper.Districtlmove,
        #endregion 区县

        渭南市 = 陕西 | 5 << RegionHelper.Citylmove,
        #region 区县
        临渭区 = 渭南市 | 1 << RegionHelper.Districtlmove,
        华县 = 渭南市 | 2 << RegionHelper.Districtlmove,
        潼关县 = 渭南市 | 3 << RegionHelper.Districtlmove,
        大荔县 = 渭南市 | 4 << RegionHelper.Districtlmove,
        合阳县 = 渭南市 | 5 << RegionHelper.Districtlmove,
        澄城县 = 渭南市 | 6 << RegionHelper.Districtlmove,
        蒲城县 = 渭南市 | 7 << RegionHelper.Districtlmove,
        白水县 = 渭南市 | 8 << RegionHelper.Districtlmove,
        富平县 = 渭南市 | 9 << RegionHelper.Districtlmove,
        韩城市 = 渭南市 | 10 << RegionHelper.Districtlmove,
        华阴市 = 渭南市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        延安市 = 陕西 | 6 << RegionHelper.Citylmove,
        #region 区县
        宝塔区 = 延安市 | 1 << RegionHelper.Districtlmove,
        延长县 = 延安市 | 2 << RegionHelper.Districtlmove,
        延川县 = 延安市 | 3 << RegionHelper.Districtlmove,
        子长县 = 延安市 | 4 << RegionHelper.Districtlmove,
        安塞县 = 延安市 | 5 << RegionHelper.Districtlmove,
        志丹县 = 延安市 | 6 << RegionHelper.Districtlmove,
        吴起县 = 延安市 | 7 << RegionHelper.Districtlmove,
        甘泉县 = 延安市 | 8 << RegionHelper.Districtlmove,
        富县 = 延安市 | 9 << RegionHelper.Districtlmove,
        洛川县 = 延安市 | 10 << RegionHelper.Districtlmove,
        宜川县 = 延安市 | 11 << RegionHelper.Districtlmove,
        黄龙县 = 延安市 | 12 << RegionHelper.Districtlmove,
        黄陵县 = 延安市 | 13 << RegionHelper.Districtlmove,
        #endregion 区县

        汉中市 = 陕西 | 7 << RegionHelper.Citylmove,
        #region 区县
        汉台区 = 汉中市 | 1 << RegionHelper.Districtlmove,
        南郑县 = 汉中市 | 2 << RegionHelper.Districtlmove,
        城固县 = 汉中市 | 3 << RegionHelper.Districtlmove,
        洋县 = 汉中市 | 4 << RegionHelper.Districtlmove,
        西乡县 = 汉中市 | 5 << RegionHelper.Districtlmove,
        勉县 = 汉中市 | 6 << RegionHelper.Districtlmove,
        宁强县 = 汉中市 | 7 << RegionHelper.Districtlmove,
        略阳县 = 汉中市 | 8 << RegionHelper.Districtlmove,
        镇巴县 = 汉中市 | 9 << RegionHelper.Districtlmove,
        留坝县 = 汉中市 | 10 << RegionHelper.Districtlmove,
        佛坪县 = 汉中市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        榆林市 = 陕西 | 8 << RegionHelper.Citylmove,
        #region 区县
        榆阳区 = 榆林市 | 1 << RegionHelper.Districtlmove,
        神木县 = 榆林市 | 2 << RegionHelper.Districtlmove,
        府谷县 = 榆林市 | 3 << RegionHelper.Districtlmove,
        横山县 = 榆林市 | 4 << RegionHelper.Districtlmove,
        靖边县 = 榆林市 | 5 << RegionHelper.Districtlmove,
        定边县 = 榆林市 | 6 << RegionHelper.Districtlmove,
        绥德县 = 榆林市 | 7 << RegionHelper.Districtlmove,
        米脂县 = 榆林市 | 8 << RegionHelper.Districtlmove,
        佳县 = 榆林市 | 9 << RegionHelper.Districtlmove,
        吴堡县 = 榆林市 | 10 << RegionHelper.Districtlmove,
        清涧县 = 榆林市 | 11 << RegionHelper.Districtlmove,
        子洲县 = 榆林市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        安康市 = 陕西 | 9 << RegionHelper.Citylmove,
        #region 区县
        汉滨区 = 安康市 | 1 << RegionHelper.Districtlmove,
        汉阴县 = 安康市 | 2 << RegionHelper.Districtlmove,
        石泉县 = 安康市 | 3 << RegionHelper.Districtlmove,
        宁陕县 = 安康市 | 4 << RegionHelper.Districtlmove,
        紫阳县 = 安康市 | 5 << RegionHelper.Districtlmove,
        岚皋县 = 安康市 | 6 << RegionHelper.Districtlmove,
        平利县 = 安康市 | 7 << RegionHelper.Districtlmove,
        镇坪县 = 安康市 | 8 << RegionHelper.Districtlmove,
        旬阳县 = 安康市 | 9 << RegionHelper.Districtlmove,
        白河县 = 安康市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        商洛市 = 陕西 | 10 << RegionHelper.Citylmove,
        #region 区县
        商州区 = 商洛市 | 1 << RegionHelper.Districtlmove,
        洛南县 = 商洛市 | 2 << RegionHelper.Districtlmove,
        丹凤县 = 商洛市 | 3 << RegionHelper.Districtlmove,
        商南县 = 商洛市 | 4 << RegionHelper.Districtlmove,
        山阳县 = 商洛市 | 5 << RegionHelper.Districtlmove,
        镇安县 = 商洛市 | 6 << RegionHelper.Districtlmove,
        柞水县 = 商洛市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        江西 = 19 << RegionHelper.Provincelmove,//省                
        #region 市
        南昌市 = 江西 | 1 << RegionHelper.Citylmove,
        #region 区县
        东湖区 = 南昌市 | 1 << RegionHelper.Districtlmove,
        南昌西湖区 = 南昌市 | 2 << RegionHelper.Districtlmove,
        青云谱区 = 南昌市 | 3 << RegionHelper.Districtlmove,
        湾里区 = 南昌市 | 4 << RegionHelper.Districtlmove,
        青山湖区 = 南昌市 | 5 << RegionHelper.Districtlmove,
        南昌县 = 南昌市 | 6 << RegionHelper.Districtlmove,
        新建县 = 南昌市 | 7 << RegionHelper.Districtlmove,
        安义县 = 南昌市 | 8 << RegionHelper.Districtlmove,
        进贤县 = 南昌市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        景德镇市 = 江西 | 2 << RegionHelper.Citylmove,
        #region 区县
        昌江区 = 景德镇市 | 1 << RegionHelper.Districtlmove,
        珠山区 = 景德镇市 | 2 << RegionHelper.Districtlmove,
        浮梁县 = 景德镇市 | 3 << RegionHelper.Districtlmove,
        乐平市 = 景德镇市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        萍乡市 = 江西 | 3 << RegionHelper.Citylmove,
        #region 区县
        安源区 = 萍乡市 | 1 << RegionHelper.Districtlmove,
        湘东区 = 萍乡市 | 2 << RegionHelper.Districtlmove,
        莲花县 = 萍乡市 | 3 << RegionHelper.Districtlmove,
        上栗县 = 萍乡市 | 4 << RegionHelper.Districtlmove,
        芦溪县 = 萍乡市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        九江市 = 江西 | 4 << RegionHelper.Citylmove,
        #region 区县
        庐山区 = 九江市 | 1 << RegionHelper.Districtlmove,
        浔阳区 = 九江市 | 2 << RegionHelper.Districtlmove,
        九江县 = 九江市 | 3 << RegionHelper.Districtlmove,
        武宁县 = 九江市 | 4 << RegionHelper.Districtlmove,
        修水县 = 九江市 | 5 << RegionHelper.Districtlmove,
        永修县 = 九江市 | 6 << RegionHelper.Districtlmove,
        德安县 = 九江市 | 7 << RegionHelper.Districtlmove,
        星子县 = 九江市 | 8 << RegionHelper.Districtlmove,
        都昌县 = 九江市 | 9 << RegionHelper.Districtlmove,
        湖口县 = 九江市 | 10 << RegionHelper.Districtlmove,
        彭泽县 = 九江市 | 11 << RegionHelper.Districtlmove,
        瑞昌市 = 九江市 | 12 << RegionHelper.Districtlmove,
        共青城市 = 九江市 | 13 << RegionHelper.Districtlmove,
        #endregion 区县

        新余市 = 江西 | 5 << RegionHelper.Citylmove,
        #region 区县
        渝水区 = 新余市 | 1 << RegionHelper.Districtlmove,
        分宜县 = 新余市 | 2 << RegionHelper.Districtlmove,
        #endregion 区县

        鹰潭市 = 江西 | 6 << RegionHelper.Citylmove,
        #region 区县
        月湖区 = 鹰潭市 | 1 << RegionHelper.Districtlmove,
        余江县 = 鹰潭市 | 2 << RegionHelper.Districtlmove,
        贵溪市 = 鹰潭市 | 3 << RegionHelper.Districtlmove,
        #endregion 区县

        赣州市 = 江西 | 7 << RegionHelper.Citylmove,
        #region 区县
        章贡区 = 赣州市 | 1 << RegionHelper.Districtlmove,
        赣县 = 赣州市 | 2 << RegionHelper.Districtlmove,
        信丰县 = 赣州市 | 3 << RegionHelper.Districtlmove,
        大余县 = 赣州市 | 4 << RegionHelper.Districtlmove,
        上犹县 = 赣州市 | 5 << RegionHelper.Districtlmove,
        崇义县 = 赣州市 | 6 << RegionHelper.Districtlmove,
        安远县 = 赣州市 | 7 << RegionHelper.Districtlmove,
        龙南县 = 赣州市 | 8 << RegionHelper.Districtlmove,
        定南县 = 赣州市 | 9 << RegionHelper.Districtlmove,
        全南县 = 赣州市 | 10 << RegionHelper.Districtlmove,
        宁都县 = 赣州市 | 11 << RegionHelper.Districtlmove,
        于都县 = 赣州市 | 12 << RegionHelper.Districtlmove,
        兴国县 = 赣州市 | 13 << RegionHelper.Districtlmove,
        会昌县 = 赣州市 | 14 << RegionHelper.Districtlmove,
        寻乌县 = 赣州市 | 15 << RegionHelper.Districtlmove,
        石城县 = 赣州市 | 16 << RegionHelper.Districtlmove,
        瑞金市 = 赣州市 | 17 << RegionHelper.Districtlmove,
        南康市 = 赣州市 | 18 << RegionHelper.Districtlmove,
        #endregion 区县


        吉安市 = 江西 | 8 << RegionHelper.Citylmove,
        #region 区县
        吉州区 = 吉安市 | 1 << RegionHelper.Districtlmove,
        青原区 = 吉安市 | 2 << RegionHelper.Districtlmove,
        吉安县 = 吉安市 | 3 << RegionHelper.Districtlmove,
        吉水县 = 吉安市 | 4 << RegionHelper.Districtlmove,
        峡江县 = 吉安市 | 5 << RegionHelper.Districtlmove,
        新干县 = 吉安市 | 6 << RegionHelper.Districtlmove,
        永丰县 = 吉安市 | 7 << RegionHelper.Districtlmove,
        泰和县 = 吉安市 | 8 << RegionHelper.Districtlmove,
        遂川县 = 吉安市 | 9 << RegionHelper.Districtlmove,
        万安县 = 吉安市 | 10 << RegionHelper.Districtlmove,
        安福县 = 吉安市 | 11 << RegionHelper.Districtlmove,
        永新县 = 吉安市 | 12 << RegionHelper.Districtlmove,
        井岗山市 = 吉安市 | 13 << RegionHelper.Districtlmove,
        #endregion 区县

        宜春市 = 江西 | 9 << RegionHelper.Citylmove,
        #region 区县
        袁州区 = 宜春市 | 1 << RegionHelper.Districtlmove,
        奉新县 = 宜春市 | 2 << RegionHelper.Districtlmove,
        万载县 = 宜春市 | 3 << RegionHelper.Districtlmove,
        上高县 = 宜春市 | 4 << RegionHelper.Districtlmove,
        宜丰县 = 宜春市 | 5 << RegionHelper.Districtlmove,
        靖安县 = 宜春市 | 6 << RegionHelper.Districtlmove,
        铜鼓县 = 宜春市 | 7 << RegionHelper.Districtlmove,
        丰城市 = 宜春市 | 8 << RegionHelper.Districtlmove,
        樟树市 = 宜春市 | 9 << RegionHelper.Districtlmove,
        高安市 = 宜春市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        抚州市 = 江西 | 10 << RegionHelper.Citylmove,
        #region 区县
        临川区 = 抚州市 | 1 << RegionHelper.Districtlmove,
        南城县 = 抚州市 | 2 << RegionHelper.Districtlmove,
        黎川县 = 抚州市 | 3 << RegionHelper.Districtlmove,
        南丰县 = 抚州市 | 4 << RegionHelper.Districtlmove,
        崇仁县 = 抚州市 | 5 << RegionHelper.Districtlmove,
        乐安县 = 抚州市 | 6 << RegionHelper.Districtlmove,
        宜黄县 = 抚州市 | 7 << RegionHelper.Districtlmove,
        金溪县 = 抚州市 | 8 << RegionHelper.Districtlmove,
        资溪县 = 抚州市 | 9 << RegionHelper.Districtlmove,
        东乡县 = 抚州市 | 10 << RegionHelper.Districtlmove,
        广昌县 = 抚州市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县
        上饶市 = 江西 | 11 << RegionHelper.Citylmove,
        #region 区县
        信州区 = 上饶市 | 1 << RegionHelper.Districtlmove,
        上饶县 = 上饶市 | 2 << RegionHelper.Districtlmove,
        广丰县 = 上饶市 | 3 << RegionHelper.Districtlmove,
        玉山县 = 上饶市 | 4 << RegionHelper.Districtlmove,
        铅山县 = 上饶市 | 5 << RegionHelper.Districtlmove,
        横峰县 = 上饶市 | 6 << RegionHelper.Districtlmove,
        弋阳县 = 上饶市 | 7 << RegionHelper.Districtlmove,
        余干县 = 上饶市 | 8 << RegionHelper.Districtlmove,
        鄱阳县 = 上饶市 | 9 << RegionHelper.Districtlmove,
        万年县 = 上饶市 | 10 << RegionHelper.Districtlmove,
        婺源县 = 上饶市 | 11 << RegionHelper.Districtlmove,
        德兴市 = 上饶市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        #endregion 市

        吉林 = 20 << RegionHelper.Provincelmove,//省                
        #region 市
        长春市 = 吉林 | 1 << RegionHelper.Citylmove,
        #region 区县
        南关区 = 长春市 | 1 << RegionHelper.Districtlmove,
        宽城区 = 长春市 | 2 << RegionHelper.Districtlmove,
        长春朝阳区 = 长春市 | 3 << RegionHelper.Districtlmove,
        二道区 = 长春市 | 4 << RegionHelper.Districtlmove,
        绿园区 = 长春市 | 5 << RegionHelper.Districtlmove,
        双阳区 = 长春市 | 6 << RegionHelper.Districtlmove,
        农安县 = 长春市 | 7 << RegionHelper.Districtlmove,
        九台市 = 长春市 | 8 << RegionHelper.Districtlmove,
        榆树市 = 长春市 | 9 << RegionHelper.Districtlmove,
        德惠市 = 长春市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        吉林市 = 吉林 | 2 << RegionHelper.Citylmove,
        #region 区县
        昌邑区 = 吉林市 | 1 << RegionHelper.Districtlmove,
        龙潭区 = 吉林市 | 2 << RegionHelper.Districtlmove,
        船营区 = 吉林市 | 3 << RegionHelper.Districtlmove,
        丰满区 = 吉林市 | 4 << RegionHelper.Districtlmove,
        永吉县 = 吉林市 | 5 << RegionHelper.Districtlmove,
        蛟河市 = 吉林市 | 6 << RegionHelper.Districtlmove,
        桦甸市 = 吉林市 | 7 << RegionHelper.Districtlmove,
        舒兰市 = 吉林市 | 8 << RegionHelper.Districtlmove,
        磐石市 = 吉林市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        四平市 = 吉林 | 3 << RegionHelper.Citylmove,
        #region 区县
        四平铁西区 = 四平市 | 1 << RegionHelper.Districtlmove,
        四平铁东区 = 四平市 | 2 << RegionHelper.Districtlmove,
        梨树县 = 四平市 | 3 << RegionHelper.Districtlmove,
        伊通满族自治县 = 四平市 | 4 << RegionHelper.Districtlmove,
        公主岭市 = 四平市 | 5 << RegionHelper.Districtlmove,
        双辽市 = 四平市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        辽源市 = 吉林 | 4 << RegionHelper.Citylmove,
        #region 区县
        龙山区 = 辽源市 | 1 << RegionHelper.Districtlmove,
        辽源西安区 = 辽源市 | 2 << RegionHelper.Districtlmove,
        东丰县 = 辽源市 | 3 << RegionHelper.Districtlmove,
        东辽县 = 辽源市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        通化市 = 吉林 | 5 << RegionHelper.Citylmove,
        #region 区县
        东昌区 = 通化市 | 1 << RegionHelper.Districtlmove,
        二道江区 = 通化市 | 2 << RegionHelper.Districtlmove,
        通化县 = 通化市 | 3 << RegionHelper.Districtlmove,
        辉南县 = 通化市 | 4 << RegionHelper.Districtlmove,
        柳河县 = 通化市 | 5 << RegionHelper.Districtlmove,
        梅河口市 = 通化市 | 6 << RegionHelper.Districtlmove,
        集安市 = 通化市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        白山市 = 吉林 | 6 << RegionHelper.Citylmove,
        #region 区县
        浑江区 = 白山市 | 1 << RegionHelper.Districtlmove,
        江源区 = 白山市 | 2 << RegionHelper.Districtlmove,
        抚松县 = 白山市 | 3 << RegionHelper.Districtlmove,
        靖宇县 = 白山市 | 4 << RegionHelper.Districtlmove,
        长白朝鲜族自治县 = 白山市 | 5 << RegionHelper.Districtlmove,
        临江市 = 白山市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        松原市 = 吉林 | 7 << RegionHelper.Citylmove,
        #region 区县
        宁江区 = 松原市 | 1 << RegionHelper.Districtlmove,
        前郭尔罗斯县 = 松原市 | 2 << RegionHelper.Districtlmove,
        长岭县 = 松原市 | 3 << RegionHelper.Districtlmove,
        乾安县 = 松原市 | 4 << RegionHelper.Districtlmove,
        扶余市 = 松原市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县


        白城市 = 吉林 | 8 << RegionHelper.Citylmove,
        #region 区县
        洮北区 = 白城市 | 1 << RegionHelper.Districtlmove,
        镇赉县 = 白城市 | 2 << RegionHelper.Districtlmove,
        通榆县 = 白城市 | 3 << RegionHelper.Districtlmove,
        洮南市 = 白城市 | 4 << RegionHelper.Districtlmove,
        大安市 = 白城市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        延边州 = 吉林 | 9 << RegionHelper.Citylmove,
        #region 区县
        延吉市 = 延边州 | 1 << RegionHelper.Districtlmove,
        图们市 = 延边州 | 2 << RegionHelper.Districtlmove,
        敦化市 = 延边州 | 3 << RegionHelper.Districtlmove,
        珲春市 = 延边州 | 4 << RegionHelper.Districtlmove,
        龙井市 = 延边州 | 5 << RegionHelper.Districtlmove,
        和龙市 = 延边州 | 6 << RegionHelper.Districtlmove,
        汪清县 = 延边州 | 7 << RegionHelper.Districtlmove,
        安图县 = 延边州 | 8 << RegionHelper.Districtlmove,
        #endregion 区县


        #endregion 市

        云南 = 21 << RegionHelper.Provincelmove,//省                
        #region 市
        昆明市 = 云南 | 1 << RegionHelper.Citylmove,
        #region 区县
        五华区 = 昆明市 | 1 << RegionHelper.Districtlmove,
        盘龙区 = 昆明市 | 2 << RegionHelper.Districtlmove,
        官渡区 = 昆明市 | 3 << RegionHelper.Districtlmove,
        西山区 = 昆明市 | 4 << RegionHelper.Districtlmove,
        东川区 = 昆明市 | 5 << RegionHelper.Districtlmove,
        呈贡区 = 昆明市 | 6 << RegionHelper.Districtlmove,
        晋宁县 = 昆明市 | 7 << RegionHelper.Districtlmove,
        富民县 = 昆明市 | 8 << RegionHelper.Districtlmove,
        宜良县 = 昆明市 | 9 << RegionHelper.Districtlmove,
        石林彝族自治县 = 昆明市 | 10 << RegionHelper.Districtlmove,
        嵩明县 = 昆明市 | 11 << RegionHelper.Districtlmove,
        禄劝县 = 昆明市 | 12 << RegionHelper.Districtlmove,
        寻甸县 = 昆明市 | 13 << RegionHelper.Districtlmove,
        安宁市 = 昆明市 | 14 << RegionHelper.Districtlmove,
        #endregion 区县

        曲靖市 = 云南 | 2 << RegionHelper.Citylmove,
        #region 区县
        麒麟区 = 曲靖市 | 1 << RegionHelper.Districtlmove,
        马龙县 = 曲靖市 | 2 << RegionHelper.Districtlmove,
        陆良县 = 曲靖市 | 3 << RegionHelper.Districtlmove,
        师宗县 = 曲靖市 | 4 << RegionHelper.Districtlmove,
        罗平县 = 曲靖市 | 5 << RegionHelper.Districtlmove,
        富源县 = 曲靖市 | 6 << RegionHelper.Districtlmove,
        会泽县 = 曲靖市 | 7 << RegionHelper.Districtlmove,
        沾益县 = 曲靖市 | 8 << RegionHelper.Districtlmove,
        宣威市 = 曲靖市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县
        玉溪市 = 云南 | 3 << RegionHelper.Citylmove,
        #region 区县
        红塔区 = 玉溪市 | 1 << RegionHelper.Districtlmove,
        江川县 = 玉溪市 | 2 << RegionHelper.Districtlmove,
        澄江县 = 玉溪市 | 3 << RegionHelper.Districtlmove,
        通海县 = 玉溪市 | 4 << RegionHelper.Districtlmove,
        华宁县 = 玉溪市 | 5 << RegionHelper.Districtlmove,
        易门县 = 玉溪市 | 6 << RegionHelper.Districtlmove,
        峨山县 = 玉溪市 | 7 << RegionHelper.Districtlmove,
        新平县 = 玉溪市 | 8 << RegionHelper.Districtlmove,
        元江县 = 玉溪市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        保山市 = 云南 | 4 << RegionHelper.Citylmove,
        #region 区县
        隆阳区 = 保山市 | 1 << RegionHelper.Districtlmove,
        施甸县 = 保山市 | 2 << RegionHelper.Districtlmove,
        腾冲县 = 保山市 | 3 << RegionHelper.Districtlmove,
        龙陵县 = 保山市 | 4 << RegionHelper.Districtlmove,
        昌宁县 = 保山市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        昭通市 = 云南 | 5 << RegionHelper.Citylmove,
        #region 区县
        昭阳区 = 昭通市 | 1 << RegionHelper.Districtlmove,
        鲁甸县 = 昭通市 | 2 << RegionHelper.Districtlmove,
        巧家县 = 昭通市 | 3 << RegionHelper.Districtlmove,
        盐津县 = 昭通市 | 4 << RegionHelper.Districtlmove,
        大关县 = 昭通市 | 5 << RegionHelper.Districtlmove,
        永善县 = 昭通市 | 6 << RegionHelper.Districtlmove,
        绥江县 = 昭通市 | 7 << RegionHelper.Districtlmove,
        镇雄县 = 昭通市 | 8 << RegionHelper.Districtlmove,
        彝良县 = 昭通市 | 9 << RegionHelper.Districtlmove,
        威信县 = 昭通市 | 10 << RegionHelper.Districtlmove,
        水富县 = 昭通市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        丽江市 = 云南 | 6 << RegionHelper.Citylmove,
        #region 区县
        古城区 = 丽江市 | 1 << RegionHelper.Districtlmove,
        玉龙县 = 丽江市 | 2 << RegionHelper.Districtlmove,
        永胜县 = 丽江市 | 3 << RegionHelper.Districtlmove,
        华坪县 = 丽江市 | 4 << RegionHelper.Districtlmove,
        宁蒗县 = 丽江市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        普洱市 = 云南 | 7 << RegionHelper.Citylmove,
        #region 区县
        思茅区 = 普洱市 | 1 << RegionHelper.Districtlmove,
        宁洱县 = 普洱市 | 2 << RegionHelper.Districtlmove,
        墨江县 = 普洱市 | 3 << RegionHelper.Districtlmove,
        景东县 = 普洱市 | 4 << RegionHelper.Districtlmove,
        景谷县 = 普洱市 | 5 << RegionHelper.Districtlmove,
        镇沅县 = 普洱市 | 6 << RegionHelper.Districtlmove,
        江城县 = 普洱市 | 7 << RegionHelper.Districtlmove,
        孟连县 = 普洱市 | 8 << RegionHelper.Districtlmove,
        澜沧县 = 普洱市 | 9 << RegionHelper.Districtlmove,
        西盟县 = 普洱市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        临沧市 = 云南 | 8 << RegionHelper.Citylmove,
        #region 区县
        临翔区 = 临沧市 | 1 << RegionHelper.Districtlmove,
        凤庆县 = 临沧市 | 2 << RegionHelper.Districtlmove,
        云县 = 临沧市 | 3 << RegionHelper.Districtlmove,
        永德县 = 临沧市 | 4 << RegionHelper.Districtlmove,
        镇康县 = 临沧市 | 5 << RegionHelper.Districtlmove,
        双江县 = 临沧市 | 6 << RegionHelper.Districtlmove,
        耿马县 = 临沧市 | 7 << RegionHelper.Districtlmove,
        沧源县 = 临沧市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        楚雄州 = 云南 | 9 << RegionHelper.Citylmove,
        #region 区县
        楚雄市 = 楚雄州 | 1 << RegionHelper.Districtlmove,
        双柏县 = 楚雄州 | 2 << RegionHelper.Districtlmove,
        牟定县 = 楚雄州 | 3 << RegionHelper.Districtlmove,
        南华县 = 楚雄州 | 4 << RegionHelper.Districtlmove,
        姚安县 = 楚雄州 | 5 << RegionHelper.Districtlmove,
        大姚县 = 楚雄州 | 6 << RegionHelper.Districtlmove,
        永仁县 = 楚雄州 | 7 << RegionHelper.Districtlmove,
        元谋县 = 楚雄州 | 8 << RegionHelper.Districtlmove,
        武定县 = 楚雄州 | 9 << RegionHelper.Districtlmove,
        禄丰县 = 楚雄州 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        红河州 = 云南 | 10 << RegionHelper.Citylmove,
        #region 区县
        个旧市 = 红河州 | 1 << RegionHelper.Districtlmove,
        开远市 = 红河州 | 2 << RegionHelper.Districtlmove,
        蒙自市 = 红河州 | 3 << RegionHelper.Districtlmove,
        屏边县 = 红河州 | 4 << RegionHelper.Districtlmove,
        建水县 = 红河州 | 5 << RegionHelper.Districtlmove,
        石屏县 = 红河州 | 6 << RegionHelper.Districtlmove,
        弥勒市 = 红河州 | 7 << RegionHelper.Districtlmove,
        泸西县 = 红河州 | 8 << RegionHelper.Districtlmove,
        元阳县 = 红河州 | 9 << RegionHelper.Districtlmove,
        红河县 = 红河州 | 10 << RegionHelper.Districtlmove,
        金平县 = 红河州 | 11 << RegionHelper.Districtlmove,
        绿春县 = 红河州 | 12 << RegionHelper.Districtlmove,
        河口县 = 红河州 | 13 << RegionHelper.Districtlmove,
        #endregion 区县

        文山州 = 云南 | 11 << RegionHelper.Citylmove,
        #region 区县
        文山县 = 文山州 | 1 << RegionHelper.Districtlmove,
        砚山县 = 文山州 | 2 << RegionHelper.Districtlmove,
        西畴县 = 文山州 | 3 << RegionHelper.Districtlmove,
        麻栗坡县 = 文山州 | 4 << RegionHelper.Districtlmove,
        马关县 = 文山州 | 5 << RegionHelper.Districtlmove,
        丘北县 = 文山州 | 6 << RegionHelper.Districtlmove,
        广南县 = 文山州 | 7 << RegionHelper.Districtlmove,
        富宁县 = 文山州 | 8 << RegionHelper.Districtlmove,
        #endregion 区县
        西双版纳州 = 云南 | 12 << RegionHelper.Citylmove,
        #region 区县
        景洪市 = 西双版纳州 | 1 << RegionHelper.Districtlmove,
        勐海县 = 西双版纳州 | 2 << RegionHelper.Districtlmove,
        勐腊县 = 西双版纳州 | 3 << RegionHelper.Districtlmove,
        #endregion 区县

        大理州 = 云南 | 13 << RegionHelper.Citylmove,
        #region 区县
        大理市 = 大理州 | 1 << RegionHelper.Districtlmove,
        漾濞县 = 大理州 | 2 << RegionHelper.Districtlmove,
        祥云县 = 大理州 | 3 << RegionHelper.Districtlmove,
        宾川县 = 大理州 | 4 << RegionHelper.Districtlmove,
        弥渡县 = 大理州 | 5 << RegionHelper.Districtlmove,
        南涧县 = 大理州 | 6 << RegionHelper.Districtlmove,
        巍山县 = 大理州 | 7 << RegionHelper.Districtlmove,
        永平县 = 大理州 | 8 << RegionHelper.Districtlmove,
        云龙县 = 大理州 | 9 << RegionHelper.Districtlmove,
        洱源县 = 大理州 | 10 << RegionHelper.Districtlmove,
        剑川县 = 大理州 | 11 << RegionHelper.Districtlmove,
        鹤庆县 = 大理州 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        德宏州 = 云南 | 14 << RegionHelper.Citylmove,
        #region 区县
        瑞丽市 = 德宏州 | 1 << RegionHelper.Districtlmove,
        芒市 = 德宏州 | 2 << RegionHelper.Districtlmove,
        梁河县 = 德宏州 | 3 << RegionHelper.Districtlmove,
        盈江县 = 德宏州 | 4 << RegionHelper.Districtlmove,
        陇川县 = 德宏州 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        怒江州 = 云南 | 15 << RegionHelper.Citylmove,
        #region 区县
        泸水县 = 怒江州 | 1 << RegionHelper.Districtlmove,
        福贡县 = 怒江州 | 2 << RegionHelper.Districtlmove,
        贡山县 = 怒江州 | 3 << RegionHelper.Districtlmove,
        兰坪县 = 怒江州 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        迪庆州 = 云南 | 16 << RegionHelper.Citylmove,
        #region 区县
        香格里拉县 = 迪庆州 | 1 << RegionHelper.Districtlmove,
        德钦县 = 迪庆州 | 2 << RegionHelper.Districtlmove,
        维西傈僳族自治县 = 迪庆州 | 3 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        新疆 = 22 << RegionHelper.Provincelmove,//省                
        #region 市
        乌鲁木齐市 = 新疆 | 1 << RegionHelper.Citylmove,
        #region 区县
        天山区 = 乌鲁木齐市 | 1 << RegionHelper.Districtlmove,
        沙依巴克区 = 乌鲁木齐市 | 2 << RegionHelper.Districtlmove,
        新市区 = 乌鲁木齐市 | 3 << RegionHelper.Districtlmove,
        水磨沟区 = 乌鲁木齐市 | 4 << RegionHelper.Districtlmove,
        头屯河区 = 乌鲁木齐市 | 5 << RegionHelper.Districtlmove,
        达坂城区 = 乌鲁木齐市 | 6 << RegionHelper.Districtlmove,
        米东区 = 乌鲁木齐市 | 7 << RegionHelper.Districtlmove,
        乌鲁木齐县 = 乌鲁木齐市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        克拉玛依市 = 新疆 | 2 << RegionHelper.Citylmove,
        #region 区县
        独山子区 = 克拉玛依市 | 1 << RegionHelper.Districtlmove,
        克拉玛依区 = 克拉玛依市 | 2 << RegionHelper.Districtlmove,
        白碱滩区 = 克拉玛依市 | 3 << RegionHelper.Districtlmove,
        乌尔禾区 = 克拉玛依市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        吐鲁番 = 新疆 | 3 << RegionHelper.Citylmove,
        #region 区县
        吐鲁番市 = 吐鲁番 | 1 << RegionHelper.Districtlmove,
        鄯善县 = 吐鲁番 | 2 << RegionHelper.Districtlmove,
        托克逊县 = 吐鲁番 | 3 << RegionHelper.Districtlmove,
        #endregion 区县

        哈密 = 新疆 | 4 << RegionHelper.Citylmove,
        #region 区县
        哈密市 = 哈密 | 1 << RegionHelper.Districtlmove,
        巴里坤县 = 哈密 | 2 << RegionHelper.Districtlmove,
        伊吾县 = 哈密 | 3 << RegionHelper.Districtlmove,
        #endregion 区县

        昌吉州 = 新疆 | 5 << RegionHelper.Citylmove,
        #region 区县
        昌吉市 = 昌吉州 | 1 << RegionHelper.Districtlmove,
        阜康市 = 昌吉州 | 2 << RegionHelper.Districtlmove,
        呼图壁县 = 昌吉州 | 3 << RegionHelper.Districtlmove,
        玛纳斯县 = 昌吉州 | 4 << RegionHelper.Districtlmove,
        奇台县 = 昌吉州 | 5 << RegionHelper.Districtlmove,
        吉木萨尔县 = 昌吉州 | 6 << RegionHelper.Districtlmove,
        木垒哈萨克自治县 = 昌吉州 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        博尔塔拉州 = 新疆 | 6 << RegionHelper.Citylmove,
        #region 区县
        博乐市 = 博尔塔拉州 | 1 << RegionHelper.Districtlmove,
        精河县 = 博尔塔拉州 | 2 << RegionHelper.Districtlmove,
        温泉县 = 博尔塔拉州 | 3 << RegionHelper.Districtlmove,
        #endregion 区县

        巴音郭楞州 = 新疆 | 7 << RegionHelper.Citylmove,
        #region 区县
        库尔勒市 = 巴音郭楞州 | 1 << RegionHelper.Districtlmove,
        轮台县 = 巴音郭楞州 | 2 << RegionHelper.Districtlmove,
        尉犁县 = 巴音郭楞州 | 3 << RegionHelper.Districtlmove,
        若羌县 = 巴音郭楞州 | 4 << RegionHelper.Districtlmove,
        且末县 = 巴音郭楞州 | 5 << RegionHelper.Districtlmove,
        焉耆回族自治县 = 巴音郭楞州 | 6 << RegionHelper.Districtlmove,
        和静县 = 巴音郭楞州 | 7 << RegionHelper.Districtlmove,
        和硕县 = 巴音郭楞州 | 8 << RegionHelper.Districtlmove,
        博湖县 = 巴音郭楞州 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        阿克苏 = 新疆 | 8 << RegionHelper.Citylmove,
        #region 区县
        阿克苏市 = 阿克苏 | 1 << RegionHelper.Districtlmove,
        温宿县 = 阿克苏 | 2 << RegionHelper.Districtlmove,
        库车县 = 阿克苏 | 3 << RegionHelper.Districtlmove,
        沙雅县 = 阿克苏 | 4 << RegionHelper.Districtlmove,
        新和县 = 阿克苏 | 5 << RegionHelper.Districtlmove,
        拜城县 = 阿克苏 | 6 << RegionHelper.Districtlmove,
        乌什县 = 阿克苏 | 7 << RegionHelper.Districtlmove,
        阿瓦提县 = 阿克苏 | 8 << RegionHelper.Districtlmove,
        柯坪县 = 阿克苏 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        克孜勒苏州 = 新疆 | 9 << RegionHelper.Citylmove,
        #region 区县
        阿图什市 = 克孜勒苏州 | 1 << RegionHelper.Districtlmove,
        阿克陶 = 克孜勒苏州 | 2 << RegionHelper.Districtlmove,
        阿合奇县 = 克孜勒苏州 | 3 << RegionHelper.Districtlmove,
        乌恰县 = 克孜勒苏州 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        喀什 = 新疆 | 10 << RegionHelper.Citylmove,
        #region 区县
        喀什市 = 喀什 | 1 << RegionHelper.Districtlmove,
        疏附县 = 喀什 | 2 << RegionHelper.Districtlmove,
        疏勒县 = 喀什 | 3 << RegionHelper.Districtlmove,
        英吉沙县 = 喀什 | 4 << RegionHelper.Districtlmove,
        泽普县 = 喀什 | 5 << RegionHelper.Districtlmove,
        莎车县 = 喀什 | 6 << RegionHelper.Districtlmove,
        叶城县 = 喀什 | 7 << RegionHelper.Districtlmove,
        麦盖提县 = 喀什 | 8 << RegionHelper.Districtlmove,
        岳普湖县 = 喀什 | 9 << RegionHelper.Districtlmove,
        伽师县 = 喀什 | 10 << RegionHelper.Districtlmove,
        巴楚县 = 喀什 | 11 << RegionHelper.Districtlmove,
        塔什库尔干塔吉克县 = 喀什 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        和田 = 新疆 | 11 << RegionHelper.Citylmove,
        #region 区县
        和田市 = 和田 | 1 << RegionHelper.Districtlmove,
        和田县 = 和田 | 2 << RegionHelper.Districtlmove,
        墨玉县 = 和田 | 3 << RegionHelper.Districtlmove,
        皮山县 = 和田 | 4 << RegionHelper.Districtlmove,
        洛浦县 = 和田 | 5 << RegionHelper.Districtlmove,
        策勒县 = 和田 | 6 << RegionHelper.Districtlmove,
        于田县 = 和田 | 7 << RegionHelper.Districtlmove,
        民丰县 = 和田 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        伊犁州 = 新疆 | 12 << RegionHelper.Citylmove,
        #region 区县
        伊宁市 = 伊犁州 | 1 << RegionHelper.Districtlmove,
        奎屯市 = 伊犁州 | 2 << RegionHelper.Districtlmove,
        伊宁县 = 伊犁州 | 3 << RegionHelper.Districtlmove,
        察布县 = 伊犁州 | 4 << RegionHelper.Districtlmove,
        霍城县 = 伊犁州 | 5 << RegionHelper.Districtlmove,
        巩留县 = 伊犁州 | 6 << RegionHelper.Districtlmove,
        新源县 = 伊犁州 | 7 << RegionHelper.Districtlmove,
        昭苏县 = 伊犁州 | 8 << RegionHelper.Districtlmove,
        特克斯县 = 伊犁州 | 9 << RegionHelper.Districtlmove,
        尼勒克县 = 伊犁州 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        塔城 = 新疆 | 13 << RegionHelper.Citylmove,
        #region 区县
        塔城市 = 塔城 | 1 << RegionHelper.Districtlmove,
        乌苏市 = 塔城 | 2 << RegionHelper.Districtlmove,
        额敏县 = 塔城 | 3 << RegionHelper.Districtlmove,
        沙湾县 = 塔城 | 4 << RegionHelper.Districtlmove,
        托里县 = 塔城 | 5 << RegionHelper.Districtlmove,
        裕民县 = 塔城 | 6 << RegionHelper.Districtlmove,
        和布克赛尔县 = 塔城 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        阿勒泰 = 新疆 | 14 << RegionHelper.Citylmove,
        #region 区县
        阿勒泰市 = 阿勒泰 | 1 << RegionHelper.Districtlmove,
        布尔津县 = 阿勒泰 | 2 << RegionHelper.Districtlmove,
        富蕴县 = 阿勒泰 | 3 << RegionHelper.Districtlmove,
        福海县 = 阿勒泰 | 4 << RegionHelper.Districtlmove,
        哈巴河县 = 阿勒泰 | 5 << RegionHelper.Districtlmove,
        青河县 = 阿勒泰 | 6 << RegionHelper.Districtlmove,
        吉木乃县 = 阿勒泰 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        石河子市 = 新疆 | 15 << RegionHelper.Citylmove,
        #region 区县
        新城街道 = 石河子市 | 1 << RegionHelper.Districtlmove,
        向阳街道 = 石河子市 | 2 << RegionHelper.Districtlmove,
        红山街道 = 石河子市 | 3 << RegionHelper.Districtlmove,
        老街街道 = 石河子市 | 4 << RegionHelper.Districtlmove,
        东城街道 = 石河子市 | 5 << RegionHelper.Districtlmove,
        北泉镇 = 石河子市 | 6 << RegionHelper.Districtlmove,
        石河子镇 = 石河子市 | 7 << RegionHelper.Districtlmove,
        兵团一五二团 = 石河子市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        阿拉尔市 = 新疆 | 16 << RegionHelper.Citylmove,
        #region 区县
        金银川路街道 = 阿拉尔市 | 1 << RegionHelper.Districtlmove,
        幸福路街道 = 阿拉尔市 | 2 << RegionHelper.Districtlmove,
        青松路街道 = 阿拉尔市 | 3 << RegionHelper.Districtlmove,
        南口街道 = 阿拉尔市 | 4 << RegionHelper.Districtlmove,
        托喀依乡 = 阿拉尔市 | 5 << RegionHelper.Districtlmove,
        兵团七团 = 阿拉尔市 | 6 << RegionHelper.Districtlmove,
        兵团八团 = 阿拉尔市 | 7 << RegionHelper.Districtlmove,
        兵团十团 = 阿拉尔市 | 8 << RegionHelper.Districtlmove,
        兵团十一团 = 阿拉尔市 | 9 << RegionHelper.Districtlmove,
        兵团十二团 = 阿拉尔市 | 10 << RegionHelper.Districtlmove,
        兵团十三团 = 阿拉尔市 | 11 << RegionHelper.Districtlmove,
        兵团十四团 = 阿拉尔市 | 12 << RegionHelper.Districtlmove,
        兵团十六团 = 阿拉尔市 | 13 << RegionHelper.Districtlmove,
        兵团农一师水利水电工程处 = 阿拉尔市 | 14 << RegionHelper.Districtlmove,
        兵团农一师塔里木灌溉水利管理处 = 阿拉尔市 | 15 << RegionHelper.Districtlmove,
        阿拉尔农场 = 阿拉尔市 | 16 << RegionHelper.Districtlmove,
        #endregion 区县

        图木舒克市 = 新疆 | 17 << RegionHelper.Citylmove,
        #region 区县
        齐干却勒街道 = 图木舒克市 | 1 << RegionHelper.Districtlmove,
        前海街道 = 图木舒克市 | 2 << RegionHelper.Districtlmove,
        永安坝街道 = 图木舒克市 | 3 << RegionHelper.Districtlmove,
        兵团四十四团 = 图木舒克市 | 4 << RegionHelper.Districtlmove,
        兵团四十九团 = 图木舒克市 | 5 << RegionHelper.Districtlmove,
        兵团五十团 = 图木舒克市 | 6 << RegionHelper.Districtlmove,
        兵团五十一团 = 图木舒克市 | 7 << RegionHelper.Districtlmove,
        兵团五十三团 = 图木舒克市 | 8 << RegionHelper.Districtlmove,
        喀拉拜勒镇 = 图木舒克市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        五家渠市 = 新疆 | 18 << RegionHelper.Citylmove,
        #region 区县
        军垦路街道 = 五家渠市 | 1 << RegionHelper.Districtlmove,
        青湖路街道 = 五家渠市 | 2 << RegionHelper.Districtlmove,
        人民路街道 = 五家渠市 | 3 << RegionHelper.Districtlmove,
        兵团一零一团 = 五家渠市 | 4 << RegionHelper.Districtlmove,
        兵团一零二团 = 五家渠市 | 5 << RegionHelper.Districtlmove,
        兵团一零三团 = 五家渠市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        贵州 = 23 << RegionHelper.Provincelmove,//省                
        #region 市
        贵阳市 = 贵州 | 1 << RegionHelper.Citylmove,
        #region 区县
        南明区 = 贵阳市 | 1 << RegionHelper.Districtlmove,
        云岩区 = 贵阳市 | 2 << RegionHelper.Districtlmove,
        花溪区 = 贵阳市 | 3 << RegionHelper.Districtlmove,
        乌当区 = 贵阳市 | 4 << RegionHelper.Districtlmove,
        贵阳白云区 = 贵阳市 | 5 << RegionHelper.Districtlmove,
        观山湖区 = 贵阳市 | 6 << RegionHelper.Districtlmove,
        开阳县 = 贵阳市 | 7 << RegionHelper.Districtlmove,
        息烽县 = 贵阳市 | 8 << RegionHelper.Districtlmove,
        修文县 = 贵阳市 | 9 << RegionHelper.Districtlmove,
        清镇市 = 贵阳市 | 10 << RegionHelper.Districtlmove,
        小河区 = 贵阳市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        六盘水市 = 贵州 | 2 << RegionHelper.Citylmove,
        #region 区县
        钟山区 = 六盘水市 | 1 << RegionHelper.Districtlmove,
        六枝特区 = 六盘水市 | 2 << RegionHelper.Districtlmove,
        水城县 = 六盘水市 | 3 << RegionHelper.Districtlmove,
        盘县 = 六盘水市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        遵义市 = 贵州 | 3 << RegionHelper.Citylmove,
        #region 区县
        红花岗区 = 遵义市 | 1 << RegionHelper.Districtlmove,
        汇川区 = 遵义市 | 2 << RegionHelper.Districtlmove,
        遵义县 = 遵义市 | 3 << RegionHelper.Districtlmove,
        桐梓县 = 遵义市 | 4 << RegionHelper.Districtlmove,
        绥阳县 = 遵义市 | 5 << RegionHelper.Districtlmove,
        正安县 = 遵义市 | 6 << RegionHelper.Districtlmove,
        道真县 = 遵义市 | 7 << RegionHelper.Districtlmove,
        务川县 = 遵义市 | 8 << RegionHelper.Districtlmove,
        凤冈县 = 遵义市 | 9 << RegionHelper.Districtlmove,
        湄潭县 = 遵义市 | 10 << RegionHelper.Districtlmove,
        余庆县 = 遵义市 | 11 << RegionHelper.Districtlmove,
        习水县 = 遵义市 | 12 << RegionHelper.Districtlmove,
        赤水市 = 遵义市 | 13 << RegionHelper.Districtlmove,
        仁怀市 = 遵义市 | 14 << RegionHelper.Districtlmove,
        #endregion 区县

        安顺市 = 贵州 | 4 << RegionHelper.Citylmove,
        #region 区县
        西秀区 = 安顺市 | 1 << RegionHelper.Districtlmove,
        平坝县 = 安顺市 | 2 << RegionHelper.Districtlmove,
        普定县 = 安顺市 | 3 << RegionHelper.Districtlmove,
        镇宁县 = 安顺市 | 4 << RegionHelper.Districtlmove,
        关岭县 = 安顺市 | 5 << RegionHelper.Districtlmove,
        紫云县 = 安顺市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        毕节市 = 贵州 | 5 << RegionHelper.Citylmove,
        #region 区县
        七星关区 = 毕节市 | 1 << RegionHelper.Districtlmove,
        大方县 = 毕节市 | 2 << RegionHelper.Districtlmove,
        黔西县 = 毕节市 | 3 << RegionHelper.Districtlmove,
        金沙县 = 毕节市 | 4 << RegionHelper.Districtlmove,
        织金县 = 毕节市 | 5 << RegionHelper.Districtlmove,
        纳雍县 = 毕节市 | 6 << RegionHelper.Districtlmove,
        威宁县 = 毕节市 | 7 << RegionHelper.Districtlmove,
        赫章县 = 毕节市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        铜仁市 = 贵州 | 6 << RegionHelper.Citylmove,
        #region 区县
        碧江区 = 铜仁市 | 1 << RegionHelper.Districtlmove,
        万山区 = 铜仁市 | 2 << RegionHelper.Districtlmove,
        江口县 = 铜仁市 | 3 << RegionHelper.Districtlmove,
        玉屏县 = 铜仁市 | 4 << RegionHelper.Districtlmove,
        石阡县 = 铜仁市 | 5 << RegionHelper.Districtlmove,
        思南县 = 铜仁市 | 6 << RegionHelper.Districtlmove,
        印江县 = 铜仁市 | 7 << RegionHelper.Districtlmove,
        德江县 = 铜仁市 | 8 << RegionHelper.Districtlmove,
        沿河县 = 铜仁市 | 9 << RegionHelper.Districtlmove,
        松桃县 = 铜仁市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        黔西南州 = 贵州 | 7 << RegionHelper.Citylmove,
        #region 区县
        兴义市 = 黔西南州 | 1 << RegionHelper.Districtlmove,
        兴仁县 = 黔西南州 | 2 << RegionHelper.Districtlmove,
        普安县 = 黔西南州 | 3 << RegionHelper.Districtlmove,
        晴隆县 = 黔西南州 | 4 << RegionHelper.Districtlmove,
        贞丰县 = 黔西南州 | 5 << RegionHelper.Districtlmove,
        望谟县 = 黔西南州 | 6 << RegionHelper.Districtlmove,
        册亨县 = 黔西南州 | 7 << RegionHelper.Districtlmove,
        安龙县 = 黔西南州 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        黔东南州 = 贵州 | 8 << RegionHelper.Citylmove,
        #region 区县
        镇远县 = 黔东南州 | 1 << RegionHelper.Districtlmove,
        凯里市 = 黔东南州 | 2 << RegionHelper.Districtlmove,
        黄平县 = 黔东南州 | 3 << RegionHelper.Districtlmove,
        施秉县 = 黔东南州 | 4 << RegionHelper.Districtlmove,
        三穗县 = 黔东南州 | 5 << RegionHelper.Districtlmove,
        岑巩县 = 黔东南州 | 6 << RegionHelper.Districtlmove,
        天柱县 = 黔东南州 | 7 << RegionHelper.Districtlmove,
        锦屏县 = 黔东南州 | 8 << RegionHelper.Districtlmove,
        剑河县 = 黔东南州 | 9 << RegionHelper.Districtlmove,
        台江县 = 黔东南州 | 10 << RegionHelper.Districtlmove,
        黎平县 = 黔东南州 | 11 << RegionHelper.Districtlmove,
        榕江县 = 黔东南州 | 12 << RegionHelper.Districtlmove,
        从江县 = 黔东南州 | 13 << RegionHelper.Districtlmove,
        雷山县 = 黔东南州 | 14 << RegionHelper.Districtlmove,
        麻江县 = 黔东南州 | 15 << RegionHelper.Districtlmove,
        丹寨县 = 黔东南州 | 16 << RegionHelper.Districtlmove,
        #endregion 区县

        黔南州 = 贵州 | 9 << RegionHelper.Citylmove,
        #region 区县
        都匀市 = 黔南州 | 1 << RegionHelper.Districtlmove,
        福泉市 = 黔南州 | 2 << RegionHelper.Districtlmove,
        荔波县 = 黔南州 | 3 << RegionHelper.Districtlmove,
        贵定县 = 黔南州 | 4 << RegionHelper.Districtlmove,
        瓮安县 = 黔南州 | 5 << RegionHelper.Districtlmove,
        独山县 = 黔南州 | 6 << RegionHelper.Districtlmove,
        平塘县 = 黔南州 | 7 << RegionHelper.Districtlmove,
        罗甸县 = 黔南州 | 8 << RegionHelper.Districtlmove,
        长顺县 = 黔南州 | 9 << RegionHelper.Districtlmove,
        龙里县 = 黔南州 | 10 << RegionHelper.Districtlmove,
        惠水县 = 黔南州 | 11 << RegionHelper.Districtlmove,
        三都水族自治县 = 黔南州 | 12 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        甘肃 = 24 << RegionHelper.Provincelmove,//省                
        #region 市
        兰州市 = 甘肃 | 1 << RegionHelper.Citylmove,
        #region 区县
        城关区 = 兰州市 | 1 << RegionHelper.Districtlmove,
        七里河区 = 兰州市 | 2 << RegionHelper.Districtlmove,
        西固区 = 兰州市 | 3 << RegionHelper.Districtlmove,
        安宁区 = 兰州市 | 4 << RegionHelper.Districtlmove,
        红古区 = 兰州市 | 5 << RegionHelper.Districtlmove,
        永登县 = 兰州市 | 6 << RegionHelper.Districtlmove,
        皋兰县 = 兰州市 | 7 << RegionHelper.Districtlmove,
        榆中县 = 兰州市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        嘉峪关市 = 甘肃 | 2 << RegionHelper.Citylmove,
        #region 区县
        雄关区 = 嘉峪关市 | 1 << RegionHelper.Districtlmove,
        长城区 = 嘉峪关市 | 2 << RegionHelper.Districtlmove,
        镜铁区 = 嘉峪关市 | 3 << RegionHelper.Districtlmove,
        #endregion 区县

        金昌市 = 甘肃 | 3 << RegionHelper.Citylmove,
        #region 区县
        金川区 = 金昌市 | 1 << RegionHelper.Districtlmove,
        永昌县 = 金昌市 | 2 << RegionHelper.Districtlmove,
        #endregion 区县

        白银市 = 甘肃 | 4 << RegionHelper.Citylmove,
        #region 区县
        白银区 = 白银市 | 1 << RegionHelper.Districtlmove,
        平川区 = 白银市 | 2 << RegionHelper.Districtlmove,
        靖远县 = 白银市 | 3 << RegionHelper.Districtlmove,
        会宁县 = 白银市 | 4 << RegionHelper.Districtlmove,
        景泰县 = 白银市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县


        天水市 = 甘肃 | 5 << RegionHelper.Citylmove,
        #region 区县
        秦州区 = 天水市 | 1 << RegionHelper.Districtlmove,
        麦积区 = 天水市 | 2 << RegionHelper.Districtlmove,
        清水县 = 天水市 | 3 << RegionHelper.Districtlmove,
        秦安县 = 天水市 | 4 << RegionHelper.Districtlmove,
        甘谷县 = 天水市 | 5 << RegionHelper.Districtlmove,
        武山县 = 天水市 | 6 << RegionHelper.Districtlmove,
        张家川回族自治县 = 天水市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        武威市 = 甘肃 | 6 << RegionHelper.Citylmove,
        #region 区县
        凉州区 = 武威市 | 1 << RegionHelper.Districtlmove,
        民勤县 = 武威市 | 2 << RegionHelper.Districtlmove,
        古浪县 = 武威市 | 3 << RegionHelper.Districtlmove,
        天祝藏族自治县 = 武威市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        张掖市 = 甘肃 | 7 << RegionHelper.Citylmove,
        #region 区县
        甘州区 = 张掖市 | 1 << RegionHelper.Districtlmove,
        肃南裕固族自治县 = 张掖市 | 2 << RegionHelper.Districtlmove,
        民乐县 = 张掖市 | 3 << RegionHelper.Districtlmove,
        临泽县 = 张掖市 | 4 << RegionHelper.Districtlmove,
        高台县 = 张掖市 | 5 << RegionHelper.Districtlmove,
        山丹县 = 张掖市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        平凉市 = 甘肃 | 8 << RegionHelper.Citylmove,
        #region 区县
        崆峒区 = 平凉市 | 1 << RegionHelper.Districtlmove,
        泾川县 = 平凉市 | 2 << RegionHelper.Districtlmove,
        灵台县 = 平凉市 | 3 << RegionHelper.Districtlmove,
        崇信县 = 平凉市 | 4 << RegionHelper.Districtlmove,
        华亭县 = 平凉市 | 5 << RegionHelper.Districtlmove,
        庄浪县 = 平凉市 | 6 << RegionHelper.Districtlmove,
        静宁县 = 平凉市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县


        酒泉市 = 甘肃 | 9 << RegionHelper.Citylmove,
        #region 区县
        肃州区 = 酒泉市 | 1 << RegionHelper.Districtlmove,
        金塔县 = 酒泉市 | 2 << RegionHelper.Districtlmove,
        瓜州县 = 酒泉市 | 3 << RegionHelper.Districtlmove,
        肃北蒙古族自治县 = 酒泉市 | 4 << RegionHelper.Districtlmove,
        阿克塞县 = 酒泉市 | 5 << RegionHelper.Districtlmove,
        玉门市 = 酒泉市 | 6 << RegionHelper.Districtlmove,
        敦煌市 = 酒泉市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县


        庆阳市 = 甘肃 | 10 << RegionHelper.Citylmove,
        #region 区县
        西峰区 = 庆阳市 | 1 << RegionHelper.Districtlmove,
        庆城县 = 庆阳市 | 2 << RegionHelper.Districtlmove,
        环县 = 庆阳市 | 3 << RegionHelper.Districtlmove,
        华池县 = 庆阳市 | 4 << RegionHelper.Districtlmove,
        合水县 = 庆阳市 | 5 << RegionHelper.Districtlmove,
        正宁县 = 庆阳市 | 6 << RegionHelper.Districtlmove,
        宁县 = 庆阳市 | 7 << RegionHelper.Districtlmove,
        镇原县 = 庆阳市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        定西市 = 甘肃 | 11 << RegionHelper.Citylmove,
        #region 区县
        安定区 = 定西市 | 1 << RegionHelper.Districtlmove,
        通渭县 = 定西市 | 2 << RegionHelper.Districtlmove,
        陇西县 = 定西市 | 3 << RegionHelper.Districtlmove,
        渭源县 = 定西市 | 4 << RegionHelper.Districtlmove,
        临洮县 = 定西市 | 5 << RegionHelper.Districtlmove,
        漳县 = 定西市 | 6 << RegionHelper.Districtlmove,
        岷县 = 定西市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        陇南市 = 甘肃 | 12 << RegionHelper.Citylmove,
        #region 区县
        武都区 = 陇南市 | 1 << RegionHelper.Districtlmove,
        成县 = 陇南市 | 2 << RegionHelper.Districtlmove,
        文县 = 陇南市 | 3 << RegionHelper.Districtlmove,
        宕昌县 = 陇南市 | 4 << RegionHelper.Districtlmove,
        康县 = 陇南市 | 5 << RegionHelper.Districtlmove,
        西和县 = 陇南市 | 6 << RegionHelper.Districtlmove,
        礼县 = 陇南市 | 7 << RegionHelper.Districtlmove,
        徽县 = 陇南市 | 8 << RegionHelper.Districtlmove,
        两当县 = 陇南市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        临夏州 = 甘肃 | 13 << RegionHelper.Citylmove,
        #region 区县
        临夏市 = 临夏州 | 1 << RegionHelper.Districtlmove,
        临夏县 = 临夏州 | 2 << RegionHelper.Districtlmove,
        康乐县 = 临夏州 | 3 << RegionHelper.Districtlmove,
        永靖县 = 临夏州 | 4 << RegionHelper.Districtlmove,
        广河县 = 临夏州 | 5 << RegionHelper.Districtlmove,
        和政县 = 临夏州 | 6 << RegionHelper.Districtlmove,
        东乡族自治县 = 临夏州 | 7 << RegionHelper.Districtlmove,
        积石山县 = 临夏州 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        甘南州 = 甘肃 | 14 << RegionHelper.Citylmove,
        #region 区县
        合作市 = 甘南州 | 1 << RegionHelper.Districtlmove,
        临潭县 = 甘南州 | 2 << RegionHelper.Districtlmove,
        卓尼县 = 甘南州 | 3 << RegionHelper.Districtlmove,
        舟曲县 = 甘南州 | 4 << RegionHelper.Districtlmove,
        迭部县 = 甘南州 | 5 << RegionHelper.Districtlmove,
        玛曲县 = 甘南州 | 6 << RegionHelper.Districtlmove,
        碌曲县 = 甘南州 | 7 << RegionHelper.Districtlmove,
        夏河县 = 甘南州 | 8 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        海南 = 25 << RegionHelper.Provincelmove,//省                
        #region 市
        海口市 = 海南 | 1 << RegionHelper.Citylmove,
        #region 区县
        秀英区 = 海口市 | 1 << RegionHelper.Districtlmove,
        龙华区 = 海口市 | 2 << RegionHelper.Districtlmove,
        琼山区 = 海口市 | 3 << RegionHelper.Districtlmove,
        美兰区 = 海口市 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        三亚市 = 海南 | 2 << RegionHelper.Citylmove,
        #region 区县
        海棠湾镇 = 三亚市 | 1 << RegionHelper.Districtlmove,
        吉阳镇 = 三亚市 | 2 << RegionHelper.Districtlmove,
        凤凰镇 = 三亚市 | 3 << RegionHelper.Districtlmove,
        崖城镇 = 三亚市 | 4 << RegionHelper.Districtlmove,
        天涯镇 = 三亚市 | 5 << RegionHelper.Districtlmove,
        育才镇 = 三亚市 | 6 << RegionHelper.Districtlmove,
        河西区街道 = 三亚市 | 7 << RegionHelper.Districtlmove,
        河东区街道 = 三亚市 | 8 << RegionHelper.Districtlmove,
        国营农场 = 三亚市 | 9 << RegionHelper.Districtlmove,
        #endregion 区县


        五指山市 = 海南 | 3 << RegionHelper.Citylmove,
        #region 区县
        冲山镇 = 五指山市 | 1 << RegionHelper.Districtlmove,
        南圣镇 = 五指山市 | 2 << RegionHelper.Districtlmove,
        毛阳镇 = 五指山市 | 3 << RegionHelper.Districtlmove,
        番阳镇 = 五指山市 | 4 << RegionHelper.Districtlmove,
        畅好乡 = 五指山市 | 5 << RegionHelper.Districtlmove,
        毛道乡 = 五指山市 | 6 << RegionHelper.Districtlmove,
        水满乡 = 五指山市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县


        琼海市 = 海南 | 4 << RegionHelper.Citylmove,
        #region 区县
        嘉积镇 = 琼海市 | 1 << RegionHelper.Districtlmove,
        万泉镇 = 琼海市 | 2 << RegionHelper.Districtlmove,
        大路镇 = 琼海市 | 3 << RegionHelper.Districtlmove,
        石壁镇 = 琼海市 | 4 << RegionHelper.Districtlmove,
        中原镇 = 琼海市 | 5 << RegionHelper.Districtlmove,
        博鳌镇 = 琼海市 | 6 << RegionHelper.Districtlmove,
        阳江镇 = 琼海市 | 7 << RegionHelper.Districtlmove,
        龙江镇 = 琼海市 | 8 << RegionHelper.Districtlmove,
        潭门镇 = 琼海市 | 9 << RegionHelper.Districtlmove,
        塔洋镇 = 琼海市 | 10 << RegionHelper.Districtlmove,
        长坡镇 = 琼海市 | 11 << RegionHelper.Districtlmove,
        会山镇 = 琼海市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        儋州市 = 海南 | 5 << RegionHelper.Citylmove,
        #region 区县
        那大镇 = 儋州市 | 1 << RegionHelper.Districtlmove,
        和庆镇 = 儋州市 | 2 << RegionHelper.Districtlmove,
        南丰镇 = 儋州市 | 3 << RegionHelper.Districtlmove,
        大成镇 = 儋州市 | 4 << RegionHelper.Districtlmove,
        雅星镇 = 儋州市 | 5 << RegionHelper.Districtlmove,
        兰洋镇 = 儋州市 | 6 << RegionHelper.Districtlmove,
        光村镇 = 儋州市 | 7 << RegionHelper.Districtlmove,
        木棠镇 = 儋州市 | 8 << RegionHelper.Districtlmove,
        海头镇 = 儋州市 | 9 << RegionHelper.Districtlmove,
        峨蔓镇 = 儋州市 | 10 << RegionHelper.Districtlmove,
        三都镇 = 儋州市 | 11 << RegionHelper.Districtlmove,
        王五镇 = 儋州市 | 12 << RegionHelper.Districtlmove,
        白马井镇 = 儋州市 | 13 << RegionHelper.Districtlmove,
        中和镇 = 儋州市 | 14 << RegionHelper.Districtlmove,
        排浦镇 = 儋州市 | 15 << RegionHelper.Districtlmove,
        东成镇 = 儋州市 | 16 << RegionHelper.Districtlmove,
        新州镇 = 儋州市 | 17 << RegionHelper.Districtlmove,
        #endregion 区县

        文昌市 = 海南 | 6 << RegionHelper.Citylmove,
        #region 区县
        文城镇 = 文昌市 | 1 << RegionHelper.Districtlmove,
        重兴镇 = 文昌市 | 2 << RegionHelper.Districtlmove,
        蓬莱镇 = 文昌市 | 3 << RegionHelper.Districtlmove,
        会文镇 = 文昌市 | 4 << RegionHelper.Districtlmove,
        东路镇 = 文昌市 | 5 << RegionHelper.Districtlmove,
        潭牛镇 = 文昌市 | 6 << RegionHelper.Districtlmove,
        东阁镇 = 文昌市 | 7 << RegionHelper.Districtlmove,
        文教镇 = 文昌市 | 8 << RegionHelper.Districtlmove,
        东郊镇 = 文昌市 | 9 << RegionHelper.Districtlmove,
        龙楼镇 = 文昌市 | 10 << RegionHelper.Districtlmove,
        昌洒镇 = 文昌市 | 11 << RegionHelper.Districtlmove,
        翁田镇 = 文昌市 | 12 << RegionHelper.Districtlmove,
        抱罗镇 = 文昌市 | 13 << RegionHelper.Districtlmove,
        冯坡镇 = 文昌市 | 14 << RegionHelper.Districtlmove,
        锦山镇 = 文昌市 | 15 << RegionHelper.Districtlmove,
        铺前镇 = 文昌市 | 16 << RegionHelper.Districtlmove,
        公坡镇 = 文昌市 | 17 << RegionHelper.Districtlmove,
        #endregion 区县

        万宁市 = 海南 | 7 << RegionHelper.Citylmove,
        #region 区县
        万城镇 = 万宁市 | 1 << RegionHelper.Districtlmove,
        龙滚镇 = 万宁市 | 2 << RegionHelper.Districtlmove,
        和乐镇 = 万宁市 | 3 << RegionHelper.Districtlmove,
        后安镇 = 万宁市 | 4 << RegionHelper.Districtlmove,
        大茂镇 = 万宁市 | 5 << RegionHelper.Districtlmove,
        东澳镇 = 万宁市 | 6 << RegionHelper.Districtlmove,
        礼纪镇 = 万宁市 | 7 << RegionHelper.Districtlmove,
        长丰镇 = 万宁市 | 8 << RegionHelper.Districtlmove,
        山根镇 = 万宁市 | 9 << RegionHelper.Districtlmove,
        北大镇 = 万宁市 | 10 << RegionHelper.Districtlmove,
        南桥镇 = 万宁市 | 11 << RegionHelper.Districtlmove,
        三更罗镇 = 万宁市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        东方市 = 海南 | 8 << RegionHelper.Citylmove,
        #region 区县
        八所镇 = 东方市 | 1 << RegionHelper.Districtlmove,
        东河镇 = 东方市 | 2 << RegionHelper.Districtlmove,
        大田镇 = 东方市 | 3 << RegionHelper.Districtlmove,
        感城镇 = 东方市 | 4 << RegionHelper.Districtlmove,
        板桥镇 = 东方市 | 5 << RegionHelper.Districtlmove,
        三家镇 = 东方市 | 6 << RegionHelper.Districtlmove,
        四更镇 = 东方市 | 7 << RegionHelper.Districtlmove,
        新龙镇 = 东方市 | 8 << RegionHelper.Districtlmove,
        天安乡 = 东方市 | 9 << RegionHelper.Districtlmove,
        江边乡 = 东方市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        定安县 = 海南 | 9 << RegionHelper.Citylmove,
        #region 区县
        定城镇 = 定安县 | 1 << RegionHelper.Districtlmove,
        新竹镇 = 定安县 | 2 << RegionHelper.Districtlmove,
        龙湖镇 = 定安县 | 3 << RegionHelper.Districtlmove,
        黄竹镇 = 定安县 | 4 << RegionHelper.Districtlmove,
        雷鸣镇 = 定安县 | 5 << RegionHelper.Districtlmove,
        龙门镇 = 定安县 | 6 << RegionHelper.Districtlmove,
        龙河镇 = 定安县 | 7 << RegionHelper.Districtlmove,
        岭口镇 = 定安县 | 8 << RegionHelper.Districtlmove,
        翰林镇 = 定安县 | 9 << RegionHelper.Districtlmove,
        富文镇 = 定安县 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        屯昌县 = 海南 | 10 << RegionHelper.Citylmove,
        #region 区县
        屯城镇 = 屯昌县 | 1 << RegionHelper.Districtlmove,
        新兴镇 = 屯昌县 | 2 << RegionHelper.Districtlmove,
        枫木镇 = 屯昌县 | 3 << RegionHelper.Districtlmove,
        乌坡镇 = 屯昌县 | 4 << RegionHelper.Districtlmove,
        南吕镇 = 屯昌县 | 5 << RegionHelper.Districtlmove,
        坡心镇 = 屯昌县 | 6 << RegionHelper.Districtlmove,
        南坤镇 = 屯昌县 | 7 << RegionHelper.Districtlmove,
        西昌镇 = 屯昌县 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        澄迈县 = 海南 | 11 << RegionHelper.Citylmove,
        #region 区县
        金江镇 = 澄迈县 | 1 << RegionHelper.Districtlmove,
        老城镇 = 澄迈县 | 2 << RegionHelper.Districtlmove,
        瑞溪镇 = 澄迈县 | 3 << RegionHelper.Districtlmove,
        永发镇 = 澄迈县 | 4 << RegionHelper.Districtlmove,
        加乐镇 = 澄迈县 | 5 << RegionHelper.Districtlmove,
        文儒镇 = 澄迈县 | 6 << RegionHelper.Districtlmove,
        中兴镇 = 澄迈县 | 7 << RegionHelper.Districtlmove,
        仁兴镇 = 澄迈县 | 8 << RegionHelper.Districtlmove,
        福山镇 = 澄迈县 | 9 << RegionHelper.Districtlmove,
        桥头镇 = 澄迈县 | 10 << RegionHelper.Districtlmove,
        大丰镇 = 澄迈县 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        临高县 = 海南 | 12 << RegionHelper.Citylmove,
        #region 区县
        临城镇 = 临高县 | 1 << RegionHelper.Districtlmove,
        波莲镇 = 临高县 | 2 << RegionHelper.Districtlmove,
        东英镇 = 临高县 | 3 << RegionHelper.Districtlmove,
        博厚镇 = 临高县 | 4 << RegionHelper.Districtlmove,
        皇桐镇 = 临高县 | 5 << RegionHelper.Districtlmove,
        和舍镇 = 临高县 | 6 << RegionHelper.Districtlmove,
        多文镇 = 临高县 | 7 << RegionHelper.Districtlmove,
        南宝镇 = 临高县 | 8 << RegionHelper.Districtlmove,
        新盈镇 = 临高县 | 9 << RegionHelper.Districtlmove,
        调楼镇 = 临高县 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        白沙县 = 海南 | 13 << RegionHelper.Citylmove,
        #region 区县
        牙叉镇 = 白沙县 | 1 << RegionHelper.Districtlmove,
        七坊镇 = 白沙县 | 2 << RegionHelper.Districtlmove,
        邦溪镇 = 白沙县 | 3 << RegionHelper.Districtlmove,
        打安镇 = 白沙县 | 4 << RegionHelper.Districtlmove,
        细水乡 = 白沙县 | 5 << RegionHelper.Districtlmove,
        元门乡 = 白沙县 | 6 << RegionHelper.Districtlmove,
        南开乡 = 白沙县 | 7 << RegionHelper.Districtlmove,
        阜龙乡 = 白沙县 | 8 << RegionHelper.Districtlmove,
        青松乡 = 白沙县 | 9 << RegionHelper.Districtlmove,
        金波乡 = 白沙县 | 10 << RegionHelper.Districtlmove,
        荣邦乡 = 白沙县 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        昌江县 = 海南 | 14 << RegionHelper.Citylmove,
        #region 区县
        石碌镇 = 昌江县 | 1 << RegionHelper.Districtlmove,
        叉河镇 = 昌江县 | 2 << RegionHelper.Districtlmove,
        十月田镇 = 昌江县 | 3 << RegionHelper.Districtlmove,
        乌烈镇 = 昌江县 | 4 << RegionHelper.Districtlmove,
        昌化镇 = 昌江县 | 5 << RegionHelper.Districtlmove,
        海尾镇 = 昌江县 | 6 << RegionHelper.Districtlmove,
        七叉镇 = 昌江县 | 7 << RegionHelper.Districtlmove,
        王下乡 = 昌江县 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        乐东县 = 海南 | 15 << RegionHelper.Citylmove,
        #region 区县
        抱由镇 = 乐东县 | 1 << RegionHelper.Districtlmove,
        万冲镇 = 乐东县 | 2 << RegionHelper.Districtlmove,
        大安镇 = 乐东县 | 3 << RegionHelper.Districtlmove,
        志仲镇 = 乐东县 | 4 << RegionHelper.Districtlmove,
        千家镇 = 乐东县 | 5 << RegionHelper.Districtlmove,
        九所镇 = 乐东县 | 6 << RegionHelper.Districtlmove,
        利国镇 = 乐东县 | 7 << RegionHelper.Districtlmove,
        黄流镇 = 乐东县 | 8 << RegionHelper.Districtlmove,
        佛罗镇 = 乐东县 | 9 << RegionHelper.Districtlmove,
        尖峰镇 = 乐东县 | 10 << RegionHelper.Districtlmove,
        莺歌海镇 = 乐东县 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        陵水县 = 海南 | 16 << RegionHelper.Citylmove,
        #region 区县
        椰林镇 = 陵水县 | 1 << RegionHelper.Districtlmove,
        光坡镇 = 陵水县 | 2 << RegionHelper.Districtlmove,
        三才镇 = 陵水县 | 3 << RegionHelper.Districtlmove,
        英州镇 = 陵水县 | 4 << RegionHelper.Districtlmove,
        隆广镇 = 陵水县 | 5 << RegionHelper.Districtlmove,
        文罗镇 = 陵水县 | 6 << RegionHelper.Districtlmove,
        本号镇 = 陵水县 | 7 << RegionHelper.Districtlmove,
        新村镇 = 陵水县 | 8 << RegionHelper.Districtlmove,
        黎安镇 = 陵水县 | 9 << RegionHelper.Districtlmove,
        提蒙乡 = 陵水县 | 10 << RegionHelper.Districtlmove,
        群英乡 = 陵水县 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        保亭县 = 海南 | 17 << RegionHelper.Citylmove,
        #region 区县
        保城镇 = 保亭县 | 1 << RegionHelper.Districtlmove,
        什玲镇 = 保亭县 | 2 << RegionHelper.Districtlmove,
        加茂镇 = 保亭县 | 3 << RegionHelper.Districtlmove,
        响水镇 = 保亭县 | 4 << RegionHelper.Districtlmove,
        新政镇 = 保亭县 | 5 << RegionHelper.Districtlmove,
        三道镇 = 保亭县 | 6 << RegionHelper.Districtlmove,
        六弓乡 = 保亭县 | 7 << RegionHelper.Districtlmove,
        南林乡 = 保亭县 | 8 << RegionHelper.Districtlmove,
        毛感乡 = 保亭县 | 9 << RegionHelper.Districtlmove,
        #endregion 区县

        琼中县 = 海南 | 18 << RegionHelper.Citylmove,
        #region 区县
        营根镇 = 琼中县 | 1 << RegionHelper.Districtlmove,
        湾岭镇 = 琼中县 | 2 << RegionHelper.Districtlmove,
        黎母山镇 = 琼中县 | 3 << RegionHelper.Districtlmove,
        和平镇 = 琼中县 | 4 << RegionHelper.Districtlmove,
        长征镇 = 琼中县 | 5 << RegionHelper.Districtlmove,
        红毛镇 = 琼中县 | 6 << RegionHelper.Districtlmove,
        中平镇 = 琼中县 | 7 << RegionHelper.Districtlmove,
        吊罗山乡 = 琼中县 | 8 << RegionHelper.Districtlmove,
        上安乡 = 琼中县 | 9 << RegionHelper.Districtlmove,
        什运乡 = 琼中县 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        三沙市 = 海南 | 19 << RegionHelper.Citylmove,
        #region 区县
        西沙群岛 = 三沙市 | 1 << RegionHelper.Districtlmove,
        南沙群岛 = 三沙市 | 2 << RegionHelper.Districtlmove,
        中沙群岛 = 三沙市 | 3 << RegionHelper.Districtlmove,
        #endregion 区县
        #endregion 市

        宁夏 = 26 << RegionHelper.Provincelmove,//省                
        #region 市
        银川市 = 宁夏 | 1 << RegionHelper.Citylmove,
        #region 区县
        兴庆区 = 银川市 | 1 << RegionHelper.Districtlmove,
        西夏区 = 银川市 | 2 << RegionHelper.Districtlmove,
        金凤区 = 银川市 | 3 << RegionHelper.Districtlmove,
        永宁县 = 银川市 | 4 << RegionHelper.Districtlmove,
        贺兰县 = 银川市 | 5 << RegionHelper.Districtlmove,
        灵武市 = 银川市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        石嘴山市 = 宁夏 | 2 << RegionHelper.Citylmove,
        #region 区县
        大武口区 = 石嘴山市 | 1 << RegionHelper.Districtlmove,
        惠农区 = 石嘴山市 | 2 << RegionHelper.Districtlmove,
        平罗县 = 石嘴山市 | 3 << RegionHelper.Districtlmove,
        #endregion 区县

        吴忠市 = 宁夏 | 3 << RegionHelper.Citylmove,
        #region 区县
        利通区 = 吴忠市 | 1 << RegionHelper.Districtlmove,
        红寺堡区 = 吴忠市 | 2 << RegionHelper.Districtlmove,
        盐池县 = 吴忠市 | 3 << RegionHelper.Districtlmove,
        同心县 = 吴忠市 | 4 << RegionHelper.Districtlmove,
        青铜峡市 = 吴忠市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        固原市 = 宁夏 | 4 << RegionHelper.Citylmove,
        #region 区县
        原州区 = 固原市 | 1 << RegionHelper.Districtlmove,
        西吉县 = 固原市 | 2 << RegionHelper.Districtlmove,
        隆德县 = 固原市 | 3 << RegionHelper.Districtlmove,
        泾源县 = 固原市 | 4 << RegionHelper.Districtlmove,
        彭阳县 = 固原市 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        中卫市 = 宁夏 | 5 << RegionHelper.Citylmove,
        #region 区县
        沙坡头区 = 中卫市 | 1 << RegionHelper.Districtlmove,
        中宁县 = 中卫市 | 2 << RegionHelper.Districtlmove,
        海原县 = 中卫市 | 3 << RegionHelper.Districtlmove,
        #endregion 区县


        #endregion 市

        青海 = 27 << RegionHelper.Provincelmove,//省                
        #region 市
        西宁市 = 青海 | 1 << RegionHelper.Citylmove,
        #region 区县
        城东区 = 西宁市 | 1 << RegionHelper.Districtlmove,
        西宁城中区 = 西宁市 | 2 << RegionHelper.Districtlmove,
        城西区 = 西宁市 | 3 << RegionHelper.Districtlmove,
        城北区 = 西宁市 | 4 << RegionHelper.Districtlmove,
        大通县 = 西宁市 | 5 << RegionHelper.Districtlmove,
        湟中县 = 西宁市 | 6 << RegionHelper.Districtlmove,
        湟源县 = 西宁市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        海东市 = 青海 | 2 << RegionHelper.Citylmove,
        #region 区县
        乐都区 = 海东市 | 1 << RegionHelper.Districtlmove,
        平安县 = 海东市 | 2 << RegionHelper.Districtlmove,
        民和县 = 海东市 | 3 << RegionHelper.Districtlmove,
        互助土族自治县 = 海东市 | 4 << RegionHelper.Districtlmove,
        化隆回族自治县 = 海东市 | 5 << RegionHelper.Districtlmove,
        循化撒拉族自治县 = 海东市 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        海北州 = 青海 | 3 << RegionHelper.Citylmove,
        #region 区县
        门源回族自治县 = 海北州 | 1 << RegionHelper.Districtlmove,
        祁连县 = 海北州 | 2 << RegionHelper.Districtlmove,
        海晏县 = 海北州 | 3 << RegionHelper.Districtlmove,
        刚察县 = 海北州 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        黄南州 = 青海 | 4 << RegionHelper.Citylmove,
        #region 区县
        同仁县 = 黄南州 | 1 << RegionHelper.Districtlmove,
        尖扎县 = 黄南州 | 2 << RegionHelper.Districtlmove,
        泽库县 = 黄南州 | 3 << RegionHelper.Districtlmove,
        河南蒙古族自治县 = 黄南州 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        海南州 = 青海 | 5 << RegionHelper.Citylmove,
        #region 区县
        共和县 = 海南州 | 1 << RegionHelper.Districtlmove,
        同德县 = 海南州 | 2 << RegionHelper.Districtlmove,
        贵德县 = 海南州 | 3 << RegionHelper.Districtlmove,
        兴海县 = 海南州 | 4 << RegionHelper.Districtlmove,
        贵南县 = 海南州 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        果洛州 = 青海 | 6 << RegionHelper.Citylmove,
        #region 区县
        玛沁县 = 果洛州 | 1 << RegionHelper.Districtlmove,
        班玛县 = 果洛州 | 2 << RegionHelper.Districtlmove,
        甘德县 = 果洛州 | 3 << RegionHelper.Districtlmove,
        达日县 = 果洛州 | 4 << RegionHelper.Districtlmove,
        久治县 = 果洛州 | 5 << RegionHelper.Districtlmove,
        玛多县 = 果洛州 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        玉树州 = 青海 | 7 << RegionHelper.Citylmove,
        #region 区县
        玉树市 = 玉树州 | 1 << RegionHelper.Districtlmove,
        杂多县 = 玉树州 | 2 << RegionHelper.Districtlmove,
        称多县 = 玉树州 | 3 << RegionHelper.Districtlmove,
        治多县 = 玉树州 | 4 << RegionHelper.Districtlmove,
        囊谦县 = 玉树州 | 5 << RegionHelper.Districtlmove,
        曲麻莱县 = 玉树州 | 6 << RegionHelper.Districtlmove,
        #endregion 区县

        海西州 = 青海 | 8 << RegionHelper.Citylmove,
        #region 区县
        格尔木市 = 海西州 | 1 << RegionHelper.Districtlmove,
        德令哈市 = 海西州 | 2 << RegionHelper.Districtlmove,
        乌兰县 = 海西州 | 3 << RegionHelper.Districtlmove,
        都兰县 = 海西州 | 4 << RegionHelper.Districtlmove,
        天峻县 = 海西州 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        #endregion 市

        西藏 = 28 << RegionHelper.Provincelmove,//省                
        #region 市
        拉萨市 = 西藏 | 1 << RegionHelper.Citylmove,
        #region 区县
        拉萨城关区 = 拉萨市 | 1 << RegionHelper.Districtlmove,
        林周县 = 拉萨市 | 2 << RegionHelper.Districtlmove,
        当雄县 = 拉萨市 | 3 << RegionHelper.Districtlmove,
        尼木县 = 拉萨市 | 4 << RegionHelper.Districtlmove,
        曲水县 = 拉萨市 | 5 << RegionHelper.Districtlmove,
        堆龙德庆县 = 拉萨市 | 6 << RegionHelper.Districtlmove,
        达孜县 = 拉萨市 | 7 << RegionHelper.Districtlmove,
        墨竹工卡县 = 拉萨市 | 8 << RegionHelper.Districtlmove,
        #endregion 区县

        昌都市 = 西藏 | 2 << RegionHelper.Citylmove,
        #region 区县
        昌都县 = 昌都市 | 1 << RegionHelper.Districtlmove,
        江达县 = 昌都市 | 2 << RegionHelper.Districtlmove,
        贡觉县 = 昌都市 | 3 << RegionHelper.Districtlmove,
        类乌齐县 = 昌都市 | 4 << RegionHelper.Districtlmove,
        丁青县 = 昌都市 | 5 << RegionHelper.Districtlmove,
        察雅县 = 昌都市 | 6 << RegionHelper.Districtlmove,
        八宿县 = 昌都市 | 7 << RegionHelper.Districtlmove,
        左贡县 = 昌都市 | 8 << RegionHelper.Districtlmove,
        芒康县 = 昌都市 | 9 << RegionHelper.Districtlmove,
        洛隆县 = 昌都市 | 10 << RegionHelper.Districtlmove,
        边坝县 = 昌都市 | 11 << RegionHelper.Districtlmove,
        #endregion 区县

        山南市 = 西藏 | 3 << RegionHelper.Citylmove,
        #region 区县
        乃东县 = 山南市 | 1 << RegionHelper.Districtlmove,
        扎囊县 = 山南市 | 2 << RegionHelper.Districtlmove,
        贡嘎县 = 山南市 | 3 << RegionHelper.Districtlmove,
        桑日县 = 山南市 | 4 << RegionHelper.Districtlmove,
        琼结县 = 山南市 | 5 << RegionHelper.Districtlmove,
        曲松县 = 山南市 | 6 << RegionHelper.Districtlmove,
        措美县 = 山南市 | 7 << RegionHelper.Districtlmove,
        洛扎县 = 山南市 | 8 << RegionHelper.Districtlmove,
        加查县 = 山南市 | 9 << RegionHelper.Districtlmove,
        隆子县 = 山南市 | 10 << RegionHelper.Districtlmove,
        错那县 = 山南市 | 11 << RegionHelper.Districtlmove,
        浪卡子县 = 山南市 | 12 << RegionHelper.Districtlmove,
        #endregion 区县

        日喀则市 = 西藏 | 4 << RegionHelper.Citylmove,
        #region 区县
        南木林县 = 日喀则市 | 1 << RegionHelper.Districtlmove,
        江孜县 = 日喀则市 | 2 << RegionHelper.Districtlmove,
        定日县 = 日喀则市 | 3 << RegionHelper.Districtlmove,
        萨迦县 = 日喀则市 | 4 << RegionHelper.Districtlmove,
        拉孜县 = 日喀则市 | 5 << RegionHelper.Districtlmove,
        昂仁县 = 日喀则市 | 6 << RegionHelper.Districtlmove,
        谢通门县 = 日喀则市 | 7 << RegionHelper.Districtlmove,
        白朗县 = 日喀则市 | 8 << RegionHelper.Districtlmove,
        仁布县 = 日喀则市 | 9 << RegionHelper.Districtlmove,
        康马县 = 日喀则市 | 10 << RegionHelper.Districtlmove,
        定结县 = 日喀则市 | 11 << RegionHelper.Districtlmove,
        仲巴县 = 日喀则市 | 12 << RegionHelper.Districtlmove,
        亚东县 = 日喀则市 | 13 << RegionHelper.Districtlmove,
        吉隆县 = 日喀则市 | 14 << RegionHelper.Districtlmove,
        聂拉木县 = 日喀则市 | 15 << RegionHelper.Districtlmove,
        萨嘎县 = 日喀则市 | 16 << RegionHelper.Districtlmove,
        岗巴县 = 日喀则市 | 17 << RegionHelper.Districtlmove,
        #endregion 区县


        那曲市 = 西藏 | 5 << RegionHelper.Citylmove,
        #region 区县
        那曲县 = 那曲市 | 1 << RegionHelper.Districtlmove,
        嘉黎县 = 那曲市 | 2 << RegionHelper.Districtlmove,
        比如县 = 那曲市 | 3 << RegionHelper.Districtlmove,
        聂荣县 = 那曲市 | 4 << RegionHelper.Districtlmove,
        安多县 = 那曲市 | 5 << RegionHelper.Districtlmove,
        申扎县 = 那曲市 | 6 << RegionHelper.Districtlmove,
        索县 = 那曲市 | 7 << RegionHelper.Districtlmove,
        班戈县 = 那曲市 | 8 << RegionHelper.Districtlmove,
        巴青县 = 那曲市 | 9 << RegionHelper.Districtlmove,
        尼玛县 = 那曲市 | 10 << RegionHelper.Districtlmove,
        #endregion 区县

        阿里市 = 西藏 | 6 << RegionHelper.Citylmove,
        #region 区县
        普兰县 = 阿里市 | 1 << RegionHelper.Districtlmove,
        札达县 = 阿里市 | 2 << RegionHelper.Districtlmove,
        噶尔县 = 阿里市 | 3 << RegionHelper.Districtlmove,
        日土县 = 阿里市 | 4 << RegionHelper.Districtlmove,
        革吉县 = 阿里市 | 5 << RegionHelper.Districtlmove,
        改则县 = 阿里市 | 6 << RegionHelper.Districtlmove,
        措勤县 = 阿里市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        林芝市 = 西藏 | 7 << RegionHelper.Citylmove,
        #region 区县
        林芝县 = 林芝市 | 1 << RegionHelper.Districtlmove,
        工布江达县 = 林芝市 | 2 << RegionHelper.Districtlmove,
        米林县 = 林芝市 | 3 << RegionHelper.Districtlmove,
        墨脱县 = 林芝市 | 4 << RegionHelper.Districtlmove,
        波密县 = 林芝市 | 5 << RegionHelper.Districtlmove,
        察隅县 = 林芝市 | 6 << RegionHelper.Districtlmove,
        朗县 = 林芝市 | 7 << RegionHelper.Districtlmove,
        #endregion 区县

        #endregion 市

        大陆外 = 31 << RegionHelper.Provincelmove,//特殊

        #region 市
        香港 = 大陆外 | 1 << RegionHelper.Citylmove,
        #region 区县
        港岛 = 香港 | 1 << RegionHelper.Districtlmove,
        九龙 = 香港 | 2 << RegionHelper.Districtlmove,
        新界 = 香港 | 3 << RegionHelper.Districtlmove,
        离岛 = 香港 | 4 << RegionHelper.Districtlmove,
        #endregion 区县

        澳门 = 大陆外 | 2 << RegionHelper.Citylmove,
        #region 区县
        澳门半岛 = 澳门 | 1 << RegionHelper.Districtlmove,
        澳门离岛 = 澳门 | 2 << RegionHelper.Districtlmove,
        #endregion 区县

        台湾 = 大陆外 | 3 << RegionHelper.Citylmove,
        #region 区县
        台北市 = 台湾 | 1 << RegionHelper.Districtlmove,
        新北市 = 台湾 | 2 << RegionHelper.Districtlmove,
        台中市 = 台湾 | 3 << RegionHelper.Districtlmove,
        台南市 = 台湾 | 4 << RegionHelper.Districtlmove,
        高雄市 = 台湾 | 5 << RegionHelper.Districtlmove,
        基隆市 = 台湾 | 6 << RegionHelper.Districtlmove,
        新竹市 = 台湾 | 7 << RegionHelper.Districtlmove,
        嘉义市 = 台湾 | 8 << RegionHelper.Districtlmove,
        桃园县 = 台湾 | 9 << RegionHelper.Districtlmove,
        新竹县 = 台湾 | 10 << RegionHelper.Districtlmove,
        苗栗县 = 台湾 | 11 << RegionHelper.Districtlmove,
        彰化县 = 台湾 | 12 << RegionHelper.Districtlmove,
        南投县 = 台湾 | 13 << RegionHelper.Districtlmove,
        云林县 = 台湾 | 14 << RegionHelper.Districtlmove,
        嘉义县 = 台湾 | 15 << RegionHelper.Districtlmove,
        屏东县 = 台湾 | 16 << RegionHelper.Districtlmove,
        宜兰县 = 台湾 | 17 << RegionHelper.Districtlmove,
        花莲县 = 台湾 | 18 << RegionHelper.Districtlmove,
        台东县 = 台湾 | 19 << RegionHelper.Districtlmove,
        澎湖县 = 台湾 | 20 << RegionHelper.Districtlmove,
        #endregion 区县

        新加坡 = 大陆外 | 4 << RegionHelper.Citylmove,
        #region 区县
        新加坡中区 = 新加坡 | 1 << RegionHelper.Districtlmove,
        新加坡东区 = 新加坡 | 2 << RegionHelper.Districtlmove,
        新加坡北区 = 新加坡 | 3 << RegionHelper.Districtlmove,
        新加坡东北区 = 新加坡 | 4 << RegionHelper.Districtlmove,
        新加坡西区 = 新加坡 | 5 << RegionHelper.Districtlmove,
        #endregion 区县

        其它国家 = 大陆外 | 5 << RegionHelper.Citylmove,
        #region 区县
        其它国家城市 = 其它国家 | 1 << RegionHelper.Districtlmove
        #endregion 区县
        #endregion 市

        #endregion 普通省
        
    
    }
}
  
