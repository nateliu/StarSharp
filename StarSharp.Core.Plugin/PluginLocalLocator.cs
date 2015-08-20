using StarSharp.Core.Utility;
using System;
using System.Collections.Generic;

namespace StarSharp.Core.Plugin
{
    public class PluginLocalLocator : PluginLocatorBase
    {
        #region IPluginLocator Members

        public PluginLocalLocator(PluginLoadProtocol theProtocol)
            : base(theProtocol)
        {
        }

        public override bool LoadDll(string folder, PluginDll dll, IList<PluginDll> expandingDlls)
        {
            try
            {
                string path = GetPluginPath(folder, dll.Name);
                //if (!File.Exists(path))
                //{
                //    path = path + ".dll";
                //}
                if (IsZip(dll.Name))
                {
                    ParseZipFile(folder, dll, expandingDlls, path);
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex);
                return false;
            }
        }

        #endregion
    }   
}
