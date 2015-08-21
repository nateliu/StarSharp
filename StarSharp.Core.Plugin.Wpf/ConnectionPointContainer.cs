using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace StarSharp.Core.Plugin
{
    public class ConnectionPointContainer : IConnectionPointContainer
    {
        string _path;
        public ConnectionPointContainer(string path)
        {
            // For compatibility
            if (path == null || path == ".")
                path = "MainForm";
            _path = path;
        }

        public string Name
        {
            get { return _path; }
        }

        IConnectionPointContainer _proxy = null;
        public IConnectionPointContainer Proxy { get { return _proxy; } }

        IList<Menu> _theMenu = new List<Menu>();
        IList<ToolBar> _theToolStrip = new List<ToolBar>();
        IList<TreeView> _theTree = new List<TreeView>();
        IList<Image> _imageList = new List<Image>();


        public IList<Menu> Menus { get { return _theMenu; } }
        public IList<ToolBar> ToolStrips { get { return _theToolStrip; } }
        public IList<TreeView> Navigations { get { return _theTree; } }
        public IList<Image> ImageList { get { return _imageList; } }

        static Dictionary<string, ConnectionPointContainer> ConnectionPointContainerList = new Dictionary<string, ConnectionPointContainer>();

        public void AddConnectionPointItem(PluginConfigItem theItem, ExecutePluginCallback callback)
        {
            if (_proxy != null)
            {
                _proxy.AddConnectionPointItem(theItem, callback);
                return;
            }
            foreach (PluginMenuPath thePath in theItem.MenuPaths)
            {
                PluginMenuBuilder.MakeBuilder(thePath.LocateType).AddIntoMenu(
                    this,
                    theItem,
                    thePath,
                    callback
                );
            }
        }
    }
}
