using Library.Core.Entities;
using Library.SqlServer.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.SqlServer.Setup
{
    /// <summary>
    /// Represents all the categories's entity configuration.
    /// To be use by the ORM entity framework.
    /// </summary>
    public static class CategoriesSetup
    {
        /// <summary>
        /// Configures the categories's ORM handler, following the business rules.
        /// </summary>
        /// <param name="modelBuilder">The ORM entity builder interface.</param>
        public static void ConfigureCategories(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Category>()
                .Property(b => b.Name)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Books)
                .WithMany(b => b.Categories);

            modelBuilder.Entity<Category>()
                .HasData(InitialData.Categories);
        }
    }
}
