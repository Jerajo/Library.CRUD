using System;
using Ardalis.GuardClauses;
using AutoMapper;
using Library.Application.DTOs;
using Library.Core.Contracts;
using Library.Core.Entities;

namespace Library.Application.Commands
{
    /// <summary>
    /// Creates a new book registry.
    /// </summary>
    public class CreateBookCommand : ICommand<BookForCreationDto>
    {
        private readonly IRepository<Book> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor for <see cref="CreateBookCommand"/>.
        /// </summary>
        /// <param name="repository">Represents the book repository instance.</param>
        /// <param name="mapper">Represents the mapper service instance.</param>
        public CreateBookCommand(IRepository<Book> repository, IMapper mapper)
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(mapper, nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public void Execute(BookForCreationDto model)
        {
            Guard.Against.Null(model, nameof(model));

            var newClient = _mapper.Map<Book>(model);

            var transaction = _repository.GetTransaction();

            _repository.Add(newClient);

            transaction.Commit();
        }
    }
}
