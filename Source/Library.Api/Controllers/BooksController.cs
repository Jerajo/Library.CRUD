using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using Library.Application.Commands;
using Library.Application.DTOs;
using Library.Application.Queries;
using System.Collections.Generic;

namespace Library.Api.Controllers
{
    /// <summary>
    /// Represents the books controller for all the business logic operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : BaseController
    {
        /// <summary>
        /// Default controller constructor.
        /// </summary>
        /// <param name="serviceProvider">The IoC application's service provider.</param>
        public BooksController(IServiceProvider serviceProvider)
            : base(serviceProvider) { }

        /// <summary>
        /// Gets the filtered list of books.
        /// </summary>
        /// <param name="filters">The filters used to query the books.</param>
        /// <returns>Returns <see cref="List{BookDto}"/>.</returns>
        [HttpGet]
        public IActionResult GetBooks([FromQuery] BookFiltersDto filters)
        {
            var getBook = _queryFactory.MakeQuery<GetBooksQuery>();

            List<BookDto> books = getBook.Execute((c => string.IsNullOrEmpty(c.DeleteFlag) &&
                (string.IsNullOrWhiteSpace(filters.Title) || c.Title.Contains(filters.Title, StringComparison.OrdinalIgnoreCase)) &&
                (c.Categories.Any(a => filters.CategoryId == null || a.Id == filters.CategoryId)),
                filters.AvailableOnly)
            );

            return Ok(books);
        }

        /// <summary>
        /// Gets the filtered book.
        /// </summary>
        /// <param name="id">The book's unique identifier.</param>
        /// <returns>Returns <see cref="BookDto"/> | <see cref="NotFoundResult"/></returns>
        [HttpGet("{id}")]
        public IActionResult GetBookById([FromRoute, FromQuery] Guid id)
        {
            var getClientById = _queryFactory.MakeQuery<GetBookQuery>();

            var client = getClientById.Execute(c =>
                c.Id == id &&
                string.IsNullOrEmpty(c.DeleteFlag) &&
                (c.Authors.Any(a => string.IsNullOrEmpty(a.DeleteFlag))) &&
                (c.Categories.Any(a => string.IsNullOrEmpty(a.DeleteFlag))) &&
                (c.BookStockByEditions.Any(a => string.IsNullOrEmpty(a.DeleteFlag)))
            );

            if (client is null)
                return NotFound();

            return Ok(client);
        }

        /// <summary>
        /// Adds a new book to the data base.
        /// </summary>
        /// <param name="model">Represents the book information to be added.</param>
        /// <returns>Returns <see cref="OkResult"/>.</returns>
        [HttpPost]
        public IActionResult CreateBook([FromBody] BookForCreationDto model)
        {
            var createClient = _commandFactory.MakeCommand<CreateBookCommand>();

            createClient.Execute(model);

            return Ok();
        }

        /// <summary>
        /// Updates selected the book information on the data base.
        /// </summary>
        /// <param name="id">Represents the book's unique identifier.</param>
        /// <param name="model">Represents the book information to be updated.</param>
        /// <returns>Returns <see cref="OkResult"/> | <see cref="NotFoundResult"/>.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateBook(
            [FromRoute, FromQuery] Guid id,
            [FromBody] BookForEditionDto model)
        {
            if (GetBookById(id) is NotFoundResult)
                return NotFound();

            var updateClient = _commandFactory.MakeCommand<UpdateBookCommand>();

            updateClient.Execute((id, model));

            return Ok();
        }

        /// <summary>
        /// Marks the selected book as deleted.
        /// </summary>
        /// <param name="bookId">Represents the book's unique identifier.</param>
        /// <param name="stockId">Represents the stock's unique identifier.</param>
        /// <returns>Returns <see cref="OkResult"/> | <see cref="NotFoundResult"/>.</returns>
        [HttpDelete("{bookId}/{stockId}")]
        public IActionResult DeleteBookStock(Guid bookId, Guid stockId)
        {
            if (GetBookById(bookId) is NotFoundResult)
                return NotFound();

            var deleteClient = _commandFactory.MakeCommand<DeleteBookStockCommand>();

            deleteClient.Execute(stockId);

            return Ok();
        }
    }
}
