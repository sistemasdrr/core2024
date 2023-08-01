using DRRCore.Domain.Entities.SQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface IEmailUserDomain
    {
        void Add();
        Task<EmailUser> GetById(string id);
    }
}
