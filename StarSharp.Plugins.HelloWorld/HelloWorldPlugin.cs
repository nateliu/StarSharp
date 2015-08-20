using StarSharp.Core.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSharp.Plugins.HelloWorldPlugin
{
    public class HelloWorldPlugin:IPlugin
    {
        public void InitPlugin(PluginConfigItem thePlugin, IPluginContext context)
        {

        }

        public void ShowPlugin(PluginConfigItem thePlugin, IPluginContext context)
        {
            HelloWorldPluginForm helloWorld = new HelloWorldPluginForm();
            helloWorld.ShowDialog();
        }

        public void ClosePlugin(PluginConfigItem thePlugin, IPluginContext context)
        {
        }
    }
}
