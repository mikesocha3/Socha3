using System;
using System.Diagnostics;
using System.Net;
using SysIO = System.IO;

namespace Socha3.Common.Path
{
    public static class WebUtil
    {
        public static void ExecuteWebFile(string url, ProcessStartInfo info = null)
        {
            var ext = SysIO.Path.GetExtension(url);
            var client = new WebClient();
            var tempFile = $"{SysIO.Path.GetTempFileName()}.{ext}"; 
            client.DownloadFile(url, tempFile);
            if (info == null)
                info = new ProcessStartInfo();

            info.FileName = tempFile;
            Process.Start(info);
        }
    }
}