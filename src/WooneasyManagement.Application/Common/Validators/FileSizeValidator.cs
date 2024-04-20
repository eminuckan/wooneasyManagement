using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WooneasyManagement.Application.Common.Validators
{
    public class FileSizeValidator : AbstractValidator<IFormFile>
    {
        public FileSizeValidator()
        {
            RuleFor(file => file).Must(CheckFileSize).WithMessage("The file size must be less than 10 MB.");
        }

        private bool CheckFileSize(IFormFile file)
        {
            return file.Length < 10485760; // 10 MB in bytes
        }
    }
}
