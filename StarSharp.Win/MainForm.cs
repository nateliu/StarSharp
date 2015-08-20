using StarSharp.Core.Plugin;

namespace StarSharp.Win
{
    public partial class MainForm : BasicForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        static MainForm theInstance = null;
        public static MainForm GetInstance()
        {
            if (theInstance == null)
                theInstance = new MainForm();
            return theInstance;
        }


        public override ConnectionPointContainer PluginContainer
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
    }
}
