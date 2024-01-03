using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Domain.Interfaces.CoreDomain
{
    public interface IPersonDomain : IBaseDomain<Person>
    {
        Task<List<Person>> GetAllByAsync(string fullname, string form, int idCountry, bool haveReport);
        Task<int> AddPersonAsync(Person person);
        Task<bool> ActivateWebAsync(int id);
        Task<bool> DesactivateWebAsync(int id);
    }
}
