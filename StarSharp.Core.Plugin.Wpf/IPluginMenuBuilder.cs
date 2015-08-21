using System;

namespace StarSharp.Core.Plugin
{
    public interface IPluginMenuBuilder
    {
        void AddIntoMenu(ConnectionPointContainer container, 
            PluginConfigItem theItem, 
            PluginMenuPath thePath, 
            ExecutePluginCallback callback);
    }
}
