using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Infraestructure.Interfaces.CoreRepository;

namespace DRRCore.Domain.Main.CoreDomain
{
    public class PermissionDomain:IPermissionDomain
    {
        private readonly IPermissionRepository _repository;
        public PermissionDomain(IPermissionRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddAsync(Permission obj)
        {
            return await _repository.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<Permission>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Permission> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Permission>> GetByNameAsync(string name)
        {
            return await _repository.GetByNameAsync(name);
        }

        public async Task<List<Permission>> GetByRol(int idRol)
        {
            return await _repository.GetByRol(idRol);
        }

        public async Task<bool> UpdateAsync(Permission obj)
        {
            return await _repository.UpdateAsync(obj);
        }
    }
}
