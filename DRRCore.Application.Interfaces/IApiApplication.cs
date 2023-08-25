using DRRCore.Application.DTO.API;

namespace DRRCore.Application.Interfaces
{
    public interface IApiApplication
    {
        Task<ReportDto> GetDummyReportAsync();
        Task<ReportDto> GetReportByCodeAndEnvironmentAsync(string code, string environment);

    }
}
