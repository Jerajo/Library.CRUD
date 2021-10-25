using System;
using Ardalis.GuardClauses;
using Library.Core.Contracts;
using Library.Core.Entities;

namespace Library.Application.Commands
{
    /// <summary>
    /// Adds the delete flag to the book registry.
    /// </summary>
    public class DeleteBookStockCommand : ICommand<Guid>
    {
        private readonly IRepository<BookStockByEdition> _repository;

        /// <summary>
        /// Default constructor for <see cref="DeleteBookStockCommand"/>.
        /// </summary>
        /// <param name="repository">Represents the stock repository instance.</param>
        public DeleteBookStockCommand(IRepository<BookStockByEdition> repository)
        {
            Guard.Against.Null(repository, nameof(repository));

            _repository = repository;
        }

        /// <inheritdoc/>
        public void Execute(Guid stockId)
        {
            var bookEdition = _repository.Get(c => c.Id == stockId);

            bookEdition.DeleteFlag = "D";

            var transaction = _repository.GetTransaction();

            transaction.Commit();
        }
    }
}
