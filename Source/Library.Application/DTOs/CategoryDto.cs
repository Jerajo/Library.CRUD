using System;

namespace Library.Application.DTOs
{
    /// <summary>
    /// Represents a category entity with all its public attributes.
    /// </summary>
    public class CategoryDto : BaseDto<Guid>
    {
        /// <summary>
        /// Holds the category's name.
        /// </summary>
        public string Name { get; set; }
    }
}
