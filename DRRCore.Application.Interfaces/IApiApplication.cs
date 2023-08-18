using DRRCore.Application.DTO.API;

namespace DRRCore.Application.Interfaces
{
    public interface IApiApplication
    {
        Task<ReportDto> GetDummyReportAsync();
    }
}
