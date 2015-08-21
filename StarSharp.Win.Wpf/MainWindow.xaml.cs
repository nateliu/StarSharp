using StarSharp.Core.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace StarSharp.Win.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IPluginContext
    {
        public MainWindow()
        {
            InitializeComponent();

            #region TestCase
            
            //CreateMenu();

            //CreatetoolBarTray();

            //CreateTreeView();
            
            #endregion

            if (this is IPluginContext)
            {
                PluginManager.PreLoadMainPlugins(this as IPluginContext);
                PluginManager.LoadPlugin(this);
            }
        }

        #region TestCase
        
        private void CreateMenu()
        {
            MenuItem itemFile = new MenuItem() { Header = "File" };
            MenuItem itemFileNew = new MenuItem() { Header = "New" };
            MenuItem itemFileOpen = new MenuItem() { Header = "Open" };
            itemFile.Items.Add(itemFileNew);
            itemFile.Items.Add(itemFileOpen);

            MenuItem itemEdit = new MenuItem() { Header = "Edit" };

            this.DMMenu.Items.Add(itemFile);
            this.DMMenu.Items.Add(itemEdit);
        }

        public void CreatetoolBarTray()
        {
            DMToolbar.Items.Add(new Button() { Content = "New" });
            DMToolbar.Items.Add(new Button() { Content = "Open" });
            DMToolbar.Items.Add(new Button() { Content = "Edit" });
        }

        public void CreateTreeView()
        {
            TreeViewItem itemFile = new TreeViewItem() { Header = "File" };
            TreeViewItem itemFileNew = new TreeViewItem() { Header = "New" };
            TreeViewItem itemFileOpen = new TreeViewItem() { Header = "Open" };
            itemFile.Items.Add(itemFileNew);
            itemFile.Items.Add(itemFileOpen);

            TreeViewItem itemEdit = new TreeViewItem() { Header = "Edit" };

            this.DMNavigation.Items.Add(itemFile);
            this.DMNavigation.Items.Add(itemEdit);
        }
        
        #endregion

        #region IPluginContext Members

        protected ConnectionPointContainer _PluginContainer;

        public virtual ConnectionPointContainer PluginContainer
        {
            get
            {
                if (null == _PluginContainer)
                {
                    _PluginContainer = new ConnectionPointContainer("MainForm");
                    _PluginContainer.Menus.Add(this.DMMenu);
                    _PluginContainer.ToolStrips.Add(this.DMToolbar);
                    _PluginContainer.Navigations.Add(this.DMNavigation);
                }
                return _PluginContainer;
            }
        }

        protected System.Xml.XmlNode _AdamData;
        public virtual System.Xml.XmlNode AdamData
        {
            get { return _AdamData; }
        }

        protected object _PluginArg;
        public virtual object PluginArg
        {
            get { return _PluginArg; }
        }

        object _PluginTag;
        public virtual object PluginTag
        {
            get
            {
                return _PluginTag;
            }
            set
            {
                _PluginTag = value;
            }
        }

        protected IList<XmlNode> _AdamDataOfSelectedRows = new List<XmlNode>();
        public virtual IList<XmlNode> SelectedAdamData
        {
            get { return _AdamDataOfSelectedRows; }
        }

        protected IList<XmlNode> _AdamDataOfModified = new List<XmlNode>();
        public virtual IList<XmlNode> ModifiedAdamData
        {
            get { return _AdamDataOfModified; }
        }

        public virtual void OnClosingPlugin(object sender, PluginClosingEventArgs e)
        {
        }

        #endregion

    }
}
