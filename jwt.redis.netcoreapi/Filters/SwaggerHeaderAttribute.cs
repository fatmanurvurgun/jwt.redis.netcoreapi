using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.redis.netcoreapi.Filters
{
    public class SwaggerHeaderAttribute : Attribute
    {
        public string HeaderName { get; }

        public SwaggerHeaderAttribute(string headerName)
        {
            HeaderName = headerName;
        }
    }
}
