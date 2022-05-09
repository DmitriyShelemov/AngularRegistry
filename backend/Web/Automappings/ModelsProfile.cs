using AutoMapper;
using Domain;
using Web.Models;

namespace Web.Automappings
{
    public class ModelsProfile : Profile
    {
        public ModelsProfile()
        {
            CreateMap<RegisterRequest, User>();

            CreateMap<Country, AreaModel>()
                .ForMember(x => x.Value, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.ViewValue, x => x.MapFrom(y => y.Name));

            CreateMap<Province, AreaModel>()
                .ForMember(x => x.Value, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.ViewValue, x => x.MapFrom(y => y.Name));
        }
    }
}
