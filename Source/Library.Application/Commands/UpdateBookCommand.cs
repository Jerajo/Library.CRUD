using System;
using Ardalis.GuardClauses;
using AutoMapper;
using Library.Application.DTOs;
using Library.Core.Contracts;
using Library.Core.Entities;

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

            var transaction = _bookRepository.GetTransaction();

            transaction.Commit();
        }
    }
}
