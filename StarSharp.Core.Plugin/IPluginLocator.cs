using System;
using System.Collections.Generic;

namespace StarSharp.Core.Plugin
{
    public interface IPluginLocator
    {
        bool LoadDll(string folder, PluginDll dll, IList<PluginDll> expandingDlls);
        bool HasNewVersion(string folder, string url, string dllName);
        PluginLoadProtocol LoadProtocol { get; }
    }
}
