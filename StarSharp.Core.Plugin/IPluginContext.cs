using System;
using System.Collections.Generic;
using System.Xml;

namespace StarSharp.Core.Plugin
{
    public interface IPluginContext
    {
        ConnectionPointContainer PluginContainer { get; }
        XmlNode AdamData { get; }
        IList<XmlNode> SelectedAdamData { get; }
        IList<XmlNode> ModifiedAdamData { get; }
        object PluginArg { get; }
        object PluginTag { get; set; }
        void OnClosingPlugin(object sender, PluginClosingEventArgs e);	
    }
}
