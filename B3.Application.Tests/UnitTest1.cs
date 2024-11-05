using B3.Application.Contracts.Request;
using B3.Domain.Models.DTO;
using B3.Infra.MySql.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace B3.Application.Tests.Services
{
    public class CDBApplicationTests
    {
        private readonly ICDBRepository _cdbRepository;

        public CDBApplicationTests()
        {
            var services = DependencyInjectionStartup.ConfigureServices();
            _cdbRepository = services.GetRequiredService<ICDBRepository>();
        }

        [Fact]
        public async Task CalculateCDB_ShouldReturnExpectedValues_WhenValidInput()
        {
            var request = new CDBDTO { InitialValue = 1000, DeadlineInMonths = 12 };
            var result = await _cdbRepository.CalculateCDB(request);
            result.Should().NotBeNull();
            result.GrossValue.Should().BeGreaterThan(0);
            result.NetValue.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task CalculateCDB_ShouldReturnCorrectGrossValue()
        {
            var request = new CDBDTO { InitialValue = 2000, DeadlineInMonths = 24 };
            var expectedGrossValue = CalculateExpectedGrossValue(2000, 24);
            var result = await _cdbRepository.CalculateCDB(request);
            result.GrossValue.Should().BeApproximately(expectedGrossValue, 0.01m);
        }

        private decimal CalculateExpectedGrossValue(decimal initialValue, int months)
        {
            decimal grossValue = initialValue;
            for (int month = 1; month <= months; month++)
            {
                grossValue *= (1 + (0.009m * 1.08m));
            }
            return grossValue;
        }

        [Fact]
        public async Task CalculateCDB_ShouldReturnCorrectNetValue_AfterTax()
        {
            var request = new CDBDTO { InitialValue = 1000, DeadlineInMonths = 36 };
            var result = await _cdbRepository.CalculateCDB(request);
            result.NetValue.Should().BeLessThan(result.GrossValue);
        }

        [Fact]
        public async Task CalculateCDB_ShouldHandleInvalidInput_Gracefully()
        {
            var invalidRequest = new CDBDTO { InitialValue = -500, DeadlineInMonths = -6 };
            await Assert.ThrowsAsync<ArgumentException>(() => _cdbRepository.CalculateCDB(invalidRequest));
        }

        [Fact]
        public async Task CalculateCDB_ShouldThrowException_WhenInitialValueOrDeadlineIsZeroOrNegative()
        {
            var requests = new[]
            {
                new CDBDTO { InitialValue = 0, DeadlineInMonths = 12 },
                new CDBDTO { InitialValue = 1000, DeadlineInMonths = 0 },
                new CDBDTO { InitialValue = -1000, DeadlineInMonths = 12 },
                new CDBDTO { InitialValue = 1000, DeadlineInMonths = -12 }
            };

            foreach (var request in requests)
            {
                await Assert.ThrowsAsync<ArgumentException>(() => _cdbRepository.CalculateCDB(request));
            }
        }

        [Fact]
        public async Task CalculateCDB_ShouldThrowException_WhenInitialValueIsTooHigh()
        {
            var request = new CDBDTO { InitialValue = 1_000_000_000, DeadlineInMonths = 12 };
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _cdbRepository.CalculateCDB(request));
        }

        [Fact]
        public async Task CalculateCDB_ShouldThrowException_WhenDeadlineIsTooHigh()
        {
            var request = new CDBDTO { InitialValue = 1000, DeadlineInMonths = 240 };
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _cdbRepository.CalculateCDB(request));
        }

        [Fact]
        public async Task CalculateCDB_ShouldThrowException_WhenInitialValueIsNull()
        {
            CDBDTO nullRequest = null;
            await Assert.ThrowsAsync<ArgumentNullException>(() => _cdbRepository.CalculateCDB(nullRequest));
        }

        [Fact]
        public async Task CalculateCDB_ShouldReturnConsistentValues_WhenCalledMultipleTimes()
        {
            var request = new CDBDTO { InitialValue = 1000, DeadlineInMonths = 12 };

            var firstResult = await _cdbRepository.CalculateCDB(request);
            var secondResult = await _cdbRepository.CalculateCDB(request);

            firstResult.GrossValue.Should().BeApproximately(secondResult.GrossValue, 0.01m);
            firstResult.NetValue.Should().BeApproximately(secondResult.NetValue, 0.01m);
        }

        [Fact]
        public async Task CalculateCDB_ShouldReturnExpectedValues_WhenFractionalInitialValue()
        {
            var request = new CDBDTO { InitialValue = 0.01m, DeadlineInMonths = 1 };
            var result = await _cdbRepository.CalculateCDB(request);

            result.Should().NotBeNull();
            result.GrossValue.Should().BeGreaterThan(0);
            result.NetValue.Should().BeGreaterThan(0);
        }
    }
}
