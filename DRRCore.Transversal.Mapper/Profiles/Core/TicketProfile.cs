using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Application.DTO.Enum;
using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Transversal.Common;

namespace DRRCore.Transversal.Mapper.Profiles.Core
{
    public class TicketProfile:Profile
    {
        public TicketProfile() {

            CreateMap<AddOrUpdateTicketRequestDto, Ticket>()
                   .ForMember(dest => dest.OrderDate, opt => opt?.MapFrom(src => src.OrderDate))
                   .ForMember(dest => dest.ExpireDate, opt => opt?.MapFrom(src => src.ExpireDate))
                    .ForMember(dest => dest.RealExpireDate, opt => opt?.MapFrom(src => src.RealExpireDate))
                    .ForMember(dest => dest.IdPerson, opt => opt?.MapFrom(src => src.IdPerson==0?null:src.IdPerson))
                    .ForMember(dest => dest.IdCompany, opt => opt?.MapFrom(src => src.IdCompany == 0 ? null : src.IdCompany))

                   .ReverseMap();

            CreateMap<Ticket, GetListSameSearchedReportResponseDto>()
                  .ForMember(dest => dest.TypeReport, opt => opt?.MapFrom(src => src.ReportType))
                  .ForMember(dest => dest.DispatchtDate, opt => opt?.MapFrom(src => src.DispatchtDate))
                  .ForMember(dest => dest.DispatchtDateString, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.DispatchtDate)))
                  .ForMember(dest => dest.IsPending, opt => opt?.MapFrom(src => src.IdStatusTicket==(int?)TicketStatusEnum.Pendiente))
                  .ForMember(dest => dest.TicketNumber, opt => opt?.MapFrom(src =>"N-"+ src.Number.ToString("D6")))
                  .ForMember(dest => dest.RequestedName, opt => opt?.MapFrom(src => src.RequestedName))
                  .ForMember(dest => dest.Dispatch, opt => opt?.MapFrom(src => src.DispatchedName))
                  .ForMember(dest => dest.Subscriber, opt => opt?.MapFrom(src => src.IdSubscriberNavigation.Code))
                  .ForMember(dest => dest.Procedure, opt => opt?.MapFrom(src => src.ProcedureType))
                  .ReverseMap();

