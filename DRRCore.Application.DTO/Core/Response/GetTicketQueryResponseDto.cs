namespace DRRCore.Application.DTO.Core.Response
{
    public class GetTicketQueryResponseDto
    {
        public int IdTicket { get; set; }
        public string QueryDate { get; set; } = string.Empty;
        public int? IdSubscriber { get; set; }
        public string SubscriberName { get; set; } = string.Empty;
        public string Language { get; set; } = null!;
        public int? IdEmployee { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public string? Response { get; set; }
        public int Status { get; set; }
        public string Report { get; set; } = string.Empty;
    
    }
}
