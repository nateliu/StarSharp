using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSharp.Core.Utility
{
    /// <summary>
    ///  unzip class
    /// THIS class HAS problem -- package size = 4194302kB
    /// Reserve this class for DOWN compatible WHEN unzip the old version
    /// </summary>

    public class UnZipHelper
    {
        public static bool IsMe(string zip, string sourceDirName, string password)
        {
            ZipInputStream s = null;
            ZipEntry theEntry = null;
            using (s = new ZipInputStream(File.OpenRead(zip)))
            {
                s.Password = password;
                if ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.Name != String.Empty)
                    {
                        string f = sourceDirName + "/";
                        if (theEntry.Name == f)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// unzip to specify dir
        /// </summary>
        /// <param name="fileToUpZip"></param>
        /// <param name="zipedFolder"></param>
        /// <param name="password"></param>
        public static void UnZip(string fileToUpZip, string zipedFolder, string password)
        {
            if (!File.Exists(fileToUpZip))
            {
                return;
            }

            if (!Directory.Exists(zipedFolder))
            {
                Directory.CreateDirectory(zipedFolder);
            }

            ZipInputStream s = null;
            ZipEntry theEntry = null;

            string fileName;
            FileStream streamWriter = null;
            try
            {
                s = new ZipInputStream(File.OpenRead(fileToUpZip));
                s.Password = password;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.Name != String.Empty)
                    {
                        fileName = Path.Combine(zipedFolder, theEntry.Name);
                        if (fileName.EndsWith("/") || fileName.EndsWith("\\"))
                        {
                            Directory.CreateDirectory(fileName);
                            continue;
                        }

                        try { streamWriter = File.Create(fileName); }
                        catch (PathTooLongException)
                        {
                            //ShellUtils.ShowWarn("The path too LONG. Please insure the paths must be less than 248 characters"
                            //     );
                            continue;
                        }
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter = null;
                }
                if (theEntry != null)
                {
                    theEntry = null;
                }
                if (s != null)
                {
                    s.Close();
                    s = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
        }
    }

}
