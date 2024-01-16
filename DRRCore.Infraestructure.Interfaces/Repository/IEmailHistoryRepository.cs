using DRRCore.Domain.Entities.SqlContext;

namespace DRRCore.Infraestructure.Interfaces.Repository
{
    public interface IEmailHistoryRepository
    {
        Task<bool> AddAsync(EmailHistory obj);
        Task<bool> UpdateAsync(EmailHistory obj);
        Task<bool> DeleteAsync(int id);
        Task<List<EmailHistory>> GetAllAsync();
        Task<List<EmailHistory>> GetByUserAsync(string user);
        Task<EmailHistory> GetByIdAsync(int id);
        Task<List<EmailHistory>> GetEmailNotSendAsync();
    }
}
