using B3.Application.Contracts.Response;
using B3.Domain.Models.Common;
using B3.Domain.Models.DTO;

namespace B3.Application.Mappers;
public static class CDBMapper
{
    public static CDBResponse ProfessionalResponseMapper(CDBDTO cdbDTO)
    {
        var response = new CDBResponse
        {
            GrossValue = cdbDTO.GrossValue,
            NetValue = cdbDTO.NetValue,
        };
        return response;
    }

}