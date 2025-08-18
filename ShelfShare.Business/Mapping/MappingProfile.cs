using AutoMapper;
using ShelfShare.Business.DTOs.BookDto;
using ShelfShare.Business.DTOs.CommonDto;
using ShelfShare.Business.DTOs.FamilyDto;
using ShelfShare.Entity.Concrete;
using System.Linq;

namespace ShelfShare.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.AverageRating,
                          opt => opt.MapFrom(src => src.Reviews.Any() ?
                                            src.Reviews.Average(r => r.Rating) : 0))
                .ForMember(dest => dest.ReviewCount,
                          opt => opt.MapFrom(src => src.Reviews.Count))
                .ForMember(dest => dest.Categories,
                          opt => opt.MapFrom(src => src.BookCategories.Select(bc => bc.Category.Name).ToList()))
                .ForMember(dest => dest.UserReadingStatus, opt => opt.Ignore());

            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            CreateMap<Family, FamilyDto>();
            
            


        }
    }
}
