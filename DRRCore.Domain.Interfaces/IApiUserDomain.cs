﻿using DRRCore.Domain.Entities.SQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface IApiUserDomain
    {
        Task<ApiUser> GetApiUserByTokenAsync(string token);
        Task<ApiUser> GetApiUserByCodeAsync(string code);
        Task<List<ApiUser>> GetApiUserListAllAsync();
        Task<bool> UpdateApiUserAsync(ApiUser obj);
        Task<bool> DeleteApiUserAsync(int id);
        Task<bool> InsertApiUserAsync(ApiUser obj);
    }
}
