using System;

namespace StarSharp.Core.Plugin
{
    public enum PluginConfigItemStatus
    {
        OK,
        ParsePluginConfigFailure,
        LoadPluginIntoNavigationFailure,
        LoadPluginDllFailure,
        CreatePluginDllFailure,
        CallLoadPluginFailure,
        CallClosePluginFailure
    }
}
