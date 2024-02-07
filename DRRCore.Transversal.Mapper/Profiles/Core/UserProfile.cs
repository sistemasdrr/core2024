using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Transversal.Common;

namespace DRRCore.Transversal.Mapper.Profiles.Core
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Process, GetProcessResponseDto>()
               .ForMember(dest => dest.OrderItem, opt => opt?.MapFrom(src => src.OrderItem))
           .ReverseMap();
            CreateMap<UserLogin, GetUserResponseDto>()
               .ForMember(dest => dest.IdEmployee, opt => opt?.MapFrom(src => src.IdEmployee == 0 ? null : src.IdEmployee))
           .ReverseMap(); 
            CreateMap<UserProcess, UserProcessDto>()
               .ForMember(dest => dest.IdProcess, opt => opt?.MapFrom(src => src.IdProcess == 0 ? null : src.IdProcess))
               .ForMember(dest => dest.IdUser, opt => opt?.MapFrom(src => src.IdUser == 0 ? null : src.IdUser))
           .ReverseMap();

        }
    }
}
