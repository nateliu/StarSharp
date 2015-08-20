using System;
using System.IO;
using System.Windows.Forms;

namespace StarSharp.Core.Utility
{
    public class LogUtils
    {
        private static string GetLogFolder()
        {
            string baseDir = Application.StartupPath;
            return Path.Combine(baseDir, "Logs");
        }

        private static string GetLogErrorFile()
        {
            return GetLogErrorFile(DateTime.Now);
        }

        private static string GetLogErrorFile(DateTime day)
        {
            return Path.Combine(GetLogFolder(), "Log_" + day.ToString("yyyyMMdd") + ".txt");
        }

        private static string GetLogInfoFile()
        {
            return GetLogInfoFile(DateTime.Now);
        }

        private static string GetLogInfoFile(DateTime day)
        {
            return Path.Combine(GetLogFolder(), "Log_Info_" + day.ToString("yyyyMMdd") + ".txt");
        }

        private static void InsureFolderExist()
        {
            if (!Directory.Exists(GetLogFolder()))
                Directory.CreateDirectory(GetLogFolder());
        }

        public static void LogError(string description, Exception ex)
        {
            try
            {
                InsureFolderExist();
                string logFile = GetLogErrorFile();
                using (StreamWriter w = new StreamWriter(logFile, true))
                {
                    // ouput user info
                    w.WriteLine(GetLogUserInfo());
                    WalkWriteError(w, ex);
                    w.WriteLine();
                }
            }
            catch
            {
                // email
            }
        }

        private static string GetLogUserInfo()
        {
            return "\r\n" + System.Environment.UserName + "@" + System.Environment.MachineName
                + " " + System.Environment.UserDomainName;
        }

        public static void LogError(Exception ex)
        {
            LogError("", ex);
        }

        public static void LogInfo(string message, Type whereAreYou)
        {
            string user = "";
            string where = whereAreYou == null ? "" : whereAreYou.FullName;
            try
            {
                InsureFolderExist();
                string logFile = GetLogInfoFile();
                using (StreamWriter w = new StreamWriter(logFile, true))
                {
                    w.WriteLine(string.Format("{0}\tuser:{1}\twhere:{2}\tmessage:{3}",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        user,
                        where,
                        message
                    ));
                    w.WriteLine();
                }
            }
            catch
            {
                // email
            }
        }

        private static void WalkWriteError(StreamWriter w, Exception ex)
        {
            if (ex != null)
            {
                w.WriteLine(string.Format("{0}\tException: {1}",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    ex.StackTrace == null || ex.StackTrace == "" ?
                    ex.Message :
                    ex.Message + System.Environment.NewLine + ex.StackTrace
                    ));
                if (ex.InnerException != null)
                {
                    WalkWriteError(w, ex.InnerException);
                }
            }
        }

        public static string GetLogError(DateTime day)
        {
            string logFile = GetLogErrorFile(day);
            if (!File.Exists(logFile))
                return null;
            using (StreamReader r = new StreamReader(logFile))
            {
                return r.ReadToEnd();
            }
        }

        public static void LogDebug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message, "DataManager");
        }
    }
}
