using B3.Application.Services;
using B3.Domain.Models.DTO;
using B3.Infra.MySql.Interfaces;
using B3.Infra.MySql.Repositories;
using Castle.Core.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3.Application.Tests
{
    public static class DependencyInjectionStartup
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            var loggerMock = new Mock<ILogger<CDBRepository>>();

            services.AddScoped<ILogger<CDBRepository>>(_ => loggerMock.Object);

            // Registro da implementação de ICDBRepository
            services.AddScoped<ICDBRepository, CDBRepository>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }


}
