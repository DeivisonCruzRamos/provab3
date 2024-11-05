using B3.Domain.Extensions;
using B3.Domain.Models.Common;
using B3.Domain.Models.DTO;
using B3.Infra.MySql.Interfaces;
using B3.Service.Interfaces;
using Microsoft.Extensions.Logging;

namespace B3.Service.Services;

public class CDBService : ICDBService
{
    private readonly ICDBRepository _cdbRepository;

    private readonly ILogger _logger;

    public CDBService(ICDBRepository professionalRepository, ILogger<CDBService> logger)
    {
        _cdbRepository = professionalRepository;
        _logger = logger;
    }


    public async Task<CDBDTO> CalculateCDB(CDB cdb)
    {
        var orderDTO = ModelTransformExtensions.ToCDBDTO(cdb);


        return await _cdbRepository.CalculateCDB(orderDTO);
    }
}