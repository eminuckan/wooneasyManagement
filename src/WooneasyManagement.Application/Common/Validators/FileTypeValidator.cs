using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooneasyManagement.Application.Common.Validators
{
    public class FileTypeValidator : AbstractValidator<IFormFile>
    {
        public FileTypeValidator()
        {
            RuleFor(file => file).Must(BeImage).WithMessage("The file type must be an image (.jpg, .png, .jpeg, .webp)");
        }
        private bool BeImage(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".png", ".jpeg", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            return allowedExtensions.Contains(extension);
        }
    }


}
