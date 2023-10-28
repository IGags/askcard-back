using System;
using Core;
using Dal;
using Logic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;

namespace Api;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    /// <summary>
    /// DI
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCore(_configuration);
        services.AddDal();
        services.AddLogic();
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