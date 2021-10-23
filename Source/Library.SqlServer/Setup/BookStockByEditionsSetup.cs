using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.SqlServer.Setup
{
    /// <summary>
    /// Represents the library stock for the books by editions's entity configuration.
    /// To be use by the ORM entity framework.
    /// </summary>
    public static class BookStockByEditionsSetup
    {
        /// <summary>
        /// Configures the library stock for the books by editions's ORM handler, following the business rules.
        /// </summary>
        /// <param name="modelBuilder">The ORM entity builder interface.</param>
        public static void ConfigureBookStockByEditions(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookStockByEdition>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<BookStockByEdition>()
                .Property(b => b.BookEdition)
                .IsRequired();

            modelBuilder.Entity<BookStockByEdition>()
                .Property(b => b.AvailabeBooks)
                .IsRequired();

            modelBuilder.Entity<BookStockByEdition>()
                .Property(b => b.BorrowedBooks)
                .IsRequired();

            modelBuilder.Entity<BookStockByEdition>()
                .HasOne(b => b.Book)
                .WithMany(b => b.BookStockByEditions);
        }
    }
}
