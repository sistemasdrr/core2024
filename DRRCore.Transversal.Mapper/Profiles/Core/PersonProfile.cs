using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Transversal.Common;

namespace DRRCore.Transversal.Mapper.Profiles.Core
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<AddOrUpdatePersonRequestDto, Person>()
                 .ForMember(dest => dest.IdCivilStatus, opt => opt?.MapFrom(src => src.IdCivilStatus == 0 ? null : src.IdCivilStatus))
                 .ForMember(dest => dest.IdLegalRegisterSituation, opt => opt?.MapFrom(src => src.IdLegalRegisterSituation == 0 ? null : src.IdLegalRegisterSituation))
                 .ForMember(dest => dest.IdCreditRisk, opt => opt?.MapFrom(src => src.IdCreditRisk == 0 ? null : src.IdCreditRisk))
                 .ForMember(dest => dest.IdPaymentPolicy, opt => opt?.MapFrom(src => src.IdPaymentPolicy == 0 ? null : src.IdPaymentPolicy))
                 .ForMember(dest => dest.IdReputation, opt => opt?.MapFrom(src => src.IdReputation == 0 ? null : src.IdReputation))
                 .ForMember(dest => dest.IdCountry, opt => opt?.MapFrom(src => src.IdCountry == 0 ? null : src.IdCountry))
                 .ForMember(dest => dest.IdDocumentType, opt => opt?.MapFrom(src => src.IdDocumentType == 0 ? null : src.IdDocumentType))
                 .ForMember(dest => dest.RelationshipDocumentType, opt => opt?.MapFrom(src => src.RelationshipDocumentType == 0 ? null : src.RelationshipDocumentType))
                 .ForMember(dest => dest.IdPersonSituation, opt => opt?.MapFrom(src => src.IdPersonSituation == 0 ? null : src.IdPersonSituation))
                 .ForMember(dest => dest.LastSearched, opt => opt?.MapFrom(src => StaticFunctions.VerifyDate(src.LastSearched)))
                 .ForMember(dest => dest.BirthDate, opt => opt?.MapFrom(src => StaticFunctions.VerifyDate(src.BirthDate)))
              .ReverseMap();
            CreateMap<Person, GetPersonResponseDto>()
                 .ForMember(dest => dest.IdCivilStatus, opt => opt?.MapFrom(src => src.IdCivilStatus == 0 ? null : src.IdCivilStatus))
                 .ForMember(dest => dest.IdLegalRegisterSituation, opt => opt?.MapFrom(src => src.IdLegalRegisterSituation == 0 ? null : src.IdLegalRegisterSituation))
                 .ForMember(dest => dest.IdCreditRisk, opt => opt?.MapFrom(src => src.IdCreditRisk == 0 ? null : src.IdCreditRisk))
                 .ForMember(dest => dest.IdPaymentPolicy, opt => opt?.MapFrom(src => src.IdPaymentPolicy == 0 ? null : src.IdPaymentPolicy))
                 .ForMember(dest => dest.IdReputation, opt => opt?.MapFrom(src => src.IdReputation == 0 ? null : src.IdReputation))
                 .ForMember(dest => dest.IdCountry, opt => opt?.MapFrom(src => src.IdCountry == 0 ? null : src.IdCountry))
                 .ForMember(dest => dest.IdDocumentType, opt => opt?.MapFrom(src => src.IdDocumentType == 0 ? null : src.IdDocumentType))
                 .ForMember(dest => dest.RelationshipDocumentType, opt => opt?.MapFrom(src => src.RelationshipDocumentType == 0 ? null : src.RelationshipDocumentType))
                 .ForMember(dest => dest.Profession, opt => opt?.MapFrom(src => src.Profession))
                 .ForMember(dest => dest.IdPersonSituation, opt => opt?.MapFrom(src => src.IdPersonSituation == 0 ? null : src.IdPersonSituation))
                 .ForMember(dest => dest.LastSearched, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.LastSearched)))
                 .ForMember(dest => dest.BirthDate, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.BirthDate)))
              .ReverseMap();
            CreateMap<Person, GetListPersonResponseDto>()
                 .ForMember(dest => dest.CreditRisk, opt => opt?.MapFrom(src => src.IdCreditRiskNavigation.Identifier))
                 .ForMember(dest => dest.Country, opt => opt?.MapFrom(src => src.IdCountryNavigation.Iso))
                 .ForMember(dest => dest.FlagCountry, opt => opt?.MapFrom(src => src.IdCountryNavigation.FlagIso))
                 .ForMember(dest => dest.DocumentType, opt => opt?.MapFrom(src => src.IdDocumentTypeNavigation.Abreviation))
                 .ForMember(dest => dest.Profession, opt => opt?.MapFrom(src => src.Profession))
                 .ForMember(dest => dest.LastSearched, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.LastSearched)))
                 .ForMember(dest => dest.BirthDate, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.BirthDate)))
                 .ForMember(dest => dest.OnWeb, opt => opt?.MapFrom(src => src.OnWeb))
                 .ForMember(dest => dest.Enable, opt => opt?.MapFrom(src => src.Enable))
              .ReverseMap();
        }
    }
}
