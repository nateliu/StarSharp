using StarSharp.Core.Utility;
using System;
using System.Windows.Controls;

namespace StarSharp.Core.Plugin
{
    public class PluginMenuToolbarBuilder : PluginMenuItemBuilder
    {
        public override void AddIntoMenu(ConnectionPointContainer container, PluginConfigItem theItem, PluginMenuPath thePath, ExecutePluginCallback callback)
        {
            if (container.ToolStrips != null && container.ToolStrips.Count > 0)
            {
                ToolBar theToolbar = SelectToolStrip(container, thePath);
                if (theToolbar != null)
                {
                    AddMenuItemIntoMenu(container, theToolbar.Items, theItem, thePath, thePath.MenuPathParts, callback);
                }
            }
        }
        private ToolBar SelectToolStrip(ConnectionPointContainer container, PluginMenuPath thePath)
        {
            int index = 0;
            if (int.TryParse(thePath.MenuIndex, out index) && container.ToolStrips.Count > index)
                return container.ToolStrips[index];
            else
                foreach (ToolBar theToolbar in container.ToolStrips)
                    if (theToolbar.Name == thePath.MenuIndex)
                        return theToolbar;
            return null;
        }

        protected void CreateMenuEndItem(PluginMenuItemPart firstPart, ToolBar theMenuItem, Image theImageList)
        {
            if (firstPart.TextStyle.ButtonType == null)
            {
                theMenuItem.Header = firstPart.TextStyle.Text;
                theMenuItem.ToolTip = firstPart.TextStyle.ToolTipText;
                if (firstPart.TextStyle.Image != null)
                {
                    try
                    {
                        string image = firstPart.TextStyle.Image;
                        LoadImage(theImageList, image);
                        //theMenuItem.ImageKey = image;
                    }
                    catch { }
                }
            }
            else
            {
                theMenuItem = ReflectUtils.CreateInstance(firstPart.TextStyle.ButtonType)
                as ToolBar;
                theMenuItem.Header = firstPart.TextStyle.Text;
                theMenuItem.ToolTip = firstPart.TextStyle.ToolTipText;
                if (firstPart.TextStyle.Image != null)
                {
                    try
                    {
                        string image = firstPart.TextStyle.Image;
                        LoadImage(theImageList, image);
                        //theMenuItem.ImageKey = image;
                    }
                    catch { }
                }
                if (firstPart.TextStyle.Tag != null)
                {
                    try
                    {
                        //theMenuItem.Alignment = (ToolStripItemAlignment)(Enum.Parse(
                        //    typeof(ToolStripItemAlignment), firstPart.TextStyle.Tag.Split(',')[0], true
                        //    ));
                    }
                    catch { }
                }
            }
        }
	
    }
}
