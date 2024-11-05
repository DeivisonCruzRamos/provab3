using Microsoft.AspNetCore.Http.HttpResults;
using static System.Net.Mime.MediaTypeNames;

namespace B3.Application.Validators.Constants;

public class ValidationMessages
{
    public static string PositiveValue(string propertyName) => $"{propertyName} O valor inicial deve ser um valor monetário positivo.";
    public static string ValueGreaterThanZero(string propertyName) => $"{propertyName} O prazo em meses deve ser maior que 1.";
}