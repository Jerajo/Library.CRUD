using System;

namespace Library.Application.DTOs
{
    /// <summary>
    /// Represents the filters use to query the books.
    /// </summary>
    public class BookFiltersDto
    {
        /// <summary>
        /// Filters by the book's title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Filters only available books.
        /// </summary>
        public bool AvailableOnly { get; set; }

        /// <summary>
        /// Filters by the category's unique identifier.
        /// </summary>
        public Guid? CategoryId { get; set; }

    }
}
