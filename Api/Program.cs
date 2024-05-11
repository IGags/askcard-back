using System.Net;
using System.Threading.Tasks;
using Core;
using Core.Smtp.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api;

public class Program
{
    /// <summary>
    /// Точка входа
    /// </summary>
    public static async Task Main(params string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(x =>
        {
            x.UseStartup<Startup>();
            //x.ConfigureKestrel(y => y.Listen(IPAddress.Parse("26.231.63.180"), 5111));
        });
        var host = hostBuilder.Build();
        await host.RunOneTimeLogicAsync();
        await host.RunAsync();
    }
}

