using DRRCore.Domain.Entities.SQLContext;
using DRRCore.Domain.Interfaces;
using DRRCore.Infraestructure.Interfaces.Repository;

namespace DRRCore.Domain.Main
{
    public class EmailConfigurationDomain : IEmailConfigurationDomain
    {
        private readonly IEmailConfigurationRepository _repository;
        public EmailConfigurationDomain(IEmailConfigurationRepository repository) { 
        
            _repository = repository;
        }
        public async Task<bool> AddAsync(EmailConfiguration obj)
        {
           return await _repository.AddAsync(obj);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<EmailConfiguration>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task<List<EmailConfiguration>> GetByNameAsync(string name)
        {
            return _repository.GetByNameAsync(name);
        }

        public Task<bool> UpdateAsync(EmailConfiguration obj)
        {
            return _repository.UpdateAsync(obj);
        }
    }
}
