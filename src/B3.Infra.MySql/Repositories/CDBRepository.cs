using B3.Domain.Models.Common;
using B3.Domain.Models.DTO;
using B3.Infra.MySql.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace B3.Infra.MySql.Repositories
{
    public class CDBRepository : ICDBRepository
    {
        private readonly ILogger _logger;
        private const decimal CDI = 0.009m; // CDI de 0,9%
        private const decimal TB = 1.08m;   // Taxa Banco de 108%

        public CDBRepository(ILogger<CDBRepository> logger)
        {
            _logger = logger;
        }

        public Task<CDBDTO> CalculateCDB(CDBDTO request)
        {
            if (request.InitialValue <= 0 || request.DeadlineInMonths <= 0)
            {
                throw new ArgumentException("Valores iniciais e prazos devem ser positivos.");
            }

            decimal grossValue = request.InitialValue;
            for (int month = 1; month <= request.DeadlineInMonths; month++)
            {
                grossValue *= (1 + CDI * TB);
            }

            decimal netValue = ApplyTax(grossValue, request.DeadlineInMonths);

            return Task.FromResult(new CDBDTO
            {
                GrossValue = grossValue,
                NetValue = netValue
            });
        }

        private decimal ApplyTax(decimal grossValue, int months)
        {
            decimal taxRate;

            if (months <= 6)
                taxRate = 0.225m; // 22.5%
            else if (months <= 12)
                taxRate = 0.20m; // 20%
            else if (months <= 24)
                taxRate = 0.175m; // 17.5%
            else
                taxRate = 0.15m; // 15%

            return grossValue * (1 - taxRate);
        }

    }

}
