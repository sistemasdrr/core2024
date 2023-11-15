using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Domain.Entities.SqlCoreContext;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace DRRCore.Transversal.Mapper.Profiles.Core
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile() {

            CreateMap<AddOrUpdateEmployeeRequestDto, Employee > ()
                 .ForMember(dest => dest.StartDate, opt => opt?.MapFrom(src => VerifyDate(src.StartDate)))
                 .ForMember(dest => dest.EndDate, opt => opt?.MapFrom(src => VerifyDate(src.EndDate)))
                 .ForMember(dest => dest.HealthInsurances, opt => opt?.MapFrom(src => src.HealthInsuranceRequestDto))
                 .ReverseMap();
            CreateMap<AddHealthInsuranceRequestDto, HealthInsurance>().ReverseMap();
            CreateMap<Employee, GetEmployeeResponseDto>()
                 .ForMember(dest => dest.ShortName, opt => opt?.MapFrom(src => src.FirstName +" "+ src.LastName))
                 .ForMember(dest => dest.Code, opt => opt?.MapFrom(src => "E" + src.Id.ToString("D3")))
                 .ForMember(dest => dest.StartDate, opt => opt?.MapFrom(src => DateTimeToString(src.StartDate)))
                 .ForMember(dest => dest.Enable, opt => opt?.MapFrom(src => src.Enable))
                 .ForMember(dest => dest.Department, opt => opt?.MapFrom(src => src.IdJobDepartmentNavigation.Name))
                 .ForMember(dest => dest.Job, opt => opt?.MapFrom(src => src.IdJobNavigation.Name))
                 .ForMember(dest => dest.EndDate, opt => opt?.MapFrom(src => DateTimeToString(src.EndDate)))
                 .ForMember(dest => dest.HealthInsuranceResponseDto, opt => opt?.MapFrom(src => src.HealthInsurances))
                 .ReverseMap();
            CreateMap<HealthInsurance, GetHealthInsuranceResponseDto>()
                 .ForMember(dest => dest.Code, opt => opt?.MapFrom(src => "HS"+src.Id.ToString("D3")))
                 .ReverseMap();

        }
        private DateTime? VerifyDate(string date)
        {
            DateTime result;
            if(string.IsNullOrEmpty(date)) return null;
            return DateTime.TryParse(date, out result) ? result : null;
        }
        private string DateTimeToString(DateTime? date)
        {
            return date?.ToShortDateString();
        }
        
    }
}
