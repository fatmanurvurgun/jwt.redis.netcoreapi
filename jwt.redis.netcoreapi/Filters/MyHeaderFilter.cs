using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.redis.netcoreapi.Filters
{
    public class MyHeaderFilter : Attribute, IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            if (context.MethodInfo.GetCustomAttribute(typeof(SwaggerHeaderAttribute)) is SwaggerHeaderAttribute attribute)
            {

                var existingParam = operation.Parameters.FirstOrDefault(p => p.In == ParameterLocation.Header && p.Name == attribute.HeaderName);

                if (existingParam != null) //remove description from [fromHeader] argument attribute
                    operation.Parameters.Remove(existingParam);

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = attribute.HeaderName,
                    In = ParameterLocation.Header,
                    Required = true
                });
            }
        }
    }
}
