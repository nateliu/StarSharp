using StarSharp.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;

namespace StarSharp.Core.Plugin
{
    public class PluginLocatorBase : IPluginLocator
    {
        public static bool IsInPluginDlls(PluginDll dll, IList<PluginDll> dlls)
        {
            foreach (PluginDll theDll in dlls)
            {
                if (dll.Name == theDll.Name)
                    return true;
            }
            return false;
        }

        public static void CreateVersionRecord(XmlDocument doc)
        {
            string lastFile = GetLastConfigPath();

            CombinePluginXmlFileDllVersion(doc, lastFile);

            string nowFile = GetNowConfigPath();

            Directory.CreateDirectory(Path.GetDirectoryName(nowFile));

            SaveXmlDocument(doc, nowFile);

            SaveXmlDocument(doc, lastFile);
        }

        static void SaveXmlDocument(XmlDocument doc, string file)
        {
            try { File.Delete(file); }
            catch { }
            using (FileStream fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write))
            {
                doc.Save(fs);
            }
        }

        private static void CombinePluginXmlFileDllVersion(XmlDocument newDoc, string lastFile)
        {
            XmlDocument oldDoc = null;
            if (File.Exists(lastFile))
            {
                oldDoc = new XmlDocument();
                oldDoc.Load(lastFile);
            }
            bool existOldConfig = oldDoc != null;
            foreach (XmlNode dllNode in newDoc.SelectNodes("Plugins/Plugin/Dlls/add"))
            {
                string url = dllNode.ParentNode.ParentNode.Attributes["url"].Value.Trim();
                string folder = url.Split('/')[2];
                string dllName = dllNode.Attributes["name"].Value.Trim();

                XmlAttribute xa = dllNode.Attributes["newVersion"];
                if (xa == null)
                {
                    xa = dllNode.OwnerDocument.CreateAttribute("newVersion");
                    dllNode.Attributes.Append(xa);
                }
                xa.Value = false.ToString();

                if (!existOldConfig)
                {
                    xa.Value = true.ToString();
                    continue;
                }

                string xpath = string.Format(
                    "Plugins/Plugin[@url='{0}']/Dlls/add[@name='{1}']",
                    url,
                    dllName
                );
                XmlNode dllNode2 = oldDoc.SelectSingleNode(xpath);
                if (dllNode2 == null)
                {
                    xa.Value = true.ToString();
                    continue;
                }

                XmlAttribute xa2 = dllNode2.Attributes["newVersion"];
                if (xa2 != null && Convert.ToBoolean(xa2.Value.Trim()))
                {
                    xa.Value = true.ToString();
                    continue;
                }

                string file = GetPluginPath(folder, dllName);
                if (!File.Exists(file + ".dll") && !File.Exists(file))
                {
                    xa.Value = true.ToString();
                    continue;
                }

                if (XmlUtils.GetXmlAttribute(dllNode, "version").CompareTo(
                        XmlUtils.GetXmlAttribute(dllNode2, "version")) > 0)
                {
                    xa.Value = true.ToString();
                    continue;
                }
            }
        }

        public static void SavePluginDllVersion(IList<PluginDll> dlls)
        {
            string nowFile = GetNowConfigPath();
            XmlDocument doc = new XmlDocument();
            doc.Load(nowFile);
            for (int i = 0; i < dlls.Count; i++)
            {
                PluginDll dll = dlls[i];
                XmlNode dllNode = doc.SelectSingleNode(string.Format("Plugins/Plugin/Dlls/add[@name='{0}']",
                    dll.Name));
                if (dllNode != null)
                {
                    XmlAttribute xa = dllNode.Attributes["newVersion"];
                    if (xa == null)
                    {
                        xa = dllNode.OwnerDocument.CreateAttribute("newVersion");
                        dllNode.Attributes.Append(xa);
                    }
                    xa.Value = dll.NewVersion.ToString();
                }
            }
            doc.Save(nowFile);

            string lastFile = GetLastConfigPath();
            try { File.Delete(lastFile); }
            catch { }
            doc.Save(lastFile);
        }

