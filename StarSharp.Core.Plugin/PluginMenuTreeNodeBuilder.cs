using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StarSharp.Core.Plugin
{
    public class PluginMenuTreeNodeBuilder : PluginMenuBuilder
    {
        public override void AddIntoMenu(ConnectionPointContainer container, PluginConfigItem theItem, PluginMenuPath thePath, ExecutePluginCallback callback)
        {
            if (container.Navigations != null && container.Navigations.Count > 0)
            {
                TreeView theTree = SelectNavigation(container, thePath);
                if (theTree != null)
                {
                    AddTreeNodeIntoTree(container, theTree.Nodes, theItem, thePath, thePath.MenuPathParts, callback);
                }
            }
        }

        private TreeView SelectNavigation(ConnectionPointContainer container, PluginMenuPath thePath)
        {
            int index = 0;
            if (int.TryParse(thePath.MenuIndex, out index) && index < container.Navigations.Count)
                return container.Navigations[index];
            return null;
        }

        protected void AddTreeNodeIntoTree(ConnectionPointContainer container, TreeNodeCollection nodes, PluginConfigItem theItem, PluginMenuPath thePath, IList<PluginMenuItemPart> thePaths, ExecutePluginCallback callback)
        {
            if (thePaths.Count < 1) return;

            PluginMenuItemPart firstPart = thePaths[0];
            PluginMenuPartStruct menuStruct = GetMenuItemIndex(firstPart, nodes);
            IList<PluginMenuItemPart> otherParts = GetLeavesMenuItemParts(thePaths);

            if (!menuStruct.IsCreate)
            {
                AddTreeNodeIntoTree(container, nodes[menuStruct.Index].Nodes,
                       theItem, thePath, otherParts, callback);
            }
            else
            {
                TreeNode theMenuItem = new TreeNode(firstPart.TextStyle.Text);
                CreateMenuEndItem(firstPart, theMenuItem, GetImageList(container, thePath.MenuImageIndex));

                nodes.Insert(
                    menuStruct.Index,
                    theMenuItem
                );

                if (thePaths.Count > 1)
                {
                    AddTreeNodeIntoTree(container, theMenuItem.Nodes, theItem, thePath, otherParts, callback);
                }
                else
                {
                    theMenuItem.Name = theItem.Url;
                    theMenuItem.Tag = new object[] { theItem, callback };

                    theMenuItem.TreeView.NodeMouseClick -= new TreeNodeMouseClickEventHandler(TreeView_NodeMouseClick);
                    theMenuItem.TreeView.NodeMouseClick += new TreeNodeMouseClickEventHandler(TreeView_NodeMouseClick);

                    return;
                }
            }
        }

        static void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode theItem = e.Node;
            if (theItem != null)
            {
                object[] args = theItem.Tag as object[];
                if (args != null && args[1] != null)
                {
                    PluginInvokeEventArgs a = new PluginInvokeEventArgs();
                    a.ConfigItem = (PluginConfigItem)(args[0]);
                    a.ConfigItem.EventSender = sender;
                    (args[1] as ExecutePluginCallback)(sender, a);
                }
            }
        }

        protected virtual void CreateMenuEndItem(PluginMenuItemPart firstPart, TreeNode theMenuItem, ImageList theImageList)
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
                    theMenuItem.SelectedImageKey = image;
                }
                catch { }
            }
        }

        protected PluginMenuPartStruct GetMenuItemIndex(PluginMenuItemPart theMenuItemPart, TreeNodeCollection nodes)
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

        private int GetMenuItemIndexById(TreeNodeCollection nodes, string menuItemIndex)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Text == menuItemIndex)
                    return i;
            }
            return nodes.Count;
        }
    }
}
