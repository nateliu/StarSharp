using StarSharp.Core.Utility;
using StarSharp.Core.Utility.FormUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StarSharp.Core.Plugin
{
    public class MultiThreadPluginRunContext : PluginRunContext
    {
        public MultiThreadPluginRunContext() { }
        public MultiThreadPluginRunContext(PluginConfigItem ConfigItem, IPluginContext Context)
            : base(ConfigItem, Context) { }

        public override IPlugin ExecutePlugin()
        {
            ConfigItem.PluginRunContext = this;
            if (Plugins.ContainsKey(ConfigItem.Url))
            {
                _init[ConfigItem.Url] = false;
                IPlugin thePlugin = Plugins[ConfigItem.Url];
                ProcessParametricPlugin(thePlugin);
                thePlugin.InitPlugin(ConfigItem, Context);
                thePlugin.ShowPlugin(ConfigItem, Context);
                return Plugins[ConfigItem.Url];
            }
            else
            {
                AutoResetEvent theSignal = new AutoResetEvent(false);
                ThreadRun runThread = new ThreadRun(ConfigItem, Context, theSignal, Plugins, this);
                ThreadStart ts = new ThreadStart(runThread.Run);
                Thread td = new Thread(ts);
                td.SetApartmentState(ApartmentState.STA);
                td.IsBackground = true;
                td.Name = ConfigItem.Url;
                td.Start();
                _init[ConfigItem.Url] = true;
                theSignal.WaitOne();

                theSignal.Close();
                return Plugins[ConfigItem.Url];
            }
        }

        public override void ShowForm()
        {
            //if (Plugins[ConfigItem.Url] != null && (Plugins[ConfigItem.Url] as Form) != null)
            //{
            //    if (_init.ContainsKey(ConfigItem.Url) && _init[ConfigItem.Url])
            //    {
            //        try
            //        {
            //            Application.Run(Plugins[ConfigItem.Url] as Form);
            //        }
            //        catch (Exception ex)
            //        {
            //            LogUtils.LogError(ex);
            //        }
            //    }
            //    else
            //    {
            //        Form theForm = (Plugins[ConfigItem.Url] as Form);
            //        if (theForm.IsDisposed)
            //        {
            //            Plugins.Remove(ConfigItem.Url);
            //            ExecutePlugin();
            //            return;
            //        }
            //        ShowFormWindow(theForm);
            //    }
            //}
        }

        class ThreadRun
        {
            AutoResetEvent theSignal;
            Dictionary<string, IPlugin> thePlugins;
            PluginConfigItem ConfigItem;
            IPluginContext Context;
            MultiThreadPluginRunContext parent;
            public ThreadRun(PluginConfigItem ConfigItem, IPluginContext Context, AutoResetEvent theSignal, Dictionary<string, IPlugin> plugins, MultiThreadPluginRunContext parent)
            {
                this.ConfigItem = ConfigItem;
                this.Context = Context;
                this.theSignal = theSignal;
                this.thePlugins = plugins;
                this.parent = parent;
            }

            public void Run()
            {
                IPlugin thePlugin = parent.CreatePluginInstance();
                if (ConfigItem == null)
                {
                    throw new ArgumentNullException();
                }
                ProcessParametricPlugin(thePlugin);
                thePlugins[ConfigItem.Url] = thePlugin;
                theSignal.Set();
                thePlugin.InitPlugin(ConfigItem, Context);
                thePlugin.ShowPlugin(ConfigItem, Context);
            }

            protected void ProcessParametricPlugin(IPlugin thePlugin)
            {
                if (thePlugin is IParametricPlugin)
                {
                    if ((thePlugin as IParametricPlugin).PluginInitArgs == null)
                        (thePlugin as IParametricPlugin).PluginInitArgs = new Dictionary<string, string>();
                    IDictionary<string, string> sourceArgs = ConfigItem.PluginInitArgs;
                    if (sourceArgs != null && sourceArgs.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> pair in sourceArgs)
                        {
                            (thePlugin as IParametricPlugin).PluginInitArgs.Add(pair);
                        }
                    }
                }
            }
        }
    
    }
}
