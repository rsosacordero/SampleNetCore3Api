using Correlate.AspNetCore;
using Correlate.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PwcBios.Api.CQRS.Queries;
using PwcBios.Api.Data;
using PwcBios.Api.Infrastructure;
using PwcBios.Api.Infrastructure.Filters;
using PwcBios.Api.Infrastructure.HealthChecks;

namespace PwcBios.Api
{
    public class Startup
    {
        private readonly ApiConfig _configuration;

        public Startup(IWebHostEnvironment env)
        {
            _configuration = new ApiConfig(env.EnvironmentName); 
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(c => c.Filters.Add<PwcBiosExceptionFilter>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });

            //Add EF core
            services.AddDbContext<HumanBiosContext>((opts) => opts.UseSqlServer(_configuration.PvcBiosApiConnectionString), ServiceLifetime.Transient);

            //Add correlation ID 
            services.AddCorrelate(options =>
            {
                options.RequestHeaders = new[]
                {
                  "X-Correlation-ID",
                };
                options.IncludeInResponse = true;
            });


            //Mapping abstractions
            services.AddTransient<IStatusQuery, StatusQuery>();
            services.AddTransient<IPwcBiosExceptionHandler, PwcBiosExceptionHandler>();

            //Health checks
            services.AddHealthChecks().AddCheck<BasicHealthCheck>("Basic Health Check");

            //Add Swagger configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PwC Bios API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCorrelate();
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PwC Bios API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            
        }
    }
}
