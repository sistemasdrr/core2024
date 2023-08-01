using DRRCore.Domain.Entities.SQLContext;
using DRRCore.Domain.Interfaces;
using DRRCore.Infraestructure.Interfaces.Repository;

namespace DRRCore.Domain.Main
{
    public class EmailUserDomain : IEmailUserDomain
    {
        private readonly IEmailUserRepository _emailUserRepository;

        public EmailUserDomain(IEmailUserRepository emailUserRepository)
        {
            _emailUserRepository = emailUserRepository;
        }
        public void Add()
        {
            _emailUserRepository.Add();
        }

        public Task<EmailUser> GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
