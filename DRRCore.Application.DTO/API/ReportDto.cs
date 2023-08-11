namespace DRRCore.Application.DTO.API
{
    public class ReportDto
    {
        public RequestClientDto RequestClient { get; set; }=new RequestClientDto();
        public InformationDto Information { get; set; } =new InformationDto();
        public SummaryDto Summary { get; set; } = new SummaryDto();

    }
}
