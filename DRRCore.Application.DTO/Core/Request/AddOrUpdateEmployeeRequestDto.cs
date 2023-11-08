namespace DRRCore.Application.DTO.Core.Request
{
    public class AddOrUpdateEmployeeRequestDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ShortName { get; set; }
        public string Address { get; set; } = null!;
        public string? Telephone { get; set; }
        public string? CivilStatus { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? Birthday { get; set; }
        public int? IdDocumentType { get; set; }
        public string? Grains { get; set; }
        public string? DocumentNumber { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Photo { get; set; }
        public int? IdJob { get; set; }
        public int? IdCountry { get; set; }
        public string? City { get; set; }
        public string? Spouse { get; set; }
        public string? Father { get; set; }
        public string? Mother { get; set; }
        public string? Children { get; set; }
        public string? LastWork { get; set; }
        public string? WorkModality { get; set; }
        public string? Observation { get; set; }    
        public bool? Enable { get; set; }
    }
}
