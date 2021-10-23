using System;
using System.Collections.Generic;

namespace Library.Core.Entities
{
    /// <summary>
    /// Represents a book from the library.
    /// </summary>
    public class Author : BaseEntity<Guid>
    {
        /// <summary>
        /// Holds the author's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Holds the author's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Holds the author's books information.
        /// </summary>
        public virtual IEnumerable<Book> Books { get; set; }
    }
}
