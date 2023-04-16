using MyApp.Data;
using MyApp.Services.Domain.Implementations;
using MyApp.Services.Domain.Interfaces;
using MyApp.Services.Factories.Implementations;
using MyApp.Services.Factories.Interfaces;

namespace MyApp.Web;

public static class ServiceExtensions
{
    public static IServiceCollection RegisterMyAppServices(this IServiceCollection services)
    {
        services.AddSingleton<DataContext>();
        
        services.AddScoped<IDataAccess, DataAccess>();

        services.AddSingleton<IServiceFactory, ServiceFactory>();
        
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}