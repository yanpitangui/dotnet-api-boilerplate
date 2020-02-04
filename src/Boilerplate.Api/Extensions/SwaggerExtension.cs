using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Boilerplate.Api.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddApiDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Boilerplate.Api",
                        Version = "v1",
                        Description = "Boilerplate de API",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Yan Pitangui",
                            Url = new System.Uri("https://github.com/yanpitangui")
                        }
                    });
                c.DescribeAllParametersInCamelCase();
                c.OrderActionsBy(x => x.RelativePath);

            });
            return services;
        }

        public static IApplicationBuilder UseApiDoc(this IApplicationBuilder app)
        {
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.RoutePrefix = "api-docs";
                   c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
                   c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
               });
            return app;
        }
    }
}
