using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace StarSharp.Core.Utility.FormUtils
{
    public static class WinFormHelper
    {
        public static void OutputAllResources()
        {
            System.Reflection.Assembly thisExe;
            thisExe = System.Reflection.Assembly.GetExecutingAssembly();
            string[] resources = thisExe.GetManifestResourceNames();
            string list = "";

            //   Build   the   string   of   resources.   
            foreach (string resource in resources)
                list += resource + "\r\n";
            System.Diagnostics.Debug.WriteLine(list);

        }

        public static string GetTextFromResources(string name)
        {
            //return new ResourceManager(typeof(Resources)).GetObject(name) as string;
            return "";
        }

        public static Image GetImageFromResources(string name)
        {
            //return new ResourceManager(typeof(Resources)).GetObject(name) as Image;
            return null;
        }

        public static byte[] GetFileFormResources(string name)
        {
            //return new ResourceManager(typeof(Resources)).GetObject(name) as byte[];
            return null;
        }

        public static Stream GetEmbededFile(string name)
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(name);
        }

        public static bool IsStartWithUrl(Uri u1, string u2)
        {
            string u = u1.AbsoluteUri;
            if (u.StartsWith("http") &&
                !u2.StartsWith("http"))
            {
                u = u1.AbsolutePath;
            }
            if (u.StartsWith("/") && !u2.StartsWith("/"))
            {
                u2 = "/" + u2;
            }
            return u.ToString().ToLower().StartsWith(u2.ToLower());
        }
        public static bool IsIndexOfUrl(Uri u1, string u2)
        {
            return u1.AbsoluteUri.ToLower().IndexOf(u2.ToLower()) > -1;
        }

        public static string GetFormattedUrl(string originalUrl)
        {
            string url = originalUrl;

            if (url.StartsWith("http://"))
            {
                url = url.Substring("http://".Length);
                url = url.TrimStart('/');
                if (url.IndexOf("?") == -1)
                {
                    url = url.Replace("//", "/");
                }
                else
                {
                    string path = url.Substring(0, url.IndexOf("?"));
                    string args = url.Substring(url.IndexOf("?"));
                    path = path.Replace("//", "/");
                    url = path + args;
                }
                return "http://" + url;
            }
            else
            {
                return url;
            }
        }

        public static AutoResetEvent are = new AutoResetEvent(false);

        public delegate void InvodeMethodhandler(Form f, string methodName, params object[] parms);

        public static void InvokeMethod(Form f, string methodName, params object[] parms)
        {
            Type t = f.GetType();
            try
            {
                MethodInfo mi = t.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                mi.Invoke(f, parms);
            }
            catch
            {
                foreach (MethodInfo mi in t.GetMethods())
                {
                    if (mi.Name == methodName
                        && mi.GetParameters().Length == parms.Length)
                    {
                        mi.Invoke(f, parms);
                        break;
                    }
                }
            }
            are.Set();
        }    
    }   
}
