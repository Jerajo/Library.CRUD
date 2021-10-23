using AutoMapper;

using Library.Application.DTOs;
using Library.Core.Entities;

namespace Library.Application.Profiles
{
    /// <summary>
    /// BookDto mapping configuration.
    /// </summary>
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookDto, Book>()
                .ForMember(book => book.Id, action => action.Ignore());

            CreateMap<Book, BookDto>();

            CreateMap<BookForCreationDto, Book>();
            CreateMap<BookForEditionDto, Book>();
        }
    }
}
