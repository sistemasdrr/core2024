﻿using DRRCore.Application.DTO.Core.Request;

namespace DRRCore.Application.DTO.Core.Response
{
    public class GetCompanyBackgroundResponseDto
    {
        public int Id { get; set; }

        public int? IdCompany { get; set; }

        //Datetime
        public string? ConstitutionDate { get; set; }

        public string? StartFunctionYear { get; set; }

        public string? OperationDuration { get; set; }

        public string? RegisterPlace { get; set; }

        public string? NotaryRegister { get; set; }

        public string? PublicRegister { get; set; }

        public decimal? CurrentPaidCapital { get; set; }

        public int? CurrentPaidCapitalCurrency { get; set; }

        public string? CurrentPaidCapitalComentary { get; set; }

        public string? Origin { get; set; }

        public string? IncreaceDateCapital { get; set; }

        public int? Currency { get; set; }

        public bool? Traded { get; set; }

        public string? TradedBy { get; set; }

        public decimal? CurrentExchangeRate { get; set; }
        //Datetime
        public string? LastQueryRrpp { get; set; }

        public string? LastQueryRrppBy { get; set; }

        public string? Background { get; set; }

        public string? History { get; set; }
        public List<TraductionDto> Traductions { get; set; } = new List<TraductionDto>();

    }
}
