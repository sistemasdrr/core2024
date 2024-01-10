namespace DRRCore.Application.DTO.Core.Response
{
    public class GetListProviderResponseDto
    {
        public int Id { get; set; }

        public int IdCompany { get; set; }

        public int? IdPerson { get; set; }

        public string? Name { get; set; }

        public string? Telephone { get; set; }

        public string? Country{ get; set; }

        public string? FlagCountry { get; set; }

        public string? MaximumAmount { get; set; }

        public string? TimeLimit { get; set; }

        public string? Compliance { get; set; }

        public string? Date { get; set; }
        public string? ProductsTheySell { get; set; }

        public string? AttendedBy { get; set; }

        public bool? Enable { get; set; }
    }
}
