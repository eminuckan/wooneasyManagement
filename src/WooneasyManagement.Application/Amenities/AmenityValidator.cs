using FluentValidation;
using WooneasyManagement.Application.Amenities.Commands;
using WooneasyManagement.Application.Amenities.Queries;

namespace WooneasyManagement.Application.Amenities
{
    public class AmenityValidator : AbstractValidator<CreateAmenityCommand>
    {
        public AmenityValidator()
        {
            RuleFor(a => a.IconClass)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);
            RuleFor(a => a.Title)
                .NotEmpty()
                .NotNull()
                .MaximumLength(200);
            RuleFor(a => a.AmenityType)
                .NotNull()
                .IsInEnum()
                .WithMessage("The selected amenity type is invalid. Please choose a valid amenity type.");
        }


    }

    public class AmenityQueryValidator : AbstractValidator<GetAllAmenitiesQuery>
    {
        public AmenityQueryValidator()
        {
            RuleFor(a => a.AmenityType)
                .IsInEnum()
                .WithMessage("The selected amenity type is invalid. Please choose a valid amenity type.");
        }
    }

    public class AmenityUpdateValidator : AbstractValidator<UpdateAmenityCommand>
    {
        public AmenityUpdateValidator()
        {
            RuleFor(a => a.IconClass)
                .MaximumLength(50);
            RuleFor(a => a.Title)
                .MaximumLength(200);
        }
    }

}
