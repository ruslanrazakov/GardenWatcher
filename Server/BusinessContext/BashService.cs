using Server.BusinessContext;
using System.Diagnostics;

public class BashService : IBashService
{
    public BashService()
    {
        StartPhotoProcessBashScript();   
    }   

    public void StartPhotoProcessBashScript()
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
}