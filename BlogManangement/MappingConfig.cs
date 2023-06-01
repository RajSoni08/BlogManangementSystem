using AutoMapper;
using Models;
using Models.DTO;

namespace BlogManangementAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Blog , BlogDTO>().ReverseMap();
            CreateMap<Subscription,SubsrciptionDTO>().ReverseMap();
        }

    }
}
