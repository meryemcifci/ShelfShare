using AutoMapper;
using ShelfShare.Business.DTOs.BookDto;
using ShelfShare.Business.DTOs.CommonDto;
using ShelfShare.Business.DTOs.FamilyDto;
using ShelfShare.Entity.Concrete;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace ShelfShare.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Categories,
                   opt => opt.MapFrom(src =>
                       !string.IsNullOrEmpty(src.Description)
                           ? src.Description.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList()
                           : new List<string>()));

            CreateMap<AppUser, UserDto>()

            .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src =>
                    !string.IsNullOrEmpty(src.UserName)
                        ? src.UserName.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList()
                        : new List<string>()));


            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<Group, GroupDto>();
            
            


        }
    }
}
