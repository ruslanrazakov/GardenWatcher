using System;

namespace Server.BusinessContext
{
    /// <summary>
    /// Turns on some helper bash scripts
    /// </summary>
    public interface IBashService
    {
        /// <summary>
        /// Starts bash script, that makes photos with linux fswebcam application
        /// with some interval
        /// </summary>
        void StartPhotoProcessBashScript();
    }
}
