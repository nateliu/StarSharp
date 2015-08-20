using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StarSharp.Core.Utility
{
    public class ReflectUtils
    {
        public static string GetPropertyValueByName(object obj, string name)
        {
            PropertyInfo p = obj.GetType().GetProperty(name);
            if (p == null)
                return null;
            return p.GetValue(obj, null).ToString();
        }

        public static Type GetType(string type)
        {
            if (string.IsNullOrEmpty(type)) return null;
            if (type.IndexOf(',') == -1) return Type.GetType(type);
            string asmName = type.Substring(type.IndexOf(",") + 1).Trim();
            foreach (Assembly theAsm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (theAsm.GetName().Name == asmName)
                {
                    return theAsm.GetType(type.Substring(0, type.IndexOf(",")).Trim());
                }
            }
            return null;
        }

        public static object CreateInstance(string type)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            bool found = false;
            foreach (Assembly theAsm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (theAsm == asm)
                {
                    found = true;
                    break;
                }
            }
            if (found)
            {
                object obj = asm.CreateInstance(type, true);

                if (obj == null)
                {
                    string[] names = type.Trim().Split(',');
                    string n1 = names[0].Trim();
                    string n2 = names[1].Trim() + ",";
                    if (names.Length > 1)
                    {
                        foreach (Assembly theAsm in AppDomain.CurrentDomain.GetAssemblies())
                        {
                            if (theAsm.FullName.StartsWith(n2))
                            {
                                obj = theAsm.CreateInstance(n1, true);
                                break;
                            }
                        }
                    }
                }

                if (obj == null)
                {
                    try
                    {
                        obj = Activator.CreateInstance(Type.GetType(type));
                    }
                    catch { }
                }
                return obj;
            }
            return null;
        }
    }
}
