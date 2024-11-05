using B3.Application.Contracts.Request;
using FluentValidation;
using B3.Application.Validators.Constants;
using B3.Application.Interfaces;

namespace B3.Application.Validators;
public class CDBValidator : AbstractValidator<CDBRequest>
{
    public CDBValidator(ICDBApplication professional)
    {
        RuleFor(x => x.InitialValue)
            .GreaterThan(0).WithMessage(ValidationMessages.PositiveValue("InitialValue"));

        RuleFor(x => x.DeadlineInMonths)
            .GreaterThan(1).WithMessage(ValidationMessages.ValueGreaterThanZero("DeadlineInMonths"));

    }
}