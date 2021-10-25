using System;
using System.Collections.Generic;
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
    /// Updates the selected book registry.
    /// </summary>
    public class UpdateBookCommand : ICommand<(Guid, BookForEditionDto)>
    {
        #region Fields

        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Author> _authorRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<BookStockByEdition> _stockRepository;
        private readonly IMapper _mapper;

        #endregion

        /// <summary>
        /// Default constructor for <see cref="UpdateBookCommand"/>.
        /// </summary>
        /// <param name="bookRepository">Represents the book repository instance.</param>
        /// <param name="mapper">Represents the mapper service instance.</param>
        public UpdateBookCommand(IRepository<Book> bookRepository,
            IRepository<BookStockByEdition> stockRepository,
            IRepository<Category> categoryRepository,
            IRepository<Author> authorRepository,
            IMapper mapper)
        {
            Guard.Against.Null(categoryRepository, nameof(categoryRepository));
            Guard.Against.Null(authorRepository, nameof(authorRepository));
            Guard.Against.Null(stockRepository, nameof(stockRepository));
            Guard.Against.Null(bookRepository, nameof(bookRepository));
            Guard.Against.Null(mapper, nameof(mapper));

            _categoryRepository = categoryRepository;
            _authorRepository = authorRepository;
            _stockRepository = stockRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public void Execute((Guid, BookForEditionDto) modelData)
        {
            Guard.Against.Null(modelData, nameof(modelData));

            (Guid bookId, BookForEditionDto model) = modelData;

            var book = _bookRepository.Get(x => x.Id == bookId);

            _mapper.Map(model, book);

            book.UpdatedDate = DateTime.Now;

            List<Category> categories = new List<Category>();
            foreach (var categoryId in model.Categories)
            {
                var category = _categoryRepository.Get(category => category.Id == categoryId);
                if (category is null)
                    throw new InvalidOperationException(string.Format(Resources.CategoryNotFound, categoryId));
                categories.Add(category);
            }
            if (!book.Categories.OrderBy(x => x.Id).SequenceEqual(categories.OrderBy(x => x.Id)))
                book.Categories = categories;

            List<Author> authors = new List<Author>();
            foreach (var authorId in model.Authors)
            {
                var author = _authorRepository.Get(author => author.Id == authorId);
                if (author is null)
                    throw new InvalidOperationException(string.Format(Resources.AuthorNotFound, authorId));
                authors.Add(author);
            }
            if (!book.Authors.OrderBy(x => x.Id).SequenceEqual(authors.OrderBy(x => x.Id)))
                book.Authors = authors;

            var version = _stockRepository.Get(stock => stock.Id == model.StockId);
            if (version is null)
                throw new InvalidOperationException(string.Format(Resources.BookEditionNotFound, model.BookEdition));

            version.BookEdition = model.BookEdition;
            version.AvailableBooks = model.AvailableBooks;
            version.BorrowedBooks = model.BorrowedBooks;
            version.UpdatedDate = DateTime.Now;

            var transaction = _bookRepository.GetTransaction();

            transaction.Commit();
        }
    }
}
