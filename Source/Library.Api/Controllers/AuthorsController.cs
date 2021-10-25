using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Application.DTOs;
using Library.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    /// <summary>
    /// Represents the authors controller for all the business logic operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : BaseController
    {
        /// <summary>
        /// Default controller constructor.
        /// </summary>
        /// <param name="serviceProvider">The IoC application's service provider.</param>
        public AuthorsController(IServiceProvider serviceProvider)
            : base(serviceProvider) { }

        /// <summary>
        /// Gets the filtered list of authors.
        /// </summary>
        /// <returns>Returns <see cref="List{AuthorDto}"/>.</returns>
        [HttpGet]
        public IActionResult GetBooks()
        {
            var getBook = _queryFactory.MakeQuery<GetAuthorsQuery>();

            List<AuthorDto> books = getBook.Execute(null);

            return Ok(books);
        }
    }
}
