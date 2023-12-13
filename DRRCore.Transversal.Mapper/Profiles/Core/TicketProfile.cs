using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Transversal.Mapper.Profiles.Core
{
    public class TicketProfile:Profile
    {
        public TicketProfile() {

            CreateMap<AddOrUpdateTicketRequestDto, Ticket>()
                   .ForMember(dest => dest.OrderDate, opt => opt?.MapFrom(src => StaticFunctions.VerifyDate(src.OrderDate)))
                   .ForMember(dest => dest.ExpireDate, opt => opt?.MapFrom(src => StaticFunctions.VerifyDate(src.ExpireDate)))
                    .ForMember(dest => dest.RealExpireDate, opt => opt?.MapFrom(src => StaticFunctions.VerifyDate(src.RealExpireDate)))
                   .ReverseMap();

        }
    }
}
