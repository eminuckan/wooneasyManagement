using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WooneasyManagement.Application.Common;

namespace WooneasyManagement.Application.Cities
{
    public static class CityErrors
    {
        public static Error CityAlreadyExists => new Error("City.AlreadyExists", "City already exists.",StatusCodes.Status400BadRequest );
        public static Error CityNotFound => new Error("City.NotFound", "City not found.", StatusCodes.Status404NotFound);

    }
}
