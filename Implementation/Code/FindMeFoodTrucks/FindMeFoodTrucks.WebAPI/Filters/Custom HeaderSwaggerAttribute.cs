using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace FindMeFoodTrucks.WebAPI.Filters
{
    /// <summary>
    /// Enables Swagger US custom header
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CustomHeaderSwaggerAttribute : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (string.Equals(context.ApiDescription.HttpMethod, HttpMethod.Get.Method, StringComparison.InvariantCultureIgnoreCase))
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "APIKey",
                    In = ParameterLocation.Header,
                    Required = false,
                    Example = new OpenApiString("MyKey")
                });
            }
        }
    }
}
