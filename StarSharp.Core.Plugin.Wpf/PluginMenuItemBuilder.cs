using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace StarSharp.Core.Plugin
{
    public class PluginMenuItemBuilder : PluginMenuBuilder
    {
        public override void AddIntoMenu(ConnectionPointContainer container, PluginConfigItem theItem, PluginMenuPath thePath, ExecutePluginCallback callback)
        {
            if (null != container.Menus && container.Menus.Count > 0)
            {
                Menu menu = SelectMenu(container, thePath);
                if (null != menu)
                {
                    AddMenuItemIntoMenu(container, menu.Items, theItem, thePath, thePath.MenuPathParts, callback);
                }
            }
        }

        private Menu SelectMenu(ConnectionPointContainer container, PluginMenuPath thePath)
        {
            int index = 0;
            if (int.TryParse(thePath.MenuIndex, out index) && container.Menus.Count > index)
                return container.Menus[index];
            else
                foreach (Menu theMenu in container.Menus)
                    if (theMenu.Name == thePath.MenuIndex)
                        return theMenu;
            return null;
        }

        protected void AddMenuItemIntoMenu(ConnectionPointContainer container, ItemCollection menuItemCollection, PluginConfigItem theItem, PluginMenuPath thePath, IList<PluginMenuItemPart> thePaths, ExecutePluginCallback callback)
        {
            if (thePaths.Count < 1) return;

            PluginMenuItemPart firstPart = thePaths[0];
            PluginMenuPartStruct menuStruct = GetMenuItemIndex(firstPart, menuItemCollection);
            IList<PluginMenuItemPart> otherParts = GetLeavesMenuItemParts(thePaths);

            if (!menuStruct.IsCreate)
            {
                AddMenuItemIntoMenu(container, (menuItemCollection[menuStruct.Index] as
                    MenuItem).Items,
                    theItem, thePath, otherParts, callback);

            }
            else
            {
                if (firstPart.TextStyle.Text.Trim() == "-")
                {
                    menuItemCollection.Insert(
                        menuStruct.Index,
                        new Separator()
                    );
                    return;
                }
                MenuItem theMenuItem = new MenuItem() { Header = firstPart.TextStyle.Text };

                CreateMenuEndItem(firstPart, theMenuItem, GetImageList(container, thePath.MenuImageIndex));
                menuItemCollection.Insert(
                    menuStruct.Index,
                    theMenuItem
                );

                if (thePaths.Count > 1)
                {
                    AddMenuItemIntoMenu(container, theMenuItem.Items, theItem, thePath, otherParts, callback);
                }
                else
                {
                    //theMenuItem.Name = theItem.Url;
                    theMenuItem.Tag = new object[] { theItem, callback };
                    string[] behaviors = theItem.Behavior.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string action in behaviors)
                    {
                        PluginConfigItemBehaviorMode theBehavior = (PluginConfigItemBehaviorMode)Enum.Parse(typeof(PluginConfigItemBehaviorMode), action, true);
                        switch (theBehavior)
                        {
                            case PluginConfigItemBehaviorMode.Click:
                                theMenuItem.Click -= TheMenuItem_Click;
                                theMenuItem.Click += TheMenuItem_Click;
                                break;
                            case PluginConfigItemBehaviorMode.MouseOver:
                                theMenuItem.MouseMove -= TheMenuItem_MouseMove;
                                theMenuItem.MouseMove += TheMenuItem_MouseMove;
                                break;
                        }
                    }
                    return;
                }
            }
        }

        protected virtual void CreateMenuEndItem(PluginMenuItemPart firstPart, MenuItem theMenuItem, Image theImageList)
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
        }

        private int GetMenuItemIndexById(ItemCollection menuItemCollection, string menuItemIndex)
        {
            for (int i = 0; i < menuItemCollection.Count; i++)
            {
                if ((menuItemCollection[i] as MenuItem).Header.ToString().Trim() == menuItemIndex)
                    return i;
            }
            return menuItemCollection.Count;
        }

        protected PluginMenuPartStruct GetMenuItemIndex(PluginMenuItemPart theMenuItemPart, ItemCollection nodes)
        {
            PluginMenuPartStruct theMenuItem = new PluginMenuPartStruct();
            int index = 0;
            string menuItemIndex = GetMenuItemIndex(theMenuItemPart.Locate);
            PluginMenuItemLocateType menuItemLocationType = (PluginMenuItemLocateType)(
                Enum.Parse(typeof(PluginMenuItemLocateType),
                    GetMenuItemLocator(theMenuItemPart.Locate),
                    true
                    )
                );
            theMenuItem.IsCreate = menuItemLocationType == PluginMenuItemLocateType.Create;

            if (int.TryParse(menuItemIndex, out index))
            {
                theMenuItem.Index = PluginMenuItemPartLocator.GetMenuLocatorIndex(menuItemLocationType, index, nodes.Count);
            }
            else if (menuItemIndex.Length == 0)
            {
                theMenuItem.Index = nodes.Count;
            }
            else
            {
                int realIndex = GetMenuItemIndexById(nodes, menuItemIndex);
                theMenuItem.Index = PluginMenuItemPartLocator.GetMenuLocatorIndex(
                    menuItemLocationType,
                    realIndex,
                    nodes.Count
                    );
                // Make only support ID
                if (menuItemLocationType == PluginMenuItemLocateType.Make)
                {
                    if (realIndex >= 0 && realIndex < nodes.Count)
                        theMenuItem.IsCreate = false;
                    else
                        theMenuItem.IsCreate = true;
                }
            }
            theMenuItem.Exist = theMenuItem.Index < nodes.Count
                    && theMenuItem.Index >= 0;
            return theMenuItem;
        }

        protected virtual void TheMenuItem_Click(object sender, EventArgs e)
        {
            MenuItem theItem = sender as MenuItem;
            if (theItem != null && theItem.Tag != null && theItem.Tag is object[])
            {
                object[] args = theItem.Tag as object[];
                if (args[1] != null)
                {
                    PluginInvokeEventArgs a = new PluginInvokeEventArgs();
                    a.ConfigItem = (PluginConfigItem)(args[0]);
                    a.ConfigItem.EventSender = sender;
                    (args[1] as ExecutePluginCallback)(sender, a);
                }
            }
        }

        protected virtual void TheMenuItem_MouseMove(object sender, EventArgs e)
        {
            TheMenuItem_Click(sender, e);
        }
    }
}
