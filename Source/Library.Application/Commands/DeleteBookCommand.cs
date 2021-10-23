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
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor for <see cref="DeleteBookCommand"/>.
        /// </summary>
        /// <param name="repository">Represents the book repository instance.</param>
        /// <param name="mapper">Represents the mapper service instance.</param>
        public DeleteBookCommand(IRepository<Book> repository, IMapper mapper)
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(mapper, nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public void Execute(Guid bookId)
        {
            var transaction = _repository.GetTransaction();

            var book = _repository.Get(c => c.Id == bookId);

            book.DeleteFlag = "D";

            transaction.Commit();
        }
    }
}
