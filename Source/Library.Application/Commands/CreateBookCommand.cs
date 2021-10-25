using System;
using System.Linq;

using Ardalis.GuardClauses;
using AutoMapper;

using Library.Application.DTOs;
using Library.Core.Contracts;
using Library.Core.Entities;
using Library.Core.Resources;

namespace Library.Application.Commands
{
    /// <summary>
    /// Creates a new book registry.
    /// </summary>
    public class CreateBookCommand : ICommand<BookForCreationDto>
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor for <see cref="CreateBookCommand"/>.
        /// </summary>
        /// <param name="bookRepository">Represents the book repository instance.</param>
        /// <param name="categoryRepository">Represents the category repository instance.</param>
        /// <param name="authorRepository">Represents the author repository instance.</param>
        /// <param name="mapper">Represents the mapper service instance.</param>
        public CreateBookCommand(IRepository<Book> bookRepository,
            IRepository<Category> categoryRepository,
            IRepository<Author> authorRepository,
            IMapper mapper)
        {
            Guard.Against.Null(categoryRepository, nameof(categoryRepository));
            Guard.Against.Null(authorRepository, nameof(authorRepository));
            Guard.Against.Null(bookRepository, nameof(bookRepository));
            Guard.Against.Null(mapper, nameof(mapper));

            _categoryRepository = categoryRepository;
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public void Execute(BookForCreationDto model)
        {
            Guard.Against.Null(model, nameof(model));

            var book = _mapper.Map<Book>(model);

            book.CreatedDate = DateTime.Now;

            foreach (var categoryId in model.Categories)
            {
                var category = _categoryRepository.Get(category => category.Id == categoryId);
                if (category is null)
                    throw new InvalidOperationException(string.Format(Resources.CategoryNotFound, categoryId));
                book.Categories.Add(category);
            }

            foreach (var authorId in model.Authors)
            {
                var author = _authorRepository.Get(author => author.Id == authorId);
                if (author is null)
                    throw new InvalidOperationException(string.Format(Resources.AuthorNotFound, authorId));
                book.Authors.Add(author);
            }

            var sameBook = _bookRepository.Get(b => b.Title.Contains(book.Title, StringComparison.OrdinalIgnoreCase) &&
                b.Authors.Contains(book.Authors.FirstOrDefault()) &&
                b.Categories.Contains(book.Categories.FirstOrDefault()));
            if (sameBook != null && sameBook.BookStockByEditions.Any(bse => bse.BookEdition == model.BookEdition))
                throw new InvalidOperationException(string.Format(Resources.CantAddTheSameBook, book.Title));

            var stock = new BookStockByEdition
            {
                BookEdition = model.BookEdition,
                AvailableBooks = model.AvailableBooks,
                BorrowedBooks = 0,
                CreatedDate = DateTime.Now,
            };

            if (sameBook != null)
                sameBook.BookStockByEditions.Add(stock);
            else
            {
                book.BookStockByEditions.Add(stock);
                _bookRepository.Add(book);
            }

            var transaction = _bookRepository.GetTransaction();

            transaction.Commit();
        }
    }
}
