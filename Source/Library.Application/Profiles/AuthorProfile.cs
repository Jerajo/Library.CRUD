using AutoMapper;
using Library.Application.DTOs;
using Library.Core.Entities;

namespace Library.Application.Profiles
{
    /// <summary>
    /// AuthorDto mapping configuration.
    /// </summary>
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<AuthorDto, Author>()
                .ForMember(client => client.Id, action => action.Ignore());

            CreateMap<Author, AuthorDto>();
        }
    }
}
