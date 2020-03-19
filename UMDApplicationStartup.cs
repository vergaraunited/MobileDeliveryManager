using MobileDataManager;
using MobileDeliveryServer;
using System;
using UMDGeneral.Interfaces;
using UMDGeneral.Utilities;
using MobileDeliveryLogger;
using System.Configuration;
using UMDGeneral.Settings;

namespace MobileDeliveryManager
{
    class UMDApplicationStartup
    {
        static Logger logger;

        static void Main(string[] args)
        {
            var config = WinformReadSettings.GetSettings(typeof(UMDApplicationStartup));

            logger = new Logger(config.AppName, config.LogPath, config.LogLevel);
            Logger.Info($"Starting {config.AppName} {config.Version} {DateTime.Now}");
            Logger.Info($"Logfile: {config.LogPath}");

            MobileDeliveryManagerAPI det = new MobileDeliveryManagerAPI();
            det.Init(config);
            Logger.Info($"Connection details {config.AppName}:/n/tUrl:/t{config.srvSet.url}/n/tPort:/t{config.srvSet.port}");
            Server srv = new Server(config.AppName, config.srvSet.url, config.srvSet.port.ToString());
            ProcessMsgDelegateRXRaw pmRx = new ProcessMsgDelegateRXRaw(det.HandleClientCmd);
            srv.Start(pmRx);
        }
    }
}
