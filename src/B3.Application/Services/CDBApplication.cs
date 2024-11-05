using B3.Application.Contracts.Request;
using B3.Application.Contracts.Response;
using B3.Application.Interfaces;
using B3.Application.Mappers;
using B3.Domain.Models.Common;
using B3.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace B3.Application.Services;
public class CDBApplication : ICDBApplication
{
    private readonly ICDBService _cdbService;

    public CDBApplication(ICDBService cdblService)
    {
        _cdbService = cdblService;
    }


    public async Task<CDBResponse> CalculateCDB(CDBRequest request)
    {
        var cdb = new Domain.Models.Common.CDB
        {
            DeadlineInMonths = request.DeadlineInMonths,
            InitialValue = request.InitialValue,
        };

        var professional = await _cdbService.CalculateCDB(cdb);

        if (professional == null)
            return null;


        return CDBMapper.ProfessionalResponseMapper(professional);
    }
}