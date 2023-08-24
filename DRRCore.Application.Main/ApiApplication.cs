﻿using DRRCore.Application.DTO;
using DRRCore.Application.DTO.API;
using DRRCore.Application.Interfaces;

namespace DRRCore.Application.Main
{
    public class ApiApplication : IApiApplication
    {
        public async Task<ReportDto> GetDummyReportAsync()
        {
            return ApiDummy.Report();
        }
        public async Task<ReportDto> GetReportByCodeAndEnvironmentAsync(string code, string environment)
        {
            return ApiDummy.Report();
        }
    }
}
