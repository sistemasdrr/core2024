using DRRCore.Application.DTO.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRRCore.Application.Interfaces
{
    public interface IApiApplication
    {
        Task<ReportDto> GetDummyReportAsync();
    }
}
