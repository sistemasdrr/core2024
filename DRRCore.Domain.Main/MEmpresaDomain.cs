﻿using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Domain.Interfaces;
using DRRCore.Infraestructure.Interfaces.MySqlRepository;

namespace DRRCore.Domain.Main
{
    public class MEmpresaDomain : IMEmpresaDomain
    {
        private readonly IMEmpresaRepository _repository;

        public MEmpresaDomain(IMEmpresaRepository repository)
        {
            _repository = repository;
        }
        public async Task<MEmpresa> GetmEmpresaByCodigoAsync(string codigo)
        {
            return await _repository.GetmEmpresaByCodigoAsync(codigo);
        }
    }
}
