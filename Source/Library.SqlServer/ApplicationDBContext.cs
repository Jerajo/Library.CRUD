using Library.Core.Entities;
using Library.SqlServer.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Library.SqlServer
{
    /// <summary>
    /// Represents the application database context,
    /// allowing the communication between the application and the database.
    /// </summary>
    public class ApplicationDBContext : DbContext
    {
        /// <summary>
        /// Default constructor needed for netcore-ef migration tools.
        /// </summary>
        public ApplicationDBContext() { }

        /// <summary>
        /// Default constructor use with DI in the startup file.
        /// </summary>
        /// <param name="options">The options to be use by the DbContext.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options,
            IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }


        #region Properties

        /// <summary>
        /// ORM access to the data base table Books.
        /// </summary>
        public virtual DbSet<Book> Books { get; set; }

        /// <summary>
        /// ORM access to the data base table Authors.
        /// </summary>
        public virtual DbSet<Author> Authors { get; set; }

        /// <summary>
        /// ORM access to the data base table Categories.
        /// </summary>
        public virtual DbSet<Category> Categories { get; set; }

        /// <summary>
        /// ORM access to the data base table BookStockByEditions.
        /// </summary>
        public virtual DbSet<BookStockByEdition> BookStockByEdition { get; set; }

        /// <summary>
        /// Represents a set of key/value application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        #endregion

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            var migrationsAssembly = typeof(ApplicationDBContext).Assembly.GetName().FullName;

            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly(migrationsAssembly))
                    .UseLazyLoadingProxies();
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureBooks();

            modelBuilder.ConfigureCategories();

            modelBuilder.ConfigureAuthors();

            modelBuilder.ConfigureBookStockByEditions();

            base.OnModelCreating(modelBuilder);
        }
    }
}
