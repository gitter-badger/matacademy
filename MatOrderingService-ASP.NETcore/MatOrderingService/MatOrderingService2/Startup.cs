using System.IO;
using AutoMapper;
using MatOrderingService2.Config;
using MatOrderingService2.Services.Auth;
using MatOrderingService2.Services.Storage.Impl;
using MatOrderingService2.Services.Swagger;
using MatOrderingService2.Filters;
using MatOrderingService2.Services.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace MatOrderingService2
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            var connectionString = Configuration.GetValue<string>("Data:ConnectionString");

            services.AddDbContext<OrdersDbContext>(options => options.UseSqlServer(
                connectionString)
                .ConfigureWarnings(warnings => warnings.Log(RelationalEventId.QueryClientEvaluationWarning)));

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(EntityNotFoundExceptionFilter));
                options.Filters.Add(typeof(EntityBadRequestExceptionFilter));
            });

            services.AddAutoMapper();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Materialise Academy Orders API", Version = "v1" });
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "MatOrderingService2.xml");
                c.IncludeXmlComments(filePath);
                c.OperationFilter<SwaggerAuthorizationHeaderParameter>(Configuration.GetValue<string>("AuthOptions:AuthenticationScheme"));
            });

            services.Configure<MatOsAuthOptions>(Configuration.GetSection("AuthOptions"));
            services.Configure<HttpClientSettings>(Configuration.GetSection("CodeGeneratorConfig"));
            services.AddAuthorization(auth =>
            {
                auth.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(Configuration.GetValue<string>("AuthOptions:AuthenticationScheme"))
                    .RequireAuthenticatedUser().Build();
            });

            services.AddSingleton<IOrderingService, OrderingService>();
            services.AddSingleton<IProductService, ProductService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMiddleware<MatOsAuthMiddleware>();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Materialise Academy Orders API");
            });
            app.Run(async context =>
            {
                await context.Response.WriteAsync(Configuration["Environment:WellcomeMessage"]);

            });
        }
    }
}
