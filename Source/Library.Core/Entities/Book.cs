using System;
using System.Linq;
using System.Collections.Generic;

namespace Library.Core.Entities
{
    /// <summary>
    /// Represents a book from the library.
    /// </summary>
    public class Book : BaseEntity<Guid>
    {
        public Book()
        {
            Categories = new List<Category>();
            Authors = new List<Author>();
            BookStockByEditions = new List<BookStockByEdition>();
        }

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
        public virtual ICollection<Category> Categories { get; set; }

        /// <summary>
        /// Holds the book's authors information.
        /// </summary>
        public virtual ICollection<Author> Authors { get; set; }

        /// <summary>
        /// Holds the library stock for the books by editions's authors information.
        /// </summary>
        public virtual ICollection<BookStockByEdition> BookStockByEditions { get; set; }
    }
}
