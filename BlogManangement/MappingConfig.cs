using AutoMapper;
using Models;

namespace BlogManangementAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Blog , BlogDTO>().ReverseMap();
        }

    }
}
