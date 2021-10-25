using Library.Api.Factories;
using Library.Application.Commands;
using Library.Application.Queries;
using Library.Core.Contracts;
using Library.SqlServer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Api
{
    /// <summary>
    /// Holds the application API configuration for dependency injections.
    /// </summary>
    public static class ApiConfig
    {
        /// <summary>
        /// Configures the list of commands for injection.
        /// </summary>
        /// <param name="services">Represents the IoC container.</param>
        public static void ConfigIoCForCommands(this IServiceCollection services)
        {
            services.AddScoped<CreateBookCommand>();
            services.AddScoped<DeleteBookCommand>();
            services.AddScoped<UpdateBookCommand>();
            services.AddScoped<DeleteBookStockCommand>();
        }

        /// <summary>
        /// Configures the list of queries for injection.
        /// </summary>
        /// <param name="services">Represents the IoC container.</param>
        public static void ConfigIoCForQueries(this IServiceCollection services)
        {
            services.AddScoped<GetBookQuery>();
            services.AddScoped<GetBooksQuery>();
            services.AddScoped<GetAuthorsQuery>();
            services.AddScoped<GetCategoriesQuery>();
        }

        /// <summary>
        /// Configures the list of factories for injection.
        /// </summary>
        /// <param name="services">Represents the IoC container.</param>
        public static void ConfigIoCForFactories(this IServiceCollection services)
        {
            services.AddScoped<ICommandFactory, CommandFactory>();
            services.AddScoped<IQueryFactory, QueryFactory>();
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
        }

        /// <summary>
        /// Configures the list of services for injection.
        /// </summary>
        /// <param name="services">Represents the IoC container.</param>
        public static void ConfigIoCServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(DataRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
