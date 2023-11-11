﻿using Bunny.NET.Client ;
using Serilog ;

namespace TestClient;

class Program
{
    static void Main()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            .Enrich.FromLogContext()
            .CreateLogger();

        Console.WriteLine("Hello, World!");

        var config = Config.SetupConfiguration();

        var bun = new BunnyClient(config.ApiToken);
    // var zones = bun.GetZones().Result;

        var statistics = bun.GetStatistics().Result;

        Console.WriteLine();
    }
}