        public const string BasePluginFolder = "Plugins";
        public static string GetBasePluginFolder()
        {
            return Path.Combine(GetExeDir(),
                BasePluginFolder);
        }

        static string GetExeDir()
        {
            System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
            string codeBase = System.IO.Path.GetDirectoryName(ass.CodeBase);
            System.Uri uri = new Uri(codeBase);
            return uri.LocalPath;
        }

        static string GetLastConfigPath()
        {
            return Path.Combine(GetBasePluginFolder(), "PluginConfig.last.xml");
        }
        static string GetNowConfigPath()
        {
            return Path.Combine(GetBasePluginFolder(), "PluginConfig.now.xml");
        }


        #region IPluginLocator Members

        PluginLoadProtocol _protocol;
        public PluginLocatorBase(PluginLoadProtocol theProtocol)
        {
            _protocol = theProtocol;
        }

        public virtual bool LoadDll(string folder, PluginDll dllName, IList<PluginDll> expandingDlls)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex);
                return false;
            }
        }

        static bool DllHasNewVersion(string folder, string url, string dllName)
        {
            try
            {
                string lastFile = GetLastConfigPath();
                string nowFile = GetNowConfigPath();
                if (!File.Exists(lastFile))
                    return true;
                string file = GetPluginPath(folder, dllName);
                if (!File.Exists(file + ".dll") && !File.Exists(file))
                    return true;

                XmlDocument lastDoc = new XmlDocument();
                lastDoc.Load(lastFile);
                XmlDocument nowDoc = new XmlDocument();
                nowDoc.Load(nowFile);

                string xpath = string.Format(
                    "Plugins/Plugin[@url='{0}']/Dlls/add[@name='{1}']",
                    url,
                    dllName
                );
                XmlNode lastNode = lastDoc.SelectSingleNode(xpath);
                XmlNode nowNode = nowDoc.SelectSingleNode(xpath);
                if (lastNode != null && nowNode != null)
                {
                    return (XmlUtils.GetXmlAttribute(nowNode, "version").CompareTo(
                        XmlUtils.GetXmlAttribute(lastNode, "version")) > 0);
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex);
                return true;
            }
        }


        public virtual bool HasNewVersion(string folder, string url, string dllName)
        {
            return DllHasNewVersion(folder, url, dllName);
        }

        protected bool IsZip(string dllName)
        {
            return dllName.ToLower().EndsWith(".zip");
        }

        protected bool IsZip(byte[] data)
        {
            if (data == null || data.Length < 4) return false;
            string zipCode = data[3].ToString("x2") + data[2].ToString("x2") + data[1].ToString("x2") + data[0].ToString("x2");
            const string ZipCode = "04034b50";
            return zipCode == ZipCode;
        }

        public PluginLoadProtocol LoadProtocol
        {
            get { return _protocol; }
        }

        protected static string GetPluginPath(string folder, string dllName)
        {
            return Path.Combine(GetPluginFolder(folder), dllName);
        }

        protected void ParseZipFile(string folder, PluginDll dll, IList<PluginDll> expandingDlls, string path)
        {
            string zipFolder = GetPluginFolder(folder) + "_zip";
            ZipHelper.UnpackFiles(path, zipFolder);
            foreach (string file in Directory.GetFiles(zipFolder))
            {
                string newFile = Path.Combine(GetPluginFolder(folder), Path.GetFileName(file));
                try
                {
                    File.Copy(file, newFile, true);
                    string fn = Path.GetFileNameWithoutExtension(newFile);
                    PluginDll theDll = new PluginDll();
                    theDll.Name = fn;
                    theDll.Version = dll.Version;
                    if (!PluginLocatorBase.IsInPluginDlls(theDll, expandingDlls))
                        expandingDlls.Add(theDll);
                }
                catch { }
            }
            try { Directory.Delete(zipFolder, true); }
            catch { }
        }

        protected static string GetPluginFolder(string folder)
        {
            string path = GetBasePluginFolder();
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path = Path.Combine(path, folder);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }

        #endregion
    }
}
