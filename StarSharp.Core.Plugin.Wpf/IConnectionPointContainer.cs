using System;

namespace StarSharp.Core.Plugin
{
    public delegate void ExecutePluginCallback(object sender, PluginInvokeEventArgs e);

    public interface IConnectionPointContainer
    {
        void AddConnectionPointItem(PluginConfigItem theItem, ExecutePluginCallback Callback);
        string Name { get; }
        IConnectionPointContainer Proxy { get; }
    }

}
