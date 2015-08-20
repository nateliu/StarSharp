using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarSharp.Core.Plugin
{
    public class PluginClosingEventArgs
    {
        public PluginClosingEventArgs(IPlugin thePlugin, PluginConfigItem thePluginConfig, string closingReason, bool cancel)
        {
            Plugin = thePlugin;
            PluginConfig = thePluginConfig;
            ClosingReason = closingReason;
            Cancel = cancel;
        }

        public IPlugin Plugin { get; set; }
        public PluginConfigItem PluginConfig { get; set; }
        public string ClosingReason { get; set; }
        public bool Cancel { get; set; }
    }
}
