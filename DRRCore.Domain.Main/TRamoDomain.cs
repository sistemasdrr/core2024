﻿using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Domain.Interfaces;

namespace DRRCore.Domain.Main
{
    public class TRamoDomain : ITRamoDomain
    {
        public readonly ITRamoDomain _repository;
        public TRamoDomain(ITRamoDomain repository)
        {
            _repository = repository;
        }

        public async Task<List<TRamo>> GetAllTRamoAsync()
        {
            return await _repository.GetAllTRamoAsync();
        }

        public async Task<TRamo> GetTRamoByCodigoAsync(string codigo)
        {
            return await _repository.GetTRamoByCodigoAsync(codigo);
        }
    }
}
