using DRRCore.Domain.Entities.SQLContext;
using DRRCore.Domain.Interfaces;
using DRRCore.Infraestructure.Interfaces.Repository;

namespace DRRCore.Domain.Main
{
    public class EmailDomain : IEmailDomain
    {
        private readonly IEmailRepository _emailRepository;

        public EmailDomain(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }
        public async Task<EmailConfiguration> GetByKey(string key)
        {
            return await _emailRepository.GetByKey(key);
        }
       
        public async Task<int> AddEmailConfiguration(EmailConfiguration emailConfiguration)
        {
            return await _emailRepository.AddEmailConfiguration(emailConfiguration);
        }

        public async Task<int> AddEmailHistory(EmailHistory emailHistory)
        {
            return await _emailRepository.AddEmailHistory(emailHistory);
        }
    }
}
