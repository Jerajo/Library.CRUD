using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using Serilog;

using Library.Api.Validations;
using Library.Application.Profiles;
using Library.SqlServer;

namespace Library.Api
{
    /// <summary>
    /// The server main configuration class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Default constructor for <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Represents a set of key/value application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the IoC container.
        /// </summary>
        /// <param name="services">The application IoC container.</param>
        public void ConfigureServices(IServiceCollection services)
        {
#if MOCK
            services.AddDbContext<ApplicationDBContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlite("Data Source=sqlite.db"));
            // If you want to do a quick test.
            // services.AddDbContext<ApplicationDBContext>(options =>
            //                 options.UseInMemoryDatabase("applicationDb"));
#else
            var migrationsAssembly = typeof(Startup).Assembly.GetName().FullName;
            services.AddDbContext<ApplicationDBContext>(options =>
                            options.UseSqlServer(Configuration.GetConnectionString(name: "DefaultConnection"),
                                sql => sql.MigrationsAssembly(migrationsAssembly))
                                .UseLazyLoadingProxies());
#endif

            services.AddControllers(
                options =>
                {
                    options.Filters.Add<ValidationResultAttribute>();
                })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GuidValidator>());

            services.AddHttpContextAccessor();

            services.AddAutoMapper(typeof(AuthorProfile).Assembly);

            services.AddTransient<IValidatorInterceptor, DtoValidatorInterceptor>();

            services.ConfigIoCServices();

            services.ConfigIoCForFactories();

            services.ConfigIoCForCommands();

            services.ConfigIoCForQueries();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Library.Api", Version = "v1" });
            });
        }

        /// <summary>
        /// Configure the services behaviors and meddle wares.
        /// </summary>
        /// <param name="app">Provides the server request pipe line configuration.</param>
        /// <param name="env">Provides information about the server environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "*/*";

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error is Exception ex)
                    {
                        await Task.Run(() =>
                        {
                            Log.Fatal("Server-side Error: {0}", ex.Message);
                        });
                    }
                });
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library.Api v1");
                        c.RoutePrefix = string.Empty;
                    });
            }

            app.UseRouting();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
