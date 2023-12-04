using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Transversal.Common;

namespace DRRCore.Transversal.Mapper.Profiles.Core
{
    public class SubscriberProfile : Profile
    {
        public SubscriberProfile() 
        {
            CreateMap<AddOrUpdateSubscriberRequestDto, Subscriber>()
            .ForMember(dest => dest.StartDate, opt => opt?.MapFrom(src => StaticFunctions.VerifyDate(src.StartDate)))
            .ForMember(dest => dest.IdContinent, opt => opt?.MapFrom(src => src.IdContinent == 0 ? null : src.IdContinent))
            .ForMember(dest => dest.IdCountry, opt => opt?.MapFrom(src => src.IdCountry == 0 ? null : src.IdCountry))
            .ForMember(dest => dest.IdCurrency, opt => opt?.MapFrom(src => src.IdCurrency == 0 ? null : src.IdCurrency))
            .ForMember(dest => dest.IdRubro, opt => opt?.MapFrom(src => src.IdRubro == 0 ? null : src.IdRubro))
        .ReverseMap();
        }
        
    }
}
