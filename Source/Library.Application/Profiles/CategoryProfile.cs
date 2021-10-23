using AutoMapper;
using Library.Application.DTOs;
using Library.Core.Entities;

namespace Library.Application.Profiles
{
    /// <summary>
    /// CategoryDto mapping configuration.
    /// </summary>
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, Category>()
                .ForMember(client => client.Id, action => action.Ignore());

            CreateMap<Category, CategoryDto>();
        }
    }
}
