using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Transversal.Mapper.Profiles.Core
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile() {

            CreateMap<Employee, AddOrUpdateEmployeeRequestDto > ().ReverseMap();
            CreateMap<GetEmployeeResponseDto, Employee>().ReverseMap();
        }
        
    }
}
