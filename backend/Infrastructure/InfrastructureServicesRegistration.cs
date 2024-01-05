using System;
using Application.Contracts.Infrastructure.Mail;
using Application.Contracts.Infrastructure.Photo;
using Application.Models.Identity;
using Application.Models.Mail;
using Infrastructure.Mail;
using Infrastructure.Photos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
   public  static class InfrastructureServiceRegistration
{
     public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.Configure<ServerSettings>(configuration.GetSection("ServerSettings"));
            services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));
            services.AddDistributedMemoryCache();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            return services;
        }
}}
