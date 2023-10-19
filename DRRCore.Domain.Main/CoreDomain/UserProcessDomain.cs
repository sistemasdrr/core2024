using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Infraestructure.Interfaces.CoreRepository;

namespace DRRCore.Domain.Main.CoreDomain
{
    public class UserProcessDomain:IUserProcessDomain
    {
        private readonly IUserProcessRepository _repository;
        public UserProcessDomain(IUserProcessRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddAsync(UserProcess obj)
        {
            return await _repository.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<UserProcess>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<UserProcess> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<UserProcess>> GetByNameAsync(string name)
        {
            return await _repository.GetByNameAsync(name);
        }

        public async Task<bool> UpdateAsync(UserProcess obj)
        {
            return await _repository.UpdateAsync(obj);
        }
    }
}
