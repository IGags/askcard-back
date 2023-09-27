using System;
using Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;

namespace Api;

public class Startup
{
    /// <summary>
    /// DI
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCore();
    }

    /// <summary>
    /// Миддлвары
    /// </summary>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseCore(env);

        app.UseEndpoints(x => x.MapControllers());  
    }
}