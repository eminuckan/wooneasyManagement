using FluentValidation;
using WooneasyManagement.Application.Cities.Commands;

namespace WooneasyManagement.Application.Cities
{
    public class CityValidator : AbstractValidator<CreateCityCommand>
    {
        public CityValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(200);
            RuleFor(c => c.Country)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(200);
        }
    }
}
