using StarSharp.Core.Utility;
using StarSharp.Core.Utility.FormUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Controls;

namespace StarSharp.Core.Plugin
{
    [Serializable]
    public class PluginRunContext : IPluginRunContext
    {
        public const string MainThread = "thread(0)";
        public const string NewThread = "thread(1)";
        public const string NewProcess = "thread(2)";
        public const string NewAppDomain = "thread(-1)";

        static Dictionary<string, PluginRunContext> Contexts = new Dictionary<string, PluginRunContext>();

        public static PluginRunContext Parse(PluginConfigItem configItem, IPluginContext context)
        {
            string runContext = configItem.RunContext;
            if (string.IsNullOrEmpty(runContext))
                runContext = MainThread;
            // on the same current thread
            if (runContext == MainThread)
            {
                if (!Contexts.ContainsKey(runContext))
                    Contexts[runContext] = new PluginRunContext(configItem, context);
            }
            else if (runContext == NewThread)
            {
                if (!Contexts.ContainsKey(runContext))
                    // get a new thread
                    Contexts[runContext] = new MultiThreadPluginRunContext(configItem, context);
            }
            else if (runContext == NewProcess)
            {
                if (!Contexts.ContainsKey(runContext))
                    // get a new process
                    Contexts[runContext] = new MultiProcessPluginRunContext(configItem, context);
            }
            else if (runContext == NewAppDomain)
            {
                if (!Contexts.ContainsKey(runContext))
                    // get a new AppDomain
                    Contexts[runContext] = new MultiAppDomainPuginRunContext(configItem, context);
            }
            Contexts[runContext].ConfigItem = configItem;
            Contexts[runContext].Context = context;
            configItem.PluginRunContext = Contexts[runContext];

            return Contexts[runContext];
        }


        protected Dictionary<string, IPlugin> Plugins = new Dictionary<string, IPlugin>();
        protected Dictionary<string, bool> _init = new Dictionary<string, bool>();

        private PluginConfigItem _theItem;

        public PluginConfigItem ConfigItem
        {
            get { return _theItem; }
            set { _theItem = value; value.PluginRunContext = this; }
        }
        private IPluginContext _context;

        public IPluginContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        public PluginRunContext()
        {
        }

        public PluginRunContext(PluginConfigItem configItem, IPluginContext context)
        {
            _theItem = configItem;
            _context = context;
            configItem.PluginRunContext = this;
        }

