using StarSharp.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;

namespace StarSharp.Core.Plugin
{
    public class PluginRemoteLocator : PluginLocatorBase
    {
        #region IPluginLocator Members

        public PluginRemoteLocator(PluginLoadProtocol theProtocol)
            : base(theProtocol)
        {
        }

        public override bool LoadDll(string folder, PluginDll dll, IList<PluginDll> expandingDlls)
        {
            try
            {
                byte[] buffer = DownloadFromRemoteServer(folder, dll.Name);
                if (buffer == null || buffer.Length == 0)
                    return false;

                string path = GetPluginPath(folder, dll.Name);
                string ext = Path.GetExtension(path).ToLower();
                if (!(ext == ".zip" || ext == ".dll" || ext == ".exe"))
                    path = path + ".dll";
                CreateFile(buffer, path);

                if (IsZip(dll.Name) || IsZip(buffer))
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

        private static void CreateFile(byte[] buffer, string path)
        {
            try
            {
                try { if (File.Exists(path)) File.Delete(path); }
                catch { }

                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(buffer, 0, buffer.Length);
                }
            }
            catch
            {
            }

        }

        private byte[] DownloadFromRemoteServer(string folder, string dllName)
        {
            PluginServiceProxy service = new PluginServiceProxy();
            //return service.GetPluginDll(folder, dllName);
            return null;
        }

        #endregion
    }
}
