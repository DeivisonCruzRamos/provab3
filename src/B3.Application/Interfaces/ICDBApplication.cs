using B3.Application.Contracts.Request;
using B3.Application.Contracts.Response;

namespace B3.Application.Interfaces;

public interface ICDBApplication
{
    Task<CDBResponse> CalculateCDB(CDBRequest request);
}