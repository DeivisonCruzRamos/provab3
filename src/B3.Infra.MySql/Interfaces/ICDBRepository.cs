using B3.Domain.Models.DTO;

namespace B3.Infra.MySql.Interfaces;
public interface ICDBRepository 
{
    Task<CDBDTO> CalculateCDB(CDBDTO cdb);
}