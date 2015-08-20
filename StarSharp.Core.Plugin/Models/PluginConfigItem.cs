using System;
using System.Collections.Generic;

namespace StarSharp.Core.Plugin
{
    [Serializable]
    public class PluginConfigItem
    {
        public string Url { get; set; }
        public string RunContext { get; set; }
        public IDictionary<string, string> PluginInitArgs { get; set; }
        public IList<PluginMenuPath> MenuPaths { get; set; }
        public IList<PluginDll> PluginDlls { get; set; }
        public PluginQualifiedName QualifiedName { get; set; }
        public PluginConfigItemStatus Status { get; set; }
        public string RequireFeatureAccess { get; set; }
        public string Behavior { get; set; }
        public PluginRunContext PluginRunContext { get; set; }
        public object EventSender { get; set; }

        public bool HasPermissionToLoad()
        {
            if (string.IsNullOrEmpty(RequireFeatureAccess)) return true;
            return false;
            //return ShellUtils.User.GetFeaturePermission(RequireFeatureAccess);
        }

        public string GetContainerName()
        {
            if (string.IsNullOrEmpty(Url))
                return string.Empty;
            string[] temp = Url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (temp.Length > 2)
                return temp[1];
            return string.Empty;
        }
        public string GetCategory()
        {
            if (string.IsNullOrEmpty(Url))
                return string.Empty;
            string[] temp = Url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (temp.Length > 2)
                return temp[2];
            return string.Empty;
        }

        public string GetId()
        {
            if (string.IsNullOrEmpty(Url))
                return string.Empty;
            string[] temp = Url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (temp.Length > 3)
                return temp[3];
            return string.Empty;
        }
    }

}
