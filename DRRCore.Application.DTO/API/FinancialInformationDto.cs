using System.Text.Json.Serialization;

namespace DRRCore.Application.DTO.API
{
    public class FinancialInformationDto
    {
        [JsonPropertyName("DRR_FININF_DISPOS")]
        public ValueDetailDto Disposition { get; set; } = new ValueDetailDto();
        [JsonPropertyName("DRR_FININF_INFPRO")]
        public string InformationProvided { get; set; } = string.Empty;
        [JsonPropertyName("DRR_FININF_INTBAL")]
        public List<BalanceSheetDto> InterimBalanceSheets { get; set; } = new List<BalanceSheetDto>();
        [JsonPropertyName("DRR_FININF")]
        public RatioSituationDto RatioSituation { get; set; } = new RatioSituationDto();
        public List<BalanceSheetDto> BalanceSheets { get; set; } = new List<BalanceSheetDto>();
        public ValueDetailDto SituationalFinancial { get; set; } = new ValueDetailDto();
        public string Comments { get; set; } = string.Empty;
        public string AnalystComments { get; set; } = string.Empty;
        public List<InsuranceCompaniesDto> Insurances { get; set; } = new List<InsuranceCompaniesDto>();


    }
    public class RatioSituationDto
    {
        public double LiquidityRatio { get; set; }
        public double DebtToEquityRatio { get; set; }
        public double ProfitabilityMargin { get; set; }
        public double WorkingCapital { get; set; }
    }
    public class InsuranceCompaniesDto
    {
        public string Name { get; set; } = string.Empty;
        public string Against { get; set; } = string.Empty;
    }
}
