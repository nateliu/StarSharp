using StarSharp.Core.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSharp.Plugins.HelloWorldPlugin
{
    public class DoNothingPlugin : IPlugin
    {
        #region IPlugin Members

        public void InitPlugin(PluginConfigItem thePlugin, IPluginContext context)
        {

        }

        public void ShowPlugin(PluginConfigItem thePlugin, IPluginContext context)
        {
            System.Windows.Forms.MessageBox.Show("I am do nothing here....");
        }

        public void ClosePlugin(PluginConfigItem thePlugin, IPluginContext context)
        {

        }

        #endregion
    }
}
