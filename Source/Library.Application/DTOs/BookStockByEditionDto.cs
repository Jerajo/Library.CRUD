using System;

namespace Library.Application.DTOs
{
    /// <summary>
    /// Represents a the stock for book group by the editions.
    /// </summary>
    public class BookStockByEditionDto : BaseDto<Guid>
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
    }
}
