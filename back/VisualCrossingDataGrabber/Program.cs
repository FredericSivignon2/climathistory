using log4net;
using log4net.Config;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VisualCrossingDataGrabber;
using VisualCrossingDataGrabber.Services;

ILog log = LogManager.GetLogger(typeof(Program));
XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));

// Renommer les fichiers si besoin en PowerShell:
// Get-ChildItem -Filter 'temperatures_paris_*.json' | Rename-Item -NewName {$_.name -replace 'paris','bordeaux' }


log.Info("Starting...");

var app = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<GrabberSettings>(hostContext.Configuration.GetSection("GrabberSettings"));
        services.AddTransient<IVisualCrossingReader, VisualCrossingReader>();
        services.AddTransient<IDataGrabber, DataGrabber>();
    }).Build();

var service = app.Services.GetRequiredService<IDataGrabber>();
await service.Run();

log.Info("Completed. Press a key to close the program.");
Console.ReadKey();

