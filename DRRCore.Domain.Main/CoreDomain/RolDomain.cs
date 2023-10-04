using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Infraestructure.Interfaces.CoreRepository;

namespace DRRCore.Domain.Main.CoreDomain
{
    public class RolDomain : IRolDomain
    {
        private readonly IRolRepository _repository;
        public RolDomain(IRolRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> AddAsync(Rol obj)
        {
           return await _repository.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<Rol>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Rol> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Rol>> GetByNameAsync(string name)
        {
            return await _repository.GetByNameAsync(name);
        }

        public async Task<bool> UpdateAsync(Rol obj)
        {
            return await _repository.UpdateAsync(obj);
        }
    }
}
