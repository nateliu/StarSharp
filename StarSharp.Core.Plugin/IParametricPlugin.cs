using System;
using System.Collections.Generic;

namespace StarSharp.Core.Plugin
{
    public interface IParametricPlugin
    {
        IDictionary<string, string> PluginInitArgs { get; set; }
    }
}
