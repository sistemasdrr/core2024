namespace DRRCore.Application.DTO.Core.Request
{
    public class AddOrUpdateCompanyPartnersRequestDto
    {
        public int Id { get; set; }

        public int? IdCompany { get; set; }

        public int? IdPerson { get; set; }

        public bool? MainExecutive { get; set; }

        public int? IdProfession { get; set; }

        public int? Participation { get; set; }

        public string? StartDate { get; set; }

    }
}
