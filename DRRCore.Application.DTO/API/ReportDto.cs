using System.Text.Json.Serialization;

namespace DRRCore.Application.DTO.API
{   
    public class ReportDto
    {
        [JsonPropertyName("DRR_REQCLI")]
        public RequestClientDto RequestClient { get; set; }=new RequestClientDto();
        [JsonPropertyName("DRR_INFORM")]
        public InformationDto Information { get; set; } =new InformationDto();
        [JsonPropertyName("DRR_SUMMAR")]
        public SummaryDto Summary { get; set; } = new SummaryDto();
        [JsonPropertyName("DRR_LEGBAC")]
        public LegalBackgroundDto LegalBackground { get; set; }= new LegalBackgroundDto();
        [JsonPropertyName("DRR_EXECUT")]
        public List<ExecutiveShareholderDto> Executives { get; set; } = new List<ExecutiveShareholderDto>();
        [JsonPropertyName("DRR_WISWHO")]
        public List<WhoIsWhoDto> WhoIsWho { get; set; }= new List<WhoIsWhoDto>();
        [JsonPropertyName("DRR_PLAHOL")]
        public List<PlaceholderDto> Placeholders { get; set; }=new List<PlaceholderDto>();
        [JsonPropertyName("DRR_BUSHIS")]
        public string BussinessHistory { get; set; } = string.Empty;
        [JsonPropertyName("DRR_RELCOM")]
        public List<RelatedCompanyDto> RelatedCompanys { get; set;} = new List<RelatedCompanyDto>();
        [JsonPropertyName("DRR_BUSINE")]
        public BusinessDto Business { get; set; } = new BusinessDto();
        [JsonPropertyName("DRR_FININF")]
        public FinancialInformationDto FinancialInformation { get; set; }= new FinancialInformationDto();
        [JsonPropertyName("DRR_BANINF")]
        public BankingInformationDto BankingInformation { get; set; }=new BankingInformationDto();
        [JsonPropertyName("DRR_PAYREC")]
        public PaymentRecordsDto PaymentRecords { get; set; }=  new PaymentRecordsDto();
        [JsonPropertyName("DRR_NEWSTO")]
        public string News { get; set; } = string.Empty;

    }
}
