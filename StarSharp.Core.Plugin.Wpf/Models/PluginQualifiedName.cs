using System;

namespace StarSharp.Core.Plugin
{
    public struct PluginQualifiedName
    {
        public string ClassName;
        public string AssemblyPath;
        public PluginLoadProtocol LoadProtocol;
    }
}
