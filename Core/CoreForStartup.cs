using System.Text;
using Core.Caching;
using Core.Migrations;
using Core.RepositoryBase.Connection;
using Core.RepositoryBase.Connection.Interfaces;
using Core.Settings;
using Core.Smtp;
using Core.Smtp.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Core;

/// <summary>
/// Добавление кора
/// </summary>
public static class CoreForStartup
{
    public static IServiceCollection AddCore(this IServiceCollection collection,
         IConfiguration configuration)
    {
        collection.AddControllers(opts =>
        {
        }).AddNewtonsoftJson();
        
        collection.AddSwaggerGen(swagger =>
        {
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
        
        collection.AddCoreSettings();

        collection.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        collection.AddCache();
        collection.AddTransient<ISmtpSender, SmtpSender>();
        collection.AddMigrationRunner(configuration);
        collection.AddLogging(x =>
        {
            x.AddConsole();
        });

        var section = configuration.GetSection("IdentitySettings");
        collection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = section.GetSection("Issuer").Value,
                ValidateAudience = true,
                ValidAudience = section.GetSection("Audience").Value,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(section.GetSection("SigningKey").Value)),
                ValidateLifetime = true
            };
        });
        
        return collection;
    }
}