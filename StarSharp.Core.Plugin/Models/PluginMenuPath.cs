using System;
using System.Collections.Generic;

namespace StarSharp.Core.Plugin
{
    public struct PluginMenuPath
    {
        public PluginMenuPathLocateType LocateType;
        public string MenuIndex;
        public string MenuImageIndex;
        public IList<PluginMenuItemPart> MenuPathParts;
    }
}
