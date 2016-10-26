using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace Lib.Csharp.Tools
{
    /// <summary>
    /// XmlHelper 的摘要说明。
    /// </summary>
    public class XmlHelper
    {
        public enum EnumXmlPathType
        {
            AbsolutePath,
            VirtualPath
        }

        private string _xmlfilepath;
        private EnumXmlPathType _xmlfilepathtype;
        private readonly XmlDocument _xmldoc = new XmlDocument();

        public string XmlFilePath
        {
            set
            {
                _xmlfilepath = value;
            }
        }

        public EnumXmlPathType XmlFilePathtype
        {
            set
            {
                _xmlfilepathtype = value;
            }
        }
        public XmlHelper()
        {

        }
        public XmlHelper(string tempxmlfilepath)
        {
            this.XmlFilePathtype = EnumXmlPathType.VirtualPath;
            this.XmlFilePath = tempxmlfilepath;
            _xmldoc = GetXmlDocument();
        }

        public XmlHelper(string tempxmlfilepath, EnumXmlPathType tempxmlfilepathtype)
        {
            this.XmlFilePathtype = tempxmlfilepathtype;
            this.XmlFilePath = tempxmlfilepath;
            _xmldoc = GetXmlDocument();
        }


        #region 读取xml文件
        // </summary>
        // <param name="strentitytypename">实体类的名称</param>
        // <returns>指定的xml描述文件的路径</returns>
        private XmlDocument GetXmlDocument()
        {
            XmlDocument doc = null;

            if (this._xmlfilepathtype == EnumXmlPathType.AbsolutePath)
            {
                doc = GetXmlDocumentFromFile(_xmlfilepath);
            }
            else if (this._xmlfilepathtype == EnumXmlPathType.VirtualPath)
            {
                doc = GetXmlDocumentFromFile(HttpContext.Current.Server.MapPath(_xmlfilepath));
            }
            return doc;
        }
        #endregion

        #region 按路径读取xml文件
        private XmlDocument GetXmlDocumentFromFile(string tempxmlfilepath)
        {
            string xmlfilefullpath = tempxmlfilepath;

            _xmldoc.Load(xmlfilefullpath);
            return _xmldoc;
        }
        #endregion

        #region 读取指定节点的指定属性值

        /// <summary>
        /// 读取指定节点的指定属性值
        /// </summary>
        /// <param name="xmlnodepath">节点路径</param>
        /// <param name="xmlnodeattribute">此节点的属性</param>
        /// <returns></returns>
        public string GetXmlNodeAttributeValue(string xmlnodepath, string xmlnodeattribute)
        {
            string strreturn = "";
            try
            {
                //根据指定路径获取节点
                var xmlnode = _xmldoc.SelectSingleNode(xmlnodepath);
                if (xmlnode != null)
                {
                    //获取节点的属性，并循环取出需要的属性值
                    var xmlattr = xmlnode.Attributes;
                    if (xmlattr != null)
                    {
                        for (var i = 0; i < xmlattr.Count; i++)
                        {
                            if (xmlattr.Item(i).Name == xmlnodeattribute)
                                strreturn = xmlattr.Item(i).Value;
                        }
                    }
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            return strreturn;
        }
        #endregion

        #region 读取指定节点集合第n个节点

        /// <summary>
        /// 读取指定节点集合第n个节点的值
        /// </summary>
        /// <param name="xmlnodepath">节点集路径</param>
        /// <param name="index">序号</param>
        /// <returns></returns>
        public XmlNode GetSubIndexNode(string xmlnodepath, int index)
        {
            XmlNode xmlnode = null;
            try
            {
                //根据路径获取节点
                var nodelist = _xmldoc.SelectNodes(xmlnodepath);
                if (nodelist != null)
                {
                    xmlnode = nodelist[index];
                }
            }
            catch (XmlException xmle)
            {
                System.Console.WriteLine(xmle.Message);
            }
            return xmlnode;
        }
        #endregion

        #region 读取指定节点的值
        
        /// <summary>
        /// 读取指定节点的值
        /// </summary>
        /// <param name="xmlnodepath"></param>
        /// <returns></returns>
        public string GetXmlNodeValue(string xmlnodepath)
        {
            string strreturn = string.Empty;
            try
            {
                //根据路径获取节点
                var xmlnode = _xmldoc.SelectSingleNode(xmlnodepath);
                if (xmlnode != null)
                {
                    strreturn = xmlnode.InnerText;
                }
            }
            catch (XmlException xmle)
            {
                System.Console.WriteLine(xmle.Message);
            }
            return strreturn;
        }
        #endregion

        #region 读取指定节点的子节点个数


        /// <summary>
        /// 读取指定节点的子节点个数
        /// </summary>
        /// <param name="xmlnodepath"></param>
        /// <returns></returns>
        public int GetXmlNodeSubCount(string xmlnodepath)
        {
            int intreturn = 0;
            try
            {
                //根据路径获取节点
                var xmlnode = _xmldoc.SelectSingleNode(xmlnodepath);
                if (xmlnode != null)
                {
                    intreturn = xmlnode.ChildNodes.Count;
                }
            }
            catch (XmlException xmle)
            {
                System.Console.WriteLine(xmle.Message);
            }
            return intreturn;
        }
        #endregion

        #region 读取指定节点的第index个子节点

        /// <summary>
        /// 读取指定节点的子节点个数
        /// </summary>
        /// <param name="xmlnodepath"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public XmlNode GetXmlNodeIndexSub(string xmlnodepath, int index)
        {
            XmlNode nodereturn = null;
            try
            {
                //根据路径获取节点
                var list = _xmldoc.SelectNodes(xmlnodepath);
                if (list != null)
                {
                    nodereturn = list[index];
                }
            }
            catch (XmlException xmle)
            {
                System.Console.WriteLine(xmle.Message);
            }
            return nodereturn;
        }
        #endregion

        #region 读取指定节点集合节点个数

        /// <summary>
        /// 读取指定节点的子节点个数
        /// </summary>
        /// <param name="xmlnodepath"></param>
        /// <returns></returns>
        public int GetSubXmlNodeCount(string xmlnodepath)
        {
            int intreturn = 0;
            try
            {
                //根据路径获取节点
                var list = _xmldoc.SelectNodes(xmlnodepath);
                if (list != null)
                {
                    intreturn = list.Count;
                }
            }
            catch (XmlException xmle)
            {
                System.Console.WriteLine(xmle.Message);
            }
            return intreturn;
        }
        #endregion

        #region 读取指定节点集合第n个节点的值

        /// <summary>
        /// 读取指定节点集合第n个节点的值
        /// </summary>
        /// <param name="xmlnodepath"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetSubIndexNodeValue(string xmlnodepath, int index)
        {
            var strreturn = string.Empty;
            try
            {
                var xmlnode = GetSubIndexNode(xmlnodepath, index);
                strreturn = xmlnode.InnerText;
            }
            catch (XmlException xmle)
            {
                System.Console.WriteLine(xmle.Message);
            }
            return strreturn;
        }
        #endregion

        #region 读取指定节点集合第n个节点的属性值

        /// <summary>
        /// 读取指定节点集合第n个节点的属性值
        /// </summary>
        /// <param name="xmlnodepath"></param>
        /// <param name="index"></param>
        /// <param name="xmlnodeattribute"></param>
        /// <returns></returns>
        public string GetSubIndexNodeAttributeValue(string xmlnodepath, int index, string xmlnodeattribute)
        {
            string strreturn = string.Empty;
            try
            {
                var xmlnode = GetSubIndexNode(xmlnodepath, index);
                //获取节点的属性，并循环取出需要的属性值
                var xmlattr = xmlnode.Attributes;
                if (xmlattr != null)
                {
                    for (int i = 0; i < xmlattr.Count; i++)
                    {
                        if (xmlattr.Item(i).Name == xmlnodeattribute)
                            strreturn = xmlattr.Item(i).Value;
                    }
                }
            }
            catch (XmlException xmle)
            {
                System.Console.WriteLine(xmle.Message);
            }
            return strreturn;
        }
        #endregion

        #region 读取指定节点的子节点中指定属性index为指定属性内容的节点值
        

        /// <summary>
        /// 读取指定节点的子节点中指定属性index为指定属性内容的节点值
        /// </summary>
        /// <param name="xmlnodepath"></param>
        /// <param name="attributeindex"></param>
        /// <param name="attributtevalue"></param>
        /// <returns></returns>
        public string GetSubNodeValueByAttributeValue(string xmlnodepath, int attributeindex, string attributtevalue)
        {
            string strreturn = string.Empty;
            try
            {
                var xmlnode = _xmldoc.SelectSingleNode(xmlnodepath);
                if (xmlnode != null)
                {
                    var nodelist = xmlnode.ChildNodes;
                    for (var i = 0; i < nodelist.Count; i++)
                    {
                        var subnode = nodelist[i];

                        if (subnode.Attributes != null && subnode.Attributes[attributeindex].Value == attributtevalue)
                        {
                            strreturn = subnode.Value;
                        }
                    }
                }
            }
            catch (XmlException xmle)
            {
                System.Console.WriteLine(xmle.Message);
            }
            return strreturn;
        }
        #endregion

        #region 读取指定节点的子节点中指定属性名为指定属性内容的节点值

        /// <summary>
        /// 读取指定节点的子节点中指定属性名为指定属性内容的节点值
        /// </summary>
        /// <param name="xmlnodepath">节点集路径</param>
        /// <param name="attributename">属性名</param>
        /// <param name="attributtevalue">属性值</param>
        /// <returns></returns>
        public string GetSubNodeValueByAttributeValue(string xmlnodepath, string attributename, string attributtevalue)
        {
            string strreturn = string.Empty;
            try
            {
                var xmlnode = _xmldoc.SelectSingleNode(xmlnodepath);
                if (xmlnode != null)
                {
                    var nodelist = xmlnode.ChildNodes;

                    for (int i = 0; i < nodelist.Count; i++)
                    {
                        var subnode = nodelist[i];
                        if (subnode.Attributes != null && subnode.Attributes[attributename].Value == attributtevalue)
                        {
                            strreturn = subnode.InnerText;
                        }
                    }
                }
            }
            catch (XmlException xmle)
            {
                System.Console.WriteLine(xmle.Message);
            }
            return strreturn;
        }
        #endregion

        #region 读取指定节点集合第n个节点的第几个属性值
       

        /// <summary>
        /// 读取指定节点集合第n个节点的第几个属性值
        /// </summary>
        /// <param name="xmlnodepath">节点集路径</param>
        /// <param name="index">序号</param>
        /// <param name="attributeindex">属性序号</param>
        /// <returns></returns>
        public string GetSubIndexNodeAttributeValue(string xmlnodepath, int index, int attributeindex)
        {
            string strreturn = string.Empty;
            try
            {
                var xmlnode = GetSubIndexNode(xmlnodepath, index);
                //获取节点的属性，并循环取出需要的属性值
                var xmlattr = xmlnode.Attributes;
                if (xmlattr != null)
                {
                    strreturn = xmlattr.Item(attributeindex).Value;
                }
            }
            catch (XmlException xmle)
            {
                System.Console.WriteLine(xmle.Message);
            }
            return strreturn;
        }
        #endregion

        #region 设置节点值

        /// <summary>
        /// 设置节点值
        /// </summary>
        /// <param name="xmlnodepath">节点的名称</param>
        /// <param name="xmlnodevalue">节点值</param>
        public void SetXmlNodeValue(string xmlnodepath, string xmlnodevalue)
        {
            try
            {
                //根据指定路径获取节点
                var xmlnode = _xmldoc.SelectSingleNode(xmlnodepath);
                if (xmlnode != null)
                {
                    //设置节点值
                    xmlnode.InnerText = xmlnodevalue;
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        #endregion

        #region 设置节点的属性值

        /// <summary>
        /// 设置节点的属性值
        /// </summary>
        /// <param name="xmlnodepath">节点名称</param>
        /// <param name="xmlnodeattribute">属性名称</param>
        /// <param name="xmlnodeattributevalue">属性值</param>
        public void SetXmlNodeValue(string xmlnodepath, string xmlnodeattribute, string xmlnodeattributevalue)
        {
            try
            {
                //根据指定路径获取节点
                var xmlnode = _xmldoc.SelectSingleNode(xmlnodepath);
                if (xmlnode != null)
                {
                    //获取节点的属性，并循环取出需要的属性值
                    var xmlattr = xmlnode.Attributes;
                    if (xmlattr != null)
                    {
                        for (var i = 0; i < xmlattr.Count; i++)
                        {
                            if (xmlattr.Item(i).Name == xmlnodeattribute)
                            {
                                xmlattr.Item(i).Value = xmlnodeattributevalue;
                                break;
                            }
                        }
                    }
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            SaveXmlDocument();
        }
        #endregion

        #region 获取xml文件的根元素

        /// <summary>
        /// 获取xml文件的根元素
        /// </summary>
        /// <returns></returns>
        public XmlNode GetXmlRoot()
        {
            return _xmldoc.DocumentElement;
        }
        #endregion

        #region 在根节点下添加父节点


        /// <summary>
        /// 在根节点下添加父节点
        /// </summary>
        /// <param name="parentnode"></param>
        public void AddParentNode(string parentnode)
        {
            var root = GetXmlRoot();
            var parentxmlnode = _xmldoc.CreateElement(parentnode);
            root.AppendChild(parentxmlnode);
        }
        #endregion

        #region 向一个已经存在的父节点中插入一个子节点


        /// <summary>
        /// 向一个已经存在的父节点中插入一个子节点
        /// </summary>
        /// <param name="parentnodepath"></param>
        /// <param name="childnodename"></param>
        public void AddChildNode(string parentnodepath, string childnodename)
        {
            var parentxmlnode = _xmldoc.SelectSingleNode(parentnodepath);
            if (parentxmlnode != null)
            {
                var childxmlnode = _xmldoc.CreateElement(childnodename);
                parentxmlnode.AppendChild(childxmlnode);
            }
        }
        #endregion

        #region 向一个节点添加属性

        /// <summary>
        /// 向一个节点添加属性
        /// </summary>
        /// <param name="xmlnodepath"></param>
        /// <param name="xmlnodeattribute"></param>
        public void AddAttribute(string xmlnodepath, string xmlnodeattribute)
        {
            var nodeattribute = _xmldoc.CreateAttribute(xmlnodeattribute);
            var nodepath = _xmldoc.SelectSingleNode(xmlnodepath);
            if (nodepath != null && nodepath.Attributes!=null)
            {
                nodepath.Attributes.Append(nodeattribute);
            }
        }
        #endregion

        #region 删除一个节点的属性

        /// <summary>
        /// 删除一个节点的属性
        /// </summary>
        /// <param name="strnodepath"></param>
        /// <param name="nodeattribute"></param>
        /// <param name="nodeattributevalue"></param>
        public void DeleteAttribute(string strnodepath, string nodeattribute, string nodeattributevalue)
        {
            if (_xmldoc != null)
            {
                var nodes = _xmldoc.SelectSingleNode(strnodepath);
                if (nodes != null)
                {
                    var nodepath = nodes.ChildNodes;

                    foreach (var xn in nodepath)
                    {
                        var xe = (XmlElement) xn;

                        if (xe.GetAttribute(nodeattribute) == nodeattributevalue)
                        {
                            xe.RemoveAttribute(nodeattribute); //删除属性
                        }
                    }
                }
            }
        }
        #endregion

        #region 删除一个节点
        // <summary>
        // 删除一个节点
        // </summary>
        public void DeleteXmlNode(string tempxmlnode)
        {

            var xmlnodepath = _xmldoc.SelectSingleNode(tempxmlnode);
            if (xmlnodepath != null && xmlnodepath.ParentNode!=null)
            {
                xmlnodepath.ParentNode.RemoveChild(xmlnodepath);
            }
        }
        #endregion

        #region 保存xml文件
        // <summary>
        // 功能: 
        // 保存xml文件
        // 
        // </summary>
        public void SaveXmlDocument()
        {
            try
            {
                //保存设置的结果
                _xmldoc.Save(HttpContext.Current.Server.MapPath(_xmlfilepath));
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        #endregion

        #region 保存xml文件
        // <summary>
        // 功能: 
        // 保存xml文件
        // 
        // </summary>
        public void SaveXmlDocument(string tempxmlfilepath)
        {
            try
            {
                //保存设置的结果
                _xmldoc.Save(tempxmlfilepath);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        #endregion
    }
}
