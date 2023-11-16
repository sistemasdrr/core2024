using AutoMapper;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Transversal.Mapper.Profiles.Core
{
    public class ComboProfile:Profile
    {
        public ComboProfile()
        {
            CreateMap<DocumentType, GetComboValueResponseDto>()
               .ForMember(dest => dest.Id, opt => opt?.MapFrom(src => src.Id))
               .ForMember(dest => dest.Valor, opt => opt?.MapFrom(src => src.Abreviation))
               .ReverseMap();

            CreateMap<CivilStatus, GetComboValueResponseDto>()
               .ForMember(dest => dest.Id, opt => opt?.MapFrom(src => src.Id))
               .ForMember(dest => dest.Valor, opt => opt?.MapFrom(src => src.Name))
               .ReverseMap();

            CreateMap<Continent, GetComboValueResponseDto>()
              .ForMember(dest => dest.Id, opt => opt?.MapFrom(src => src.Id))
              .ForMember(dest => dest.Valor, opt => opt?.MapFrom(src => src.Name))
              .ReverseMap();

            CreateMap<JobDepartment, GetComboValueResponseDto>()
              .ForMember(dest => dest.Id, opt => opt?.MapFrom(src => src.Id))
              .ForMember(dest => dest.Valor, opt => opt?.MapFrom(src => src.Name))
              .ReverseMap();

            CreateMap<Job, GetComboValueResponseDto>()
            .ForMember(dest => dest.Id, opt => opt?.MapFrom(src => src.Id))
            .ForMember(dest => dest.Valor, opt => opt?.MapFrom(src => src.Name))
            .ReverseMap();

            CreateMap<BankAccountType, GetComboValueResponseDto>()
          .ForMember(dest => dest.Id, opt => opt?.MapFrom(src => src.Id))
          .ForMember(dest => dest.Valor, opt => opt?.MapFrom(src => src.Name))
          .ReverseMap();

            CreateMap<LegalPersonType, GetComboValueResponseDto>()
         .ForMember(dest => dest.Id, opt => opt?.MapFrom(src => src.Id))
         .ForMember(dest => dest.Valor, opt => opt?.MapFrom(src => src.Name))
         .ReverseMap();

            CreateMap<FamilyBondType, GetComboValueResponseDto>()
         .ForMember(dest => dest.Id, opt => opt?.MapFrom(src => src.Id))
         .ForMember(dest => dest.Valor, opt => opt?.MapFrom(src => src.Name))
         .ReverseMap();

            CreateMap<Currency, GetComboValueResponseDto>()
        .ForMember(dest => dest.Id, opt => opt?.MapFrom(src => src.Id))
        .ForMember(dest => dest.Valor, opt => opt?.MapFrom(src => src.Symbol+"-"+src.Name+"("+src.Abreviation+")"))
        .ReverseMap();

            CreateMap<CreditRisk, GetComboCreditRiskResponseDto>()
       .ForMember(dest => dest.Id, opt => opt?.MapFrom(src => src.Id))
       .ForMember(dest => dest.Valor, opt => opt?.MapFrom(src =>  src.Name))
       .ForMember(dest => dest.Rate, opt => opt?.MapFrom(src => src.Rate))
       .ForMember(dest => dest.Color, opt => opt?.MapFrom(src => src.Color))
       .ForMember(dest => dest.Identifier, opt => opt?.MapFrom(src => src.Identifier))
       .ForMember(dest => dest.Abreviation, opt => opt?.MapFrom(src => src.Abreviation))
       .ReverseMap();

            CreateMap<Country, GetComboValueFlagResponseDto>()
             .ForMember(dest => dest.Id, opt => opt?.MapFrom(src => src.Id))
             .ForMember(dest => dest.Valor, opt => opt?.MapFrom(src => src.Name))
             .ForMember(dest => dest.Bandera, opt => opt?.MapFrom(src => src.FlagIso))
             .ReverseMap();
        }
    }
}
