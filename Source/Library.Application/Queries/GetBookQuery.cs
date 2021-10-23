using System;
using Ardalis.GuardClauses;
using AutoMapper;
using Library.Application.DTOs;
using Library.Core.Contracts;
using Library.Core.Entities;

namespace Library.Application.Queries
{
    /// <summary>
    /// Query that gets a book for the data base.
    /// </summary>
    public class GetBookQuery : IQuery<Func<Book, bool>, BookDto>
    {
        private readonly IRepository<Book> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor for <see cref="GetBookQuery"/>.
        /// </summary>
        /// <param name="repository">Book repository instance.</param>
        /// <param name="mapper">Application mapper instance.</param>
        public GetBookQuery(IRepository<Book> repository, IMapper mapper)
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(mapper, nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public BookDto Execute(Func<Book, bool> query)
        {
            Guard.Against.Null(query, nameof(query));

            var Books = _repository.Get(query);

            var BooksDto = _mapper.Map<BookDto>(Books);

            return BooksDto;
        }
    }
}
