using DRRCore.Domain.Entities.SQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface IEmailConfigurationDomain
    {
        Task<bool> AddAsync(EmailConfiguration obj);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(EmailConfiguration obj);
        Task<List<EmailConfiguration>> GetByNameAsync(string name);
        Task<List<EmailConfiguration>> GetAllAsync();
    }
}
