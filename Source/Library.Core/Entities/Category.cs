using System;
using System.Collections.Generic;

namespace Library.Core.Entities
{
    /// <summary>
    /// Represents a category for the books in the library.
    /// </summary>
    public class Category : BaseEntity<Guid>
    {
        /// <summary>
        /// Holds the category's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Holds the category's books information.
        /// </summary>
        public virtual ICollection<Book> Books { get; set; }
    }
}
