using B3.Infra.MySql.Interfaces;
using B3.Infra.MySql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System.Data;

namespace B3.Infra.MySql
{
    public static class InfraDependencyInjection
    {
        public static void AddInfraMySqlModule(this IServiceCollection services)
        {
            services.AddScoped<ICDBRepository, CDBRepository>();
        }
    }
}