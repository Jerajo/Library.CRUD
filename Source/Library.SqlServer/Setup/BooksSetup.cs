using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.SqlServer.Setup
{
    /// <summary>
    /// Represents all the books's entity configuration.
    /// To be use by the ORM entity framework.
    /// </summary>
    public static class BooksSetup
    {
        /// <summary>
        /// Configures the books's ORM handler, following the business rules.
        /// </summary>
        /// <param name="modelBuilder">The ORM entity builder interface.</param>
        public static void ConfigureBooks(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Categories)
                .WithMany(c => c.Books);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(c => c.Books);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.BookStockByEditions)
                .WithOne(c => c.Book);
        }
    }
}