        public virtual IPlugin ExecutePlugin()
        {
            IPlugin thePlugin = CreatePluginInstance();
            ProcessParametricPlugin(thePlugin);
            Plugins[ConfigItem.Url] = thePlugin;
            Plugins[ConfigItem.Url].InitPlugin(ConfigItem, Context);
            Plugins[ConfigItem.Url].ShowPlugin(ConfigItem, Context);
            return Plugins[ConfigItem.Url];
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

        protected virtual IPlugin CreatePluginInstance()
        {
            Type pluginType = CreatePluginType();
            return Activator.CreateInstance(pluginType) as IPlugin;
        }

        public virtual bool LoadPluginDlls()
        {
            string baseFolder = Path.Combine(PluginLocatorBase.GetBasePluginFolder(), ConfigItem.GetContainerName());

            foreach (PluginDll dll in ConfigItem.PluginDlls)
            {
                if (dll.Name.ToLower().EndsWith(".zip"))
                    continue;

                bool found = LoadPluginDll(dll.Name, baseFolder);
                if (!found)
                    return false;
            }
            return true;
        }

        protected virtual bool LoadPluginDll(string fileName, string baseFolder)
        {
            try
            {
                Assembly assemblyObj = null;
                string oldAsmName = fileName;
                string asmName = oldAsmName.ToLower();
                Assembly foundAsm = FindAssemblyInAppDomain(asmName);
                if (foundAsm != null)
                    return true;

                string assemblyPath = Path.Combine(baseFolder, oldAsmName);

                if (File.Exists(assemblyPath + ".dll")) assemblyObj = Assembly.LoadFrom(assemblyPath + ".dll");
                if (File.Exists(assemblyPath)) assemblyObj = Assembly.LoadFrom(assemblyPath);

                GetBaseAppDomain().Load(assemblyObj.GetName());

                return true;
            }
            catch { return false; }
        }

        protected Assembly FindAssemblyInAppDomain(string asmName)
        {
            Assembly foundAsm = null;
            foreach (Assembly theAsm in GetBaseAppDomain().GetAssemblies())
            {
                if (theAsm.GetName().Name.ToLower() == asmName)
                {
                    foundAsm = theAsm;
                    break;
                }
            }
            return foundAsm;
        }

        protected virtual AppDomain GetBaseAppDomain()
        {
            return AppDomain.CurrentDomain;
        }

        protected virtual Type CreatePluginType()
        {
            string dll = _theItem.QualifiedName.AssemblyPath;
            if (!string.IsNullOrEmpty(dll))
            {
                Assembly foundAsm = FindAssemblyInAppDomain(_theItem.QualifiedName.AssemblyPath.ToLower());
                if (foundAsm == null) return null;

                return foundAsm.GetType(_theItem.QualifiedName.ClassName);
            }
            else
            {
                return ReflectUtils.GetType(_theItem.QualifiedName.ClassName);
            }
        }

        public virtual IPlugin GetPlugin(string url)
        {
            if (Plugins.ContainsKey(url))
                return Plugins[url];
            return null;
        }

        public virtual void ShowForm()
        {
            //if (Plugins[ConfigItem.Url] != null && (Plugins[ConfigItem.Url] as Form) != null)
            //{
            //    Form theForm = (Plugins[ConfigItem.Url] as Form);
            //    if (_init.ContainsKey(ConfigItem.Url) && _init[ConfigItem.Url])
            //    {
            //        ShowFormWindow(theForm);
            //    }
            //    else
            //    {
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

        protected void ShowFormWindow(/*Form*/object theForm)
        {
            //if (theForm.InvokeRequired)
            //{
            //    Delegate d = new WinFormHelper.InvodeMethodhandler(WinFormHelper.InvokeMethod);
            //    theForm.Invoke(d, new object[] { theForm, "Show", new object[] { } });

            //    try
            //    {
            //        theForm.Invoke(d, new object[] { theForm, "Activate", new object[] { } });

            //    }
            //    catch { }
            //}
            //else
            //{
            //    theForm.Show();
            //    try { theForm.Activate(); }
            //    catch { }
            //}
        }

        public virtual void CloseForm()
        {
            //if (Plugins[ConfigItem.Url] != null && (Plugins[ConfigItem.Url] as Form) != null)
            //{
            //    Form theForm = (Plugins[ConfigItem.Url] as Form);

            //    if (theForm.InvokeRequired)
            //    {
            //        Delegate d = new WinFormHelper.InvodeMethodhandler(WinFormHelper.InvokeMethod);
            //        theForm.Invoke(d, new object[] { theForm, "Close", new object[] { } });
            //    }
            //    else
            //    {
            //        theForm.Close();
            //    }
            //}
        }

        public virtual bool ClosePlugin(bool isClosingForm)
        {
            if (Context != null && Plugins.ContainsKey(ConfigItem.Url))
            {
                PluginClosingEventArgs e = new PluginClosingEventArgs(
                    Plugins[ConfigItem.Url] as IPlugin,
                    ConfigItem,
                    this.GetType().FullName + ".ClosePlugin",
                    false);
                Context.OnClosingPlugin(this, e);
                if (e.Cancel)
                    return false;
            }
            if (isClosingForm)
            {
                CloseForm();
            }
            Plugins.Remove(ConfigItem.Url);
            return true;
        }
    }
	
}
