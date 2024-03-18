using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Infraestructure.Interfaces.CoreRepository
{
    public interface IPersonRepository: IBaseRepository<Person>
    {
        Task<List<Person>> GetAllByAsync(string fullname, string form, int idCountry, bool haveReport, bool similar);
        Task<int> AddPersonAsync(Person person); 
        Task<int> UpdatePersonAsync(Person person);
        Task<bool> ActivateWebAsync(int id);
        Task<bool> DesactivateWebAsync(int id);
        Task<Person> GetByOldCode(string? empresa);
    }
}
