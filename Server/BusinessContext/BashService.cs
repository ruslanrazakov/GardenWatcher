using Microsoft.Extensions.Logging;
using Server.BusinessContext;
using System;
using System.Diagnostics;

namespace Server.BusinessContext
{
    public class BashService : IBashService
    {
        ILogger _logger;
        public BashService(ILogger<BashService> logger)
        {
            _logger = logger;
            StartPhotoProcessBashScript();
        }

        public void StartPhotoProcessBashScript()
        {
            try
            {
                var process = new Process()
                {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "webcam.sh",
                            RedirectStandardOutput = true,
                            UseShellExecute = false,
                            CreateNoWindow = true,
                        }
                };
                process.Start();
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Warning, "BASH SERVICE STARTING ERROR: " + ex.Message);
            }
        }
    }
}