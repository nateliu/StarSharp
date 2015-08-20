using StarSharp.Core.Utility;
using System;
using System.Windows.Forms;

namespace StarSharp.Core.Plugin
{
    public class PluginMenuToolbarBuilder : PluginMenuItemBuilder
    {
        public override void AddIntoMenu(ConnectionPointContainer container, PluginConfigItem theItem, PluginMenuPath thePath, ExecutePluginCallback callback)
        {
            if (container.ToolStrips != null && container.ToolStrips.Count > 0)
            {
                ToolStrip theToolbar = SelectToolStrip(container, thePath);
                if (theToolbar != null)
                {
                    AddMenuItemIntoMenu(container, theToolbar.Items, theItem, thePath, thePath.MenuPathParts, callback);
                }
            }
        }
        private ToolStrip SelectToolStrip(ConnectionPointContainer container, PluginMenuPath thePath)
        {
            int index = 0;
            if (int.TryParse(thePath.MenuIndex, out index) && container.ToolStrips.Count > index)
                return container.ToolStrips[index];
            else
                foreach (ToolStrip theToolbar in container.ToolStrips)
                    if (theToolbar.Name == thePath.MenuIndex)
                        return theToolbar;
            return null;
        }

        protected override void CreateMenuEndItem(PluginMenuItemPart firstPart, ToolStripMenuItem theMenuItem, ImageList theImageList)
        {
            if (firstPart.TextStyle.ButtonType == null)
            {
                theMenuItem.Text = firstPart.TextStyle.Text;
                theMenuItem.ToolTipText = firstPart.TextStyle.ToolTipText;
                if (firstPart.TextStyle.Image != null)
                {
                    try
                    {
                        string image = firstPart.TextStyle.Image;
                        LoadImage(theImageList, image);
                        theMenuItem.ImageKey = image;
                    }
                    catch { }
                }
            }
            else
            {
                theMenuItem = ReflectUtils.CreateInstance(firstPart.TextStyle.ButtonType)
                as ToolStripMenuItem;
                theMenuItem.Text = firstPart.TextStyle.Text;
                theMenuItem.ToolTipText = firstPart.TextStyle.ToolTipText;
                if (firstPart.TextStyle.Image != null)
                {
                    try
                    {
                        string image = firstPart.TextStyle.Image;
                        LoadImage(theImageList, image);
                        theMenuItem.ImageKey = image;
                    }
                    catch { }
                }
                if (firstPart.TextStyle.Tag != null)
                {
                    try
                    {
                        theMenuItem.Alignment = (ToolStripItemAlignment)(Enum.Parse(
                            typeof(ToolStripItemAlignment), firstPart.TextStyle.Tag.Split(',')[0], true
                            ));
                    }
                    catch { }
                }
            }
        }
	
    }
}
