using System;

namespace StarSharp.Core.Plugin
{
    /// <summary>
    /// This class is just stay here right now, my consider is to get remote pluginconfig.xml to use it.
    /// </summary>
    public  class PluginServiceProxy
    {
        //public PluginServiceProxy()
        //{
        //    try
        //    {
        //        this.Url = DMAppArgHelper.GetWebServiceAbsUrl(ShellRegion.US,
        //            "FileService_PluginService");
        //    }
        //    catch { }
        //    // Set [SoapHeader("Credentials")]
        //    this.AuthenticHeaderValue = new Morningstar.DataManagerPro.DataAccess.AddinService.AuthenticHeader();
        //    Morningstar.DataManagerPro.ForWinIE.AuthenticHeader ah = DMAppArgHelper.GetAuthenticHeader();
        //    if (ah != null)
        //    {
        //        this.AuthenticHeaderValue.UniqueID = ah.UniqueID;
        //        this.AuthenticHeaderValue.Verification = ah.Verification;
        //        this.AuthenticHeaderValue.UserId = ah.UserId;
        //        this.AuthenticHeaderValue.SoapUserInfo = ah.SoapUserInfo;
        //    }
        //    else
        //    {
        //        this.AuthenticHeaderValue.UniqueID = "DataManager.NET.GetSystemList in Login";
        //    }
        //    this.EnableDecompression = true;
        //}
        static PluginServiceProxy _instance = new PluginServiceProxy();
        public static PluginServiceProxy GetInstance()
        {
            return _instance;
        }
    }
}
