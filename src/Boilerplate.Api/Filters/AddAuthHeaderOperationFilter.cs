using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Boilerplate.Api.Filters
{
    public class AddAuthHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var actionMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;
            var isAuthorized = actionMetadata.Any(metadataItem => metadataItem is AuthorizeAttribute);
            var allowAnonymous = actionMetadata.Any(metadataItem => metadataItem is AllowAnonymousAttribute);

            if (!isAuthorized || allowAnonymous)
            {
                return;
            }
            operation.Parameters ??= new List<OpenApiParameter>();

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                //Add JWT bearer type
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                // Definition name. 
                                // Should exactly match the one given in the service configuration
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            };

            
        }
    }
}
