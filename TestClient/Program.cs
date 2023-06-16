// See https://aka.ms/new-console-template for more information

using Bunny.NET.Client;
using Serilog;
using TestClient;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateLogger();

Console.WriteLine("Hello, World!");

var config = Config.SetupConfiguration();

var bun = new BunnyClient(config.ApiToken);
var zones = bun.GetZones().Result;

Console.WriteLine();
