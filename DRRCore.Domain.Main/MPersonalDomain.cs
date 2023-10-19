using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Domain.Interfaces;
using DRRCore.Infraestructure.Interfaces.MySqlRepository;

namespace DRRCore.Domain.Main
{
    public class MPersonalDomain : IMPersonalDomain
    {
        private readonly IMPersonalRepository _repository;

        public MPersonalDomain(IMPersonalRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<MPersonal>> GetAllActivePersonal()
        {
           return await _repository.GetAllActivePersonal();
        }
    }
}
