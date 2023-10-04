using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Infraestructure.Interfaces.CoreRepository;

namespace DRRCore.Domain.Main.CoreDomain
{
    public class UserPermissionDomain:IUserPermisionDomain
    {
        private readonly IUserPermissionRepository _repository;
        public UserPermissionDomain(IUserPermissionRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddAsync(UserPermission obj)
        {
            return await _repository.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<UserPermission>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<UserPermission> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<UserPermission>> GetByNameAsync(string name)
        {
            return await _repository.GetByNameAsync(name);
        }

        public async Task<bool> UpdateAsync(UserPermission obj)
        {
            return await _repository.UpdateAsync(obj);
        }
    }
}
