﻿using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
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
            CreateMap<Subscriber, GetListSubscriberResponseDto>()
            .ForMember(dest => dest.Id, opt => opt?.MapFrom(src => src.Id))
            .ForMember(dest => dest.Code, opt => opt?.MapFrom(src => src.Code))
            .ForMember(dest => dest.Name, opt => opt?.MapFrom(src => src.Name))
            .ForMember(dest => dest.Acronym, opt => opt?.MapFrom(src => src.Acronym))
            .ForMember(dest => dest.Address, opt => opt?.MapFrom(src => src.Address))
            .ForMember(dest => dest.Country, opt => opt?.MapFrom(src => src.IdCountryNavigation != null ? src.IdCountryNavigation.Name : string.Empty))
            .ForMember(dest => dest.IsoCountry, opt => opt?.MapFrom(src => src.IdCountryNavigation != null ? src.IdCountryNavigation.Iso : string.Empty))
            .ForMember(dest => dest.FlagCountry, opt => opt?.MapFrom(src => src.IdCountryNavigation != null ? src.IdCountryNavigation.FlagIso : string.Empty))
            .ForMember(dest => dest.Enable, opt => opt?.MapFrom(src => src.Enable))
        .ReverseMap();
            CreateMap<Subscriber, GetSubscriberRequestDto>()
            .ForMember(dest => dest.Id, opt => opt?.MapFrom(src => src.Id))
            .ForMember(dest => dest.IdContinent, opt => opt?.MapFrom(src => src.IdContinent))
            .ForMember(dest => dest.IdCountry, opt => opt?.MapFrom(src => src.IdCountry))
            .ForMember(dest => dest.StartDate, opt => opt?.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.IdCurrency, opt => opt?.MapFrom(src => src.IdCurrency))
            .ForMember(dest => dest.Enable, opt => opt?.MapFrom(src => src.Enable))
        .ReverseMap();
        }
        
    }
}
