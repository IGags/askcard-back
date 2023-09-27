using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Api;

public class Program
{
    /// <summary>
    /// Точка входа
    /// </summary>
    public static async Task Main(params string[] args)
    {
        var host = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(x => x.UseStartup<Startup>()).Build();
        await host.RunAsync();
    }
}

