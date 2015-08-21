using System;

namespace StarSharp.Core.Plugin
{
    public interface IPluginRunContext
    {
        IPlugin ExecutePlugin();
        bool ClosePlugin(bool isCloseForm);
        bool LoadPluginDlls();
        IPlugin GetPlugin(string url);
        void ShowForm();
        void CloseForm();
        PluginConfigItem ConfigItem { get; set; }
        IPluginContext Context { get; set; }
    }
}
