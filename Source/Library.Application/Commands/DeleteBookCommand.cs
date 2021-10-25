using System;
using Ardalis.GuardClauses;
using AutoMapper;
using Library.Core.Contracts;
using Library.Core.Entities;

namespace Library.Application.Commands
{
    /// <summary>
    /// Adds the delete flag to the book registry.
    /// </summary>
    public class DeleteBookCommand : ICommand<Guid>
    {
        private readonly IRepository<Book> _repository;
        private readonly IRepository<BookStockByEdition> _stockRepository;

        /// <summary>
        /// Default constructor for <see cref="DeleteBookCommand"/>.
        /// </summary>
        /// <param name="bookRepository">Represents the book repository instance.</param>
        /// <param name="stockRepository">Represents the stock repository instance.</param>
        public DeleteBookCommand(IRepository<Book> bookRepository,
            IRepository<BookStockByEdition> stockRepository)
        {
            Guard.Against.Null(stockRepository, nameof(stockRepository));
            Guard.Against.Null(bookRepository, nameof(bookRepository));

            _stockRepository = stockRepository;
            _repository = bookRepository;
        }

        /// <inheritdoc/>
        public void Execute(Guid bookId)
        {

            var book = _repository.Get(c => c.Id == bookId);

            book.DeleteFlag = "D";

            var bookEditions = _stockRepository.Query(stock => stock.BookId == book.Id);

            foreach (var edition in bookEditions)
                edition.DeleteFlag = "D";

            var transaction = _repository.GetTransaction();

            transaction.Commit();
        }
    }
}
