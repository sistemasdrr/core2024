using DRRCore.Domain.Entities.SQLContext;

namespace DRRCore.Infraestructure.Interfaces.Repository
{
    public interface IEmailUserRepository
    {
        void Add();
        Task<EmailUser> GetById(string id);
    }
}
