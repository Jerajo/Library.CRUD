using Library.Core.Entities;
using Library.SqlServer.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.SqlServer.Setup
{
    /// <summary>
    /// Represents all the authors's entity configuration.
    /// To be use by the ORM entity framework.
    /// </summary>
    public static class AuthorsSetup
    {
        /// <summary>
        /// Configures the authors's ORM handler, following the business rules.
        /// </summary>
        /// <param name="modelBuilder">The ORM entity builder interface.</param>
        public static void ConfigureAuthors(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Author>()
                .Property(a => a.FirstName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Author>()
                .Property(a => a.LastName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithMany(b => b.Authors);

            modelBuilder.Entity<Author>()
                .HasData(InitialData.Authors);
        }
    }
}
