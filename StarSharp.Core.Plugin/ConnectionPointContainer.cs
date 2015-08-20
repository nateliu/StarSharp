using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        IList<MenuStrip> _theMenu = new List<MenuStrip>();
        IList<ToolStrip> _theToolStrip = new List<ToolStrip>();
        IList<TreeView> _theTree = new List<TreeView>();
        IList<ImageList> _imageList = new List<ImageList>();


        public IList<MenuStrip> Menus { get { return _theMenu; } }
        public IList<ToolStrip> ToolStrips { get { return _theToolStrip; } }
        public IList<TreeView> Navigations { get { return _theTree; } }
        public IList<ImageList> ImageList { get { return _imageList; } }

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
