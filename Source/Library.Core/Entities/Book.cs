using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Core.Entities
{
    /// <summary>
    /// Represents a book from the library.
    /// </summary>
    public class Book : BaseEntity<Guid>
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
        public virtual IEnumerable<Category> Categories { get; set; }

        /// <summary>
        /// Holds the book's authors information.
        /// </summary>
        public virtual IEnumerable<Author> Authors { get; set; }

        /// <summary>
        /// Holds the library stock for the books by editions's authors information.
        /// </summary>
        public virtual IEnumerable<BookStockByEdition> BookStockByEditions { get; set; }
    }
}
