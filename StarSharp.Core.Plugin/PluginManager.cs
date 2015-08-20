using System;
using System.Collections.Generic;

namespace StarSharp.Core.Plugin
{
    public class PluginManager
    {
        public static IList<PluginConfigItem> LoadPlugin(IPluginContext parentProgram)
        {
            PluginConfig pluginConfig = new PluginConfig(parentProgram);

            if (configs.ContainsKey(parentProgram))
                pluginConfig = configs[parentProgram];
            else
                configs[parentProgram] = pluginConfig;

            return pluginConfig.LoadPlugIn();
        }

        static Dictionary<IPluginContext, PluginConfig> configs = new Dictionary<IPluginContext, PluginConfig>();

        public static void ExecutePlugin(IPluginContext parentProgram, string url)
        {
            if (!configs.ContainsKey(parentProgram))
            {
                //ShellUtils.ShowWarn("Please call LoadPlugin at first.");
                return;
            }

            configs[parentProgram].ExecutePlugin(url);
        }

        public static IPlugin GetPlugin(IPluginContext parentProgram, string url)
        {
            if (!configs.ContainsKey(parentProgram))
            {
                //ShellUtils.ShowWarn("Please call LoadPlugin at first.");
                return null;
            }

            IPluginRunContext theRunContext = GetPluginConfigItem(parentProgram, url).PluginRunContext;
            if (theRunContext == null)
                return null;
            return theRunContext.GetPlugin(url);
        }

        public static PluginConfigItem GetPluginConfigItem(IPluginContext context, string url)
        {
            PluginConfigItem theItem = PluginConfigParser.GetPluginByUrl(
               PluginConfigParser.GetCachePluginList()[context.PluginContainer.Name], url
               );
            if (theItem != null)
                return theItem;
            throw new ArgumentOutOfRangeException();
        }

        public static void PreLoadMainPlugins(IPluginContext mainPluginContext)
        {
            if (null != mainPluginContext)
            {
                PluginConfig pluginConfig = new PluginConfig(mainPluginContext);
                pluginConfig.PreLoadPlugins();
            }
        }

        public static IDictionary<string, string> GetCategoryUrlList(IPluginContext context, string category)
        {
            if (null != context)
            {
                PluginConfig pluginConfig = new PluginConfig(context);
                bool inited = pluginConfig.InitPluginParser();
                if (inited)
                {
                    return PluginConfigParser.GetCategoryUrlList(category);
                }
                throw new ArgumentException("Please check PluginConfig.xml.");
            }
            throw new ArgumentException("context != null");
        }
    }
}
