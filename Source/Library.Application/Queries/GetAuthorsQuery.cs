using System.Collections.Generic;
using Ardalis.GuardClauses;
using AutoMapper;
using Library.Application.DTOs;
using Library.Core.Contracts;
using Library.Core.Entities;

namespace Library.Application.Queries
{
    /// <summary>
    /// Query that gets the authors for the data base.
    /// </summary>
    public class GetAuthorsQuery : IQuery<object, List<AuthorDto>>
    {
        private readonly IRepository<Author> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Default constructor for <see cref="GetAuthorsQuery"/>.
        /// </summary>
        /// <param name="repository">Author repository instance.</param>
        /// <param name="mapper">Application mapper instance.</param>
        public GetAuthorsQuery(IRepository<Author> repository, IMapper mapper)
        {
            Guard.Against.Null(repository, nameof(repository));
            Guard.Against.Null(mapper, nameof(mapper));

            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public List<AuthorDto> Execute(object param)
        {

            var authors = _repository.GetDataSet();

            var authorsDto = _mapper.Map<List<AuthorDto>>(authors);

            return authorsDto;
        }
    }
}
