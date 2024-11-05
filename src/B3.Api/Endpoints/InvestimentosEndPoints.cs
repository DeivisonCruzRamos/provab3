using B3.Application.Contracts.Request;
using B3.Application.Contracts.Response;
using B3.Application.Interfaces;
using B3.Application.Validators;
using Microsoft.AspNetCore.Mvc;

namespace B3.Api.Endpoints;
public static class InvestimentosEndPoints
{

    public static void AddInvestimentosEndpoints(this WebApplication app)
    {
        string routeName = "Investimentos";
        string rootName = $"api/{routeName}";

        app.MapPost(rootName + "/CalculateCDB", async ([FromServices] ICDBApplication service, [FromBody] CDBRequest request) =>
        {
            if (request is null)
            {
                return Results.BadRequest("Parâmetros ausentes.");
            }

            var validationResponse = await new CDBValidator(service).ValidateAsync(request);
            if (!validationResponse.IsValid)
            {
                return Results.BadRequest(validationResponse.Errors.Select(a => a.ErrorMessage).ToList());
            }

            var result = await service.CalculateCDB(request);

            if (result != null)
            {
                return Results.Ok(new {Result = result });
            }
            return Results.NotFound("Erro na calculo");

        })
          .Produces(StatusCodes.Status200OK)
          .Produces(StatusCodes.Status404NotFound)
          .Produces(StatusCodes.Status500InternalServerError)
          .Produces(StatusCodes.Status400BadRequest)
          .WithTags(routeName);
    }
}