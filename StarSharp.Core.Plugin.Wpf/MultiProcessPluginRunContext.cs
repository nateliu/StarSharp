using StarSharp.Core.Utility.FormUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarSharp.Core.Plugin
{
    public class MultiProcessPluginRunContext : PluginRunContext
    {
        public MultiProcessPluginRunContext() { }
        public MultiProcessPluginRunContext(PluginConfigItem theItem, IPluginContext context)
            : base(theItem, context) { }

        public override IPlugin ExecutePlugin()
        {
            throw new NotSupportedException();
        }
    }
}
