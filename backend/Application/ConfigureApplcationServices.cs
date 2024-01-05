using System;
using System.Reflection;
using Application.Contracts.Persistence;
using Application.Profiles;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Application
{
   public static class ApplicationServicesRegistration
{
    public static IServiceCollection ConfigureApplicationService(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure Serilog logging
        Log.Logger = new LoggerConfiguration()
            // .MinimumLevel.Debug()
            .MinimumLevel.Information()
            .WriteTo.File("Log/RideshareErrorLog.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Console()
            .CreateLogger();

        services.AddScoped<IMapper>(
            provider => {
                var unitOfWork = provider.GetRequiredService<IUnitOfWork>();

                var profile = new MappingProfile();
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(profile);
                });
                return configuration.CreateMapper();
            }
        );
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}

}