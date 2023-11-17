﻿using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Infraestructure.Interfaces.CoreRepository
{
    public interface ITraductionRepository:IBaseRepository<Traduction>
    {
        Task<List<Traduction>> GetByKeyAndCompanyAsync(string key,int id);
    }
}
