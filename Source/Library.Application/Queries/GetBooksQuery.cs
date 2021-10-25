using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
using AutoMapper;
using Library.Application.DTOs;
using Library.Core.Contracts;
using Library.Core.Entities;

namespace Library.Application.Queries
{
    /// <summary>
    /// Query that gets the books for the data base.
    /// </summary>
    public class GetBooksQuery : IQuery<(Func<Book, bool>, bool), List<BookDto>>
    {
        private readonly IRepository<Book> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor for <see cref="GetBookQuery"/>.
        /// </summary>
        /// <param name="repository">Book repository instance.</param>
        /// <param name="mapper">Application mapper instance.</param>
        public GetBooksQuery(IRepository<Book> repository, IMapper mapper)
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(mapper, nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public List<BookDto> Execute((Func<Book, bool>, bool) param)
        {
            Guard.Against.Null(param, nameof(param));

            (Func<Book, bool> query, bool availableOnly) = param;

            var books = _repository.Query(query,
                c => new Book
                {
                    Id = c.Id,
                    Title = c.Title,
                    Authors = c.Authors.Where(a => string.IsNullOrEmpty(a.DeleteFlag)).ToList(),
                    Categories = c.Categories.Where(a => string.IsNullOrEmpty(a.DeleteFlag)).ToList(),
                    BookStockByEditions = c.BookStockByEditions.Where(a => string.IsNullOrEmpty(a.DeleteFlag) &&
                        c.BookStockByEditions.Any(a => a.AvailableBooks > 0 || !availableOnly)).ToList()
                });

            var booksDto = _mapper.Map<List<BookDto>>(books);

            return booksDto;
        }
    }

    /// <summary>
    /// Query that gets the categories for the data base.
    /// </summary>
    public class GetCategoriesQuery : IQuery<object, List<CategoryDto>>
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor for <see cref="GetCategoriesQuery"/>.
        /// </summary>
        /// <param name="repository">Category repository instance.</param>
        /// <param name="mapper">Application mapper instance.</param>
        public GetCategoriesQuery(IRepository<Category> repository, IMapper mapper)
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(mapper, nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public List<CategoryDto> Execute(object param)
        {

            var authors = _repository.GetDataSet();

            var authorsDto = _mapper.Map<List<CategoryDto>>(authors);

            return authorsDto;
        }
    }
}
