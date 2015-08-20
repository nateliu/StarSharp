using StarSharp.Core.Plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace StarSharp.Win
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    [Serializable]
    public partial class BasicForm : Form, IPluginContext
    {
        public BasicForm()
        {
            InitializeComponent();
        }

        #region IPluginContext Members

		protected ConnectionPointContainer _PluginContainer;
		public virtual ConnectionPointContainer PluginContainer
		{
			get { return _PluginContainer; }
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

        protected override void OnShown(EventArgs e)
        {
            if (this is IPluginContext)
            {
                PluginManager.PreLoadMainPlugins(this as IPluginContext);
                PluginManager.LoadPlugin(this );
            }
        }

    }
}
