using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Application.DTOs
{
    /// <summary>
    /// Represents a book entity with all its public attributes.
    /// </summary>
    public class BookDto : BaseDto<Guid>
    {
        /// <summary>
        /// Holds the book's title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Holds the book's publication date.
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Holds the book's categories information.
        /// </summary>
        public virtual List<CategoryDto> Categories { get; set; }

        /// <summary>
        /// Holds the book's authors information.
        /// </summary>
        public virtual List<AuthorDto> Authors { get; set; }

        /// <summary>
        /// Holds the library stock for the books by editions's authors information.
        /// </summary>
        public virtual List<BookStockByEditionDto> BookStockByEditions { get; set; }
    }

    /// <summary>
    /// Represents a book DTO for the creation operation.
    /// </summary>
    public class BookForCreationDto
    {
        /// <summary>
        /// Holds the book's title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Holds the book's publication date.
        /// </summary>
        public DateTime PublicationDate { get; set; }

        /// <summary>
        /// Holds the book's edition.
        /// </summary>
        public int BookEdition { get; set; }

        /// <summary>
        /// Holds how many books are available on stock.
        /// </summary>
        public int AvailableBooks { get; set; }

        /// <summary>
        /// Holds the book's authors unique identifiers.
        /// </summary>
        public List<Guid> Authors { get; set; }

        /// <summary>
        /// Holds the book's category unique identifier.
        /// </summary>
        public List<Guid> Categories { get; set; }
    }

    /// <summary>
    /// Represents a book DTO for the creation operation.
    /// </summary>
    public class BookForEditionDto : BookForCreationDto
    {
        /// <summary>
        /// Holds the book's stock unique identifier.
        /// </summary>
        public Guid StockId { get; set; }

        /// <summary>
        /// Holds how many books are borrowed.
        /// </summary>
        public int BorrowedBooks { get; set; }
    }
}
