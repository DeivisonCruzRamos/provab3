using B3.Domain.Models.DTO;

namespace B3.Service.Interfaces;
public interface ICDBService
{
    Task<CDBDTO> CalculateCDB(Domain.Models.Common.CDB cdb);
}