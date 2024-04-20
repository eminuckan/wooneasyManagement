using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WooneasyManagement.Application.Common;

namespace WooneasyManagement.Application.Files
{
    public class FileErrors
    {
        public static readonly Error FileNotFoundError = new()
        {
            Title = "File Not Found",
            Detail = "Please double-check the filename and file path. Ensure the file exists in the specified location.",
            StatusCode = StatusCodes.Status404NotFound
        };
    }
}
