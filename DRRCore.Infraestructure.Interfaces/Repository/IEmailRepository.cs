using DRRCore.Domain.Entities.SQLContext;

namespace DRRCore.Infraestructure.Interfaces.Repository
{
    public interface IEmailRepository
    {
        Task<EmailConfiguration> GetByKey(string key);
        Task<int> AddEmailHistory(EmailHistory emailHistory);
        Task<int> AddEmailConfiguration(EmailConfiguration emailHistory);

    }
}
