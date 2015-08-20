using StarSharp.Core.Utility.FormUtils;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StarSharp.Core.Plugin
{
    public class PluginMenuBuilder : IPluginMenuBuilder
    {
        protected struct PluginMenuPartStruct
        {
            public bool Exist;
            public int Index;
            public bool IsCreate;
        }

        public virtual void AddIntoMenu(ConnectionPointContainer container, PluginConfigItem theItem, PluginMenuPath thePath, ExecutePluginCallback callback)
        {
        }


        protected ImageList GetImageList(ConnectionPointContainer container, string menuImageIndex)
        {
            int index;
            if (int.TryParse(menuImageIndex, out index) && container.ImageList.Count > 0)
                return container.ImageList[index];
            return null;
        }

        protected string GetMenuItemIndex(string locate)
        {
            int start = locate.IndexOf("(");
            int end = locate.LastIndexOf(")");
            return locate.Substring(start + 1, end - start - 1).Trim();
        }

        protected string GetMenuItemLocator(string locator)
        {
            return locator.Substring(0, locator.IndexOf("(")).Trim();
        }

        protected void LoadImage(ImageList theImageList, string image)
        {
            if (theImageList == null) return;
            if (!theImageList.Images.ContainsKey(image))
            {
                theImageList.Images.Add(image, WinFormHelper.GetImageFromResources(image));
            }
        }

        protected IList<PluginMenuItemPart> GetLeavesMenuItemParts(IList<PluginMenuItemPart> thePaths)
        {
            IList<PluginMenuItemPart> leaves = new List<PluginMenuItemPart>();
            for (int i = 1; i < thePaths.Count; i++)
            {
                leaves.Add(thePaths[i]);
            }
            return leaves;
        }

        public static PluginMenuBuilder MakeBuilder(PluginMenuPathLocateType locate)
        {
            switch (locate)
            {
                case PluginMenuPathLocateType.Menu:
                    return new PluginMenuItemBuilder();
                case PluginMenuPathLocateType.Toolbar:
                    return new PluginMenuToolbarBuilder();
                case PluginMenuPathLocateType.Navigation:
                    return new PluginMenuTreeNodeBuilder();
            }
            return new PluginMenuItemBuilder();
        }	

    }
}
