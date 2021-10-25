using System;
using System.Collections.Generic;
using Library.Application.DTOs;
using Library.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    /// <summary>
    /// Represents the categories controller for all the business logic operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : BaseController
    {
        /// <summary>
        /// Default controller constructor.
        /// </summary>
        /// <param name="serviceProvider">The IoC application's service provider.</param>
        public CategoriesController(IServiceProvider serviceProvider)
            : base(serviceProvider) { }

        /// <summary>
        /// Gets the filtered list of categories.
        /// </summary>
        /// <returns>Returns <see cref="List{GetCategoriesQuery}"/>.</returns>
        [HttpGet]
        public IActionResult GetBooks()
        {
            var getBook = _queryFactory.MakeQuery<GetCategoriesQuery>();

            List<CategoryDto> books = getBook.Execute(null);

            return Ok(books);
        }
    }
}
