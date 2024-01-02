using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Application.DTO.Enum;
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
                  .ForMember(dest => dest.IsPending, opt => opt?.MapFrom(src => src.IdStatusTicket==(int?)TicketStatusEnum.Pendiente))
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

            CreateMap<Ticket, GetTicketRequestDto>()
                 .ForMember(dest => dest.IdSubscriber, opt => opt?.MapFrom(src => src.IdSubscriber == null ? 0 : src.IdSubscriber))
                 .ForMember(dest => dest.IdContinent, opt => opt?.MapFrom(src => src.IdContinent == null ? 0 : src.IdContinent))
                 .ForMember(dest => dest.IdCountry, opt => opt?.MapFrom(src => src.IdCountry == null ? 0 : src.IdCountry))
                 .ForMember(dest => dest.IdCompany, opt => opt?.MapFrom(src => src.IdCompany == null ? 0 : src.IdCompany))
                 .ForMember(dest => dest.IdPerson, opt => opt?.MapFrom(src => src.IdPerson == null ? 0 : src.IdPerson))
                 .ReverseMap();
            CreateMap<Ticket, GetListTicketResponseDto>()
                 .ForMember(dest => dest.IdSubscriber, opt => opt?.MapFrom(src => src.IdSubscriber == null ? 0 : src.IdSubscriber))
                 .ForMember(dest => dest.IdContinent, opt => opt?.MapFrom(src => src.IdContinent == null ? 0 : src.IdContinent))
                 .ForMember(dest => dest.IdCountry, opt => opt?.MapFrom(src => src.IdCountry == null ? 0 : src.IdCountry))
                 .ForMember(dest => dest.IdCompany, opt => opt?.MapFrom(src => src.IdCompany == null ? 0 : src.IdCompany))
                 .ForMember(dest => dest.IdPerson, opt => opt?.MapFrom(src => src.IdPerson == null ? 0 : src.IdPerson))
                 .ForMember(dest => dest.Number, opt => opt?.MapFrom(src => src.Number.ToString("D6")))
                 .ForMember(dest => dest.Status, opt => opt?.MapFrom(src => src.IdStatusTicketNavigation.Abrev))
                 .ForMember(dest => dest.StatusColor, opt => opt?.MapFrom(src => src.IdStatusTicketNavigation.Color))
                 .ForMember(dest => dest.StatusFinalOwner, opt => opt?.MapFrom(src => GetStatusFinalOwner(src.TicketHistories)))

                 .ForMember(dest => dest.SubscriberCode, opt => opt?.MapFrom(src => src.IdSubscriberNavigation.Code))
                 .ForMember(dest => dest.SubscriberName, opt => opt?.MapFrom(src => src.IdSubscriberNavigation.Name))
                 .ForMember(dest => dest.SubscriberCountry, opt => opt?.MapFrom(src => src.IdSubscriberNavigation.IdCountryNavigation.Name))
                 .ForMember(dest => dest.SubscriberFlag, opt => opt?.MapFrom(src => src.IdSubscriberNavigation.IdCountryNavigation.FlagIso))
                 .ForMember(dest => dest.SubscriberIndications, opt => opt?.MapFrom(src => src.IdSubscriberNavigation.Indications))

                 .ForMember(dest => dest.InvestigatedContinent, opt => opt?.MapFrom(src => src.IdCompanyNavigation.IdCountryNavigation.IdContinentNavigation.Name))
                 .ForMember(dest => dest.InvestigatedCountry, opt => opt?.MapFrom(src => src.IdCompanyNavigation.IdCountryNavigation.Name))
                 .ForMember(dest => dest.InvestigatedFlag, opt => opt?.MapFrom(src => src.IdCompanyNavigation.IdCountryNavigation.FlagIso))


                 .ForMember(dest => dest.OrderDate, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.OrderDate)))
                 .ForMember(dest => dest.ExpireDate, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.ExpireDate)))
                 .ForMember(dest => dest.RealExpireDate, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.RealExpireDate)))

                 .ForMember(dest => dest.Price, opt => opt?.MapFrom(src => src.Price == null ? 0 : src.Price))
                 .ForMember(dest => dest.Quality, opt => opt?.MapFrom(src => src.About == "E" ? src.IdCompanyNavigation.Quality : src.IdPersonNavigation.Language))
                 .ForMember(dest => dest.DispatchDate, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.DispatchtDate)))
                 .ReverseMap();


            CreateMap<Ticket, GetListPendingTicketResponseDto>()
                .ForMember(dest => dest.Number, opt => opt?.MapFrom(src => src.Number.ToString("D6")))
                .ForMember(dest => dest.Name, opt => opt?.MapFrom(src => src.BusineesName))
                .ForMember(dest => dest.Commentary, opt => opt?.MapFrom(src => src.TicketAssignation==null?string.Empty:src.TicketAssignation.Commentary??string.Empty))
                .ForMember(dest => dest.Receptor, opt => opt?.MapFrom(src => src.TicketAssignation == null?0:src.TicketAssignation.IdEmployee))
                .ForMember(dest => dest.HasFiles, opt => opt?.MapFrom(src => src.TicketFiles.Count>0))
                .ForMember(dest => dest.Files, opt => opt?.MapFrom(src => src.TicketFiles))
                .ForMember(dest => dest.OrderDate, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.OrderDate)))
                .ForMember(dest => dest.ExpireDate, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.ExpireDate)))

                .ReverseMap();

            CreateMap<TicketFileResponseDto, TicketFile>().ReverseMap();
        }

        private string GetStatusFinalOwner(ICollection<TicketHistory> ticketHistories)
        {
            var lastHistory = ticketHistories.FirstOrDefault();

            if (lastHistory == null) return string.Empty;

            switch (lastHistory.IdStatusTicket)
            {
                case (int?)TicketStatusEnum.Asig_Agente:
                case (int?)TicketStatusEnum.Asig_Digitidor:
                case (int?)TicketStatusEnum.Asig_Reportero:
                case (int?)TicketStatusEnum.Asig_Traductor:
                case (int?)TicketStatusEnum.Asig_Supervisor:
                case (int?)TicketStatusEnum.Asig_Multiple:
                    return lastHistory.AsignedTo??string.Empty;
                case (int?)TicketStatusEnum.En_Consulta:
                    return "Abonado";
                default:
                    return string.Empty;
            }
        }

        private string GetStatusColor(ICollection<TicketHistory> ticketHistories)
        {
            throw new NotImplementedException();
        }

        private string GetStatus(ICollection<TicketHistory> ticketHistories)
        {
           
            
            
            return string.Empty;
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
