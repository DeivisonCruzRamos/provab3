using B3.Service.Interfaces;
using B3.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace B3.Service;

public static class ServiceDependencyInjection
{
    public static void AddServiceModule(this IServiceCollection services)
    {
        services.AddScoped<ICDBService, CDBService>();
    }
}
