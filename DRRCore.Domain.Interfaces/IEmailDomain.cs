using DRRCore.Domain.Entities.SQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface IEmailDomain
    {
        Task<EmailConfiguration> GetByKey(string key);
        Task<int> AddEmailHistory(EmailHistory emailHistory);
        Task<int> AddEmailConfiguration(EmailConfiguration emailConfiguration);
    }
}
