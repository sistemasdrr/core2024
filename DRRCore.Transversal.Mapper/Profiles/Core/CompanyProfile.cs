using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Transversal.Common;

namespace DRRCore.Transversal.Mapper.Profiles.Core
{
    public class CompanyProfile : Profile
    {
       
        public CompanyProfile() {

            CreateMap<AddOrUpdateCompanyRequestDto, Company>()
                   .ForMember(dest => dest.IdLegalPersonType, opt => opt?.MapFrom(src => src.IdLegalPersonType == 0 ? null : src.IdLegalPersonType))
                  .ForMember(dest => dest.IdLegalRegisterSituation, opt => opt?.MapFrom(src => src.IdLegalRegisterSituation == 0 ? null : src.IdLegalRegisterSituation))
                  .ForMember(dest => dest.IdCreditRisk, opt => opt?.MapFrom(src => src.IdCreditRisk == 0 ? null : src.IdCreditRisk))
                  .ForMember(dest => dest.IdPaymentPolicy, opt => opt?.MapFrom(src => src.IdPaymentPolicy == 0 ? null : src.IdPaymentPolicy))
                  .ForMember(dest => dest.IdReputation, opt => opt?.MapFrom(src => src.IdReputation == 0 ? null : src.IdReputation))
                  .ForMember(dest => dest.IdCountry, opt => opt?.MapFrom(src => src.IdCountry==0?null:src.IdCountry))
                   .ForMember(dest => dest.LastSearched, opt => opt?.MapFrom(src => StaticFunctions.VerifyDate(src.LastSearched)))
                   .ForMember(dest => dest.ConstitutionDate, opt => opt?.MapFrom(src => StaticFunctions.VerifyDate(src.ConstitutionDate)))
                   .ReverseMap();
            CreateMap<TraductionDto, Traduction>()
                 .ReverseMap();



        }
    }
}
