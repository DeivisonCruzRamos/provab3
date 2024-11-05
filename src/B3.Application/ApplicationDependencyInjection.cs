using B3.Application.Interfaces;
using B3.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace B3.Application;
public static class ApplicationDependencyInjection
{
    public static void AddApplicationModule(this IServiceCollection services)
    {
        services.AddScoped<ICDBApplication, CDBApplication>();
    }
}