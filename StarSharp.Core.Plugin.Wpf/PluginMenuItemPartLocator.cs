using System;
using System.Collections.Generic;
using System.Text;

namespace StarSharp.Core.Plugin
{
	class PluginMenuItemPartLocator
	{
		public static int GetMenuLocatorIndex(PluginMenuItemLocateType type, int index, int count)
		{
			if (index < 0)
				return GetNegativeIndex(index, count);
			return GetPositiveIndex(index, count);
		}
		private static int GetNegativeIndex(int index, int count)
		{
			int plus = Math.Abs(index);
			if (count == 1)
				return 1;
			if (count == 2)
			{
				if (plus == 1) { return 1; } else { return 1; }
			}

			plus = count - plus;
			if (plus < 0) return count;

			return plus;
		}
		private static int GetPositiveIndex(int index, int count)
		{
			if (index > count)
				return count;
			return index;
		}
	}
}
