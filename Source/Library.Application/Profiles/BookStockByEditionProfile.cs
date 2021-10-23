using AutoMapper;
using Library.Application.DTOs;
using Library.Core.Entities;

namespace Library.Application.Profiles
{
    /// <summary>
    /// BookStockByEditionDTO mapping configuration.
    /// </summary>
    public class BookStockByEditionProfile : Profile
    {
        public BookStockByEditionProfile()
        {
            CreateMap<BookStockByEditionDto, BookStockByEdition>()
                .ForMember(client => client.Id, action => action.Ignore());

            CreateMap<BookStockByEdition, BookStockByEditionDto>();
        }
    }
}
