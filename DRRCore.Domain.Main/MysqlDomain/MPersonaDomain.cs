using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Domain.Interfaces.MysqlDomain;
using DRRCore.Infraestructure.Interfaces.MySqlRepository;

namespace DRRCore.Domain.Main.MysqlDomain
{
    public class MPersonaDomain : IMPersonaDomain
    {
        public readonly IMPersonaRepository _repository;
        public MPersonaDomain(IMPersonaRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<MPersona>> GetAllMPersonaAsync()
        {
            return await _repository.GetAllMPersonaAsync();
        }

        public async Task<MPersona> GetMPersonaByCodigoAsync(string codigo)
        {
            return await _repository.GetMPersonaByCodigoAsync(codigo);
        }
    }
}
