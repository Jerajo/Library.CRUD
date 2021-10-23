using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Core.Entities
{
    /// <summary>
    /// Represents a the stock for book group by the editions.
    /// </summary>
    public class BookStockByEdition : BaseEntity<Guid>
    {
        /// <summary>
        /// Holds the book's unique identifier.
        /// </summary>
        public int BookEdition { get; set; }

        /// <summary>
        /// Indicates how many books are available.
        /// </summary>
        public int AvailabeBooks { get; set; }

        /// <summary>
        /// Indicates how many books are borrowed.
        /// </summary>
        public int BorrowedBooks { get; set; }

        /// <summary>
        /// Holds the book's unique identifier.
        /// </summary>
        public Guid BookId { get; set; }

        /// <summary>
        /// Holds the book information related to this stock.
        /// </summary>
        [ForeignKey(nameof(BookId))]
        public virtual Book Book { get; set; }
    }
}
