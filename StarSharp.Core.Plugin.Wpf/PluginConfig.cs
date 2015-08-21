using StarSharp.Core.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StarSharp.Core.Plugin
{
    public class PluginConfig
    {
        private IPluginContext _pluginContext;
        private Dictionary<string, List<PluginConfigItem>> PluginList = null;
        private PluginConfigParser _parser = null;

        public PluginConfig(IPluginContext pluginPara)
        {
            this._pluginContext = pluginPara;
            this.PluginList = PluginConfigParser.GetCachePluginList();
            this._parser = new PluginConfigParser(pluginPara);
        }

        public IList<PluginConfigItem> LoadPlugIn()
        {
            if (!_inited)
            {
                bool succeed = InitPluginParser();
                if (!succeed)
                    return null;
            }

            try
            {
                LoadIntoNavigation(PluginList[_pluginContext.PluginContainer.Name]);
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex);
                //ShellUtils.ShowError(ex);
            }
            return PluginList[_pluginContext.PluginContainer.Name];
        }

        private void ParsePluginByContainer(ConnectionPointContainer container)
        {
            _parser.ParsePluginByContainer(container);
        }

        public void PreLoadPlugins()
        {
            if (!_inited)
            {
                bool succeed = InitPluginParser();
                if (!succeed)
                    return;
            }

            foreach (PluginConfigItem configItem in PluginList[_pluginContext.PluginContainer.Name])
            {
                Debug.Assert(configItem.Url != null);

                try
                {
                    LoadPluginDll(configItem);
                }
                catch (Exception ex)
                {
                    LogUtils.LogError(ex);
                }
            }
        }

        bool _inited = false;
        public bool InitPluginParser()
        {
            if (_inited)
                return true;

            _inited = false;

            ConnectionPointContainer container = this._pluginContext.PluginContainer;
            if (container == null || string.IsNullOrEmpty(container.Name))
                return false;
            try
            {
                if (!PluginList.ContainsKey(container.Name))
                {
                    ParsePluginByContainer(container);
                }
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex);
                //ShellUtils.ShowError(ex);
            }

            if (!PluginList.ContainsKey(container.Name))
                return false;

            _inited = true;
            return true;
        }

        public void ExecutePlugin(string pluginUrl)
        {
            Debug.Assert(pluginUrl != null);
            PluginConfigItem configItem = PluginConfigParser.GetPluginByUrl(
                PluginList[this._pluginContext.PluginContainer.Name], pluginUrl
                );
            if (configItem == null)
            {
                LogUtils.LogError("GetPluginByUrl Error." + pluginUrl, new ArgumentOutOfRangeException());
                return;
            }
            ExecutePlugin(configItem);
        }

        public void ExecutePlugin(PluginConfigItem configItem)
        {
            Debug.Assert(configItem.Url != null);
            try
            {
                LoadPluginDll(configItem);

                PluginRunContext runContext = PluginRunContext.Parse(configItem, _pluginContext);

                bool dllLoaded = runContext.LoadPluginDlls();
                if (!dllLoaded)
                {
                    configItem.Status = PluginConfigItemStatus.CreatePluginDllFailure;
                }

                configItem.PluginRunContext = runContext;

                runContext.ExecutePlugin();
            }
            catch (Exception ex)
            {
                LogUtils.LogError(ex);
            }
        }

        private void LoadIntoNavigation(List<PluginConfigItem> plugins)
        {
            for (int i = 0; i < plugins.Count; i++)
            {
                PluginConfigItem theItem = plugins[i];
                try
                {
                    if (theItem.Status == PluginConfigItemStatus.OK)
                    {
                        this._pluginContext.PluginContainer.AddConnectionPointItem(theItem,
                            ExecutePlugin
                        );
                    }
                    else
                    {
                        LogUtils.LogError(new Exception(theItem.Status.ToString()));
                    }
                }
                catch (Exception ex)
                {
                    LogUtils.LogError(ex);
                    theItem.Status = PluginConfigItemStatus.LoadPluginIntoNavigationFailure;
                }
            }
        }

        void ExecutePlugin(object sender, PluginInvokeEventArgs e)
        {
            ExecutePlugin(e.ConfigItem);
        }

        private void LoadPluginDll(PluginConfigItem configItem)
        {
            IPluginLocator theLocator = PluginLocatorFactory.GetLocator(configItem.QualifiedName.LoadProtocol);
            IList<PluginDll> expandingDlls = new List<PluginDll>();
            IList<PluginDll> _changedList = new List<PluginDll>();
            for (int i = 0; i < configItem.PluginDlls.Count; i++)
            {
                PluginDll theDll = configItem.PluginDlls[i];
                if (theDll.NewVersion)
                {
                    bool loaded = theLocator.LoadDll(this._pluginContext.PluginContainer.Name, theDll, expandingDlls);

                    if (!loaded)
                    {
                        configItem.Status = PluginConfigItemStatus.LoadPluginDllFailure;
                        break;
                    }
                    else
                    {
                        theDll.NewVersion = false;
                        _changedList.Add(theDll);
                    }
                }
            }

            if (_changedList.Count > 0)
            {
                PluginLocatorBase.SavePluginDllVersion(_changedList);
            }
            foreach (PluginDll expandingDll in expandingDlls)
            {
                if (!PluginLocatorBase.IsInPluginDlls(expandingDll, configItem.PluginDlls))
                    configItem.PluginDlls.Add(expandingDll);
            }
        }
	
    }
}
