using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using WooneasyManagement.Application.Common.Validators;
using WooneasyManagement.Application.Files.Commands;

namespace WooneasyManagement.Application.Files
{
    public class FileValidator : AbstractValidator<UploadFilesCommand>
    {
        public FileValidator()
        {
            RuleFor(r => r.Files)
                .NotEmpty()
                .NotNull();
            RuleFor(r => r.BucketOrMainDirectory)
                .NotEmpty()
                .NotNull();
            RuleForEach(r => r.Files)
                .SetValidator(new FileSizeValidator());
        }
    }
}
