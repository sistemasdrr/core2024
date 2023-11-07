﻿using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Transversal.Common;

namespace DRRCore.Application.Interfaces.CoreApplication
{
    public interface IEmployeeApplication
    {
        Task<Response<GetEmployeeResponseDto>> GetByIdAsync(int id);
        Task<Response<List<GetEmployeeResponseDto>>> GetAllAsync();
        Task<Response<List<GetEmployeeResponseDto>>> GetByNameAsync(string name);
        Task<Response<bool>> AddAsync(AddOrUpdateEmployeeRequestDto obj);
        Task<Response<bool>> UpdateAsync(AddOrUpdateEmployeeRequestDto obj);
        Task<Response<bool>> DeleteAsync(int id);
    }
}
