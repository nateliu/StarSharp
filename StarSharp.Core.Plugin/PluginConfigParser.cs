using StarSharp.Core.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace StarSharp.Core.Plugin
{
    public class PluginConfigParser
    {
        const string LocalAddinFolder = "Addins";
        const string LocalAddinWebService = "FileService_AddinService";

        #region member variable

        private static XmlDocument pluginXmlDoc = new XmlDocument();
        private const string pluginXmlFile = "StarSharp.Core.Plugin.PluginConfig.xml";
        private IPluginContext _pluginContext;
        private static Dictionary<string, List<PluginConfigItem>> PluginList = new Dictionary<string, List<PluginConfigItem>>();

        #endregion

        public PluginConfigParser(IPluginContext pluginPara)
        {
            this._pluginContext = pluginPara;
        }

        public void ParsePluginByContainer(ConnectionPointContainer container)
        {
            XmlNodeList pluginNodeList = GetPluginNodeList(container);
            if (pluginNodeList == null || pluginNodeList.Count == 0)
                return;
            if (!PluginList.ContainsKey(container.Name))
                PluginList.Add(container.Name, new List<PluginConfigItem>());
            List<PluginConfigItem> containerPluginList = PluginList[container.Name];

            foreach (XmlNode node in pluginNodeList)
            {
                PluginConfigItem foundItem = null;

                string url = ParsePluginUrl(node);

                foundItem = GetPluginByUrl(containerPluginList, url);
                if (foundItem == null)
                {
                    containerPluginList.Add(
                        ParsePluginConfigItem(node)
                        );
                }
            }
        }

        public static Dictionary<string, List<PluginConfigItem>> GetCachePluginList()
        {
            return PluginList;
        }

        public static IDictionary<string, string> GetCategoryUrlList(string category)
        {
            Debug.Assert(category != null);
            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (XmlNode pluginNode in GetPluginXmlDoc().SelectNodes("Plugins/Plugin"))
            {
                PluginConfigItem item = new PluginConfigItem();
                item.Url = ParsePluginUrl(pluginNode);
                item.MenuPaths = ParsePluginMenuPaths(pluginNode);

                if (category == item.GetCategory())
                {
                    string key = item.GetId();
                    if (item.MenuPaths != null && item.MenuPaths.Count > 0)
                    {
                        key = item.MenuPaths[0].MenuPathParts[item.MenuPaths[0].MenuPathParts.Count - 1].TextStyle.Text;
                    }
                    result[key] = item.Url;
                }
            }
            return result;
        }

        #region Private: Parse Xml file

        private PluginConfigItem ParsePluginConfigItem(XmlNode node)
        {
            PluginConfigItem theItem = new PluginConfigItem();
            theItem.Status = PluginConfigItemStatus.OK;
            try
            {
                theItem.Url = ParsePluginUrl(node);
                theItem.PluginInitArgs = ParsePluginInitArgs(node);
                theItem.QualifiedName = ParsePluginLocate(node);
                theItem.Behavior = ParsePluginBehavior(node);
                theItem.RequireFeatureAccess = ParsePluginPermission(node);
                theItem.RunContext = ParsePluginRunContext(node);
                theItem.MenuPaths = ParsePluginMenuPaths(node);
                theItem.PluginDlls = ParsePluginDlls(node);

            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex);
                theItem.Status = PluginConfigItemStatus.ParsePluginConfigFailure;
            }
            return theItem;
        }

        private IDictionary<string, string> ParsePluginInitArgs(XmlNode node)
        {
            IDictionary<string, string> args = new Dictionary<string, string>();
            XmlNode argNode = node.SelectSingleNode("Arguments");
            if (argNode == null)
                return args;
            foreach (XmlNode argSubnode in argNode.SelectNodes("add"))
            {
                args[argSubnode.Attributes["key"].Value.Trim()] = argSubnode.Attributes["value"].Value.Trim();
            }
            return args;
        }

        private PluginQualifiedName ParsePluginLocate(XmlNode thePluginNode)
        {
            string locator = thePluginNode.Attributes["locate"].Value.Trim();
            PluginQualifiedName theLocate = new PluginQualifiedName();
            if (locator != null)
            {
                string locateValue = locator.Trim();

                theLocate.LoadProtocol = (PluginLoadProtocol)Enum.Parse(
                    typeof(PluginLoadProtocol), locateValue.Split(':')[0].Trim(), true);
                string[] classNames = locateValue.Substring(locateValue.IndexOf("://") + 3).Split(',');
                theLocate.ClassName = classNames[0].Trim();
                if (classNames.Length > 1)
                    theLocate.AssemblyPath = classNames[1].Trim();
            }
            return theLocate;
        }

        private string ParsePluginRunContext(XmlNode node)
        {
            if (node.Attributes["runContext"] != null)
            {
                return node.Attributes["runContext"].Value.Trim().ToLower();
            }
            return string.Empty;
        }

        private static string ParsePluginUrl(XmlNode node)
        {
            if (node.Attributes.GetNamedItem("url") != null)
            {
                return node.Attributes.GetNamedItem("url").Value.Trim();
            }
            return string.Empty;
        }

        private string ParsePluginPermission(XmlNode node)
        {
            if (node.Attributes["requireFeatureAccess"] != null)
            {
                return node.Attributes["requireFeatureAccess"].Value.Trim();
            }
            return string.Empty;
        }

        private string ParsePluginBehavior(XmlNode node)
        {
            if (node.Attributes["behavior"] != null)
            {
                return node.Attributes["behavior"].Value.Trim();
            }
            return "Click";
        }

        private static PluginTextStyle ParsePluginTextStyle(string text)
        {
            PluginTextStyle textStyle = new PluginTextStyle();
            string[] styles = text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (styles.Length > 0)
                textStyle.Text = styles[0];
            if (styles.Length > 1)
                textStyle.ToolTipText = styles[1];
            if (styles.Length > 2)
                textStyle.Image = styles[2];
            if (styles.Length > 3)
                textStyle.ButtonType = styles[3];
            if (styles.Length > 4)
                textStyle.ShortKey = styles[4];
            if (styles.Length > 5)
                textStyle.Tag = styles[5];
            return textStyle;
        }

        private IList<PluginDll> ParsePluginDlls(XmlNode node)
        {
            List<PluginDll> theDllList = new List<PluginDll>();
            foreach (XmlNode theDll in node.SelectNodes("Dlls/add"))
            {
                PluginDll dll = new PluginDll();
                dll.Name = theDll.Attributes["name"].Value;
                dll.Version = theDll.Attributes["version"].Value;
                XmlAttribute xa = theDll.Attributes["newVersion"];
                if (xa != null)
                {
                    dll.NewVersion = Convert.ToBoolean(xa.Value.Trim());
                }
                else
                {
                    dll.NewVersion = false;
                }
                theDllList.Add(dll);
            }
            return theDllList;
        }

        private static IList<PluginMenuPath> ParsePluginMenuPaths(XmlNode node)
        {
            List<PluginMenuPath> pathList = new List<PluginMenuPath>();
            foreach (XmlNode theConnectionPointNode in node.SelectNodes("ConnectionPoints/ConnectionPoint"))
            {
                PluginMenuPath point = new PluginMenuPath();
                point.LocateType = (PluginMenuPathLocateType)(Enum.Parse(
                    typeof(PluginMenuPathLocateType),
                    XmlUtils.GetXmlAttribute(theConnectionPointNode, "menuType"))
                    );
                point.MenuIndex = XmlUtils.GetXmlAttribute(theConnectionPointNode, "menuIndex");
                point.MenuImageIndex = XmlUtils.GetXmlAttribute(theConnectionPointNode, "menuImageIndex");
                point.MenuPathParts = new List<PluginMenuItemPart>();
                foreach (XmlNode addNode in theConnectionPointNode.SelectNodes("add"))
                {
                    PluginMenuItemPart thePart = new PluginMenuItemPart();
                    thePart.Locate = XmlUtils.GetXmlAttribute(addNode, "locate");
                    thePart.TextStyle = ParsePluginTextStyle(XmlUtils.GetXmlAttribute(addNode, "text"));
                    point.MenuPathParts.Add(thePart);
                }
                pathList.Add(point);
            }
            return pathList;
        }

        public static PluginConfigItem GetPluginByUrl(List<PluginConfigItem> containerPluginList, string url)
        {
            PluginConfigItem foundItem = null;
            foreach (PluginConfigItem theItem in containerPluginList)
                if (theItem.Url == url)
                {
                    foundItem = theItem;
                    break;
                }
            return foundItem;
        }

        private static XmlDocument GetPluginXmlDoc()
        {
            if (pluginXmlDoc.ChildNodes.Count == 0)
            {
                Stream pluginStream = (typeof(PluginConfig).Assembly).GetManifestResourceStream(pluginXmlFile);

                pluginXmlDoc.Load(pluginStream);

                CombineRemotePluginXmlDoc(pluginXmlDoc);

                PluginLocatorBase.CreateVersionRecord(pluginXmlDoc);
            }
            return pluginXmlDoc;
        }

        private static void CombineRemotePluginXmlDoc(XmlDocument xmlDocument)
        {
            try
            {
                PluginServiceProxy service = PluginServiceProxy.GetInstance();
                string xml = string.Empty;// service.GetPluginConfigXml();
                if (xml != null)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    CombinePluginXmlFile(doc, xmlDocument);
                }
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex);
            }
        }

        private static void CombinePluginXmlFile(XmlDocument serverDoc, XmlDocument localDoc)
        {
            foreach (XmlNode node in serverDoc.DocumentElement.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element
                    && localDoc.SelectSingleNode(string.Format("Plugins/Plugin[@url='{0}']", node.Attributes["url"].Value.Trim())) == null)
                {
                    XmlNode nodeCopy = localDoc.ImportNode(node, true);

                    localDoc.DocumentElement.AppendChild(nodeCopy);
                }
            }
        }

        private XmlNodeList GetPluginNodeList(ConnectionPointContainer container)
        {
            string xPath = String.Format("Plugin[starts-with(@url,'addin://{0}/')]", container.Name);
            XmlDocument doc = GetPluginXmlDoc();
            XmlNodeList nodeList = doc.DocumentElement.SelectNodes(xPath);
            return nodeList;
        }

        //bool _loadedConnectionPointContainer = false;
        //private void LoadConnectionPointContainerList()
        //{
        //    if (!_loadedConnectionPointContainer)
        //    {
        //        XmlNode root = GetPluginXmlDoc().DocumentElement;
        //        string xpath = "ConnectionPointContainers/add";
        //        foreach (XmlNode containerNode in root.SelectNodes(xpath))
        //        {
        //            ConnectionPointContainer.AddConnectionPointContainer(
        //                containerNode.Attributes["key"].Value,
        //                new ConnectionPointContainer(containerNode.Attributes["key"].Value)
        //            );
        //        }

        //        _loadedConnectionPointContainer = true;
        //    }
        //}

        #endregion
    }

}
