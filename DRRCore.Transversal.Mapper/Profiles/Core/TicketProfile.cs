using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Domain.Entities.MYSQLContext;
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
                    .ForMember(dest => dest.IdPerson, opt => opt?.MapFrom(src => src.IdPerson==0?null:src.IdPerson))
                    .ForMember(dest => dest.IdCompany, opt => opt?.MapFrom(src => src.IdCompany == 0 ? null : src.IdCompany))

                   .ReverseMap();

            CreateMap<Ticket, GetListSameSearchedReportResponseDto>()
                  .ForMember(dest => dest.TypeReport, opt => opt?.MapFrom(src => src.ReportType))
                  .ForMember(dest => dest.DispatchtDate, opt => opt?.MapFrom(src => src.DispatchtDate))
                  .ForMember(dest => dest.IsPending, opt => opt?.MapFrom(src => src.Status==1))
                  .ForMember(dest => dest.TicketNumber, opt => opt?.MapFrom(src =>"N"+ src.Number.ToString("D6")))
                  .ForMember(dest => dest.RequestedName, opt => opt?.MapFrom(src => src.RequestedName))
                  .ForMember(dest => dest.Dispatch, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.DispatchtDate)))
                  .ForMember(dest => dest.Subscriber, opt => opt?.MapFrom(src => src.IdSubscriberNavigation.Code))
                  .ForMember(dest => dest.Procedure, opt => opt?.MapFrom(src => src.ProcedureType))
                  .ReverseMap();

            CreateMap<TCupon, GetListSameSearchedReportResponseDto>()
                  .ForMember(dest => dest.TypeReport, opt => opt?.MapFrom(src => src.CupTipinf))
                  .ForMember(dest => dest.DispatchtDate, opt => opt?.MapFrom(src => src.CupFecdes))
                  .ForMember(dest => dest.IsPending, opt => opt?.MapFrom(src => src.CupEstado !="J" && src.CupEstado!="I"))
                  .ForMember(dest => dest.TicketNumber, opt => opt?.MapFrom(src =>"A"+ src.CupCodigo.ToString("D6")))
                  .ForMember(dest => dest.RequestedName, opt => opt?.MapFrom(src => src.CupNomsol))
                  .ForMember(dest => dest.Dispatch, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.CupFecdes)))
                  .ForMember(dest => dest.Subscriber, opt => opt?.MapFrom(src => src.AboCodigo))
                  .ForMember(dest => dest.Procedure, opt => opt?.MapFrom(src => GetProcedureType(src.TramCodigo)))
                  .ReverseMap();
        }
        private static string GetProcedureType(string? tram)
        {
            if (!string.IsNullOrEmpty(tram))
            {
                var value = int.Parse(tram);
                if (value <= 8)
                {
                    return "T" + value;
                }
                else
                {
                    switch (value)
                    {
                        case 9:
                            return "O1";
                        case 10:
                            return "O2";
                        case 11:
                            return "O3";
                    }
                }
            }
            return string.Empty;
        }
      
    }
}
