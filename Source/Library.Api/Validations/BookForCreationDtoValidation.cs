using System;

using FluentValidation;

using Library.Core.Contracts;
using Library.Core.Entities;
using Library.Application.DTOs;
using Microsoft.AspNetCore.Http;

namespace Library.Api.Validations
{
    /// <summary>
    /// Automatic validation configuration for the BookForCreationDto.
    /// </summary>
    public class BookForCreationDtoValidation : AbstractValidator<BookForCreationDto>
    {
        public BookForCreationDtoValidation(IHttpContextAccessor context,
            IRepository<BookStockByEdition> repository)
        {
            RuleFor(book => book.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(book => book.PublicationDate)
                .NotNull()
                .NotEmpty()
                .InclusiveBetween(DateTime.MinValue, DateTime.MaxValue);

            RuleFor(book => book.BookEdition)
                .NotNull()
                .NotEmpty()
                .Must(bookEdition => !repository.Any(stock => stock.BookId == Guid.Empty && stock.BookEdition == bookEdition))
                .InclusiveBetween(1, int.MaxValue);

            RuleFor(book => book.AvailableBooks)
                .NotNull()
                .NotEmpty()
                .InclusiveBetween(1, int.MaxValue);

            RuleFor(book => book.Authors)
                .NotNull()
                .NotEmpty();

            RuleFor(book => book.Categories)
                .NotNull()
                .NotEmpty();
        }
    }
}
