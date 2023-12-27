using DRRCore.Application.DTO.Core.Request;
using Microsoft.AspNetCore.Http;

namespace DRRCore.Application.DTO.Core.Response
{
    public class GetCompanyImageResponseDto
    {
        public int Id { get; set; }

        public int? IdCompany { get; set; }

        public string? ImgDesc1 { get; set; }

        public string? Path1 { get; set; }

        public string? ImgDesc2 { get; set; }

        public string? Path2 { get; set; }

        public string? ImgDesc3 { get; set; }

        public string? Path3 { get; set; }

        public string? ImgDesc4 { get; set; }

        public string? Path4 { get; set; }
        public List<TraductionDto> Traductions { get; set; } = new List<TraductionDto>();

    }
}
