namespace DRRCore.Application.DTO.Core.Response
{
    public class GetSuscriberPriceResponseDto
    {
        public int Id { get; set; }

        public int? IdSubscriber { get; set; }

        public string? Date { get; set; }

        public string? Country { get; set; }

        public string? FlagCountry { get; set; }

        public string? Currency { get; set; }

        public string? PriceT1 { get; set; }

        public string? PriceT2 { get; set; }

        public string? PriceT3 { get; set; }

        public string? PriceB { get; set; }
    }
    public class GetSuscriberPriceByIdResponseDto
    {
        public int Id { get; set; }
        public string? Date { get; set; }

        public int? IdSubscriber { get; set; }

        public int? IdCountry { get; set; }

        public int? IdContinent { get; set; }

        public int? IdCurrency { get; set; }

        public int? PriceT1 { get; set; }

        public int? DayT1 { get; set; }

        public int? PriceT2 { get; set; }

        public int? DayT2 { get; set; }

        public int? PriceT3 { get; set; }

        public int? DayT3 { get; set; }

        public int? PriceB { get; set; }
    }
}
