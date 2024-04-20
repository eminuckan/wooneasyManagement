using FluentValidation;
using Microsoft.AspNetCore.Http;
using WooneasyManagement.Application.Cities.Commands;
using WooneasyManagement.Application.Common.Validators;
using static System.Net.Mime.MediaTypeNames;

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

    public class CityCoverImageUploadValidator : AbstractValidator<UploadCityCoverImageCommand>
    {
        public CityCoverImageUploadValidator()
        {
            RuleFor(c => c.CityId)
                .NotEmpty()
                .NotNull();
            RuleFor(c => c.File).SetValidator(new FileTypeValidator());
            RuleFor(c => c.File).SetValidator(new FileSizeValidator());
        }
    }

}
