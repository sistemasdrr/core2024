﻿using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Domain.Interfaces;
using DRRCore.Infraestructure.Interfaces.MySqlRepository;

namespace DRRCore.Domain.Main
{
    public class TCuponDomain : ITCuponDomain
    {
        public readonly ITCuponRepository _repository;
        public TCuponDomain(ITCuponRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<TCupon>> GetAllTCuponAsync()
        {
            return await _repository.GetAllTCuponAsync();
        }

        public async Task<TCupon> GetTCuponByCodigoAsync(int codigo)
        {
            return await _repository.GetTCuponByCodigoAsync(codigo);
        }
    }
}
