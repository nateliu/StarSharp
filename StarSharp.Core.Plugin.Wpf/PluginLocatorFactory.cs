using System;

namespace StarSharp.Core.Plugin
{
    public class PluginLocatorFactory
    {
        static PluginLocatorFactory _instance = new PluginLocatorFactory();
        public static PluginLocatorFactory GetInstance()
        {
            return _instance;
        }

        public static IPluginLocator GetLocator(PluginLoadProtocol theProtocol)
        {
            if (theProtocol != PluginLoadProtocol.Unknown)
            {
                if (theProtocol == PluginLoadProtocol.Local)
                    return new PluginLocalLocator(theProtocol);
                else if (theProtocol == PluginLoadProtocol.Remote)
                    return new PluginRemoteLocator(theProtocol);
            }
            throw new NotSupportedException();
        }
    }
}
