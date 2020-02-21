using Boilerplate.Api.Extensions;
using Boilerplate.Application.Interfaces;
using Boilerplate.Application.Services;
using Boilerplate.Domain.Repositories;
using Boilerplate.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace Boilerplate.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public static void ConfigureServices(IServiceCollection services)
        {
            //Extension method for less clutter in startup
            services.AddApplicationDbContext();

            //DI Services and Repos
            services.AddScoped<IHeroRepository, HeroRepository>();
            services.AddScoped<IHeroAppService, HeroAppService>();

            // WebApi Configuration
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // for enum as strings
            });

            // AutoMapper settings
            services.AddAutoMapperSetup();

            // HttpContext for log enrichment 
            services.AddHttpContextAccessor();

            // Swagger settings
            services.AddApiDoc();
            // GZip compression
            services.AddCompression();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCustomSerilogRequestLogging();
            app.UseRouting();
            app.UseApiDoc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //added request logging


            app.UseHttpsRedirection();


            app.UseResponseCompression();

        }
    }
}
