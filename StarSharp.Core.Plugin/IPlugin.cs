using System;

namespace StarSharp.Core.Plugin
{
    public interface IPlugin
    {
        void InitPlugin(PluginConfigItem thePlugin, IPluginContext context);
        void ShowPlugin(PluginConfigItem thePlugin, IPluginContext context);
        void ClosePlugin(PluginConfigItem thePlugin, IPluginContext context);
    }
}
