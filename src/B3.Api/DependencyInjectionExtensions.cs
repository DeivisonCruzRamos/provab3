using B3.Application;
using B3.Infra.MySql;
using B3.Service;

namespace B3.Api;
public static class DependencyInjectionExtensions
{
    public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationModule();
        services.AddServiceModule();
        services.AddInfraMySqlModule();
    }
}