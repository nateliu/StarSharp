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
        }

        protected override void OnActivated(EventArgs e)
        {
            if (this is IPluginContext)
            {
                PluginManager.PreLoadMainPlugins(this as IPluginContext);
                PluginManager.LoadPlugin(this);
            }
        }       

        #region IPluginContext Members

        protected ConnectionPointContainer _PluginContainerWpf;

        public virtual ConnectionPointContainer PluginContainer
        {
            get { return _PluginContainerWpf; }
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