            CreateMap<OldTicket, GetListSameSearchedReportResponseDto>()
                  .ForMember(dest => dest.TypeReport, opt => opt?.MapFrom(src => src.TipoInforme))
                  .ForMember(dest => dest.DispatchtDate, opt => opt?.MapFrom(src =>src.FechaDespacho))
                  .ForMember(dest => dest.DispatchtDateString, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.FechaDespacho)))
                  .ForMember(dest => dest.IsPending, opt => opt?.MapFrom(src => false))
                  .ForMember(dest => dest.TicketNumber, opt => opt?.MapFrom(src =>"A-"+ src.Cupcodigo))
                  .ForMember(dest => dest.RequestedName, opt => opt?.MapFrom(src => src.NombreSolicitado))
                  .ForMember(dest => dest.Dispatch, opt => opt?.MapFrom(src => src.NombreDespachado))
                  .ForMember(dest => dest.Subscriber, opt => opt?.MapFrom(src => src.Abonado))
                  .ForMember(dest => dest.Procedure, opt => opt?.MapFrom(src => src.Tramite))
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
                 .ForMember(dest => dest.ProcedureType, opt => opt?.MapFrom(src => src.ProcedureType == null ? string.Empty : src.ProcedureType.Trim()))
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
                 .ForMember(dest => dest.StatusQuery, opt => opt?.MapFrom(src => src.TicketQuery!=null?src.TicketQuery.Status:0))
                  .ForMember(dest => dest.HasQuery, opt => opt?.MapFrom(src => src.TicketQuery!=null))

                   .ForMember(dest => dest.Commentary, opt => opt?.MapFrom(src => src.TicketAssignation == null ? string.Empty : src.TicketAssignation.Commentary ?? string.Empty))
                .ForMember(dest => dest.Receptor, opt => opt?.MapFrom(src => src.TicketAssignation == null ? 0 : src.TicketAssignation.IdEmployeeNavigation.UserLogins.FirstOrDefault().Id))
                .ForMember(dest => dest.HasFiles, opt => opt?.MapFrom(src => src.TicketFiles.Count > 0))
                .ForMember(dest => dest.Files, opt => opt?.MapFrom(src => src.TicketFiles))

                  .ReverseMap();
            CreateMap<TicketHistory, GetListTicketResponseDto>()
                 .ForMember(dest => dest.IdSubscriber, opt => opt?.MapFrom(src => src.IdTicketNavigation.IdSubscriber == null ? 0 : src.IdTicketNavigation.IdSubscriber))
                 .ForMember(dest => dest.ProcedureType, opt => opt?.MapFrom(src => src.IdTicketNavigation.ProcedureType == null ? string.Empty : src.IdTicketNavigation.ProcedureType.Trim()))
                 .ForMember(dest => dest.IdContinent, opt => opt?.MapFrom(src => src.IdTicketNavigation.IdContinent == null ? 0 : src.IdTicketNavigation.IdContinent))
                 .ForMember(dest => dest.IdCountry, opt => opt?.MapFrom(src => src.IdTicketNavigation.IdCountry == null ? 0 : src.IdTicketNavigation.IdCountry))
                 .ForMember(dest => dest.IdCompany, opt => opt?.MapFrom(src => src.IdTicketNavigation.IdCompany == null ? 0 : src.IdTicketNavigation.IdCompany))
                 .ForMember(dest => dest.IdPerson, opt => opt?.MapFrom(src => src.IdTicketNavigation.IdPerson == null ? 0 : src.IdTicketNavigation.IdPerson))
                 .ForMember(dest => dest.Number, opt => opt?.MapFrom(src => src.IdTicketNavigation.Number.ToString("D6")))
                 .ForMember(dest => dest.Status, opt => opt?.MapFrom(src => src.IdStatusTicketNavigation.Abrev))
                 .ForMember(dest => dest.StatusColor, opt => opt?.MapFrom(src => src.IdStatusTicketNavigation.Color))
                 .ForMember(dest => dest.StatusFinalOwner, opt => opt?.MapFrom(src => GetStatusFinalOwner(src.IdTicketNavigation.TicketHistories)))
                 .ForMember(dest => dest.Language, opt => opt?.MapFrom(src => src.IdTicketNavigation.IdCompanyNavigation.Language))
                 .ForMember(dest => dest.About, opt => opt?.MapFrom(src => src.IdTicketNavigation.About))
                 .ForMember(dest => dest.AsignedTo, opt => opt?.MapFrom(src => src.AsignedTo == null ? "" : src.AsignedTo))
                 .ForMember(dest => dest.QueryCredit, opt => opt?.MapFrom(src => src.IdTicketNavigation.QueryCredit == null ? "" : src.IdTicketNavigation.QueryCredit))
                 .ForMember(dest => dest.TimeLimit, opt => opt?.MapFrom(src => src.IdTicketNavigation.TimeLimit == null ? "" : src.IdTicketNavigation.TimeLimit))
                 .ForMember(dest => dest.RevealName, opt => opt?.MapFrom(src => src.IdTicketNavigation.RevealName))
                 .ForMember(dest => dest.NameRevealed, opt => opt?.MapFrom(src => src.IdTicketNavigation.NameRevealed == null ? "" : src.IdTicketNavigation.NameRevealed))
                 .ForMember(dest => dest.ReferenceNumber, opt => opt?.MapFrom(src => src.IdTicketNavigation.ReferenceNumber == null ? "" : src.IdTicketNavigation.ReferenceNumber))
                 .ForMember(dest => dest.AditionalData, opt => opt?.MapFrom(src => src.IdTicketNavigation.AditionalData == null ? "" : src.IdTicketNavigation.AditionalData))
                 .ForMember(dest => dest.BusineesName, opt => opt?.MapFrom(src => src.IdTicketNavigation.BusineesName == null ? "" : src.IdTicketNavigation.BusineesName))
                 .ForMember(dest => dest.ComercialName, opt => opt?.MapFrom(src => src.IdTicketNavigation.ComercialName == null ? "" : src.IdTicketNavigation.ComercialName))
                 .ForMember(dest => dest.RequestedName, opt => opt?.MapFrom(src => src.IdTicketNavigation.RequestedName == null ? "" : src.IdTicketNavigation.RequestedName))
                 .ForMember(dest => dest.TaxType, opt => opt?.MapFrom(src => src.IdTicketNavigation.TaxType == null ? "" : src.IdTicketNavigation.TaxType))
                 .ForMember(dest => dest.TaxCode, opt => opt?.MapFrom(src => src.IdTicketNavigation.TaxCode == null ? "" : src.IdTicketNavigation.TaxCode))
                 .ForMember(dest => dest.City, opt => opt?.MapFrom(src => src.IdTicketNavigation.City == null ? "" : src.IdTicketNavigation.City))
                 .ForMember(dest => dest.Email, opt => opt?.MapFrom(src => src.IdTicketNavigation.Email == null ? "" : src.IdTicketNavigation.Email))
                 .ForMember(dest => dest.Address, opt => opt?.MapFrom(src => src.IdTicketNavigation.Address == null ? "" : src.IdTicketNavigation.Address))
                 .ForMember(dest => dest.Telephone, opt => opt?.MapFrom(src => src.IdTicketNavigation.Telephone == null ? "" : src.IdTicketNavigation.Telephone))
                 .ForMember(dest => dest.ReportType, opt => opt?.MapFrom(src => src.IdTicketNavigation.ReportType == null ? "" : src.IdTicketNavigation.ReportType))
                 .ForMember(dest => dest.DispatchDate, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.IdTicketNavigation.DispatchtDate)))
                 .ForMember(dest => dest.Creditrisk, opt => opt?.MapFrom(src => src.IdTicketNavigation.IdCompanyNavigation.IdCreditRisk == null ? 0 : src.IdTicketNavigation.IdCompanyNavigation.IdCreditRisk))

                 .ForMember(dest => dest.SubscriberCode, opt => opt?.MapFrom(src => src.IdTicketNavigation.IdSubscriberNavigation.Code))
                 .ForMember(dest => dest.SubscriberName, opt => opt?.MapFrom(src => src.IdTicketNavigation.IdSubscriberNavigation.Name))
                 .ForMember(dest => dest.SubscriberCountry, opt => opt?.MapFrom(src => src.IdTicketNavigation.IdSubscriberNavigation.IdCountryNavigation.Name))
                 .ForMember(dest => dest.SubscriberFlag, opt => opt?.MapFrom(src => src.IdTicketNavigation.IdSubscriberNavigation.IdCountryNavigation.FlagIso))
                 .ForMember(dest => dest.SubscriberIndications, opt => opt?.MapFrom(src => src.IdTicketNavigation.IdSubscriberNavigation.Indications))

                 .ForMember(dest => dest.InvestigatedContinent, opt => opt?.MapFrom(src => src.IdTicketNavigation.IdCompanyNavigation.IdCountryNavigation.IdContinentNavigation.Name))
                 .ForMember(dest => dest.InvestigatedCountry, opt => opt?.MapFrom(src => src.IdTicketNavigation.IdCompanyNavigation.IdCountryNavigation.Name))
                 .ForMember(dest => dest.InvestigatedFlag, opt => opt?.MapFrom(src => src.IdTicketNavigation.IdCompanyNavigation.IdCountryNavigation.FlagIso))


                 .ForMember(dest => dest.OrderDate, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.IdTicketNavigation.OrderDate)))
                 .ForMember(dest => dest.ExpireDate, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.IdTicketNavigation.ExpireDate)))
                 .ForMember(dest => dest.RealExpireDate, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.IdTicketNavigation.RealExpireDate)))

                 .ForMember(dest => dest.Price, opt => opt?.MapFrom(src => src.IdTicketNavigation.Price == null ? 0 : src.IdTicketNavigation.Price))
                 .ForMember(dest => dest.Quality, opt => opt?.MapFrom(src => src.IdTicketNavigation.About == "E" ? src.IdTicketNavigation.IdCompanyNavigation.Quality : src.IdTicketNavigation.IdPersonNavigation.Language))
                 .ForMember(dest => dest.DispatchDate, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.IdTicketNavigation.DispatchtDate)))
                 .ForMember(dest => dest.StatusQuery, opt => opt?.MapFrom(src => src.IdTicketNavigation.TicketQuery != null ? src.IdTicketNavigation.TicketQuery.Status : 0))
                  .ForMember(dest => dest.HasQuery, opt => opt?.MapFrom(src => src.IdTicketNavigation.TicketQuery != null))

                   .ForMember(dest => dest.Commentary, opt => opt?.MapFrom(src => src.IdTicketNavigation.TicketAssignation == null ? string.Empty : src.IdTicketNavigation.TicketAssignation.Commentary ?? string.Empty))
                .ForMember(dest => dest.Receptor, opt => opt?.MapFrom(src => src.IdTicketNavigation.TicketAssignation == null ? 0 : src.IdTicketNavigation.TicketAssignation.IdEmployeeNavigation.UserLogins.FirstOrDefault().Id))
                .ForMember(dest => dest.HasFiles, opt => opt?.MapFrom(src => src.IdTicketNavigation.TicketFiles.Count > 0))
                .ForMember(dest => dest.Files, opt => opt?.MapFrom(src => src.IdTicketNavigation.TicketFiles))

                  .ReverseMap();

            CreateMap<Ticket, GetListPendingTicketResponseDto>()
                .ForMember(dest => dest.ProcedureType, opt => opt?.MapFrom(src => src.ProcedureType == null ? string.Empty : src.ProcedureType.Trim()))
                .ForMember(dest => dest.Number, opt => opt?.MapFrom(src => src.Number.ToString("D6")))
                .ForMember(dest => dest.Name, opt => opt?.MapFrom(src => src.BusineesName))
                .ForMember(dest => dest.Commentary, opt => opt?.MapFrom(src => src.TicketAssignation==null?string.Empty:src.TicketAssignation.Commentary??string.Empty))
                .ForMember(dest => dest.Receptor, opt => opt?.MapFrom(src => src.TicketAssignation == null?0:src.TicketAssignation.IdEmployeeNavigation.UserLogins.FirstOrDefault().Id))
                .ForMember(dest => dest.HasFiles, opt => opt?.MapFrom(src => src.TicketFiles.Count>0))
                .ForMember(dest => dest.Files, opt => opt?.MapFrom(src => src.TicketFiles))
                .ForMember(dest => dest.OrderDate, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.OrderDate)))
                .ForMember(dest => dest.ExpireDate, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.ExpireDate)))

                .ReverseMap();
            CreateMap<Personal, GetPersonalAssignationResponseDto>()
                .ForMember(dest => dest.IdEmployee, opt => opt?.MapFrom(src => src.IdEmployee == null ? 0 : src.IdEmployee))
                .ForMember(dest => dest.Fullname, opt => opt?.MapFrom(src => src.IdEmployeeNavigation == null ? "" : src.IdEmployeeNavigation.FirstName.ToUpper() + " " + src.IdEmployeeNavigation.LastName.ToUpper()))
                .ForMember(dest => dest.Type, opt => opt?.MapFrom(src => src.Type))
                .ForMember(dest => dest.Code, opt => opt?.MapFrom(src => src.Code == null ? "" : src.Code))
                .ForMember(dest => dest.IdUserLogin, opt => opt?.MapFrom(src => src.IdEmployeeNavigation != null ? src.IdEmployeeNavigation.UserLogins.FirstOrDefault().Id : 0))
                .ForMember(dest => dest.Internal, opt => opt?.MapFrom(src => src.Internal))
                .ReverseMap();
            CreateMap<Agent, GetPersonalAssignationResponseDto>()
                .ForMember(dest => dest.IdEmployee, opt => opt?.MapFrom(src => src.Id == null ? 0 : src.Id))
                .ForMember(dest => dest.Fullname, opt => opt?.MapFrom(src => src.Name == null ? "" : src.Name.ToUpper()))
                .ForMember(dest => dest.Type, opt => opt?.MapFrom(src => "AG"))
                .ForMember(dest => dest.Code, opt => opt?.MapFrom(src => src.Code == null ? "" : src.Code))
                .ForMember(dest => dest.Internal, opt => opt?.MapFrom(src => src.Internal))
                .ReverseMap();

            CreateMap<TicketFileResponseDto, TicketFile>().ReverseMap();
            CreateMap<GetTicketFileResponseDto, TicketFile>().ReverseMap();
            CreateMap<TicketQuery, GetTicketQueryResponseDto>()
             .ForMember(dest => dest.QueryDate, opt => opt?.MapFrom(src => StaticFunctions.DateTimeToString(src.QueryDate)))
             .ForMember(dest => dest.SubscriberName, opt => opt?.MapFrom(src =>src.IdSubscriberNavigation==null?string.Empty: src.IdSubscriberNavigation.Code +"||"+src.IdSubscriberNavigation.Name))
             .ForMember(dest => dest.Report, opt => opt?.MapFrom(src => src.IdTicketNavigation == null ? string.Empty : src.IdTicketNavigation.RequestedName))
             .ForMember(dest => dest.Response, opt => opt?.MapFrom(src => src.IdTicketNavigation == null ? string.Empty : src.Response))
            .ReverseMap();
            CreateMap<SendTicketQueryRequestDto, TicketQuery>()
            .ForMember(dest => dest.QueryDate, opt => opt?.MapFrom(src => StaticFunctions.VerifyDate(src.QueryDate)))
            .ForMember(dest => dest.Status, opt => opt?.MapFrom(src =>(int?)TicketQueryEnum.En_Consulta))          
            .ReverseMap();

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
