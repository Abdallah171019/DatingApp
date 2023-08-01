using MongoDB.Driver;
using API.Models;
using API.Services;
using API.Interfaces;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServiceExtensions(this IServiceCollection services,
                                                                          IConfiguration config){
            services.Configure<DatabaseSettings>(
            config.GetSection("MongoDB"));
            services.AddSingleton<UserService>(); /*DI container will create a single instance of that service throughout the lifetime of the application.
                                                    Any subsequent requests for that service will receive the same instance.*/
            services.AddCors();// To make the browser able to read the request without errors.
            services.AddScoped<ITokenService,TokenService>(); //ITokenService is added to the AddScoped for unit testing issues.
            return services;
        }
    }
}