namespace DRRCore.Application.DTO.Core.Response
{
    public class GetCompanySbsResponseDto
    {
        public int Id { get; set; }

        public int? IdCompany { get; set; }

        public int? IdOpcionalCommentarySbs { get; set; }

        public string? AditionalCommentaryRiskCenter { get; set; }

        public string? DebtRecordedDate { get; set; }

        public decimal? ExchangeRate { get; set; }

        public string? BankingCommentary { get; set; }

        public string? EndorsementsObservations { get; set; }

        public string? ReferentOrAnalyst { get; set; }

        public string? Date { get; set; }

        public string? LitigationsCommentary { get; set; }

        public string? CreditHistoryCommentary { get; set; }

    }
}
