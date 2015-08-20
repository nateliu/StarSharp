using StarSharp.Core.Utility;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace StarSharp.Core.Plugin
{
    public class MultiAppDomainPuginRunContext : PluginRunContext
    {
        public MultiAppDomainPuginRunContext() { }
        public MultiAppDomainPuginRunContext(PluginConfigItem theItem, IPluginContext context)
            : base(theItem, context) { }

        Dictionary<string, AppDomain> AppDomainList = new Dictionary<string, AppDomain>();
        Dictionary<string, IPluginRunContext> ContextList = new Dictionary<string, IPluginRunContext>();

        public override IPlugin ExecutePlugin()
        {
            AppDomain appDomain = GetBaseAppDomain();
            IPluginRunContext obj = GetContext(appDomain);
            return obj.ExecutePlugin();
        }

        private IPluginRunContext GetContext(AppDomain appDomain)
        {
            if (!ContextList.ContainsKey(ConfigItem.Url))
            {
                PluginRunContext obj = appDomain.CreateInstanceAndUnwrap(
                             Assembly.GetExecutingAssembly().GetName().Name,
                             typeof(PluginRunContext).FullName.Split(',')[0])
                             as PluginRunContext;
                obj.ConfigItem = this.ConfigItem;
                obj.Context = this.Context;
                ContextList[ConfigItem.Url] = obj;
            }
            return ContextList[ConfigItem.Url];
        }

        public override bool LoadPluginDlls()
        {
            AppDomain appDomain = GetBaseAppDomain();
            IPluginRunContext obj = GetContext(appDomain);
            return obj.LoadPluginDlls();
        }

        protected override AppDomain GetBaseAppDomain()
        {
            if (ConfigItem != null)
            {
                if (!AppDomainList.ContainsKey(ConfigItem.Url))
                {
                    AppDomainList[ConfigItem.Url] = CreateAppDomain();
                }
                return AppDomainList[ConfigItem.Url];
            }
            return null;
        }

        private AppDomain CreateAppDomain()
        {
            AppDomainSetup info = new AppDomainSetup();

            info.AppDomainInitializer = AppDomainInitializer;
            info.AppDomainInitializerArguments = GetAppDomainInitializerArguments(ConfigItem.Url);
            info.ApplicationBase = Application.StartupPath;

            return AppDomain.CreateDomain(ConfigItem.Url, null, info);
        }

        static string[] GetAppDomainInitializerArguments(string url)
        {
            // Get from DataManagerContext
            return new string[] { };//{ url, DataManagerContext.ToXmlString(DataManagerContext.MakeInstance(null)) };
        }

        static void AppDomainInitializer(string[] args)
        {
            // restore DataManagerContext
            LogUtils.LogDebug("**********");
            LogUtils.LogDebug(System.AppDomain.CurrentDomain.FriendlyName);
            LogUtils.LogDebug("**********");
            //new DataManagerContext().ReInit(
            //    DataManagerContext.FromXmlString(args[1])
            //    );
        }

        public override IPlugin GetPlugin(string url)
        {
            AppDomain appDomain = GetBaseAppDomain();
            IPluginRunContext obj = GetContext(appDomain);
            return obj.GetPlugin(url);
        }

        public override void ShowForm()
        {
            AppDomain appDomain = GetBaseAppDomain();
            IPluginRunContext obj = GetContext(appDomain);
            obj.ShowForm();
        }

        public override void CloseForm()
        {
            AppDomain appDomain = GetBaseAppDomain();
            IPluginRunContext obj = GetContext(appDomain);
            obj.CloseForm();
        }

        public override bool ClosePlugin(bool isClosingForm)
        {
            AppDomain appDomain = GetBaseAppDomain();
            IPluginRunContext obj = GetContext(appDomain);
            bool exited = obj.ClosePlugin(isClosingForm);
            if (!exited)
                return false;
            ContextList.Remove(ConfigItem.Url);
            return true;
        }
    }

}
