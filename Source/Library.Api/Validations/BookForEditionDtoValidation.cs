using System;
using Library.Application.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Library.Api.Validations
{
    /// <summary>
    /// Automatic validation configuration for the BookForEditionDto.
    /// </summary>
    public class BookForEditionDtoValidation : AbstractValidator<BookForEditionDto>
    {
        public BookForEditionDtoValidation()
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

            RuleFor(book => book.StockId)
                .NotNull()
                .NotEmpty();

            RuleFor(book => book.BorrowedBooks)
                .NotNull()
                .NotEmpty()
                .InclusiveBetween(1, int.MaxValue);
        }
    }
}
