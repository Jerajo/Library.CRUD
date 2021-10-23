using System;

namespace Library.Application.DTOs
{
    /// <summary>
    /// Represents a author entity with all its public attributes.
    /// </summary>
    public class AuthorDto : BaseDto<Guid>
    {
        /// <summary>
        /// Holds the author's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Holds the author's last name.
        /// </summary>
        public string LastName { get; set; }
    }
}
