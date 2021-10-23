using System;
using System.Collections.Generic;

using Library.Core.Entities;

namespace Library.SqlServer.Data
{
    /// <summary>
    /// Holds the data base entities initial data.
    /// </summary>
    public static class InitialData
    {
        /// <summary>
        /// Authors initial information for data base seeding.
        /// </summary>
        public static List<Author> Authors => new List<Author>
        {
            new Author
            {
                Id = Guid.Parse("BA4238D9-E878-4FA5-8D76-2177978315BF"),
                FirstName = "James",
                LastName = "Bond",
                CreatedDate = DateTime.Now
            },
            new Author
            {
                Id = Guid.Parse("56ACAFB7-DCBD-42D9-923D-B05716D183F4"),
                FirstName = "Max",
                LastName = "Powers",
                CreatedDate = DateTime.Now
            },
            new Author
            {
                Id = Guid.Parse("0BDB0BB9-4FE3-4C6D-AAB5-C7C918B7AD25"),
                FirstName = "Jesus",
                LastName = "Cris",
                CreatedDate = DateTime.Now
            },
        };

        /// <summary>
        /// Categories initial information for data base seeding.
        /// </summary>
        public static List<Category> Categories => new List<Category>
        {
            new Category
            {
                Id = Guid.Parse("861964EE-C9B8-4580-ACFB-93C913590A07"),
                Name = "Historia",
                CreatedDate = DateTime.Now
            },
            new Category
            {
                Id = Guid.Parse("6F928818-80E3-40C1-9DFD-A9F684F6A83B"),
                Name = "Novela",
                CreatedDate = DateTime.Now
            },
            new Category
            {
                Id = Guid.Parse("36E8E3B3-7027-42E4-9BBD-B7DA3E9DAC6C"),
                Name = "Espiritual",
                CreatedDate = DateTime.Now
            },
        };
    }
}
